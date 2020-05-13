using GoogleApi.Entities.Maps.Geocoding;
using GoogleApi.Entities.Maps.Geocoding.Address.Request;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TrainingFinder.Helpers;

namespace TrainingFinder.Services.GymLocationService
{
    public class GymLocationService : IGymLocationService
    {
        private readonly AddressGeocodeRequest _addressGeocodeRequest;

        public GymLocationService(IOptions<AppSettings> appSettings)
        {
            var appSettings1 = appSettings.Value;
            _addressGeocodeRequest = new AddressGeocodeRequest {Key = appSettings1.GoogleApiKey};
        }

        public double GetLatitude(string address)
        {
            _addressGeocodeRequest.Address = address;

            var response = GoogleApi.GoogleMaps.AddressGeocode.Query(_addressGeocodeRequest);
            GeocodeResponse geocodeResponse = JsonConvert.DeserializeObject<GeocodeResponse>(response.RawJson);
            var results = geocodeResponse.Results;
            foreach (var result in results)
            {
                return result.Geometry.Location.Latitude;
            }

            return 0;
        }

        public double GetLongitude(string address)
        {
            _addressGeocodeRequest.Address = address;

            var response = GoogleApi.GoogleMaps.AddressGeocode.Query(_addressGeocodeRequest);
            GeocodeResponse geocodeResponse = JsonConvert.DeserializeObject<GeocodeResponse>(response.RawJson);
            var results = geocodeResponse.Results;
            foreach (var result in results)
            {
                return result.Geometry.Location.Longitude;
            }

            return 0;
        }
    }
}