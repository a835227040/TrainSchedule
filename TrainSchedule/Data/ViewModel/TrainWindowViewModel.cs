using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using TrainSchedule.Data.Interface;

namespace TrainSchedule.Data.ViewModel
{
    public class TrainWindowViewModel : BaseViewModel, ITrainWindowViewModel
    {
        public ObservableCollection<ITrainDetailInfoViewModel> train_table
        {
            get => _train_table;
            set
            {
                _train_table = value;
            }
        }
        public bool? is_filter_gc
        {
            get => _is_filter_gc;
            set
            {
                _is_filter_gc = value;
                if (value.HasValue && !value.Value) is_filter_all = false;
            }
        }
        public bool? is_filter_d
        {
            get => _is_filter_d;
            set
            {
                _is_filter_d = value;
                if (value.HasValue && !value.Value) is_filter_all = false;
            }
        }
        public bool? is_filter_z
        {
            get => _is_filter_z;
            set
            {
                _is_filter_z = value;
                if (value.HasValue && !value.Value) is_filter_all = false;
            }
        }
        public bool? is_filter_t
        {
            get => _is_filter_t;
            set
            {
                _is_filter_t = value;
                if (value.HasValue && !value.Value) is_filter_all = false;
            }
        }
        public bool? is_filter_k
        {
            get => _is_filter_k;
            set
            {
                _is_filter_k = value;
                if (value.HasValue && !value.Value) is_filter_all = false;
            }
        }
        public bool? is_filter_all
        {
            get => _is_filter_all;
            set
            {
                _is_filter_all = value;
                if (value.HasValue && !value.Value)
                {
                    is_filter_d = true;
                    is_filter_gc = true;
                    is_filter_k = true;
                    is_filter_t = true;
                    is_filter_z = true;
                }
            }

        }
        private DateTime? _start_filter_start_time = new DateTime(2010, 10, 10, 0, 0, 0);
        private DateTime? _start_filter_end_time = new DateTime(2010, 10, 10, 23, 59, 59);
        private DateTime? _over_filter_start_time = new DateTime(2010, 10, 10, 0, 0, 0);
        private DateTime? _over_filter_end_time = new DateTime(2010, 10, 10, 23, 59, 59);
        public ObservableCollection<ITrainDetailInfoViewModel> all_train_table
        {
            get => _all_train_table;
            set
            {
                _all_train_table = value;
            }
        }
        public DateTime? start_filter_start_time
        {
            get => _start_filter_start_time;
            set
            {
                _start_filter_start_time = value;
            }
        }
        public DateTime? start_filter_end_time
        {
            get => _start_filter_end_time;
            set
            {
                _start_filter_end_time = value;
            }
        }
        public DateTime? over_filter_start_time
        {
            get => _over_filter_start_time;
            set
            {
                _over_filter_start_time = value;
            }
        }
        public DateTime? over_filter_end_time
        {
            get => _over_filter_end_time;
            set
            {
                _over_filter_end_time = value;
            }
        }
        private ObservableCollection<ITrainDetailInfoViewModel> _all_train_table = new ObservableCollection<ITrainDetailInfoViewModel>();

        private bool? _is_filter_gc = true;
        private bool? _is_filter_d = true;
        private bool? _is_filter_z = true;
        private bool? _is_filter_t = true;
        private bool? _is_filter_k = true;
        private bool? _is_filter_all = true;

        private ObservableCollection<ITrainDetailInfoViewModel> _train_table = new ObservableCollection<ITrainDetailInfoViewModel>();

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool InternetGetCookieEx(string pchURL, string pchCookieName, StringBuilder pchCookieData, ref System.UInt32 pcchCookieData, int dwFlags, IntPtr lpReserved);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int InternetSetCookieEx(string lpszURL, string lpszCookieName, string lpszCookieData, int dwFlags, IntPtr dwReserved);

        private static string GetCookies(string url)
        {
            uint datasize = 256;
            StringBuilder cookieData = new StringBuilder((int)datasize);
            if (!InternetGetCookieEx(url, null, cookieData, ref datasize, 0x2000, IntPtr.Zero))
            {
                if (datasize < 0)
                    return null;


                cookieData = new StringBuilder((int)datasize);
                if (!InternetGetCookieEx(url, null, cookieData, ref datasize, 0x00002000, IntPtr.Zero))
                    return null;
            }
            return cookieData.ToString();
        }
        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }

        public void SetStationAsync(IStationModel start, IStationModel end, DateTime date)
        {
            Task.Run(async () =>
            {

                while(MainWindow.Cookie == null || MainWindow.Cookie == string.Empty)
                {
                    Thread.Sleep(100);
                }
                var url = new StringBuilder("https://kyfw.12306.cn/otn/leftTicket/queryA");
                url
                    .Append("?leftTicketDTO.train_date=").Append(date.ToString("yyyy-MM-dd")) //2019-09-20
                    .Append("&leftTicketDTO.from_station=").Append(start.code)//SYT
                    .Append("&leftTicketDTO.to_station=").Append(end.code)//BJP
                    .Append("&purpose_codes=ADULT");

                var cookies = MainWindow.Cookie;
                var cookieArr = cookies.Split(';');
                HttpWebRequest req = WebRequest.Create(url.ToString()) as HttpWebRequest;
                req.Headers[HttpRequestHeader.Cookie] = cookies;
                req.Method = "GET";

                req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                req.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate, br";
                req.Headers[HttpRequestHeader.AcceptLanguage] = "zh-CN,zh;q=0.9";
                SetHeaderValue(req.Headers, "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.110 Safari/537.36");
                var resp = req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream, Encoding.UTF8);
                var json = sr.ReadToEnd();
                var root = JToken.Parse(json);
                var result = root["data"]["result"].ToObject<string[]>();
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    _train_table.Clear();
                });
                foreach (var trainstring in result)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        _train_table.Add(new TrainDetailInfoViewModel(new Model.TrainDetailInfoModel(trainstring)));
                    });
                }
            });
        }

        public TrainWindowViewModel()
        {
        }
    }
}
