using System.Threading.Tasks;

namespace NearMe.Domain.Code
{
    /// transform any sync code to async
    /// Nice when your implementing an Interface that is "async" but your implementation is no so async
    public class AsyncUtils
    {
        public static Task<T> FromResultAsync<T>(T result)
        {
            var tcs = new TaskCompletionSource<T>();
            tcs.SetResult(result);
            return tcs.Task;
        }


        private static Task _completedTask;
        public static Task CompletedTaskAsync()
        {
            return _completedTask ?? InitCompletedTaskAsync();
        }

        private static Task InitCompletedTaskAsync()
        {
            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(null);
            _completedTask = tcs.Task;
            return _completedTask;
        }
    }
}
