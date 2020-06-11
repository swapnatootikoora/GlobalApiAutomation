using System.Collections.Generic;
using Common.ConfigUtils;
using Common.Models;
using NUnit.Framework;
using System.Net;
using Common.Tests;
using BillingLineProductApi.SetUp;
using Common.Helpers;

namespace BillingLineProductApi.Tests
{
    [TestFixture]
    public class WhenPostingBillingLineProduct : TestSetup<BillingLineProduct>
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
        public void Should_create_billing_line_product_successfully()
        {
            // arrange
            var request = GetValidRequest();

            //act
            var apiResponse = httpService.PerformPost(request);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_create_inactive_billing_line_product_successfully()
        {
            // arrange
            var request = GetValidRequest();
            request.IsDisabled = true;

            //act
            var apiResponse = httpService.PerformPost(request);

            // assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_return_bad_request_for_disabled_BuyingAreaId()
        {
            // arrange
            var request = GetValidRequest();
            request.BuyingAreaIds = new List<string> { new BillingLineBuyingAreaCreation().GetDisabledBuyingAreaId() };

            //act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "No Product Buying Area for given Buying Area Id");
        }

        [Test]
        public void Should_return_bad_request_when_name_is_null()
        {
            // arrange
            var request = GetValidRequest();
            request.Name = null;

            //act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Product Name cannot be empty");
        }

        [Test]
        public void Should_return_bad_request_for_empty_name()
        {
            // arrange
            var request = GetValidRequest();
            request.Name = "";

            //act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Product Name should be between 3 and 512");
        }

        [Test]
        public void Should_return_bad_request_for_name_less_than_three_characters()
        {
            // arrange
            var request = GetValidRequest();
            request.Name = "st";

            //act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Product Name should be between 3 and 512");
        }

        [Test]
        public void Should_return_bad_request_for_name_greater_than_512_characters()
        {
            // arrange
            var request = GetValidRequest();
            request.Name = GenericLibrary.RandomString(513);

            //act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Product Name should be between 3 and 512");
        }
    }
}


