using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TinyShop.RestUtils.Common;
using TinyShop.RestUtils.Common.Dto;

namespace TinyShop.RestUtils.NovaPoshta
{
    public class NovaPoshtaClient : DeliveryRestClientAbstract
    {
        public NovaPoshtaClient()
        {
            BaseUrl = NovaPoshtaOptions.BaseUrl;
            ApiKey = NovaPoshtaOptions.ApiKey;

            _restClient = new RestClient( BaseUrl );
        }
        public override string ApiKey { get; protected set; }
        public override string BaseUrl { get; protected set; }

        protected async Task<string> GetJsonString(string jsonBody)
        {
            var request = new RestRequest();
            request.AddHeader( "Content-Type", "application/json" );
            request.AddHeader( "Accept", "application/json" );
            request.AddHeader( "User-Agent", "TinyShop" );
            request.AddJsonBody( jsonBody );
            var response = await _restClient.GetAsync( request );
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return string.Empty;
            }
            return response.Content;
        }
        public override async Task<List<RegionDto>> GetRegionsAsync()
        {
            string jsonString = await GetJsonString(
$@"
{{
   ""apiKey"": ""{ApiKey}"",
   ""modelName"": ""Address"",
   ""calledMethod"": ""getAreas"",
   ""methodProperties"": {{   }}
}}
"
            );
            var jsonValue = JsonNode.Parse( jsonString ).AsObject();            
            return jsonValue["data"].AsArray().Select( x =>
                new RegionDto
                {
                    Id = (string)x.AsObject()["Ref"],
                    Name = (string)x.AsObject()["Description"],
                    RawJson = x.ToString()
                } )
                .ToList();
        }

        public override async Task<List<CityDto>> GetCitiesByRegionAsync(string regionId)
        {
            string jsonString = await GetJsonString(
$@"
{{
""apiKey"": ""{ApiKey}"",
""modelName"": ""Address"",
""calledMethod"": ""getSettlements"",
""methodProperties"": {{
""AreaRef"" : ""{regionId}"",
""Ref"" : """",
""RegionRef"" : """",
""Page"" : ""15"",
""Warehouse"" : ""1"",
""FindByString"" : """",
""Limit"" : ""150""
   }}
}}"
            );
            var jsonValue = JsonNode.Parse( jsonString ).AsObject();
            return jsonValue["data"].AsArray().Select( x =>
                new CityDto
                {
                    Id = (string)x.AsObject()["Ref"],
                    Name = (string)x.AsObject()["Description"],
                    RawJson = x.ToString()
                } )
                .ToList();
        }

        private RestClient _restClient;
    }
}
