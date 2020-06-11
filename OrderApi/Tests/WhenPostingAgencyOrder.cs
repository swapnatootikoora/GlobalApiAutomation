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
    public class WhenPostingAgencyOrder : TestSetup<Order>
    {
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
        }

        [Test]
        public void Should_create_agency_order_successfully()
        {
            // arrange
            var request = GetValidRequest();

            //act
            var apiResponse = httpService.PerformPost(request);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_create_agency_order_successfully_without_client_brand_and_product()
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
        public void Should_return_bad_request_when_creating_agency_order_with_contact_not_exist_for_given_agency()
        {
            // arrange
            var request = GetValidRequest();
            request.ContactIds = new List<string> { DataClass.Agency2.AgencyContact[0].Id };

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "No Agency Contact found for given Contact Id");
        }

        [Test]
        public void Should_return_bad_request_when_creating_agency_order_with_disabled_owner()
        {
            // arrange
            var request = GetValidRequest();
            request.OwnerIds = new List<string> { DataClass.DisabledOwnerAndPlanner[0].Id };

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "One or more Owners are invalid because they have been disabled");
        }

        [Test]
        public void Should_return_bad_request_when_creating_agency_order_with_name_greater_than_1000_characters()
        {
            // arrange
            var request = GetValidRequest();
            request.Name = GenericLibrary.RandomString(1001);

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Order Name length should be between 3 and 1000 characters");
        }

        [Test]
        public void Should_return_bad_request_when_creating_agency_order_with_blank_order_name()
        {
            // arrange
            var request = GetValidRequest();
            request.Name = "";

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "Order Name has to be specified");
        }

        [Test]
        public void Should_return_bad_request_when_creating_agency_order_with_client_not_exist_for_given_agency()
        {
            // arrange
            var request = GetValidRequest();
            request.ClientId = DataClass.Agency2.ClientData[0].Client.Id;

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "No Client found for given Agency");
        }

        [Test]
        public void Should_return_bad_request_when_creating_agency_order_with_disabled_agency()
        {
            // arrange
            var request = GetValidRequest();
            request.ClientId = DataClass.DisabledAgency.ClientData[0].Client.Id;
            request.ContactIds = new List<string> { DataClass.DisabledAgency.AgencyContact[0].Id };
            request.ClientBrandId = null;
            request.BrandProductId = null;
            request.AgencyId = DataClass.DisabledAgency.Agency.Id;

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "The specified Agency is invalid because it has been disabled");
        }

        [Test]
        public void Should_return_bad_request_when_creating_agency_order_with_enabled_agency_having_disabled_client()
        {
            // arrange
            var request = GetValidRequest();
            request.ClientId = DataClass.AgencyWithDisabledClient.ClientData[0].Client.Id;
            request.ContactIds = new List<string> { DataClass.AgencyWithDisabledClient.AgencyContact[0].Id };
            request.ClientBrandId = null;
            request.BrandProductId = null;
            request.AgencyId = DataClass.AgencyWithDisabledClient.Agency.Id;

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "The specified Client is invalid because it has been disabled");
        }

        [Test]
        public void Should_return_bad_request_when_creating_agency_order_with_agency_and_client_having_null_values()
        {
            // arrange
            var request = GetValidRequest();
            request.AgencyId = null;
            request.ClientId = null;

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest); //Error message is not implemented for this scenario
        }

        [Test]
        public void Should_return_bad_request_when_creating_agency_order_with_disabled_contact()
        {
            // arrange
            var request = GetValidRequest();
            request.ClientId = DataClass.AgencyWithDisabledContact.ClientData[0].Client.Id;
            request.ContactIds = new List<string> { DataClass.AgencyWithDisabledContact.AgencyContact[0].Id };
            request.ClientBrandId = null;
            request.BrandProductId = null;
            request.AgencyId = DataClass.AgencyWithDisabledContact.Agency.Id;

            // act
            var apiResponse = httpService.PerformPost(request);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.BadRequest, "One or more Contacts are invalid because they have been disabled");
        }
    }
}


