using System.Collections.Generic;
using BillingLineProductApi.SetUp;
using Common.Helpers;
using Common.Models;
using Common.Tests;
using NUnit.Framework;

namespace BillingLineProductApi.Tests
{
    [TestFixture]
    public class WhenGettingBillingLineProduct : TestSetup<BillingLineProduct>
    {
        private BillingLineProduct Request;
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
            Request = GetValidRequest();
            var postResponse = httpService.PerformPost(Request);
            var response = JSONLibrary.DeserializeJSon<BillingLineProduct>(postResponse.Content);
            Request.Id = response.Id;
        }

        [Test]
        public void Should_return_all_products_successfully()
        {
            //act
            var apiResponse = httpService.PerformGet();

            //assert
            ResponseShouldContainInstances(apiResponse, Request.Id);
        }

        [Test]
        public void Should_return_specific_product_successfully()
        {
            //act
            var apiResponse = httpService.PerformGet(Request.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, Request);
        }
    }
}
