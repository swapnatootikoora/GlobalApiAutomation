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
    public class WhenUpdatingAgencyOrder : TestSetup<Order>
    {
        public Order Request { get; set; }

        public Order GetValidRequest()
        {
            return new Order
            {
                Name = "AgencyOrder",
                ClientId = DataClass.Agency1.ClientData[0].Client.Id,
                ContactIds = new List<string> { DataClass.Agency1.AgencyContact[0].Id },
                OwnerIds = new List<string> { DataClass.OwnerOnly[0].Id },
                Status = 1,
                ClientBrandId = DataClass.Agency1.ClientData[0].BrandData[0].Brand.Id,
                BrandProductId = DataClass.Agency1.ClientData[0].BrandData[0].Product[0].Id,
                AgencyId = DataClass.Agency1.Agency.Id
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
        public void Should_update_agency_order_when_changing_client_within_same_agency_and_its_respective_fields()
        {
            // arrange
            var request = GetValidRequest();
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<Order>(postResponse.Content);
            request.Id = response.Id;
            request.Name = "UpdatedAgencyOrder";
            request.ClientId = DataClass.Agency1.ClientData[1].Client.Id;
            request.ClientBrandId = DataClass.Agency1.ClientData[1].BrandData[0].Brand.Id;
            request.BrandProductId = DataClass.Agency1.ClientData[1].BrandData[0].Product[0].Id;

            //act
            var apiResponse = httpService.PerformPut(request, request.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_update_agency_order_when_changing_agency_and_its_respective_fields()
        {
            // arrange
            var request = GetValidRequest();
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<Order>(postResponse.Content);
            request.Id = response.Id;
            request.Name = "UpdatedAgencyOrder";
            request.ClientId = DataClass.Agency2.ClientData[0].Client.Id;
            request.ContactIds = new List<string> { DataClass.Agency2.AgencyContact[0].Id };
            request.ClientBrandId = DataClass.Agency2.ClientData[0].BrandData[0].Brand.Id;
            request.BrandProductId = DataClass.Agency2.ClientData[0].BrandData[0].Product[0].Id;
            request.AgencyId = DataClass.Agency2.Agency.Id;

            //act
            var apiResponse = httpService.PerformPut(request, request.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_update_agency_order_when_setting_client_brand_and_product_to_null()
        {
            // arrange
            var request = GetValidRequest();
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<Order>(postResponse.Content);
            request.Id = response.Id;
            request.Name = "UpdatedAgencyOrder";
            request.ClientBrandId = null;
            request.BrandProductId = null;

            //act
            var apiResponse = httpService.PerformPut(request, request.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_update_agency_order_when_providing_multiple_contacts()
        {
            // arrange
            var request = GetValidRequest();
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<Order>(postResponse.Content);
            request.Id = response.Id;
            request.Name = "UpdatedAgencyOrder";
            request.ContactIds = new List<string> { DataClass.Agency1.AgencyContact[0].Id, DataClass.Agency1.AgencyContact[1].Id };

            //act
            var apiResponse = httpService.PerformPut(request, request.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_update_agency_order_when_providing_multiple_owners()
        {
            // arrange
            var request = GetValidRequest();
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<Order>(postResponse.Content);
            request.Id = response.Id;
            request.Name = "UpdatedAgencyOrder";
            request.OwnerIds = new List<string> { DataClass.OwnerOnly[0].Id, DataClass.OwnerOnly[1].Id };

            //act
            var apiResponse = httpService.PerformPut(request, request.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_return_bad_request_when_updating_order_with_name_less_than_3_characters()
        {
            // arrange
            var request = (Order)((ICloneable)Request).Clone();
            request.Name = GenericLibrary.RandomString(2);

            // act
            var apiResponse = httpService.PerformPut(request, request.Id);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Order Name length should be between 3 and 1000 characters");
        }

        [Test]
        public void Should_return_bad_request_when_updating_order_with_blank_agency()
        {
            // arrange
            var request = (Order)((ICloneable)Request).Clone();
            request.AgencyId = null;

            // act
            var apiResponse = httpService.PerformPut(request, request.Id);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "No Client Contact found for given Contact Id");
        }

        [Test]
        public void Should_return_bad_request_when_updating_order_with_enabled_agency_but_disabled_client()
        {
            // arrange
            var request = (Order)((ICloneable)Request).Clone();
            request.ClientId = DataClass.AgencyWithDisabledClient.ClientData[0].Client.Id;
            request.ContactIds = new List<string> { DataClass.AgencyWithDisabledClient.AgencyContact[0].Id };
            request.ClientBrandId = null;
            request.BrandProductId = null;
            request.AgencyId = DataClass.AgencyWithDisabledClient.Agency.Id;

            // act
            var apiResponse = httpService.PerformPut(request, request.Id);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "The specified Client is invalid because it has been disabled");
        }

        [Test]
        public void Should_return_bad_request_when_updating_order_with_blank_client()
        {
            // arrange
            var request = (Order)((ICloneable)Request).Clone();
            request.ClientId = null;

            // act
            var apiResponse = httpService.PerformPut(request, request.Id);

            // assert
            //Not having meaningful error description for this scenario
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_return_bad_request_when_updating_client_not_linked_with_agency()
        {
            // arrange
            var request = (Order)((ICloneable)Request).Clone();
            request.ClientId = DataClass.Agency2.ClientData[0].Client.Id;
            request.ClientBrandId = DataClass.Agency2.ClientData[0].BrandData[0].Brand.Id;
            request.BrandProductId = DataClass.Agency2.ClientData[0].BrandData[0].Product[0].Id;

            // act
            var apiResponse = httpService.PerformPut(request, request.Id);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "No Client found for given Agency");
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
        public void Should_return_bad_request_when_updating_order_with_disabled_client_product()
        {
            // arrange
            var request = (Order)((ICloneable)Request).Clone();
            request.ClientId = DataClass.AgencyWithDisabledClientBrandAndProduct.ClientData[1].Client.Id;
            request.ContactIds = new List<string> { DataClass.AgencyWithDisabledClientBrandAndProduct.AgencyContact[0].Id };
            request.ClientBrandId = DataClass.AgencyWithDisabledClientBrandAndProduct.ClientData[1].BrandData[0].Brand.Id;
            request.BrandProductId = DataClass.AgencyWithDisabledClientBrandAndProduct.ClientData[1].BrandData[0].Product[0].Id;
            request.AgencyId = DataClass.AgencyWithDisabledClientBrandAndProduct.Agency.Id;

            // act
            var apiResponse = httpService.PerformPut(request, request.Id);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "The specified Brand Product is invalid because it has been disabled");
        }

        [Test]
        public void Should_return_bad_request_when_updating_order_with_multiple_contacts_including_disabled_contact()
        {
            // arrange
            var request = (Order)((ICloneable)Request).Clone();
            request.ClientId = DataClass.AgencyWithDisabledContact.ClientData[0].Client.Id;
            request.ContactIds = new List<string> { DataClass.AgencyWithDisabledContact.AgencyContact[0].Id, DataClass.AgencyWithDisabledContact.AgencyContact[1].Id };
            request.ClientBrandId = null;
            request.BrandProductId = null;
            request.AgencyId = DataClass.AgencyWithDisabledContact.Agency.Id;

            // act
            var apiResponse = httpService.PerformPut(request, request.Id);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "One or more Contacts are invalid because they have been disabled");
        }
    }
}
