using System.Threading.Tasks;

namespace NearMe.Domain.Interfaces
{
    public interface IPlatform
    {

        Task<string> AppName();
        Task<string> AppVersion();

        Task<string> DeviceId();
        Task<string> DeviceName();
        Task<string> OsName();
        Task<string> OsVersion();
    }
}
