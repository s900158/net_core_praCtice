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
            //�򥻳]�w(�z���O��j���O)
            services.Configure<AppSettingsOptions>(Configuration);

            // HTTP�j��w���ǿ�޳N
            services.AddHsts(options =>
            {
                // �s�����V XXX �Ψ�l��W�ǰe HTTP �ШD�ɡA�N�j��ҥ� HTTPS �ӵo�_�s��
                options.Preload = true;
                // �p�G ���A���ǰe�� TLS ���ҵL�ġA�Τᤣ�੿���s����ĵ�i�~��s������
                options.IncludeSubDomains = true;
            });

            //DB �s�u 
            services.AddTransient<IDBService, DBService>();
            services.AddTransient<IDBServiceFactory, DBServiceFactory>();

            var _apiRouter = Configuration.GetSection(nameof(AppSettingsOptions.ApiRouter)).Get<ApiRouterModel>();

            services.AddHttpClient<IWeatherAPIService, WeatherAPIService>(c =>
            {
                c.BaseAddress = new Uri(_apiRouter.Weather.BaseUrl);
                //c.DefaultRequestHeaders.Add("x-api-key", _apiRouter.Weather.partner_key);
                // timeout �ɶ��� 30 ��
                c.Timeout = TimeSpan.FromSeconds(30);
            });

            //Note:�۩w�qService�A�����B�~�]�w��Service�Τ@��o��
            services.AddCustomServices();

            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                // �r���s�X�A�קK������ ASCII �X
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            // ���U Swagger
            services.AddSwaggerGen(c =>
            {
                // ���C�X Enum ���e
                c.SchemaFilter<EnumSchemaFilter>();
                // Ū�� XML �ɮײ��� API ����
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                // �����D��T
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "�ܽdAPI",
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
                // ��}�o�Ҧ��ɤ~�ҥ� Swagger ����
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
