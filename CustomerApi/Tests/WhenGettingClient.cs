using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Common.Helpers;
using Common.Models;
using Common.TestData;
using Common.Tests;
using FluentAssertions;
using NUnit.Framework;

namespace CustomerApi.Tests
{
    [TestFixture]
    public class WhenGettingClient : TestSetup<Client>
    {
        [SetUp]
        public new void SetUp()
        {
            httpService.EndPoint = AutomationVariables.ClientAPI;
        }

        [Test]
        public void Should_return_all_active_clients_successfully()
        {
            //arrange
            var request = DataClass.DirectClientOnly;
            Dictionary<string, object> filterList = new Dictionary<string, object>();
            filterList.Add("IsDisabled", false);

            //act
            var apiResponse = httpService.PerformGet();

            //assert
            HandledRequestSuccessfullyAndFilterValueMatches(apiResponse, filterList);
            ResponseShouldContainInstances(apiResponse, request.Client.Id);
        }

        [Test]
        public void Should_return_specific_client_successfully()
        {
            //arrange
            var request = DataClass.ClientWithDisabledContact;

            //act           
            var apiResponse = httpService.PerformGet(request.Client.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request.Client);
        }

        [Test]
        public void Should_return_bad_request_when_client_is_invalid()
        {
            //arrange
            var invalidClientId = GenericLibrary.GenerateGuid();

            // act
            var apiResponse = httpService.PerformGet(invalidClientId);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.NotFound);
        }

        [Test]
        public void Should_return_all_active_agency_clients_for_a_specific_agency()
        {
            //arrange
            httpService.EndPoint = AutomationVariables.AgencyAPI;
            var request = DataClass.Agency1;

            //act
            var apiResponse = httpService.PerformGet(request.Agency.Id + "/clients");

            //assert
            ResponseShouldContainInstances(apiResponse, request.ClientData[0].Client.Id);
        }

        [Test]
        public void Should_return_specific_agency_client()
        {
            //arrange
            httpService.EndPoint = AutomationVariables.AgencyAPI;
            var request = DataClass.Agency2;

            //act           
            var apiResponse = httpService.PerformGet(request.Agency.Id + "/clients/" + request.ClientData[0].Client.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request.ClientData[0].Client);
        }
    }
}
