using Aquality.Selenium.Browsers;
using RestSharp;
using System;
using System.Collections.Generic;
using TestVK.Utils;

namespace TestVK.ApiRequests
{
    internal static class ApiUtils
    {
        private static RestClient client = new RestClient(JsonUtils.GetUrl().ApiUrl);

        public static IRestResponse GetRequest(string path)
        {
            try
            {
                AqualityServices.Logger.Info($"Send GET request by '{path}'");
                RestRequest request = new RestRequest(path, Method.GET);
                return client.Execute(request);
            }
            catch (Exception e)
            {
                AqualityServices.Logger.Error(e.Message);
                return new RestResponse();
            }
        }

        public static IRestResponse PostRequest(string path, Dictionary<string, string> queryParams)
        {
            try
            {
                AqualityServices.Logger.Info($"Send POST request by '{path}'");
                RestRequest request = new RestRequest(path, Method.POST);
                foreach (KeyValuePair<string, string> item in queryParams)
                {
                    request.AddQueryParameter(item.Key, item.Value);
                }
                return client.Execute(request);
            }
            catch (Exception e)
            {
                AqualityServices.Logger.Error(e.Message);
                return new RestResponse();
            }
        }

        public static IRestResponse PostRequestFormData(string uploadURL, string filePath)
        {
            try
            {
                AqualityServices.Logger.Info($"Send POST request by '{uploadURL}'");
                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "multipart/form-data");
                request.AddFile("photo", filePath);
                RestClient client = new RestClient(uploadURL);
                return client.Execute(request);
            }
            catch (Exception e)
            {
                AqualityServices.Logger.Error(e.Message);
                return new RestResponse();
            }
        }
    }
}
