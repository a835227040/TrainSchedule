using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TrainSchedule.Data;
using TrainSchedule.Data.Interpreter;
using TrainSchedule.Data.Model;
using TrainSchedule.Data.SQLite;
using TrainSchedule.Data.ViewModel;

namespace TrainSchedule
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string Cookie;
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            (new TrainInfo()).Show();
            this.DataContext = UnitTestViewModel.GetWindowViewModel();
            browser.Navigate("https://www.12306.cn");
            DispatcherTimer t = new DispatcherTimer();
            t.Interval = TimeSpan.FromMilliseconds(100);
            t.Tick += (e, s) =>
            {
                Cookie = browser.Document.Cookie;
            };
            t.Start();
            //TrainModel tm = new TrainModel();
            //tm.train_no_str = "D1";
            //var r = tm.Select<TrainModel>();
#endif
            //using (StreamReader file = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @"data\train_list.json"))
            //{
            //    using (JsonTextReader reader = new JsonTextReader(file))
            //    {
            //        var token = JToken.ReadFrom(reader);
            //        var list = new List<TrainData>();
            //        DateTime? date = null;
            //        foreach (JProperty property in token)
            //        {
            //            //冒泡排序(伪)
            //            var currert = DateTime.Parse(property.Name);
            //            if (date.HasValue)
            //                date = DateTime.Compare(currert, date.Value) > 0 ? currert : date;
            //            else
            //                date = currert;
            //            //获取最新的表单
            //        }
            //        var t = date.Value.ToString("yyyy-MM-dd");
            //        TrainDataListFromTime f = token[date.Value.ToString("yyyy-MM-dd")].ToObject<TrainDataListFromTime>();

            //    }
            //}
        }
    }
}
