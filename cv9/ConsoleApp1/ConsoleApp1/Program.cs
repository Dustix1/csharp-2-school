using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using HttpClient client = new HttpClient();
            using HttpResponseMessage response = await client.GetAsync("https://www.lupa.cz/rss/clanky/");
            response.EnsureSuccessStatusCode();

            string xml = await response.Content.ReadAsStringAsync();

            XmlDocument doc2 = new XmlDocument();
            XmlNode rootNode = doc2.CreateElement("root");
            doc2.AppendChild(rootNode);

            for (int i = 0; i < 5; i++) 
            {
                XmlNode customerNode = doc2.CreateElement("customer");
                rootNode.AppendChild(customerNode);

                XmlNode nameNode = doc2.CreateElement("name");
                customerNode.AppendChild(nameNode);

                nameNode.AppendChild(doc2.CreateTextNode("Jan " + i));

                XmlAttribute idAttr = doc2.CreateAttribute("id");
                customerNode.Attributes.Append(idAttr);
                idAttr.Value = i.ToString();
            }

            XmlNode custNode = rootNode.ChildNodes[1];
            custNode.ParentNode.RemoveChild(custNode); 

            doc2.Save("test.xml");

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xml);

            /*foreach (XmlNode titleNode in xDoc.SelectNodes("/rss/channel/item/title")) // dá se tady dát "/rss/channel/item/title/text()" a nemusí se pak dávat FirstChild.Value protože vrátí rovnou text.         "//item/title" hledá všude
            {
                string title = titleNode.FirstChild.Value;

                Console.WriteLine(title);
                Console.WriteLine();
            }*/

            foreach (XmlNode itemNode in xDoc.SelectNodes("//item"))
            {
                XmlNode titleNode = itemNode.SelectSingleNode("title/text()");
                XmlNode linkNode = itemNode.SelectSingleNode("link/text()");
                string title = titleNode.Value;
                string link = linkNode.Value;

                Console.WriteLine(title);
                Console.WriteLine(link);
                Console.WriteLine();

                linkNode.ParentNode.ParentNode.RemoveChild(linkNode.ParentNode);
            }

            xDoc.Save("rss.xml");




            /*
            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                { "name", "Jan" },
                { "orderName", "Černoch" },
                { "price", "20 000 Kč" }
            };

            string txt = "Ahoj {name}. Tvá objednávka „{orderName}“ v ceně {price} byla úspěšně uhrazena.";
            Regex regex = new Regex(@"\{\s*([a-zA-Z]+)\s*\}");

            string result = regex.Replace(txt, (Match match) =>
            {
                string key = match.Groups[1].Value;
                return data[key];
            });

            Console.WriteLine(result);

            /*

            Regex loginRegex = new Regex(@"^[A-Z]{3}[0-9]{3,4}$", RegexOptions.IgnoreCase | RegexOptions.Compiled); // kompilace trvá nějakou dobu ale může zrychlit skibidi
            // "^[a-zA-Z+_0-9.]+@[a-zA-Z-]+\.[a-zA-Z]{2,24}$"
            Regex urlRegex = new Regex(@"^(https?):\/\/(?:([a-z]+)\.)?([a-z]+\.[a-z]{2,24})(?:\/|#|\?|$)");


            string urlStr = @"https://katedrainformatiky.cz/cs/pro-uchazece/zamereni-studia
http://katedrainformatiky.cz/
https://katedrainformatiky.cz?page=5
https://page.katedrainformatiky.cz?url=http://test.cz/
hahasomebs.com";

            string[] urls = urlStr.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (string url in urls)
            {
                Console.WriteLine(url);

                Match match = urlRegex.Match(url);
                if (match.Success)
                {
                    string protocol = match.Groups[1].Value;
                    string subdomain = match.Groups[2].Value;
                    string domain = match.Groups[3].Value;
                    Console.WriteLine(protocol);
                    Console.WriteLine(subdomain);
                    Console.WriteLine(domain);
                    Console.WriteLine("all gud");
                } else
                {
                    Console.WriteLine("neplatna url");
                }

                    Console.WriteLine();
            }

            /*
            while (true)
            {
                string str = Console.ReadLine();
                if (loginRegex.IsMatch(str))
                {
                    Console.WriteLine("Validní login.");
                }
                else
                {
                    Console.WriteLine("Chybný login!");
                }
            }*/
        }
    }
}
