using System;
using System.Net;
using Common.Helpers;
using Common.Models;
using Common.Tests;
using NUnit.Framework;

namespace BillingLineBuyingAreaApi.Tests
{
    [TestFixture]
    public class WhenGettingBillingLineBuyingArea : TestSetup<BillingLineBuyingArea>
    {
        BillingLineBuyingArea Request;

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
            Request = GetValidRequest();
            var postResponse = httpService.PerformPost(Request);
            var response = JSONLibrary.DeserializeJSon<BillingLineBuyingArea>(postResponse.Content);
            Request.Id = response.Id;
        }

        [Test]
        public void Should_return_all_buying_areas_sucuessfully()
        {
            //act
            var apiResponse = httpService.PerformGet();

            //assert
            ResponseShouldContainInstances(apiResponse, Request.Id);
        }

        [Test]
        public void Should_return_specific_buying_area_successfully()
        {
            //act
            var apiResponse = httpService.PerformGet(Request.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, Request);
        }

        [Test]
        public void Should_return_not_found_when_invalid_buying_area_provided()
        {
            // arrange
            var invalidBuyingAreaId = GenericLibrary.GenerateGuid();

            // act
            var apiResponse = httpService.PerformGet(invalidBuyingAreaId);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.NotFound);
        }
    }
}

