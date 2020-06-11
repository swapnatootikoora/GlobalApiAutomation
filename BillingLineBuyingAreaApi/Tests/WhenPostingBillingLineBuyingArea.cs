using System;
using System.Net;
using Common.Helpers;
using Common.Models;
using Common.Tests;
using NUnit.Framework;

namespace BillingLineBuyingAreaApi.Tests
{
    [TestFixture]
    public class WhenPostingBillingLineBuyingArea : TestSetup<BillingLineBuyingArea>
    {
        public BillingLineBuyingArea GetValidRequest()
        {
            return new BillingLineBuyingArea
            {
                ParentId = null,
                Name = "NewBuyingArea",
                EndDate = new DateTimeOffset(DateTime.Now.AddMonths(1)),
                AvailableForBillingLine = true,
                IsDisabled = false
            };
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            httpService.EndPoint = AutomationVariables.BillingLineBuyingAreaAPI;
        }

        [Test]
        public void Should_create_billing_line_buying_area_successfully_with_null_parent_buying_area_id()
        {
            // arrange
            var request = GetValidRequest();

            //act
            var apiResponse = httpService.PerformPost(request);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_create_billing_line_buying_area_successfully()
        {
            // arrange
            var request = GetValidRequest();
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<BillingLineBuyingArea>(postResponse.Content);
            request.ParentId = response.Id;
            request.Name = "NewChildBuyingArea";

            //act
            var apiResponse = httpService.PerformPost(request);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_return_inactive_buying_area_when_creating_active_buying_area_under_inactive_parent_buying_area()
        {
            // arrange
            var request = GetValidRequest();
            request.IsDisabled = true;
            var postResponse = httpService.PerformPost(request);
            var inActiveParentResponse = JSONLibrary.DeserializeJSon<BillingLineBuyingArea>(postResponse.Content);
            var ParentId = inActiveParentResponse.Id;
            request.ParentId = ParentId;
            request.IsDisabled = false;

            //act
            var apiResponse = httpService.PerformPost(request);

            //assert
            request.IsDisabled = true;
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_return_inactive_buying_area_when_creating_inactive_buying_area_under_active_parent_buying_area()
        {
            // arrange
            var request = GetValidRequest();
            var postResponse = httpService.PerformPost(request);
            var activeParentResponse = JSONLibrary.DeserializeJSon<BillingLineBuyingArea>(postResponse.Content);
            var ParentId = activeParentResponse.Id;
            request.ParentId = ParentId;
            request.IsDisabled = true;

            //act
            var apiResponse = httpService.PerformPost(request);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_return_bad_request_when_creating_buying_area_without_name()
        {
            // arrange
            var request = GetValidRequest();
            request.Name = null;

            //act
            var apiResponse = httpService.PerformPost(request);

            //assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Name has to be specified");
        }

        [Test]
        public void Should_return_bad_request_when_creating_buying_area_with_name_less_than_three_characters()
        {
            // arrange
            var request = GetValidRequest();
            request.Name = "st";

            //act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Name length should be between 3 and 512 characters");
        }

        [Test]
        public void Should_return_bad_request_when_creating_buying_area_with_name_greater_than_512_characters()
        {
            // arrange
            var request = GetValidRequest();
            request.Name = "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS";

            //act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Name length should be between 3 and 512 characters");
        }
    }
}
