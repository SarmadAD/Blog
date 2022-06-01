using Newtonsoft.Json;

namespace Blog.Classes.ObjClasses
{
    public class ConvertObject
    {
        public static string ConvertObjectToJson(object obj) => JsonConvert.SerializeObject(obj);
    }
}
