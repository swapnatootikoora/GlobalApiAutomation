using NUnit.Framework;
using System.Net;
using Common.Models;
using Common.Helpers;
using Common.TestData;
using Common.ConfigUtils;
using System;
using System.Threading;
using Common.Tests;
using OrderApi.TestSetup;

namespace OrderApi.Tests
{
    [TestFixture]
    public class WhenGettingOrderItem: TestSetup<OrderItem>
    {
        private string newOrderId;
        private OrderItemType newOrderItemTypeDetails;
        private OrderItem Request;

        public OrderItem GetValidRequest()
        {
            return new OrderItem
            {
                OrderId = newOrderId,
                Name = "OrderItem",
                ExternalId = GenericLibrary.GenerateGuid(),
                TypeId = newOrderItemTypeDetails.Id,
                TypeKey = newOrderItemTypeDetails.Key,
                OwnerId = DataClass.PlannerOnly[0].Id,
                IsDisabled = false,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1),
                Revenue = 50.60M
            };
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            newOrderId = new OrderCreation().GetOrderID();
            newOrderItemTypeDetails = new OrderItemTypeCreation().GetOrderItemType();
            Thread.Sleep(5000); //Caching time is 5 sec for getting order item type
            httpService.EndPoint = AutomationVariables.OrderAPI + $"/{newOrderId}/orderItems";
            Request = GetValidRequest();
            var postResponse = httpService.PerformPost(Request);
            var response = JSONLibrary.DeserializeJSon<Order>(postResponse.Content);
            Request.Id = response.Id;
        }

        [Test]
        public void Should_get_all_order_items_successfully()
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
            Request.TypeKey = null;
            HandledRequestSuccessfully(apiResponse, Request);
        }

        [Test]
        public void Should_return_bad_request_when_getting_order_item_with_invalid_id()
        {
            // arrange
            var invalidId = GenericLibrary.GenerateGuid();

            // act
            var apiResponse = httpService.PerformGet(invalidId);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.NotFound);
        }
    }
}


