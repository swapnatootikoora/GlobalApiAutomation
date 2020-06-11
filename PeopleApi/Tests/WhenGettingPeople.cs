using NUnit.Framework;
using Common.Models;
using Common.Helpers;
using Common.ConfigUtils;
using Common.Tests;
using Common.TestData;
using System.Net;
using System.Collections.Generic;

namespace PeopleApi.Tests
{
    [TestFixture]
    public class WhenGettingPeople : TestSetup<People>
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            httpService.EndPoint = AutomationVariables.PeopleAPI;
        }

        [Test]
        public void Should_return_all_owners_successfully()
        {
            //assign
            Dictionary<string, object> filterList = new Dictionary<string, object>();
            filterList.Add("Capabilities", new List<int>() { 1 });

            //act
            var apiResponse = httpService.PerformGet("?Capability=Owner");

            //assert
            HandledRequestSuccessfullyAndFilterValueMatches(apiResponse, filterList);
        }

        [Test]
        public void Should_return_all_planners_successfully()
        {
            //assign
            Dictionary<string, object> filterList = new Dictionary<string, object>();
            filterList.Add("Capabilities", new List<int>() { 2 });

            //act
            var apiResponse = httpService.PerformGet("?Capability=Planner");

            //assert
            HandledRequestSuccessfullyAndFilterValueMatches(apiResponse, filterList);
        }

        //To fix assertion
        //[Test]
        //public void Should_return_all_owners_and_planners_successfully()
        //{
        //    //assign
        //    Dictionary<string, object> filterList = new Dictionary<string, object>();
        //    filterList.Add("Capabilities", new List<int>() { 1, 2 });

        //    //act
        //    var apiResponse = httpService.PerformGet("?Capability=Owner&Capability=Planner");

        //    //assert
        //    HandledRequestSuccessfullyAndFilterValueMatches(apiResponse, filterList);
        //}

        [Test]
        public void Should_return_specific_people_successfully()
        {
            //assign
            var request = DataClass.OwnerAndPlanner[0];

            //act
            var apiResponse = httpService.PerformGet(request.Id);

            //assert
            HandledRequestSuccessfully(apiResponse, request);
        }

        [Test]
        public void Should_return_bad_request_when_getting_people_with_invalid_Id()
        {
            //assert
            var invalidPeopleId = GenericLibrary.GenerateGuid();

            // act
            var apiResponse = httpService.PerformGet(invalidPeopleId);

            // assert
            StatusCodeShouldBe(apiResponse, HttpStatusCode.NotFound);
        }
    }
}