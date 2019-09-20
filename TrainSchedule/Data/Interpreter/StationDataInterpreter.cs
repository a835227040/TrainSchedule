using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TrainSchedule.Data.Interpreter
{
    /// <summary>
    /// 一个分析12306网站返回报文的解释器
    /// </summary>
    public class StationDataInterpreter
    {
        public string[] this[int index]
        {
            get { return _stationData[index].Split('|'); }
        }
        public string[] stationData
        {
            get { return _stationData; }
        }

        private string[] _stationData;
        private const string URL = "https://kyfw.12306.cn/otn/resources/js/framework/station_name.js";
        public StationDataInterpreter()
        {
        }
        public async Task ExecuteSyncAsync()
        {
            await Task.Run(() =>
            {
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                var text = client.DownloadString(URL);
                text = text.Replace("var station_names ='", string.Empty);
                text = text.Replace("';", string.Empty);
                _stationData = text.Split('@');
            });
        }
        /// <summary>
        /// 判断有没有必要新建所有数据?
        /// </summary>
        public async Task<bool> IsCreateAsync()
        {
            StationModel model = null;
            await Task.Run(() => { model = new StationModel(); });
            return 0 == model.GetDBRowCount();
        }
        /// <summary>
        /// 用解析器为数据库建立新数据
        /// </summary>
        public async Task CreateDataAsync()
        {
            await Task.Run(() =>
            {
                for (var i = 1; i < this.stationData.Length; i++)
                {
                    var stationInfos = this[i];
                    StationModel m = new StationModel(stationInfos);
                    m.Insert();
                }
            });
        }
        /// <summary>
        /// 在后台线程中同步数据
        /// </summary>
        public async void AsyncDataAsync()
        {
            await Task.Run(() =>
            {
                for (var i = 1; i < this.stationData.Length; i++)
                {
                    var stationInfos = this[i];
                    
                    StationModel m = new StationModel(stationInfos);
                    //根据缩写找出符合条件的数据库
                    var result = m.Select<StationModel>();
                    if(result.Count() >= 0)
                    {
                        //如果在数据库中，则更新数据
                        result[0].SetStationDataFormString(stationInfos);
                        result[0].Update();
                    }
                    else
                    {
                        //如果不在数据库中,则将其加入库中
                        m.Insert();
                    }
                }
            });
        }
    }
}
