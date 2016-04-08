namespace NearMe.Domain.Code
{
    public class SuperApp
    {
        static SuperApp _instance = null;
        public static SuperApp Instance => _instance ?? (_instance = new SuperApp());
    }
}
