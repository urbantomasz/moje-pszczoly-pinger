using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;

namespace MojePszczolyPinger
{
    public class Function1
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<Function1> logger;

        public Function1(HttpClient httpClient, ILogger<Function1> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }

        [Function("Function1")]
        public async Task Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
        {
            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            try
            {
                string url = "https://mojepszczoly-h6crb8dragdhfhe7.polandcentral-01.azurewebsites.net/api/breads";


                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    logger.LogInformation($"Successfully pinged {url}");
                }
                else
                {
                    logger.LogError($"Failed to ping {url}. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception occurred while pinging: {ex.Message}");
            }
        }
    }
}
