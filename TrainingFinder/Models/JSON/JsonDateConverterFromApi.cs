using Newtonsoft.Json.Converters;

namespace TrainingFinder.Models.JSON
{
    class JsonDateConverterFromApi : IsoDateTimeConverter
    {
        public JsonDateConverterFromApi()
        {
            DateTimeFormat = "H:m d-M-yyyy";
        }
    }
}