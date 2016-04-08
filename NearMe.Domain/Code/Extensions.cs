using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NearMe.Domain.Code
{
    public static class AsyncHelper
    {
        private static readonly TaskFactory MyTaskFactory = new
            TaskFactory(CancellationToken.None,
                TaskCreationOptions.None,
                TaskContinuationOptions.None,
                TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return MyTaskFactory
                .StartNew<Task<TResult>>(func)
                .Unwrap<TResult>()
                .GetAwaiter()
                .GetResult();
        }

        public static void RunSync(Func<Task> func)
        {
            MyTaskFactory
                .StartNew<Task>(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }
    }
    public static class Extensions
    {

        public static string ConvertToString(this Enum eff)
        {

            return Enum.GetName(eff.GetType(), eff);

        }
        public static string ToJson(this object objectToSave)
        {
            try
            {
                if (objectToSave == null) return null;
                var st = new JsonSerializerSettings {StringEscapeHandling = StringEscapeHandling.EscapeHtml};
                return JsonConvert.SerializeObject(objectToSave, Formatting.None, st);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public static TJson FromJson<TJson>(this string jsonString)
        {
            try
            {

                return JsonConvert.DeserializeObject<TJson>(jsonString, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
