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
    public class WhenGettingClientContact : TestSetup<Contact>
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            httpService.EndPoint = AutomationVariables.ClientAPI;
        }

        [Test]
        public void Should_return_specific_client_contact_successfully()
        {
            //arrange
            var request = DataClass.ClientWithMultipleBrandsAndProducts;

            //act
            var apiResponse = httpService.PerformGet(request.Client.Id + "/contacts/" + request.Contact[0].Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request.Contact[0]);
        }

        [Test]
        public void Should_return_all_active_client_contact_successfully()
        {
            //arrange
            var request = DataClass.DirectClientOnly;
            Dictionary<string, object> filterList = new Dictionary<string, object>();
            filterList.Add("IsDisabled", false);

            //act
            var apiResponse = httpService.PerformGet(request.Client.Id + "/contacts/");

            //assert
            HandledRequestSuccessfullyAndFilterValueMatches(apiResponse, filterList);
        }

        [Test]
        public void Should_return_bad_request_when_client_contact_not_found()
        {
            //arrange
            var request = DataClass.ClientWithMultipleBrandsAndProducts;
            var invalidcontactId = GenericLibrary.GenerateGuid();

            //act
            var apiResponse = httpService.PerformGet(request.Client.Id + "/contacts/" + invalidcontactId);

            //assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.NotFound);
        }
    }
}
