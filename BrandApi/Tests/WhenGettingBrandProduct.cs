using System;
using System.Collections.Generic;
using System.Net;
using Common.Helpers;
using Common.Models;
using Common.TestData;
using Common.Tests;
using NUnit.Framework;

namespace BrandApi.Tests
{
    [TestFixture]
    public class WhenGettingBrandProduct : TestSetup<Product>
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            httpService.EndPoint = AutomationVariables.ClientAPI;
        }

        [Test]
        public void Should_return_all_products_for_specific_client_successfully()
        {
            //arrange
            var request = DataClass.ClientWithMultipleBrandsAndProducts;

            //act
            var apiResponse = httpService.PerformGet(request.Client.Id + "/brands/" + request.BrandData[0].Brand.Id + "/products");

            //assert
            ResponseShouldContainInstances(apiResponse, new List<string> { request.BrandData[0].Product[0].Id, request.BrandData[0].Product[1].Id });
        }

        [Test]
        public void Should_return_a_specific_product_for_specific_client_successfully()
        {
            //arrange
            var request = DataClass.ClientWithMultipleBrandsAndProducts;

            //act
            var apiResponse = httpService.PerformGet(request.Client.Id + "/brands/" + request.BrandData[0].Brand.Id + "/products/" + request.BrandData[0].Product[0].Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request.BrandData[0].Product[0]);

        }

        [Test]
        public void Should_return_bad_request_when_client_product_mismatch()
        {
            //arrange
            var request = DataClass.ClientWithMultipleBrandsAndProducts;
            var invalidProductId = GenericLibrary.GenerateGuid();

            //act
            var apiResponse = httpService.PerformGet(request.Client.Id + "/brands/" + request.BrandData[0].Brand.Id + "/products/" + invalidProductId);

            //assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.NotFound);
        }
    }
}
