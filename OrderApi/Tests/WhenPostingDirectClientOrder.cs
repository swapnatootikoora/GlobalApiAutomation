using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using Common.Models;
using Common.Helpers;
using Common.TestData;
using Common.ConfigUtils;
using Common.Tests;

namespace OrderApi.Tests
{
    [TestFixture]
    public class WhenPostingDirectClientOrder : TestSetup<Order>
    {
        public Order GetValidRequest()
        {
            return new Order
            {
                Id = GenericLibrary.GenerateGuid(),
                Name = "ClientOrder",
                ClientId = DataClass.Agency1.ClientData[0].Client.Id,
                ContactIds = new List<string> { DataClass.Agency1.ClientData[0].Contact[0].Id },
                OwnerIds = new List<string> { DataClass.OwnerOnly[0].Id },
                Status = 1,
                ClientBrandId = DataClass.Agency1.ClientData[0].BrandData[0].Brand.Id,
                BrandProductId = DataClass.Agency1.ClientData[0].BrandData[0].Product[0].Id,
                AgencyId = null
            };
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            httpService.EndPoint = AutomationVariables.OrderAPI;
        }

        [Test]
        public void Should_create_direct_client_order_successfully()
        {
            // arrange
            var request = GetValidRequest();

            //act
            var apiResponse = httpService.PerformPost(request);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_create_direct_client_order_successfully_without_client_brand_and_product()
        {
            // arrange
            var request = GetValidRequest();
            request.ClientBrandId = null;
            request.BrandProductId = null;

            //act
            var apiResponse = httpService.PerformPost(request);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_create_direct_client_order_successfully_with_client_brand_but_without_brand_product()
        {
            // arrange
            var request = GetValidRequest();
            request.BrandProductId = null;

            //act
            var apiResponse = httpService.PerformPost(request);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_return_bad_request_when_creating_order_with_brand_product_but_without_client_brand()
        {
            // arrange
            var request = GetValidRequest();
            request.ClientBrandId = null;

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Either the Brand Product does not exist or it is not valid for the supplied Client Brand");
        }

        [Test]
        public void Should_return_bad_request_when_creating_order_with_disabled_client_brand()
        {
            // arrange
            var request = GetValidRequest();
            request.ClientId = DataClass.AgencyWithDisabledClientBrandAndProduct.ClientData[0].Client.Id;
            request.ContactIds = new List<string> { DataClass.AgencyWithDisabledClientBrandAndProduct.AgencyContact[0].Id };
            request.ClientBrandId = DataClass.AgencyWithDisabledClientBrandAndProduct.ClientData[0].BrandData[0].Brand.Id;
            request.BrandProductId = DataClass.AgencyWithDisabledClientBrandAndProduct.ClientData[0].BrandData[0].Product[0].Id;
            request.AgencyId = DataClass.AgencyWithDisabledClientBrandAndProduct.Agency.Id;

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "The specified Client Brand is invalid because it has been disabled");
        }

        [Test]
        public void Should_return_bad_request_when_creating_order_with_client_brand_and_brand_product_not_linked_together()
        {
            // arrange
            var request = GetValidRequest();
            request.BrandProductId = DataClass.Agency2.ClientData[0].BrandData[0].Product[0].Id;

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Either the Brand Product does not exist or it is not valid for the supplied Client Brand");
        }

        [Test]
        public void Should_return_bad_request_when_creating_direct_order_with_wrong_client_trading_type()
        {
            // arrange
            var request = GetValidRequest();
            request.ClientId = DataClass.Agency2.ClientData[0].Client.Id;

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Client is wrong trading type");
        }

        [Test]
        public void Should_return_bad_request_when_creating_direct_order_with_client_not_linked_with_client_brand_and_product()
        {
            // arrange
            var request = GetValidRequest();
            request.ClientBrandId = DataClass.Agency2.ClientData[0].BrandData[0].Brand.Id;
            request.BrandProductId = DataClass.Agency2.ClientData[0].BrandData[0].Product[0].Id;

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Either the Client Brand does not exist or it is not valid for the supplied Client");
        }

        [Test]
        public void Should_return_bad_request_when_creating_direct_order_with_owner_not_having_owner_capabilities()
        {
            // arrange
            var request = GetValidRequest();
            request.OwnerIds = new List<string> { DataClass.PlannerOnly[0].Id };

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "One or more Owners are invalid because they do not have Owner capability");
        }
    }
}


