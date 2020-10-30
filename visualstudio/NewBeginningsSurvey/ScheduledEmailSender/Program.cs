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
        static void Main(string[] args)
        {
            Console.WriteLine("Sending emails...");

            try
            {
                HttpClient client = new HttpClient();
                var response = client.PostAsync("", null).Result;
                response.EnsureSuccessStatusCode();

                var responseContent = response.Content.ReadAsStringAsync().Result;
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
