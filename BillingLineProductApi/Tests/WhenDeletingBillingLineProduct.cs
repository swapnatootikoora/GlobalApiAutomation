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
    public class WhenDeletingBillingLineProduct : TestSetup<BillingLineProduct>
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
        public void Should_delete_specific_product_successfully()
        {
            //arrange
            var request = GetValidRequest();
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<BillingLineProduct>(postResponse.Content);
            request.Id = response.Id;

            //act
            var apiResponse = httpService.PerformDelete(request.Id);

            //assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.OK);
        }
        [Test]
        public void Should_delete_inactive_product_successfully()
        {
            //arrange
            var request = GetValidRequest();
            request.IsDisabled = true;
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<BillingLineProduct>(postResponse.Content);
            request.Id = response.Id;

            //act
            var apiResponse = httpService.PerformDelete(request.Id);

            //assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.OK);
        }
    }
}