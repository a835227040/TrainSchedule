using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainSchedule.Data.Interface;

namespace TrainSchedule.Data.Model
{
    public class TrainStationModel : ITrainStationModel
    {
        public IStationModel start_station
        {
            get;
            set;
        }
        public IStationModel over_station
        {
            get;
            set;
        }
        public TrainStationModel(IStationModel start, IStationModel end)
        {
            start_station = start;
            over_station = end;
        }
    }
}
