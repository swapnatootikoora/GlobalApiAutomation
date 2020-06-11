using NUnit.Framework;
using System.Collections.Generic;
using Common.Api;
using System.Net;
using Common.Models;
using Common.Helpers;
using Common.TestData;
using Common.ConfigUtils;
using Common.Tests;
using System.Threading;

namespace OrderItemTypeApi.Tests
{
    [TestFixture]
    public class WhenPostingOrderItemType : TestSetup<OrderItemType>
    {
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
        }


        [Test]
        public void Should_create_order_item_successfully()
        {
            // arrange
            var request = GetValidRequest();

            //act
            var apiResponse = httpService.PerformPost(request);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_return_bad_request_when_creating_order_item_with_blank_name()
        {
            // arrange
            var request = GetValidRequest();
            request.Name = null;

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Campaign Item Type Name needs to be specified");
        }

        [Test]
        public void Should_return_bad_request_when_creating_order_item_with_name_length_less_than_3_characters()
        {
            // arrange
            var request = GetValidRequest();
            request.Name = "AA";

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Campaign Item Type Name length should be between 3 and 50");
        }

        [Test]
        public void Should_return_bad_request_when_creating_order_item_with_blank_status()
        {
            // arrange
            var request = GetValidRequest();
            request.InitialStatus = null;

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Campaign Item Type Initial Status needs to be specified");
        }

        [Test]
        public void Should_return_bad_request_when_creating_order_item_with_invalid_url()
        {
            // arrange
            var request = GetValidRequest();
            request.Url = "test";

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Campaign Item Type URL is not in correct format");
        }

        [Test]
        public void Should_return_bad_request_when_creating_order_item_with_invalid_status()
        {
            // arrange
            var request = GetValidRequest();
            request.InitialStatus = "Ready";

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Campaign Item Type Initial Status not found in list of valid statuses");
        }

        [Test]
        public void Should_return_bad_request_when_creating_order_item_with_existing_order_item_name()
        {
            // arrange
            var request = GetValidRequest();
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<OrderItem>(postResponse.Content);
            request.Id = response.Id;
            request.Key = GenericLibrary.GenerateGuid();

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "This Campaign Item Type Name already exists");
        }

        [Test]
        public void Should_return_bad_request_when_creating_order_item_with_existing_order_item_key()
        {
            // arrange
            var request = GetValidRequest();
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<OrderItemType>(postResponse.Content);
            request.Id = response.Id;
            request.Name = "OrderItem_" + GenericLibrary.GenerateGuid();

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "This Campaign Item Type Key already exists");
        }
    }
}


