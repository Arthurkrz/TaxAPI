namespace RegistroNF.API.Core.Common
{
    public class SystemTime
    {
        public static Func<DateTime> Now = () => DateTime.Now;

        public static void Reset() { Now = ()=> DateTime.Now; }
    }
}
