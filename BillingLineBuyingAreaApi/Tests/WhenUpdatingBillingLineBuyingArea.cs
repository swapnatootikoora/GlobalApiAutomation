using System;
using System.Net;
using Common.Helpers;
using Common.Models;
using Common.Tests;
using NUnit.Framework;

namespace BillingLineBuyingAreaApi.Tests
{
    [TestFixture]
    public class WhenUpdatingBillingLineBuyingArea : TestSetup<BillingLineBuyingArea>
    {
        public BillingLineBuyingArea GetValidRequest()
        {
            return new BillingLineBuyingArea
            {
                ParentId = null,
                Name = "UpdateBuyingArea",
                EndDate = new DateTimeOffset(DateTime.Now.AddMonths(1)),
                AvailableForBillingLine = true,
                IsDisabled = false
            };
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            httpService.EndPoint = AutomationVariables.BillingLineBuyingAreaAPI;
        }

        [Test]
        public void Should_update_billing_line_buying_area_all_fields_successfully()
        {
            //act
            var request = GetValidRequest();
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<BillingLineBuyingArea>(postResponse.Content);
            request.Id = response.Id;
            request.Name = "UpdateName";
            request.EndDate = new DateTimeOffset(DateTime.Now.AddMonths(2));
            request.AvailableForBillingLine = false;
            request.IsDisabled = true;

            //arrange
            var apiResponse = httpService.PerformPut(request, request.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_update_billing_line_buying_area_from_active_to_inactive_successfully()
        {
            //act
            var request = GetValidRequest();
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<BillingLineBuyingArea>(postResponse.Content);
            request.Id = response.Id;
            request.IsDisabled = true;

            //arrange
            var apiResponse = httpService.PerformPut(request, request.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_return_bad_request_when_trying_to_move_active_buying_area_under_inactive_buying_area()
        {
            //arrange
            var request = GetValidRequest();
            request.IsDisabled = true;
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<BillingLineBuyingArea>(postResponse.Content);
            var parentId = response.Id;

            request = GetValidRequest();
            postResponse = httpService.PerformPost(request);
            response = JSONLibrary.DeserializeJSon<BillingLineBuyingArea>(postResponse.Content);
            request.Id = response.Id;
            request.ParentId = parentId;

            //act
            var apiResponse = httpService.PerformPut(request, request.Id);

            //assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Billing Line Buying Area isDisabled cannot be false when the Parent's is true");
        }
    }
}
