using System;
using Newtonsoft.Json;

namespace TGBotComp
{
    public class FNS
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }

        [JsonProperty("filter")]
        public string Filter { get; set; }

        [JsonProperty("Count")]
        public long Count { get; set; }
    }
    
    public partial class Item
    {
        [JsonProperty("ЮЛ")]
        public Inn Inn { get; set; }
    }
    
    public partial class Inn
    {
        [JsonProperty("ИНН")]
        public string INN { get; set; }

        [JsonProperty("ОГРН")]
        public string Ogrn { get; set; }

        [JsonProperty("НаимСокрЮЛ")]
        public string NaimSocrYul { get; set; }

        [JsonProperty("НаимПолнЮЛ")]
        public string NaimFullYul { get; set; }

        [JsonProperty("ДатаОГРН")]
        public DateTimeOffset DataOgrn { get; set; }

        [JsonProperty("Статус")]
        public string Status { get; set; }

        [JsonProperty("АдресПолн")]
        public string AddressFull { get; set; }

        [JsonProperty("ОснВидДеят")]
        public string MainTypeAction { get; set; }

        [JsonProperty("ГдеНайдено")]
        public string WhereFound { get; set; }
    }
}