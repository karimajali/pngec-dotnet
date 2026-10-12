using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using PNGEC.API.Helper;

namespace PNGEC.ScheduleJob
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            MainAPI();
            Console.ReadKey();
        }

        #region Main API
        /// <summary>
        /// Mains the API.
        /// </summary>
        private async static void MainAPI()
        {

            // TLS Security Protocol
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            Console.WriteLine("{0} API call continue...", "Schedule Notification");

            await CallApis();

            Console.WriteLine("{0} API call completed.", "Schedule Notification");
            Environment.Exit(0);
        }
        #endregion

        #region CallApis
        /// <summary>
        /// Calls the apis.
        /// </summary>
        /// <param name="Apiname">The apiname.</param>
        protected static async Task CallApis()
        {
            HttpResponseMessage response = null;
           
            //GET Method  
            response = await APIs.ScheduleJob.ScheduleNotification();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("API Response : Successfully");
            }
            else
            {
                Console.WriteLine("API Response : Internal server Error");
            }
        }
        #endregion
    }
}
