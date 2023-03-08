using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using net_core_practice.Filter;
using net_core_practice.Models.Options.ApiRouter;
using net_core_practice.Services.DB;
using net_core_practice.Services.Weather.API;
using net_core_practice.Utilis.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace net_core_practice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //基本設定(弱型別轉強型別)
            services.Configure<AppSettingsOptions>(Configuration);

            // HTTP強制安全傳輸技術
            services.AddHsts(options =>
            {
                // 瀏覽器向 XXX 或其子域名傳送 HTTP 請求時，將強制啟用 HTTPS 來發起連接
                options.Preload = true;
                // 如果 伺服器傳送的 TLS 憑證無效，用戶不能忽略瀏覽器警告繼續存取網站
                options.IncludeSubDomains = true;
            });

            //DB 連線 
            services.AddTransient<IDBService, DBService>();
            services.AddTransient<IDBServiceFactory, DBServiceFactory>();

            var _apiRouter = Configuration.GetSection(nameof(AppSettingsOptions.ApiRouter)).Get<ApiRouterModel>();

            services.AddHttpClient<IWeatherAPIService, WeatherAPIService>(c =>
            {
                c.BaseAddress = new Uri(_apiRouter.Weather.BaseUrl);
                //c.DefaultRequestHeaders.Add("x-api-key", _apiRouter.Weather.partner_key);
                // timeout 時間為 30 秒
                c.Timeout = TimeSpan.FromSeconds(30);
            });

            //Note:自定義Service，不須額外設定的Service統一放這裡
            services.AddCustomServices();

            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                // 字元編碼，避免中文變 ASCII 碼
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            // 註冊 Swagger
            services.AddSwaggerGen(c =>
            {
                // 條列出 Enum 內容
                c.SchemaFilter<EnumSchemaFilter>();
                // 讀取 XML 檔案產生 API 說明
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                // 文件標題資訊
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "示範API",
                    Description = @"API URL : ",
                    //TermsOfService = new Uri("https://example.com/terms"),
                    //Contact = new OpenApiContact
                    //{
                    //    Name = "Example Contact",
                    //    Url = new Uri("https://example.com/contact")
                    //},
                    //License = new OpenApiLicense
                    //{
                    //    Name = "Example License",
                    //    Url = new Uri("https://example.com/license")
                    //}
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // 於開發模式時才啟用 Swagger 路由
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.InjectJavascript("/swagger-ui/custom.js");
                    c.InjectJavascript("/swagger-ui/rapipdf-min.js");
                });
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
