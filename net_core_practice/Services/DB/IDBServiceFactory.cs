using net_core_practice.Enums;

namespace net_core_practice.Services.DB
{
    public interface IDBServiceFactory
    {
        IDBService CreateDBService(DBServiceEnum connectionEnum);
    }
}
