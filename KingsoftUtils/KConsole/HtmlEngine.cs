#pragma warning disable CS0618

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Kingsoft.Utils.TypeExtensions;
using Kingsoft.Utils.TypeExtensions.Arrays;
using Kingsoft.Utils.TypeExtensions.Dictionaries;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Kingsoft.Utils.KConsole
{
    public static class HtmlEngine
    {
        public static void RunHtml(string html)
        {
            XmlDocument doc = html.xdoc();
            XmlNodeList main = doc.GetElementsByTagName("main")[0].ChildNodes;
            for (int i = 0; i < main.Count; i++)
            {
                XmlNode node = main[i];
                Dictionary<string, string> nodeAttributes = node.Attributes.ToDictionary();
                if (node.Name == "chart")
                {
                    Dictionary<string, Dictionary<string, string>>[] childs = node.ChildNodes.json().jobj<Dictionary<string, Dictionary<string, string>>[]>();
                    Dictionary<string, string> attr = node.Attributes.ToDictionary();
                    BarChart chart = new BarChart()
                        .Width(attr.ContainsKey("@size") ? int.Parse(attr["@size"]) : 30)
                        .Label(attr.ContainsKey("@lbl") ? attr["@lbl"] : "null");
                    foreach (Dictionary<string, Dictionary<string, string>> item in childs)
                    {
                        Color color = Color.Red;
                        Dictionary<string, string> cItem = item["item"];
                        if (cItem.ContainsKey("@color"))
                        {
                            Dictionary<string, int> args = cItem["@color"].jobj<Dictionary<string, int>>();
                            color = new Color((byte)args["r"], (byte)args["g"], (byte)args["b"]);
                        }
                        chart.AddItem(
                            cItem.ContainsKey("@name") ? cItem["@name"] : "null",
                            cItem.ContainsKey("@value") ? int.Parse(cItem["@value"]) : 1,
                            color);
                    }
                    if (childs.Length == 0) chart.AddItem("null", 1, Color.Red);
                    AnsiConsole.Render(chart);
                } else if (node.Name == "text")
                {
                    IRenderable textItem = nodeAttributes.ContainsKey("@markup") ? 
                        (IRenderable)new Markup(node.InnerText) : new Text(node.InnerText);
                    AnsiConsole.Render(textItem);
                }
            }
        }
    }
}
