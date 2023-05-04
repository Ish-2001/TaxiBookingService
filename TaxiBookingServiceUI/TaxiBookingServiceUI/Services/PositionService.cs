using Newtonsoft.Json.Linq;
using TaxiBookingServiceUI.Models;

namespace TaxiBookingServiceUI.Services
{
    public static class PositionService
    {
        public static async Task<PositionViewModel> CalculateLocation(string address)
        {
            HttpClient httpClient = new HttpClient();

            string apiKey = "CUBhRBqPcYqsY8mz6inKzHqnG6pyF5yARXjxydVP0Pk";

            string requestUrl = $"https://atlas.microsoft.com/search/address/json?api-version=1.0&subscription-key={apiKey}&query={Uri.EscapeDataString(address)}";

            HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

            string responseJson = await response.Content.ReadAsStringAsync();
            JObject responseObj = JObject.Parse(responseJson);
            JToken positionToken = responseObj.SelectToken("$.results[0].position");

            return new PositionViewModel
            {
                Latitude = (double)positionToken.SelectToken("lat"),
                Longitude = (double)positionToken.SelectToken("lon")
            };

    }
}
}
