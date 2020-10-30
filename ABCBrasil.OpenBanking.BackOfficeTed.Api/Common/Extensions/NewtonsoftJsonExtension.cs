using Microsoft.AspNetCore.Mvc;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Extensions
{
    internal static class NewtonsoftJsonExtension
    {
        internal static void ActionNewtonsoftJson(MvcNewtonsoftJsonOptions options)
        {
            options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        }
    }
}
