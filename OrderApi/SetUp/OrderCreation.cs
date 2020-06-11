using System.Collections.Generic;
using System.Linq;
using Common.Api;
using Common.ConfigUtils;
using Common.Helpers;
using Common.Models;
using Common.TestData;

namespace OrderApi.TestSetup
{
    public class OrderCreation : Assertions<Order>
    {
        public string GetOrderID()
        {
            var automationVariables = AppSettingsInitialization.GetConfigInstance();
            httpService.EndPoint = automationVariables.OrderAPI;
            var orderResponse = httpService.PerformGet();
            var response = JSONLibrary.DeserializeJSon<List<Order>>(orderResponse.Content);
            var selectedResponse = response.FirstOrDefault(r => r.Name == "AutomationClientOrder");
            return (selectedResponse == null) ? GetNewOrderID() : selectedResponse.Id;
        }

        private string GetNewOrderID()
        {
            var orderRequest = new Order
            {
                Name = "AutomationClientOrder",
                ClientId = DataClass.Agency1.ClientData[0].Client.Id,
                ContactIds = new List<string> { DataClass.Agency1.ClientData[0].Contact[0].Id },
                OwnerIds = new List<string> { DataClass.OwnerOnly[0].Id },
                Status = 1,
                ClientBrandId = DataClass.Agency1.ClientData[0].BrandData[0].Brand.Id,
                BrandProductId = DataClass.Agency1.ClientData[0].BrandData[0].Product[0].Id,
                AgencyId = null
            };
            var automationVariables = AppSettingsInitialization.GetConfigInstance();
            httpService.EndPoint = automationVariables.OrderAPI;
            var orderResponse = httpService.PerformPost(orderRequest);
            var response = JSONLibrary.DeserializeJSon<Order>(orderResponse.Content);
            orderRequest.Id = response.Id;
            return orderRequest.Id;
        }
    }
}
