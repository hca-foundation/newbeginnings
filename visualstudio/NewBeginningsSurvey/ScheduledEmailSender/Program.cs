using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledEmailSender
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Sending emails...");

            try
            {
                HttpClient client = new HttpClient();
                var response = await client.PostAsync("", null);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);
                Console.WriteLine("Done.");
            }
            catch(Exception e)
            {
                Console.WriteLine("Error sending emails");
                Console.WriteLine(e);
            }
        }
    }
}
