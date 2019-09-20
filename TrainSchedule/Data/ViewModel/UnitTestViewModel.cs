using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainSchedule.Data.Model;

namespace TrainSchedule.Data.ViewModel
{
#if DEBUG
    public class UnitTestViewModel
    {
        public static TrainWindowViewModel GetWindowViewModel()
        {
            var m = new TrainWindowViewModel();
            m.SetStationAsync(
                new StationModel() { name = "沈阳" }.Select<StationModel>()[0], 
                new StationModel() { name = "北京" }.Select<StationModel>()[0],
                DateTime.Now.AddDays(1));

            return m;
        }
        public static TrainDetailInfoViewModel GetTrainDetailInfoViewModelD233()
        {
            var text = @"oAQJJTy4vbJ12BHEDtmTYtmzqvZXrXCpmdE%2BrNv7NlvEcacshog7ZuOJKrb1UpVvOZQE0bnN18ii%0Ai2qjqRoo01h2B3K9PRZwbOcJCNmfNXIBCR5cqgfmV1h08otqX4ryJj4hcV%2BhIXstIeiAb2TZwt%2B1%0AwV4FmXTGajMi4nNoML0%2FTY0%2BQHzSOLoZ6aqn%2FOgzaDrrA9MCZojd4dwL2oohbXdHWMlbZ5h%2B7cYk%0A%2FL22IHvfYGWfmAlKGzJdWOROlmBF0ETOYZoVwYfq0h6P8%2FgwolHD25ZsZYNyPBnoTRVN2rDJwHhk%0Az6AL8Q%3D%3D|预订|200000K2160X|K216|TML|BJP|SYT|BJP|01:21|11:54|10:33|Y|Mu5SH46r3keY%2F3RBLl6%2BlSc6KKLtA2%2FXpo1j1a8Y4fpn6SEaGf2UHVAyFZ4%3D|20190919|3|T2|12|20|0|0||||无|||有||无|有|||||10401030|1413|1|1|";
            var m = new TrainDetailInfoModel(text);
            
            var r = new TrainDetailInfoViewModel(m);
            return r;
        }
        public static TrainInfoViewModel GetTrainInfoViewModel()
        {
            var t = new TrainInfoViewModel()
            {
                end_station = new StationModel()
                {
                    abbreviation = "gzd",
                    name = "广州东"
                },
                start_station = new StationModel()
                {
                    abbreviation = "bjb",
                    name = "北京北"
                },
                run_time = new TimeSpan(12, 04, 00),
                train_no = new Interpreter.TrainNumberInterpreter("g233")
            };
            t.station_list.Add(new TrainOverStationModel()
            {
                over_time = DateTime.Parse("2010-01-01 22:03:00"),
                station = new StationModel()
                {
                    abbreviation = "bjb",
                    name = "北京北"
                },
                stop_time = 4,
                train_no = 1
            });
            t.station_list.Add(new TrainOverStationModel()
            {
                over_time = DateTime.Parse("2010-01-02 21:03:00"),
                station = new StationModel()
                {
                    abbreviation = "gzd",
                    name = "广州东"
                },
                stop_time = 4,
                train_no = 2
            });
            

            return t;
        }
    }
#endif
}
