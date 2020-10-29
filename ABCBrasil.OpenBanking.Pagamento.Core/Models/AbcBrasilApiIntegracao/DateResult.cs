using Newtonsoft.Json;
using System;
using System.Globalization;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao
{
    public class DateResult
    {
        [JsonProperty("date")]
        public string DateString { get; set; }

        [JsonIgnore]
        public DateTime? Date => DateTime.ParseExact(DateString, "dd-MM-yyyy", CultureInfo.InvariantCulture);

        public string DayOfWeek { get; set; }
    }
}
