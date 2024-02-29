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
            var response = await _restClient.PostAsync( request );
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
   ""calledMethod"": ""getSettlementAreas"",
   ""methodProperties"": {{
""Ref"" : """"
   }}
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
            List<CityDto> result = new List<CityDto>();
            int pageNumber = 1;
            int safeGuardCounter = 0;
            JsonArray itemArray = null;
            do
            {
                string requestText =
    $@"
{{
""apiKey"": ""{ApiKey}"",
""modelName"": ""Address"",
""calledMethod"": ""getSettlements"",
""methodProperties"": {{
""AreaRef"" : ""{regionId}"",
""Ref"" : """",
""RegionRef"" : """",
""Page"" : ""{pageNumber}"",
""Warehouse"" : ""1"",
""FindByString"" : """",
""Limit"" : """"
   }}
}}";
                string jsonAsString = await GetJsonString( requestText );
                var jsonValue = JsonNode.Parse( jsonAsString ).AsObject();
                itemArray = jsonValue["data"].AsArray();
                foreach (var item in itemArray)
                {
                    result.Add( new CityDto
                    {
                        Id = (string)item.AsObject()["Ref"],
                        Name = (string)item.AsObject()["Description"],
                        TypeDescription = (string)item.AsObject()["SettlementTypeDescription"],
                        Index = (string)item.AsObject()["Index1"],
                        RawJson = item.ToString()
                    } );
                }
                pageNumber++;
                safeGuardCounter++;
                if (safeGuardCounter > _CRITICAL_GUARD_VALUE)
                {
                    break;
                }
            }
            while ( itemArray.Count > 0 );
            return result;
        }

        public override async Task<List<WarehouseDto>> GetWarehousesByCityAsync(string cityId)
        {
            List<WarehouseDto> result = new List<WarehouseDto>();
            int pageNumber = 1;
            int safeGuardCounter = 0;
            JsonArray itemArray = null;
            do
            {
                string requestText =
$@"
{{
""apiKey"": ""{ApiKey}"",
""modelName"": ""Address"",
""calledMethod"": ""getWarehouses"",
""methodProperties"": {{
""FindByString"" : """",
""CityName"" : """",
""CityRef"" : ""{cityId}"",
""Page"" : ""{pageNumber}"",
""Limit"" : """",
""Language"" : ""UA"",
""TypeOfWarehouseRef"" : """",
""WarehouseId"" : """"
   }}
}}
";
                string jsonAsString = await GetJsonString( requestText );
                var jsonValue = JsonNode.Parse( jsonAsString ).AsObject();
                itemArray = jsonValue["data"].AsArray();
                foreach (var item in itemArray)
                {
                    result.Add( new WarehouseDto
                    {
                        Id = (string)item.AsObject()["Ref"],
                        Name = (string)item.AsObject()["Description"],
                        RawJson = item.ToString()
                    } );
                }
                pageNumber++;
                safeGuardCounter++;
                if (safeGuardCounter > _CRITICAL_GUARD_VALUE)
                {
                    break;
                }
            }
            while (itemArray.Count > 0);
            return result;
        }

        private RestClient _restClient;
        private const int _CRITICAL_GUARD_VALUE = 100;
    }// NovaPoshtaClient
}
