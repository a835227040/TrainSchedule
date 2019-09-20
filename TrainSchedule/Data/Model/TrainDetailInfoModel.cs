using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainSchedule.Data.Interface;
using TrainSchedule.Data.Interpreter;

namespace TrainSchedule.Data.Model
{
    /// <summary>
    /// 表示列车在某站的到发时刻
    /// </summary>
    public class TrainRunTimeModel : ITrainRunTimeModel
    {
        public TimeInterpreter start_time { get; set ; }
        public TimeInterpreter end_time { get; set; }
    }
    /// <summary>
    /// 表示一趟列车的票价/剩余票数信息
    /// </summary>
    public class TrainDetailInfoModel : ITrainDatailInfoModel
    {
        private const string STR_YOU = "有";
        private const string STR_WU = "无";
        public string train_no { get; set; }
        public IStationModel start_station { get; set; }
        public IStationModel end_station { get; set; }
        public int min { get; set; }
        public int wz { get; set; }
        public int yz { get; set; }
        public int rz { get; set; }
        public int rw { get; set; }
        public int gjrw { get; set; }
        public int d2 { get; set; }
        public int d1 { get; set; }
        public int dw { get; set; }
        public int yw { get; set; }
        public bool is_start { get; set; }
        public bool is_end { get; set; }
        public ITrainRunTimeModel run_time { get; set; }
        public TimeSpan time { get; set; }
        public int td { get; set; }
        public IStationModel train_start_station { get; set; }
        public IStationModel train_end_station { get; set; }
        public TrainDetailInfoModel(string trainText)
        {
            var texts = trainText.Split('|');
            train_no = texts[3];

            start_station = new StationModel() { code = texts[6] }.Select<StationModel>()[0];
            end_station = new StationModel() { code = texts[7] }.Select<StationModel>()[0];
            train_start_station = new StationModel() { code = texts[4] }.Select<StationModel>()[0];
            train_end_station = new StationModel() { code = texts[5] }.Select<StationModel>()[0];
            // 26 29
            time = TimeSpan.Parse(texts[10]);
            run_time = new TrainRunTimeModel() { start_time =new TimeInterpreter(texts[8]), end_time = new TimeInterpreter(texts[9]) };
            //min = Convert.ToInt32(texts[1]);
            //wz = Convert.ToInt32(texts[1]);
            yz = IsTicketCount(texts[29]);
            //rz = Convert.ToInt32(texts[1]);
            rw = IsTicketCount(texts[23]);
            //gjrw = Convert.ToInt32(texts[1]);
            //d2 = Convert.ToInt32(texts[1]);
            //d1 = Convert.ToInt32(texts[1]);
            //dw = Convert.ToInt32(texts[1]);
            yw = IsTicketCount(texts[28]);
            //td = Convert.ToInt32(texts[1]);

        }

        private int IsTicketCount(string countString)
        {
            if (countString == null || countString == string.Empty)
                return 0;
            else if (countString == STR_YOU)
                return int.MaxValue;
            else if (countString == STR_WU)
                return int.MinValue;
            else
                return Convert.ToInt32(countString);
        }

    }
}
