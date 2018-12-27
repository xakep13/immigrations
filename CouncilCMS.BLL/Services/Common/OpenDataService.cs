using RestSharp;
using System;
using Bissoft.CouncilCMS.BLL.ViewModels;

namespace Bissoft.CouncilCMS.BLL.Services
{
    public class OpenDataService : BaseService
    {

        public OpenDataService(String connectionString) : base(connectionString)
        {
            
        }

        public String InsertOpenData(OpenDataDocument model)
        {            
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("http://data.gov.ua/api-dataset/dataset?api=");   
                    
            RestRequest request = new RestRequest();
            request.AddParameter("api", model.ApiKey, ParameterType.UrlSegment);
            request.Resource = "{api}";
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(model);
            request.Method = Method.POST;
            var responce = client.Execute(request);
            
            var responceData = responce.Content;
            return null;
        }
        public String UpdateOpenData(OpenDataDocument model)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("http://data.gov.ua/api-dataset/dataset/");

            RestRequest request = new RestRequest();
            request.AddParameter("dataset-id", model.Id, ParameterType.UrlSegment);
            request.AddParameter("api", model.ApiKey, ParameterType.UrlSegment);
            request.Resource = "{dataset-id}?api-key={api}";
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(model);
            request.Method = Method.POST;
            var responce = client.Execute(request);

            var responceData = responce.Content;
            return null;
        }
    }
}
