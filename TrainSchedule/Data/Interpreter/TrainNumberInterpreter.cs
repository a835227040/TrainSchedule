using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainSchedule.Data.Interpreter
{
    /// <summary>
    /// 车次解释器，输入一个字符串类型的车次，可以得到车型号(高铁?特快?）
    /// </summary>
    public class TrainNumberInterpreter
    {
        public TrainType type { get; private set; }

        public string number { get => _number; }
        private string _number;
        public TrainNumberInterpreter(string number)
        {
            _number = number;
            type = (TrainType)System.Enum.Parse(typeof(TrainType), number[0].ToString().ToUpper());

        }

        public override string ToString()
        {
            return number;
        }
    }
}
