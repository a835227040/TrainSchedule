using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainSchedule.Data.Interpreter
{
    public struct MoneyDataInterpreter
    {
        public float money;
        public MoneyDataInterpreter(float money)
        {
            this.money = money;
        }
        public override string ToString()
        {
            return $"￥{money}";
        }
    }
}
