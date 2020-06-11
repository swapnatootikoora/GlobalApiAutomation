using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using Common.Models;
using Common.Helpers;
using Common.TestData;
using Common.ConfigUtils;
using Common.Tests;
using System;

namespace OrderApi.Tests
{
    [TestFixture]
    public class WhenUpdatingDirectClientOrder : TestSetup<Order>
    {
        public Order Request { get; set; }

        public Order GetValidRequest()
        {
            return new Order
            {
                Name = "DirectOrder",
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
            Request = GetValidRequest();
            var postResponse = httpService.PerformPost(Request);
            var response = JSONLibrary.DeserializeJSon<Order>(postResponse.Content);
            Request.Id = response.Id;
        }


        [Test]
        public void Should_update_direct_client_order_with_all_fields_updated()
        {
            // arrange
            var request = GetValidRequest();
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<Order>(postResponse.Content);
            request.Id = response.Id;
            request.Name = "UpdatedDirectOrder";
            request.ClientId = DataClass.Agency1.ClientData[1].Client.Id;
            request.ContactIds = new List<string> { DataClass.Agency1.ClientData[1].Contact[0].Id };
            request.OwnerIds = new List<string> { DataClass.OwnerOnly[1].Id };
            request.ClientBrandId = DataClass.Agency1.ClientData[1].BrandData[0].Brand.Id;
            request.BrandProductId = DataClass.Agency1.ClientData[1].BrandData[0].Product[0].Id;

            //act
            var apiResponse = httpService.PerformPut(request, request.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_update_direct_client_order_with_updated_client_brand_and_product()
        {
            // arrange
            var request = GetValidRequest();
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<Order>(postResponse.Content);
            request.Id = response.Id;
            request.Name = "UpdatedDirectOrder";
            request.ClientId = DataClass.ClientWithMultipleBrandsAndProducts.Client.Id;
            request.ContactIds = new List<string> { DataClass.ClientWithMultipleBrandsAndProducts.Contact[0].Id };
            request.ClientBrandId = DataClass.ClientWithMultipleBrandsAndProducts.BrandData[1].Brand.Id;
            request.BrandProductId = DataClass.ClientWithMultipleBrandsAndProducts.BrandData[1].Product[0].Id;

            //act
            var apiResponse = httpService.PerformPut(request, request.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_update_direct_client_order_with_blank_product()
        {
            // arrange
            var request = GetValidRequest();
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<Order>(postResponse.Content);
            request.Id = response.Id;
            request.Name = "UpdatedDirectOrder";
            request.BrandProductId = null;

            //act
            var apiResponse = httpService.PerformPut(request, request.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_return_bad_request_when_updating_order_with_blank_name()
        {
            // arrange
            var request = (Order)((ICloneable)Request).Clone();
            request.Name = null;

            // act
            var apiResponse = httpService.PerformPut(request, request.Id);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Order Name has to be specified");
        }

        [Test]
        public void Should_return_bad_request_when_updating_order_with_blank_client_details()
        {
            // arrange
            var request = (Order)((ICloneable)Request).Clone();
            request.ClientId = null;

            // act
            var apiResponse = httpService.PerformPut(request, request.Id);

            // assert
            //Not having meaninful error description for this scenario
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_return_bad_request_when_updating_order_with_disabled_client()
        {
            // arrange
            var request = (Order)((ICloneable)Request).Clone();
            request.ClientId = DataClass.AgencyWithDisabledClient.ClientData[0].Client.Id;
            request.ContactIds = new List<string> { DataClass.AgencyWithDisabledClient.ClientData[0].Contact[0].Id };
            request.ClientBrandId = null;
            request.BrandProductId = null;

            // act
            var apiResponse = httpService.PerformPut(request, request.Id);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "The specified Client is invalid because it has been disabled");
        }

        [Test]
        public void Should_return_bad_request_when_updating_order_with_client_trading_type_2()
        {
            // arrange
            var request = (Order)((ICloneable)Request).Clone();
            request.ClientId = DataClass.Agency2.ClientData[0].Client.Id;
            request.ContactIds = new List<string> { DataClass.Agency2.ClientData[0].Contact[0].Id };
            request.ClientBrandId = DataClass.Agency2.ClientData[0].BrandData[0].Brand.Id;
            request.BrandProductId = DataClass.Agency2.ClientData[0].BrandData[0].Product[0].Id;

            // act
            var apiResponse = httpService.PerformPut(request, request.Id);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Client is wrong trading type");
        }

        [Test]
        public void Should_return_bad_request_when_updating_order_with_blank_contact()
        {
            // arrange
            var request = (Order)((ICloneable)Request).Clone();
            request.ContactIds = null;

            // act
            var apiResponse = httpService.PerformPut(request, request.Id);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "One or more Contact(s) needs to be specified");
        }

        [Test]
        public void Should_return_bad_request_when_updating_order_with_disabled_contact()
        {
            // arrange
            var request = (Order)((ICloneable)Request).Clone();
            request.ClientId = DataClass.ClientWithDisabledContact.Client.Id;
            request.ContactIds = new List<string> { DataClass.ClientWithDisabledContact.Contact[0].Id };
            request.ClientBrandId = null;
            request.BrandProductId = null;

            // act
            var apiResponse = httpService.PerformPut(request, request.Id);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "One or more Contacts are invalid because they have been disabled");
        }

        [Test]
        public void Should_return_bad_request_when_updating_order_with_contact_not_linked_to_client()
        {
            // arrange
            var request = (Order)((ICloneable)Request).Clone();
            request.ContactIds = new List<string> { DataClass.Agency1.ClientData[1].Contact[0].Id };

            // act
            var apiResponse = httpService.PerformPut(request, request.Id);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "No Client Contact found for given Contact Id");
        }

        [Test]
        public void Should_return_bad_request_when_updating_order_with_blank_owner()
        {
            // arrange
            var request = (Order)((ICloneable)Request).Clone();
            request.OwnerIds = null;

            // act
            var apiResponse = httpService.PerformPut(request, request.Id);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "One or more Owner(s) needs to be specified");
        }

        [Test]
        public void Should_return_bad_request_when_updating_order_with_multiple_owners_including_disabled()
        {
            // arrange
            var request = (Order)((ICloneable)Request).Clone();
            request.OwnerIds = new List<string> { DataClass.OwnerOnly[0].Id, DataClass.DisabledOwnerAndPlanner[0].Id };

            // act
            var apiResponse = httpService.PerformPut(request, request.Id);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "One or more Owners are invalid because they have been disabled");
        }

        [Test]
        public void Should_return_bad_request_when_updating_order_with_disabled_Client_brand()
        {
            // arrange
            var request = (Order)((ICloneable)Request).Clone();
            request.ClientId = DataClass.AgencyWithDisabledClientBrandAndProduct.ClientData[0].Client.Id;
            request.ContactIds = new List<string> { DataClass.AgencyWithDisabledClientBrandAndProduct.ClientData[0].Contact[0].Id };
            request.ClientBrandId = DataClass.AgencyWithDisabledClientBrandAndProduct.ClientData[0].BrandData[0].Brand.Id;
            request.BrandProductId = DataClass.AgencyWithDisabledClientBrandAndProduct.ClientData[0].BrandData[0].Product[0].Id;

            // act
            var apiResponse = httpService.PerformPut(request, request.Id);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "The specified Client Brand is invalid because it has been disabled");
        }

        [Test]
        public void Should_return_bad_request_when_updating_order_with_client_not_linked_with_brand_and_product()
        {
            // arrange
            var request = (Order)((ICloneable)Request).Clone();
            request.ClientId = DataClass.Agency1.ClientData[1].Client.Id;
            request.ContactIds = new List<string> { DataClass.Agency1.ClientData[1].Contact[0].Id };

            // act
            var apiResponse = httpService.PerformPut(request, request.Id);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Either the Client Brand does not exist or it is not valid for the supplied Client");
        }

        [Test]
        public void Should_return_bad_request_when_updating_order_with_product_not_linked_with_brand()
        {
            // arrange
            var request = (Order)((ICloneable)Request).Clone();
            request.BrandProductId = DataClass.Agency1.ClientData[1].BrandData[0].Brand.Id;

            // act
            var apiResponse = httpService.PerformPut(request, request.Id);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Either the Brand Product does not exist or it is not valid for the supplied Client Brand");
        }
    }
}
