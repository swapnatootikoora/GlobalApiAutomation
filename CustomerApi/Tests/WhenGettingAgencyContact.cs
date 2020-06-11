using System.Net;
using Common.Helpers;
using Common.Models;
using Common.TestData;
using Common.Tests;
using NUnit.Framework;

namespace CustomerApi.Tests
{
    [TestFixture]
    public class WhenGettingAgencyContact : TestSetup<Contact>
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            httpService.EndPoint = AutomationVariables.AgencyAPI;
        }

        [Test]
        public void Should_return_all_contacts_for_a_specific_agency()
        {
            //arrange
            var request = DataClass.Agency1;

            //act
            var apiResponse = httpService.PerformGet(request.Agency.Id + "/contacts");

            //assert
            ResponseShouldContainInstances(apiResponse, request.AgencyContact[0].Id);
        }

        [Test]
        public void Should_return_single_contact_for_a_specific_agency()
        {
            //arrange
            var request = DataClass.Agency1;

            //act
            var apiResponse = httpService.PerformGet(request.Agency.Id + "/contacts/" + request.AgencyContact[0].Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request.AgencyContact[0]);
        }

        [Test]
        public void Should_return_bad_request_when_agency_contact_is_invalid()
        {
            //arrange
            var request = DataClass.Agency1;
            var invalidAgencyContactId = GenericLibrary.GenerateGuid();

            // act
            var apiResponse = httpService.PerformGet("/" + request.Agency.Id + "/contacts/" + invalidAgencyContactId);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.NotFound);
        }
    }
}

