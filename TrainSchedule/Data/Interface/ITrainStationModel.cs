using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainSchedule.Data.Interface
{
    public interface ITrainStationModel
    {
        IStationModel start_station { get; set; }
        IStationModel over_station { get; set; }
    }
}
