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

        public override async Task<List<RegionDto>> GetRegionsAsync()
        {
            var request = new RestRequest();
            request.AddHeader( "Content-Type", "application/json" );
            request.AddHeader( "Accept", "application/json" );
            request.AddHeader( "User-Agent", "TinyShop" );
            request.AddJsonBody(
@"
{
   ""apiKey"": ""110236d75ca7388f9838437f1b5421d3"",
   ""modelName"": ""Address"",
   ""calledMethod"": ""getAreas"",
   ""methodProperties"": {   }
}
" );
            var response = await _restClient.GetAsync( request );
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }
            var jsonRegions = JsonNode.Parse( response.Content ).AsObject();
            List<RegionDto> regions = new List<RegionDto>();
            //foreach (var region in jsonRegions["data"].AsArray())
            //{
            //    regions.Add( new RegionDto
            //    {
            //        Id = (string)region.AsObject()["Ref"],
            //        Name = (string)region.AsObject()["Description"]
            //    } );
            //}   
            //return regions;
            return jsonRegions["data"].AsArray().Select( x =>
                new RegionDto
                {
                    Id = (string)x.AsObject()["Ref"],
                    Name = (string)x.AsObject()["Description"]
                } )
                .ToList();
        }

        private RestClient _restClient;
    }
}
