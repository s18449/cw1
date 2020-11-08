using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentNullException("You have to pass the URL as first parameter");
            }


            bool result = Uri.TryCreate(args[0], UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttps || uriResult.Scheme == Uri.UriSchemeHttp);

            if (!result)
            {
                throw new ArgumentException("URL is not connect");
            }

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(args[0]);

            if(response.StatusCode == HttpStatusCode.OK)
            {
                var html = await response.Content.ReadAsStringAsync();
                var regex = new Regex("[a-zA-Z0-9]+@[a-z.]+");

                MatchCollection matches = regex.Matches(html);
                HashSet<string> hashSet = new HashSet<string>();
                foreach(Match i in matches)
                {
                    hashSet.Add(i.Value);
                }

                foreach(string i in hashSet)
                {
                    Console.WriteLine(i);
                }
            }
            else
            {
                Console.WriteLine("Błąd w czasie pobierania strony");
            }

        }
    }
}
