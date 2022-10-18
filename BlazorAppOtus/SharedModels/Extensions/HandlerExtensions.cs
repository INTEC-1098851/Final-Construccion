using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Extensions
{
    public static class HandlerExtensions
    {
        public static bool TryParseJSON(this string json)
        {
            try
            {
                JObject.Parse(json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ToEnumMemberAttrValue(this Enum @enum)
        {
            var attr =
                @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?.
                    GetCustomAttributes(false).OfType<EnumMemberAttribute>().
                    FirstOrDefault();
            if (attr == null)
                return @enum.ToString();
            return attr.Value;
        }

        public static string ToEnumString(this Enum @enum)
      => ToEnumMemberAttrValue(@enum);

        public static T ToEnum<T>(this string value)
        => JsonConvert.DeserializeObject<T>($"\"{value}\"");

        public static T ToEnum<T>(this int value)
      => JsonConvert.DeserializeObject<T>($"\"{value}\"");

    }
}
