

namespace NearMe.Rest.Models
{
    public class RestResult<T>
    {
        public Error Error { get; set; }
        public T Data { get; set; }
        public string Raw { get; set; }

        public RestResult()
        {
            Error = new Error();
        }
    }
}
