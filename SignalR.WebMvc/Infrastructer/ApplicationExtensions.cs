using SignalR.WebMvc.Hubs;
using System.Security.Cryptography;


namespace SignalR.WebMvc.Infrastructer
{
    public static class ApplicationExtensions
    {
        public static void ConfigureSignalR(this WebApplication app)
        {
            app.MapHub<ChatHub>($"/{nameof(ChatHub)}");
            app.MapHub<ClientHub>($"/{nameof(ClientHub)}");
        }


        public static void ConfigureHeaderSecurity(this HttpContext context)
        {
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
            context.Response.Headers.Add("Referrer-Policy", "no-referrer");
            context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");

            context.Response.Headers.Add("Feature-Policy", "accelerometer 'none'; camera 'none'; geolocation 'none'; gyroscope 'none'; magnetometer 'none'; microphone 'none'; payment 'none'; usb 'none'");

            context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
            //context.Response.Headers.Add("X-Frame-Options", "DENY");


            var rng = RandomNumberGenerator.Create();
            var nonceBytes = new byte[32];
            rng.GetBytes(nonceBytes);
            var nonce = Convert.ToBase64String(nonceBytes);
            context.Items.Add("ScriptNonce", nonce);
            context.Response.Headers.Add("Content-Security-Policy",
            new[] { string.Format("script-src 'self' https://cdnjs.cloudflare.com https://gstatic.com https://google.com 'nonce-{0}'", nonce) });


            context.Response.Headers.Remove("X-Powered-By");
            context.Response.Headers.Remove("X-AspNet-Version");
            context.Response.Headers.Remove("X-AspNetMvc-Version");
            context.Response.Headers.Remove("Server");
        }


    }
}