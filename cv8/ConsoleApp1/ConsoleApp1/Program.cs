using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            /*using HttpClient client = new HttpClient();

            string url = "https://www.vsb.cz";
            url = "https://1000logos.net/wp-content/uploads/2025/03/Rust-Logo.png";
            url = "https://www.7timer.info/bin/astro.php?lon=18.160005506399536&lat=49.831015379859586&ac=0&unit=metric&output=json&tzshift=0";
            url = "https://www.postb.in/1776065819048-2769169057719?test=ano";

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            /*request.Content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "nazevPromenne", "hodnota" },
                { "cislo", "20" }
            });*/

            /*request.Content = new StringContent(
                "{\"a\":5,\"b\":\"test\"}",
                Encoding.UTF8,
                "application/json"
                );*/

            /*
            using FileStream fs = new FileStream("rust.png", FileMode.Open);
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("hodnota proměnné"), "nazevPromenne");
            content.Add(new StringContent("20"), "b");
            content.Add(new StreamContent(fs), "promennaSeSouborem");
            request.Content = content;
            */

            /*request.Headers.Add("X-MyName", "Jan");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "ABC");

            using HttpResponseMessage response = await client.SendAsync(request);


            //using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            //using HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("Stránka nenalezena.");
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Chybaná odpověď.");
            }

            response.EnsureSuccessStatusCode(); // pokud je status code 200 - 299 tak se nic nestane. pokud něco jiného vyvolá vyjímku
            


            /*foreach(var header in response.Headers)
            {
                Console.WriteLine(header.Key + ": " + header.Value.First());
            }*/



            //string data = await client.GetStringAsync(url);

            //using Stream stream = await response.Content.ReadAsStreamAsync();
            //using FileStream fs = new FileStream("rust.png", FileMode.Create);
            //await stream.CopyToAsync(fs);


            //Console.WriteLine(data);

            /*string json = await response.Content.ReadAsStringAsync();
            JsonData data = JsonSerializer.Deserialize<JsonData>(json);
            foreach (JsonDataseries item in data.Dataseries)
            {
                Console.WriteLine(data.Init + " | " + item.Timepoint + ": " + item.Temp2m);
            }*/

            /*JsonData data = await response.Content.ReadFromJsonAsync<JsonData>();
            foreach (JsonDataseries item in data.Dataseries)
            {
                Console.WriteLine(data.Init + " | " + item.Timepoint + ": " + item.Temp2m);
            }*/

            MimeMessage msg = new MimeMessage();
            msg.Subject = "První email...";
            msg.To.Add(new MailboxAddress("Pepa", "kan0316@vsb.cz"));
            msg.From.Add(new MailboxAddress("Test", "atnet2019@seznam.cz"));

            BodyBuilder bb = new BodyBuilder();
            bb.TextBody = "helo :3";
            bb.HtmlBody = "helo <strong>:3</strong>";

            using FileStream fs = new FileStream("rust.png", FileMode.Open);
            bb.Attachments.Add("rust.png", fs);

            msg.Body = bb.ToMessageBody();

            using SmtpClient client = new SmtpClient();
            await client.ConnectAsync("smtp.seznam.cz", 465);
            await client.AuthenticateAsync("atnet2019@seznam.cz", "cviceni-C#2025");

            await client.SendAsync(msg);

            await client.DisconnectAsync(true);
        }
    }
}
