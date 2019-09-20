using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainSchedule.Data.Interpreter;

namespace TrainSchedule.Data.Interface
{
    public interface ITrainDataTabel
    {
        TrainType train_type { get; }
        string train_type_str { set; }
        TrainNumberInterpreter train_no { get; }
        string train_no_str { set; }
        string station_train_code { get; }
    }
}
