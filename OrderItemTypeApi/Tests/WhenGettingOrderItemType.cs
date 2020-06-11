using NUnit.Framework;
using Common.Models;
using Common.Helpers;
using Common.ConfigUtils;
using Common.Tests;

namespace OrderItemTypeApi.Tests
{
    [TestFixture]
    public class WhenGettingOrderItemType: TestSetup<OrderItemType>
    {
        OrderItemType Request;

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
		public void Should_return_all_order_items_successfully()
		{
			//act
			var apiResponse = httpService.PerformGet();

			//assert
			ResponseShouldContainInstances(apiResponse, Request.Id);
		}

        [Test]
        public void Should_return_specific_order_item_successfully()
        {
            //act
            var apiResponse = httpService.PerformGet(Request.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, Request);
        }      
    }
}
