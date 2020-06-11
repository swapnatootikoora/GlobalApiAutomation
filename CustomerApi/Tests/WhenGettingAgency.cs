using System;
using System.Collections.Generic;
using System.Net;
using Common.Helpers;
using Common.Models;
using Common.TestData;
using Common.Tests;
using NUnit.Framework;

namespace CustomerApi.Tests
{
    [TestFixture]
    public class WhenGettingAgency : TestSetup<Agency>
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            httpService.EndPoint = AutomationVariables.AgencyAPI;
        }

        [Test]
        public void Should_return_all_active_agencies_successfully()
        {
            //arrange
            var request = DataClass.Agency1;
            Dictionary<string, object> filterList = new Dictionary<string, object>();
            filterList.Add("IsDisabled", false);

            //act
            var apiResponse = httpService.PerformGet();

            //assert
            HandledRequestSuccessfullyAndFilterValueMatches(apiResponse, filterList);
            ResponseShouldContainInstances(apiResponse, request.Agency.Id);
        }

        [Test]
        public void Should_return_specific_agency_successfully()
        {
            //arrange
            var request = DataClass.Agency1;

            //act           
            var apiResponse = httpService.PerformGet(request.Agency.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request.Agency);
        }

        [Test]
        public void Should_return_bad_request_when_agency_is_invalid()
        {
            //arrange
            var invalidAgencyId = GenericLibrary.GenerateGuid();

            // act
            var apiResponse = httpService.PerformGet(invalidAgencyId);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.NotFound);
        }
    }
}

