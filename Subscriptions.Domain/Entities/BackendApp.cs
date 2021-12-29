namespace Subscriptions.Domain.Entities
{
    public class BackendApp : App
    {
        public string Secret { get; set; }

        public BackendApp()
        {
            Type = AppType.Backend;
        }
    }
}