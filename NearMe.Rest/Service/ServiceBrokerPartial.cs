using System.Text;
using System.Threading.Tasks;
using NearMe.Rest.Models;

namespace NearMe.Rest.Service
{
    public partial class ServiceBroker
    {


        public async Task<RestResult<string>> GetPois()
        {
                var url =
            "https://dl.dropboxusercontent.com/u/2795386/nei/NearMe.csv";


           return await this.DoHttpGet<string>(new StringBuilder(url));


        }
}
}
