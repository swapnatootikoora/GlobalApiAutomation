using Newtonsoft.Json;

namespace Common.Helpers
{
    public static class JSONLibrary
    {
        public static T DeserializeJSon<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static string SerializeJSon<T>(T t)
        {
            return JsonConvert.SerializeObject(t);
        }
    }
}
