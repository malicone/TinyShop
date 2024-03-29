﻿using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TinyShop.Models;
using TinyShop.RestUtils.Common;

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
        public override async Task<List<WarehouseType>> GetWarehouseTypesAsync()
        {
            string jsonString = await GetJsonString(
$@"
{{
	""apiKey"": ""{ApiKey}"",
	""modelName"": ""Address"",
	""calledMethod"": ""getWarehouseTypes"",
	""methodProperties"": {{}}
}}
"
            );
            var jsonValue = JsonNode.Parse( jsonString ).AsObject();
            return jsonValue[ "data" ].AsArray().Select( x =>
                new WarehouseType
                {
                    IdExternal = (string)x.AsObject()[ "Ref" ],
                    Name = (string)x.AsObject()[ "Description" ],
                    RawJson = x.ToString()
                } )
                .ToList();
        }

        public override async Task<List<Region>> GetRegionsAllAsync()
        {
            string jsonString = await GetJsonString(
$@"
{{
	""apiKey"": ""{ApiKey}"",
	""modelName"": ""Address"",
	""calledMethod"": ""getAreas"",
	""methodProperties"": {{}}
}}"
            );
            var jsonValue = JsonNode.Parse( jsonString ).AsObject();            
            return jsonValue["data"].AsArray().Select( x =>
                new Region
                {
                    IdExternal = (string)x.AsObject()["Ref"],
                    Name = (string)x.AsObject()["Description"],
                    RawJson = x.ToString()
                } )
                .ToList();
        }

        public override async Task<List<City>> GetCitiesByRegionAsync(string regionIdExternal)
        {
            List<City> result = new List<City>();
            JsonArray itemArray = null;
            string requestText =
$@"
{{
	""apiKey"": ""{ApiKey}"",
	""modelName"": ""Address"",
	""calledMethod"": ""getCities"",
	""methodProperties"": {{}}
}}
";
            string jsonAsString = await GetJsonString( requestText );
            var jsonValue = JsonNode.Parse( jsonAsString ).AsObject();
            itemArray = jsonValue["data"].AsArray();
            foreach (var item in itemArray)
            {
                // to-do: it's bad to fetch and then filter, should be done by request parameters
                if (item.AsObject()["Area"].ToString() == regionIdExternal)
                {
                    result.Add( new City
                    {
                        IdExternal = (string)item.AsObject()["Ref"],
                        Name = (string)item.AsObject()["Description"],
                        TypeDescription = (string)item.AsObject()["SettlementTypeDescription"],                        
                        RegionIdExternal = (string)item.AsObject()["Area"],
                        RawJson = item.ToString()
                    } );
                }
            }
            return result;
        }
        public override async Task<List<City>> GetCitiesAllAsync()
        {
            List<City> result = new List<City>();
            JsonArray itemArray = null;
            string requestText =
$@"
{{
	""apiKey"": ""{ApiKey}"",
	""modelName"": ""Address"",
	""calledMethod"": ""getCities"",
	""methodProperties"": {{}}
}}
";
            string jsonAsString = await GetJsonString( requestText );
            var jsonValue = JsonNode.Parse( jsonAsString ).AsObject();
            itemArray = jsonValue[ "data" ].AsArray();
            foreach ( var item in itemArray )
            {
                result.Add( new City
                {
                    IdExternal = (string)item.AsObject()[ "Ref" ],
                    Name = (string)item.AsObject()[ "Description" ],
                    TypeDescription = (string)item.AsObject()[ "SettlementTypeDescription" ],                    
                    RegionIdExternal = (string)item.AsObject()[ "Area" ],
                    RawJson = item.ToString()
                } );

            }
            return result;
        }

        public override async Task<List<Warehouse>> GetWarehousesByCityAsync(string cityIdExternal)
        {
            // todo: it's bad to fetch and then filter, but it's a quick solution
            const string WAREHOUSE_TYPE_OFFICE_30KG_REF = "841339c7-591a-42e2-8233-7a0a00f0ed6f";
            const string WAREHOUSE_TYPE_OFFICE_1000KG_REF = "9a68df70-0267-42a8-bb5c-37f427e36ee4";
            List<Warehouse> result = new List<Warehouse>();
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
""CityRef"" : ""{cityIdExternal}"",
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
                    bool isOffice30Kg = item.AsObject()["TypeOfWarehouse"].ToString() == WAREHOUSE_TYPE_OFFICE_30KG_REF;
                    bool isOffice1000Kg = item.AsObject()["TypeOfWarehouse"].ToString() == WAREHOUSE_TYPE_OFFICE_1000KG_REF;
                    //if (isOffice30Kg || isOffice1000Kg)
                    //{
                        result.Add( new Warehouse
                        {
                            IdExternal = item.AsObject()["Ref"].ToString(),
                            Name = item.AsObject()["Description"].ToString(),
                            WarehouseTypeIdExternal = item.AsObject()["TypeOfWarehouse"].ToString(),
                            CityIdExternal = item.AsObject()[ "CityRef" ].ToString(),
                            RawJson = item.ToString()
                        } );
                    //}
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
        public override async Task<List<Warehouse>> GetWarehousesAllAsync()
        {
            List<Warehouse> result = new List<Warehouse>();
            JsonArray itemArray = null;
            string requestText =
$@"
{{
	""apiKey"": ""{ApiKey}"",
	""modelName"": ""Address"",
	""calledMethod"": ""getWarehouses"",
	""methodProperties"": {{}}
}}
";
            string jsonAsString = await GetJsonString( requestText );
            var jsonValue = JsonNode.Parse( jsonAsString ).AsObject();
            itemArray = jsonValue[ "data" ].AsArray();
            foreach ( var item in itemArray )
            {
                result.Add( new Warehouse
                {
                    IdExternal = item.AsObject()[ "Ref" ].ToString(),
                    Name = item.AsObject()[ "Description" ].ToString(),
                    WarehouseTypeIdExternal = item.AsObject()[ "TypeOfWarehouse" ].ToString(),
                    CityIdExternal = item.AsObject()[ "CityRef" ].ToString(),                    
                    RawJson = item.ToString()
                } );
            }
            return result;
        }

        private RestClient _restClient;
        private const int _CRITICAL_GUARD_VALUE = 100;

    }// NovaPoshtaClient
}