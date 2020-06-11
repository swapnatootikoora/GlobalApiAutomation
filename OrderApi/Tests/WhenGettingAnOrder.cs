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
    public class WhenGettingAnOrder: TestSetup<Order>
    {
        Order Request;

		public Order GetValidRequest()
		{
			return new Order
			{
				Id = GenericLibrary.GenerateGuid(),
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
		public void Should_return_all_orders_successfully()
		{
			//act
			var apiResponse = httpService.PerformGet();

			//assert
			ResponseShouldContainInstances(apiResponse, Request.Id);
		}


        [Test]
        public void Should_return_specific_direct_client_order_successfully()
        {
            // arrange
            var request = GetValidRequest();
            request.Name = "Direct order";
            request.AgencyId = null;
            request.ContactIds = new List<string> { DataClass.Agency1.ClientData[0].Contact[0].Id };
            var postResponse = httpService.PerformPost(request);
            var response = JSONLibrary.DeserializeJSon<Order>(postResponse.Content);
            request.Id = response.Id;

            //act
            var apiResponse = httpService.PerformGet(request.Id);

			//assert
			HandledRequestSuccessfully(apiResponse, request);
		}

        [Test]
        public void Should_return_specific_agency_order_successfully()
        {
            //act
            var apiResponse = httpService.PerformGet(Request.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, Request);
        }

        [Test]
        public void Should_return_not_found_when_invalid_order_provided()
        {
			// arrange
			var invalidOrderId = GenericLibrary.GenerateGuid();

            // act
            var apiResponse = httpService.PerformGet(invalidOrderId);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.NotFound);
        }        
    }
}
