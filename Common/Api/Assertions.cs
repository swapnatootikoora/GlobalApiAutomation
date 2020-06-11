
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using Common.Helpers;
using Common.Models;
using Common.Tests;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;

namespace Common.Api
{
    public class Assertions<T> where T : class, IIdentity
    {
        protected HttpService<T> httpService { get; set; }

        public Assertions()
        {
            httpService = new HttpService<T>();
        }

        protected void HandledRequestSuccessfully(IRestResponse response, T expectedResult)
        {
            StatusCodeShouldBeIsSuccessful(response);
            var actualResponse = JSONLibrary.DeserializeJSon<T>(response.Content);
            expectedResult.Id = actualResponse.Id;
            actualResponse.Should().BeEquivalentTo(expectedResult);
        }

        protected void HandledRequestSuccessfullyAndFieldValueMatches(IRestResponse response, T expectedResult, List<string> fieldTypes)
        {
            StatusCodeShouldBeIsSuccessful(response);
            var actualResponse = JSONLibrary.DeserializeJSon<T>(response.Content);
            expectedResult.Id = actualResponse.Id;

            Type objectType = typeof(T);
            PropertyInfo[] objectProperties = objectType.GetProperties();

            foreach (string fieldType in fieldTypes)
            {
                foreach (PropertyInfo propertyInfo in objectProperties)
                {
                    var fieldName = propertyInfo.Name;
                    if (fieldName == fieldType)
                    {
                        actualResponse.GetType().GetRuntimeProperty(fieldName).Should().BeSameAs(expectedResult.GetType().GetRuntimeProperty(fieldName));
                    }
                }
            }
        }

        protected void HandledRequestSuccessfullyAndFieldValueMatches(IRestResponse response, T expectedResult, string fieldType)
        {
            HandledRequestSuccessfullyAndFieldValueMatches(response, expectedResult, new List<string> { fieldType });
        }

        protected void StatusCodeShouldBeIsSuccessful(IRestResponse response)
        {
            StatusCodeShouldBe(response, new List<HttpStatusCode> { HttpStatusCode.OK, HttpStatusCode.Created });
        }

        protected void StatusCodeShouldBeNotFound(IRestResponse response)
        {
            StatusCodeShouldBe(response, HttpStatusCode.NotFound);
        }

        protected void StatusCodeShouldBe(IRestResponse response, List<HttpStatusCode> expectedStatus)
        {
            response.StatusCode.Should().Match<HttpStatusCode>(s => expectedStatus.Any(e => e == s));
        }

        protected void StatusCodeShouldBe(IRestResponse response, HttpStatusCode expectedStatus, string message = null)
        {
            response.StatusCode.Should().Be(expectedStatus);
            if (message != null)
            {
                response.Content.Should().Contain(message);
            }
        }

        protected void ResponseShouldContainInstances(IRestResponse response, List<string> expectedInstances)
        {
            StatusCodeShouldBeIsSuccessful(response);
            var actualResponseInstances = JSONLibrary.DeserializeJSon<List<T>>(response.Content);
            List<string> actualResponseInstanceIds = actualResponseInstances.Select(i => i.Id).ToList();

            foreach (string expectedInstance in expectedInstances)
            {
                actualResponseInstanceIds.Should().Contain(expectedInstance);
            }
        }

        protected void ResponseShouldContainInstances(IRestResponse response, string expectedInstance)
        {
            ResponseShouldContainInstances(response, new List<string> { expectedInstance });
        }

        protected void HandledRequestSuccessfullyAndFilterValueMatches(IRestResponse response, Dictionary<string, object> filterDictionary)
        {
            StatusCodeShouldBeIsSuccessful(response);
            var actualResponses = JSONLibrary.DeserializeJSon<List<T>>(response.Content);

            foreach (var actualResponse in actualResponses)
            {
                foreach (KeyValuePair<string, object> entry in filterDictionary)
                {
                    Type objectType = typeof(T);
                    PropertyInfo[] objectProperties = objectType.GetProperties();

                    foreach (PropertyInfo propertyInfo in objectProperties)
                    {
                        var fieldName = propertyInfo.Name;
                        if (fieldName == entry.Key)
                        {
                            if (entry.Value is IList)
                            {
                                PropertyInfo pi = actualResponse.GetType().GetProperty(fieldName);
                                object responseFieldValue = pi.GetValue(actualResponse);
                                ((IList)(responseFieldValue)).Should().Contain((IList)(entry.Value));
                            }
                            else
                            {
                                actualResponse.GetType().GetRuntimeProperty(fieldName).Should().Equals(entry.Value);
                            }
                        }
                    }
                }
            }
        }
    }
}
