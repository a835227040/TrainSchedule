using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainSchedule.Data.Interpreter;
using TrainSchedule.Data.Model;

namespace TrainSchedule.Data.Interface
{
    /// <summary>
    /// 列车运行时刻的M层(也就是出发时间和到达时间)
    /// </summary>
    public interface ITrainRunTimeModel
    {
        TimeInterpreter start_time { get; set; }
        TimeInterpreter end_time { get; set; }
    }
    public interface ITrainDatailInfoModel
    {
        bool is_start { get; set; }
        bool is_end { get; set; }
        ITrainRunTimeModel run_time { get; set; }
        TimeSpan time { get; set; }
        string train_no { get; set; }
        IStationModel train_start_station { get; set; }
        IStationModel train_end_station { get; set; }
        IStationModel start_station { get; set; }
        IStationModel end_station { get; set; }
        int min { get; set; }
        int yw { get; set; }
        int wz { get; set; }
        int yz { get; set; }
        int rz { get; set; }
        int rw { get; set; }
        int gjrw { get; set; }
        int d2 { get; set; }
        int d1 { get; set; }
        int td { get; set; }
        int dw { get; set; }

    }
}
