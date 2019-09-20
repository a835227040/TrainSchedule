using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainSchedule.Data
{
    /// <summary>
    /// 从12306官网直接弄下来的原始json数据
    /// </summary>
    public class TrainDataListFromTime
    {
        public TrainData[] D;
        public TrainData[] T;
        public TrainData[] G;
        public TrainData[] C;
        public TrainData[] O;
        public TrainData[] K;
        public TrainData[] Z;
    }
}
