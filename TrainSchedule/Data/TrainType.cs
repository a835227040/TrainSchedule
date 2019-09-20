using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainSchedule.Data
{
    public enum TrainType
    {
        /// <summary>
        /// 高速动车组列车
        /// </summary>
        G,
        /// <summary>
        /// 城际动车组列车
        /// </summary>
        C,
        /// <summary>
        /// 动车组列车
        /// </summary>
        D,
        /// <summary>
        /// 直达特快旅客列车
        /// </summary>
        Z,
        /// <summary>
        /// 特快旅客列车
        /// </summary>
        T,
        /// <summary>
        /// 市郊列车 - 仅在北京
        /// </summary>
        S,
        /// <summary>
        /// 进港直通列车
        /// </summary>
        P,
        /// <summary>
        /// 快速旅客列车
        /// </summary>
        K,
        /// <summary>
        /// 行包快运列车
        /// </summary>
        X,
        /// <summary>
        /// 跨局普通旅客快车 1001-3998
        /// </summary>
        N1,
        /// <summary>
        /// 管内普通旅客快车 4001-5998
        /// </summary>
        N4,
        /// <summary>
        /// 普通旅客列车 6001-7598
        /// </summary>
        N6,
        /// <summary>
        /// 通勤列车 7601-8998
        /// </summary>
        N8,
        /// <summary>
        /// 临时旅客列车
        /// </summary>
        L,
        /// <summary>
        /// 旅游列车
        /// </summary>
        Y,
        /// <summary>
        /// 折返列车
        /// </summary>
        F

    }
}
