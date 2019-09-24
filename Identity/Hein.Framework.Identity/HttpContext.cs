namespace Hein.Framework.Identity
{
    public static class HttpContext
    {
        public static Microsoft.AspNetCore.Http.HttpContext Current { get; private set; }
        public static void Set(Microsoft.AspNetCore.Http.HttpContext context)
        {
            Current = context;
        }

        public static void Flush()
        {
            Current = null;
        }
    }
}
