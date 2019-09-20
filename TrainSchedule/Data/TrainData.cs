using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainSchedule.Data
{
    public class TrainData
    {
        [JsonIgnore]
        /// <summary>
        /// 车次信息
        /// </summary>
        public string train_no { get; private set; }
        /// <summary>
        /// 始发站
        /// </summary>
        [JsonIgnore]
        public string start_station { get; private set; }
        /// <summary>
        /// 终点站
        /// </summary>
        [JsonIgnore]
        public string over_station { get; private set; }
        /// <summary>
        /// 约定格式:D1(北京-沈阳南)
        /// </summary>
        [JsonProperty(PropertyName = "station_train_code")]
        public string code
        {
            get => _code;
            set
            {
                _code = value;
                var temp = value.Split('(');
                train_no = temp[0];
                var stations = temp[1].Split('-');
                start_station = stations[0];
                over_station = stations[1].Substring(0, stations[1].Length - 1);
            }
        }
        /// <summary>
        /// 约定格式:24000000D121
        /// </summary>
        [JsonProperty(PropertyName = "train_no")]
        public string number { get; set; }
        private string _code;
    }
}
