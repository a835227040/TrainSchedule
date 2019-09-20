using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainSchedule.Data.Interface;
using TrainSchedule.Data.Interpreter;

namespace TrainSchedule.Data.ViewModel
{
    public class TrainInfoViewModel : BaseViewModel, ITrainInfoViewModel
    {
        public StationModel start_station
        {
            get => _start_station;
            set => _start_station = value;
        }
        public StationModel end_station
        {
            get => _end_station;
            set => _end_station = value;
        }
        public TrainNumberInterpreter train_no
        {
            get => _train_no;
            set => _train_no = value;
        }
        public TimeSpan run_time
        {
            get => _run_time;
            set => _run_time = value;
        }
        public int station_count
        {
            get => _station_list.Count;
        }

        public ObservableCollection<ITrainOverStationModel> station_list => _station_list;

        public string window_title => $"{train_no.number} {start_station} - {end_station}";

        private StationModel _start_station;
        private StationModel _end_station;
        private TrainNumberInterpreter _train_no;
        private TimeSpan _run_time;
        private int _station_count;
        private ObservableCollection<ITrainOverStationModel> _station_list = new ObservableCollection<ITrainOverStationModel>();
    }
}
