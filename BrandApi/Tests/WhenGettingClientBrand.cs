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
    public class WhenGettingClientBrand : TestSetup<Brand>
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            httpService.EndPoint = AutomationVariables.ClientAPI;
        }

        [Test]
        public void Should_return_all_client_brands_successfully()
        {
            //arrange
            var request = DataClass.ClientWithMultipleBrandsAndProducts;

            //act
            var apiResponse = httpService.PerformGet(request.Client.Id + "/brands");

            //assert
            ResponseShouldContainInstances(apiResponse, new List<string> { request.BrandData[0].Brand.Id, request.BrandData[1].Brand.Id });
        }

        [Test]
        public void Should_return_specific_client_brand_successfully()
        {
            //arrange
            var request = DataClass.ClientWithMultipleBrandsAndProducts;

            //act
            var apiResponse = httpService.PerformGet(request.Client.Id + "/brands/" + request.BrandData[0].Brand.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request.BrandData[0].Brand);
        }

        [Test]
        public void Should_return_bad_request_when_client_brand_mismatch()
        {
            //arrange
            var request = DataClass.ClientWithMultipleBrandsAndProducts;
            var invalidBrandId = GenericLibrary.GenerateGuid();

            //act
            var apiResponse = httpService.PerformGet(request.Client.Id + "/brands/" + invalidBrandId);

            //assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.NotFound);
        }
    }
}

