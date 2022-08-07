using Kingsoft.Utils.TypeExtensions.Dictionaries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Kingsoft.Utils.TypeExtensions
{
    public static class GeneralExtension
    {
        public static string json<T>(this T obj, Newtonsoft.Json.Formatting formatting = Newtonsoft.Json.Formatting.None)
        {
            return JsonConvert.SerializeObject(obj, formatting);
        }

        public static void json<T>(this T obj, out string str)
        {
            str = obj.json();
            return;
        }

        public static void json<T, T2>(this T obj, Dictionary<T2, string> dict, T2 index)
        {
            dict[index] = obj.json();
        }

        public static T jobj<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        public static void jobj<T>(this string str, out T res)
        {
            res = str.jobj<T>();
            return;
        }

        public static XmlDocument xdoc(this string str)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(str);
            return doc;
        }

        public static T xobj<T>(this string str)
        {
            XmlDocument doc = str.xdoc();
            return doc.json().jobj<T>();
        }

        public static void xobj<T>(this string str, out T res)
        {
            res = str.xobj<T>();
            return;
        }

        public static string xjson(this string str)
        {
            return str.xobj<object>().json();
        }

        public static void xjson(this string str, out string res)
        {
            res = str.xjson();
            return;
        }

        public static Dictionary<string, string> ToDictionary(this XmlAttributeCollection attr)
        {
            return attr.json().jobj<Dictionary<string, string>[]>().ConvertToDict(item =>
            {
                string key = item.Keys.ToArray()[0];
                return (true, key, item[key]);
            });
        }
    }
}
