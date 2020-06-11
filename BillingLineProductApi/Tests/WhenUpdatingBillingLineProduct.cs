using System.Collections.Generic;
using System.Net;
using BillingLineProductApi.SetUp;
using Common.Helpers;
using Common.Models;
using Common.Tests;
using NUnit.Framework;

namespace BillingLineProductApi.Tests
{
    [TestFixture]
    public class WhenUpdatingBillingLineProduct : TestSetup<BillingLineProduct>
    {
        private List<string> BuyingAreaIds;

        public BillingLineProduct GetValidRequest()
        {
            return new BillingLineProduct
            {
                Name = "NewBillingLineProduct",
                BuyingAreaIds = BuyingAreaIds,
                IsDisabled = false
            };
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            httpService.EndPoint = AutomationVariables.BillingLineProductAPI;
            BuyingAreaIds = new List<string> { new BillingLineBuyingAreaCreation().GetBuyingAreaId(), new BillingLineBuyingAreaCreation().GetBuyingAreaId() };
        }

        [Test]
        public void Should_update_billing_line_product_name_successfully()
        {
            //act
            var request = GetValidRequest();
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<BillingLineProduct>(postResponse.Content);
            request.Id = response.Id;
            request.Name = "UpdateName";

            //assign
            var apiResponse = httpService.PerformPut(request, request.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }
        [Test]
        public void Should_update_billing_line_product_isdisabled_successfully()
        {
            //act
            var request = GetValidRequest();
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<BillingLineProduct>(postResponse.Content);
            request.Id = response.Id;
            request.IsDisabled = true;

            //assign
            var apiResponse = httpService.PerformPut(request, request.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_return_bad_request_when_update_product_with_inactive_buying_area_id()
        {
            //act
            var request = GetValidRequest();
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<BillingLineProduct>(postResponse.Content);
            request.Id = response.Id;
            request.BuyingAreaIds = new List<string> { new BillingLineBuyingAreaCreation().GetDisabledBuyingAreaId() };

            //assign
            var apiResponse = httpService.PerformPut(request, request.Id);

            //assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "No Product Buying Area for given Buying Area Id");
        }
    }
}
