using Common.Api;
using Common.Helpers;
using RestSharp;
using Common.Models;
using Common.ConfigUtils;

namespace Common.Tests
{
    public class HttpService<T> : IHttpService<T> where T : class, IIdentity
    {
        public virtual string EndPoint { get; set; }
        public ApiRequest ApiRequest { get; set; }

        public HttpService()
        {
            ApiRequest = new ApiRequest();
        }

        public IRestResponse PerformGet(string queryString = null)
        {
            var endPoint = (queryString != null) ? EndPoint + "/" + queryString : EndPoint;
            Report.WriteToFile("HTTP Verb: GET");
            var getResponse = ApiRequest.GetApi(endPoint);
            Report.WriteToFile("Response : " + "\n" + getResponse.Content);
            Report.WriteToFile("Status Code : " + getResponse.StatusCode);
            return getResponse;
        }

        public IRestResponse PerformPost(T ExpectedObject)
        {
            var postRequestBody = JSONLibrary.SerializeJSon<T>(ExpectedObject);
            Report.WriteToFile("HTTP Verb: POST");
            var postResponse = ApiRequest.PostApi(EndPoint, postRequestBody);
            Report.WriteToFile("Request : " + "\n" + postRequestBody);
            Report.WriteToFile("Response : " + "\n" + postResponse.Content);
            Report.WriteToFile("Status Code : " + postResponse.StatusCode);
            return postResponse ;
        }

        public IRestResponse PerformPut(T ExpectedObject, string queryString = null)
        {
            var endPoint = (queryString != null) ? EndPoint + "/" + queryString : EndPoint;
            var putRequestBody = JSONLibrary.SerializeJSon<T>(ExpectedObject);
            Report.WriteToFile("HTTP Verb: PUT");
            var putResponse = ApiRequest.PutApi(endPoint, putRequestBody);
            Report.WriteToFile("Request : " + "\n" + putRequestBody);
            Report.WriteToFile("Response : " + "\n" + putResponse.Content);
            Report.WriteToFile("Status Code : " + putResponse.StatusCode);
            return putResponse;
        }

        public IRestResponse PerformPatch(T ExpectedObject, string queryString = null)
        {
            var endPoint = (queryString != null) ? EndPoint + "/" + queryString : EndPoint;
            var patchRequestBody = JSONLibrary.SerializeJSon<T>(ExpectedObject);
            Report.WriteToFile("HTTP Verb: PATCH");
            var patchResponse = ApiRequest.PatchApi(endPoint, patchRequestBody);
            Report.WriteToFile("Request : " + "\n" + patchRequestBody);
            Report.WriteToFile("Response : " + "\n" + patchResponse.Content);
            Report.WriteToFile("Status Code : " + patchResponse.StatusCode);
            return patchResponse;
        }

        public IRestResponse PerformDelete(string id)
        {
            var endPoint = EndPoint + "/" + id;
            Report.WriteToFile("HTTP Verb: DELETE");
            var deleteResponse = ApiRequest.DeleteApi(endPoint);
            Report.WriteToFile("Status Code : " + deleteResponse.StatusCode);
            return deleteResponse;
        }
    }

    public interface IHttpService<T> where T : class, IIdentity
    {
        public IRestResponse PerformGet(string queryString = null);

        public IRestResponse PerformPost(T ExpectedObject);

        public IRestResponse PerformPut(T ExpectedObject, string queryString = null);

        public IRestResponse PerformDelete(string id);
    }
}
