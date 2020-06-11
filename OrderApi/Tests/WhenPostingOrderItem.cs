using NUnit.Framework;
using System.Net;
using Common.Models;
using Common.Helpers;
using Common.TestData;
using Common.ConfigUtils;
using System;
using OrderApi.TestSetup;
using System.Threading;
using Common.Tests;

namespace OrderApi.Tests
{
    [TestFixture]
    public class WhenPostingOrderItem: TestSetup<OrderItem>
    {       
        private string newOrderId;
        private static OrderItemType newOrderItemTypeDetails;

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
                StartDate = new DateTimeOffset(DateTime.Now),
                EndDate = new DateTimeOffset(DateTime.Now.AddMonths(1)),
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
        }


        [Test]
        public void Should_create_order_item_successfully()
        {
            // arrange
            var request = GetValidRequest();

            //act
            var apiResponse = httpService.PerformPost(request);

            //assert
            request.TypeKey = null;
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_create_order_item_without_revenue_successfully()
        {
            // arrange
            var request = GetValidRequest();
            request.Revenue = null;

            //act
            var apiResponse = httpService.PerformPost(request);

            //assert
            request.TypeKey = null;
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_create_order_item_with_null_external_id_successfully()
        {
            // arrange
            var request = GetValidRequest();
            request.ExternalId = null;

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            request.TypeKey = null;
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_return_bad_request_when_creating_order_item_with_null_type_key()
        {
            // arrange
            var request = GetValidRequest();
            request.TypeKey = null;

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            request.TypeKey = null;
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Order Item Type Key has to be specified");
        }

        [Test]
        public void Should_return_bad_request_when_creating_order_item_with_null_owner_id()
        {
            // arrange
            var request = GetValidRequest();
            request.OwnerId = null;

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            request.TypeKey = null;
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_return_bad_request_when_creating_order_item_with_blank_name()
        {
            // arrange
            var request = GetValidRequest();
            request.Name = "";

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            request.TypeKey = null;
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Order Item Name has to be specified");
        }

        [Test]
        public void Should_return_bad_request_when_creating_order_item_with_null_start_date()
        {
            // arrange
            var request = GetValidRequest();
            request.StartDate = null;

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            request.TypeKey = null;
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_return_bad_request_when_creating_order_item_with_null_end_date()
        {
            // arrange
            var request = GetValidRequest();
            request.EndDate = null;

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            request.TypeKey = null;
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest);
        }
    }
}


