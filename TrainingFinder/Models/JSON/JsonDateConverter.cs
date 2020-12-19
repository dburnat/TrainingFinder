using Newtonsoft.Json.Converters;

namespace TrainingFinder.Models.JSON
{
    class JsonDateConverter : IsoDateTimeConverter
    {
        public JsonDateConverter()
        {
            DateTimeFormat = "HH:mm dd-MM-yyyy";
        }
    }
}