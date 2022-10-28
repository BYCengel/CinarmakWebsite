using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace WebsiteUI.Extensions
{
    public static class TempdataExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T: class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;

            tempData.TryGetValue(key, out o);
            if(o == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<T>(o.ToString());
        }
    }
}
