using System;
using System.Collections.ObjectModel;
using TrainSchedule.Data.Interpreter;

namespace TrainSchedule.Data.Interface
{
    public interface ITrainInfoViewModel
    {
        string window_title { get; }
        StationModel start_station { get; set; }
        StationModel end_station { get; set; }
        TrainNumberInterpreter train_no { get; set; }
        TimeSpan run_time { get; set; }
        int station_count { get; }
        ObservableCollection<ITrainOverStationModel> station_list { get; }
    }
}
