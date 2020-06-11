using NUnit.Framework;
using Common.Models;
using Common.Helpers;
using Common.ConfigUtils;
using Common.Tests;

namespace OrderItemTypeApi.Tests
{
    [TestFixture]
    public class WhenUpdatingOrderItemType : TestSetup<OrderItemType>
    {
        public OrderItemType Request { get; set; }

        public OrderItemType GetValidRequest()
        {
            return new OrderItemType
            {
                Name = "OrderItem_" + GenericLibrary.GenerateGuid(),
                IsDisabled = false,
                Url = "https://www.test.com",
                Key = GenericLibrary.GenerateGuid(),
                InitialStatus = "ReadyToBook"
            };
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            httpService.EndPoint = AutomationVariables.OrderItemTypeAPI;
			Request = GetValidRequest();
            var postResponse = httpService.PerformPost(Request);
            var response = JSONLibrary.DeserializeJSon<OrderItemType>(postResponse.Content);
            Request.Id = response.Id;
        }


        [Test]
        public void Should_update_all_fields_of_order_item_successfully()
        {
            // arrange
            var request = GetValidRequest();
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<OrderItemType>(postResponse.Content);
            request.Id = response.Id;
            request.Name = response.Name + "X";
            request.IsDisabled = true;
            request.Url = "https://test.com";
            request.InitialStatus = "Completed";

            //act
            var apiResponse = httpService.PerformPut(request, request.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }
    }
}