using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Extensions
{
    public class ApiResult<T>
    {
        public ApiResult(string name)
        {
            Name = name;
            Status = true;
            SetEnvironment();
            Date = DateTime.Now;
        }

        private void SetEnvironment()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isNotProd = !environment?.StartsWith("Prod", StringComparison.CurrentCultureIgnoreCase) ?? false;
            if (isNotProd)
            {
                EnvironmentName = environment;
            }
        }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public bool Status { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public string Name { get; set; }
        public string EnvironmentName { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public DateTime Date { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public T Data { get; set; }

        public List<BasicEntity> Infos { get; set; }
        public List<BasicEntity> Errors { get; set; }

        public void AddInfo(BasicEntity entity)
        {
            if (entity == null) return;

            if (Infos == null) Infos = new List<BasicEntity>();
            Infos.Add(entity);
        }

        public void AddInfos(IEnumerable<BasicEntity> entities)
        {
            var hasData = !entities?.Any() ?? true;
            if (hasData) return;

            if (Infos == null) Infos = new List<BasicEntity>();
            Infos.AddRange(entities);
        }

        public void AddError(BasicEntity entity)
        {
            if (entity == null) return;

            if (Errors == null) Errors = new List<BasicEntity>();
            Errors.Add(entity);
        }

        public void AddErrors(IEnumerable<BasicEntity> entities)
        {
            var hasData = !entities?.Any() ?? true;
            if (hasData) return;

            if (Errors == null) Errors = new List<BasicEntity>();
            Errors.AddRange(entities);
        }

        public override string ToString() => JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }

    public class ApiResult : ApiResult<object>
    {
        public ApiResult(string name) :
            base(name)
        {
        }
    }
}
