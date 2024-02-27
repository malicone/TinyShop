namespace TinyShop.RestUtils.NovaPoshta
{
    public class NovaPoshtaOptions
    {
        // todo: read from appsettings.json
        public static string BaseUrl { get; set; } = "https://api.novaposhta.ua/v2.0/json/";
        public static string ApiKey { get; set; } = "110236d75ca7388f9838437f1b5421d3";
    }
}
