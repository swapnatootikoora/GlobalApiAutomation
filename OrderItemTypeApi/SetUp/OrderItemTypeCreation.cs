using System.Collections.Generic;
using System.Linq;
using Common.Api;
using Common.ConfigUtils;
using Common.Helpers;
using Common.Models;

namespace OrderApi.TestSetup
{
    public class OrderItemTypeCreation : Assertions<OrderItemType>
    {
        public OrderItemType GetOrderItemType()
        {
            var automationVariables = AppSettingsInitialization.GetConfigInstance();
            httpService.EndPoint = automationVariables.OrderItemTypeAPI;
            var orderResponse = httpService.PerformGet();
            var response = JSONLibrary.DeserializeJSon<List<OrderItemType>>(orderResponse.Content);
            var selectedResponse = response.FirstOrDefault(r => r.Name == "AutomationOrderItem");
            return (selectedResponse == null) ? GetNewOrderItemType() : selectedResponse;
        }

        private OrderItemType GetNewOrderItemType()
        {
            var orderItemTypeRequest = new OrderItemType
            {
                Name = "AutomationOrderItem",
                IsDisabled = false,
                Url = "https://www.test.com",
                Key = GenericLibrary.RandomString(10),
                InitialStatus = "ReadyToBook"
            };
            var automationVariables = AppSettingsInitialization.GetConfigInstance();
            httpService.EndPoint = automationVariables.OrderItemTypeAPI;
            var orderItemTypeResponse = httpService.PerformPost(orderItemTypeRequest);
            var response = JSONLibrary.DeserializeJSon<OrderItemType>(orderItemTypeResponse.Content);
            orderItemTypeRequest.Id = response.Id;
            return orderItemTypeRequest;
        }
    }
}
