using System;
using TrainSchedule.Data.Interface;

namespace TrainSchedule.Data.Model
{
    public class TrainOverStationModel : ITrainOverStationModel
    {
        public int train_no { get; set; }
        public StationModel station { get; set; }
        public DateTime over_time
        {
            get => _over_time;
            set
            {
                _over_time = value;
                _start_time = value.AddMinutes(stop_time);
            }
        }
        public int stop_time
        {
            get => _stop_time;
            set
            {
                _stop_time = value;
                _start_time = over_time.AddMinutes(value);
            }
        }
        public DateTime start_time { get=>_start_time; }
        private int _stop_time;
        private DateTime _over_time;
        private DateTime _start_time;
    }
}
