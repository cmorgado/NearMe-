using System;
using System.Threading.Tasks;
using Cimbalino.Toolkit.Services;
using NearMe.Domain.Code;
using NearMe.Domain.Interfaces;

namespace NearMe.Services
{
    public class Platform : IPlatform
    {
        IWindowsStoreService _windowsInfo = new WindowsStoreService();
        IRuntimeInformationService _runInfo = new RuntimeInformationService();
        public async Task<string> AppName()
        {
            return await AsyncUtils.FromResultAsync(Constants.AppName);
        }

        public Task<string> AppVersion()
        {
            return AsyncUtils.FromResultAsync(string.Format("{0}.{1}.{2}", _runInfo.Version.Major
                , _runInfo.Version.Minor, _runInfo.Version.Build));
        }

        public Task<string> DeviceId()
        {
            throw new NotImplementedException();
        }

        public Task<string> DeviceName()
        {
            throw new NotImplementedException();
        }

        public async Task<string> OsName()
        {
            var wi = await _windowsInfo.GetAppInformationAsync();
            return wi.Package.Title;
        }

        public  Task<string> OsVersion()
        {
            throw new NotImplementedException();
        }
    }
}
