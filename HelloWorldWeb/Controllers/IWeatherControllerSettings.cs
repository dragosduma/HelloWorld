namespace HelloWorldWeb.Controllers
{
    public interface IWeatherControllerSettings
    {
        string Longitude { get; }

        string ApiKey { get; }

        string Latitude { get; }
    }
}