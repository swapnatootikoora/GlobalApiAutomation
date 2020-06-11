using Common.ConfigUtils;
using RestSharp;
using System;

namespace Common.Api
{
    public class ApiRequest
    {
        public RestClient Client { get; }
        private string EndPoint;
        private string BaseUrl { get; }

        public AutomationVariables AutomationVariables { get; set; }

        public ApiRequest()
        {
            this.AutomationVariables = AppSettingsInitialization.GetConfigInstance();

            var environment = Environment.GetEnvironmentVariable("RUNTIME_ENVIRONMENT") ?? "int";
            switch (environment.ToLower())
            {
                case "test":
                    BaseUrl = AutomationVariables.TestUrl;
                    break;
                case "int":
                    BaseUrl = AutomationVariables.IntUrl;
                    break;
                case "dev":
                    BaseUrl = AutomationVariables.DevUrl;
                    break;
            }

            Client = new RestClient(BaseUrl);
        }

        public IRestResponse GetApi(string endPoint)
        {
            EndPoint = BaseUrl + "/" + endPoint;
            Report.WriteToFile("EndPoint: " + EndPoint);

            var request = new RestRequest(EndPoint, Method.GET);
            return Client.Execute(request);
        }

        public IRestResponse PostApi(string endPoint, string postRequestBody)
        {
            EndPoint = BaseUrl + "/" + endPoint;
            Report.WriteToFile("EndPoint: " + EndPoint);
            var request = new RestRequest(EndPoint, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(postRequestBody);
            return Client.Execute(request);
        }

        public IRestResponse PutApi(string endPoint, string putRequestBody)
        {
            EndPoint = BaseUrl + "/" + endPoint;
            Report.WriteToFile("EndPoint: " + EndPoint);
            var request = new RestRequest(EndPoint, Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(putRequestBody);
            return Client.Execute(request);
        }

        public IRestResponse PatchApi(string endPoint, string patchRequestBody)
        {
            EndPoint = BaseUrl + "/" + endPoint;
            Report.WriteToFile("EndPoint: " + EndPoint);
            var request = new RestRequest(EndPoint, Method.PATCH);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(patchRequestBody);
            return Client.Execute(request);
        }
        public IRestResponse DeleteApi(string endPoint)
        {
            EndPoint = BaseUrl + "/" + endPoint;
            Report.WriteToFile("EndPoint: " + EndPoint);

            var request = new RestRequest(EndPoint, Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            return Client.Execute(request);
        }
    }
}
