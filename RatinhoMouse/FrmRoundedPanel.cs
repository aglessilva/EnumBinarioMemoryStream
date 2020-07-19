using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RatinhoMouse
{
    public partial class RoundedPanel : Form
    {
        public RoundedPanel()
        {
            InitializeComponent();
        }

        [DllImport("kernel32.dll")]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [Flags]
        enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        bool ret = false;
        Stopwatch stopwatch = new Stopwatch();
        class NorthwindEntities
        {

            public int Ano { get; set; }
            public int Mes { get; set; }
            public double Valor { get; set; }
        }

        public class Revenue
        {
            //public string Medicamentos { get; set; }
            //public int TotalM { get; set; }
            //public string Aparelhos { get; set; }
            //public int TotalA { get; set; }
            //public string Cuidados { get; set; }
            //public int TotalC { get; set; }

            public int Year { get; set; }
            public int Month { get; set; }
            public double Value { get; set; }
        }

        private void RoundedPanel_Load(object sender, EventArgs e)
        {
            timer1.Stop();
            stopwatch.Reset();
            List<Revenue> db = new List<Revenue>()
            {
                new Revenue() {  Year = 2020,  Month = 1,  Value = 10.00 },
                new Revenue() {  Year = 2021,  Month = 6,  Value = 40.00 },
                new Revenue() {  Year = 2020,  Month = 10,  Value = 70.00 },
            };

            //List<Revenue> db = new List<Revenue>()
            //{
            //    new Revenue(){ Aparelhos = "REspirador", Cuidados = "Isolamento", Medicamentos = "Targea Preta", TotalA = 45, TotalC = 10, TotalM = 9  },
            //    new Revenue(){ Aparelhos = "REspirador 1", Cuidados = "Pre-sio", Medicamentos = "Modera do", TotalA = 20, TotalC = 68, TotalM = 12  },
            //    new Revenue(){ Aparelhos = "REspirador 2", Cuidados = "UTI", Medicamentos = "pre-moderado", TotalA = 75, TotalC = 100, TotalM = 54  },
            //    new Revenue(){ Aparelhos = "REspirador 3", Cuidados = "Clinica Medica", Medicamentos = "observação", TotalA = 19, TotalC = 20, TotalM = 73  },
            //    new Revenue(){ Aparelhos = "REspirador 4", Cuidados = "Total", Medicamentos = "não sei", TotalA = 45, TotalC = 97, TotalM = 88  },
            //    new Revenue(){ Aparelhos = "REspiradorv5", Cuidados = "Recuperação", Medicamentos = "Acompanhamento", TotalA = 13, TotalC = 20, TotalM = 07  },
            //};

            revenueBindingSource.DataSource = db;


            //cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            //{
            //    Title = "Herbert Agles da Silva\n",
            //    Labels = new string[] { "Sna 1", "Sna 2", "Sna 3", "Sna 4" } as IList<string>
            //});

            //Func<double, string> Sum = (x)
            //       =>
            //{
            //    decimal percent = Convert.ToDecimal(x / 100);
            //    string v = decimal.Round(percent, 2).ToString("P1", CultureInfo.InvariantCulture);
            //    return v;
            //};

            //cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            //{
            //    Title = "Evolução Semanal",

            //    LabelFormatter = value => Sum(value)
            //});
            //cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;




           // revenueBindingSource.DataSource = new List<Revenue>();
                cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
                {
                    Title = "Month",
                    Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
                });
                cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
                {
                    Title = "Revenue",
                    LabelFormatter = value => (value /100).ToString("P2")
                });
                cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
           
            timer1.Start();
            stopwatch.Restart();
            //cartesianChart1.Series.Clear();
            //LiveCharts.SeriesCollection series = new LiveCharts.SeriesCollection();
            //var aparelhos = revenueBindingSource.DataSource as List<Revenue>;
            //int[] value = { };

            //foreach (var ap in aparelhos)
            //{
            //    List<int> values = new List<int>();

            //    value = new int[] { ap.TotalA, ap.TotalM, ap.TotalC };
            //    values.AddRange(value);

            //    series.Add(new LineSeries() { Title = ap.Cuidados, Values = new ChartValues<int>(values) });
            //}
            //cartesianChart1.Series = series;



            cartesianChart1.Series.Clear();
            LiveCharts.SeriesCollection series = new LiveCharts.SeriesCollection();
            var years = (from o in revenueBindingSource.DataSource as List<Revenue>
                         select new { Year = o.Year }).Distinct();
            foreach (var year in years)
            {
                List<double> values = new List<double>();
                for (int month = 1; month <= 12; month++)
                {
                    double value = 0;
                    var data = from o in revenueBindingSource.DataSource as List<Revenue>
                               where o.Year.Equals(year.Year) && o.Month.Equals(month)
                               orderby o.Month ascending
                               select new { o.Value, o.Month };
                    if (data.SingleOrDefault() != null)
                        value = data.SingleOrDefault().Value;
                    values.Add(value);
                }
                series.Add(new LineSeries() { Title = year.Year.ToString(), Values = new ChartValues<double>(values) });
            }
            cartesianChart1.Series = series;
        }

        private void button2_Click(object sender, EventArgs e)
        {

           // solidGauge1.Value = 0.1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrWhiteSpace(textBox1.Text))
            //{
            //    solidGauge1.Value = Convert.ToDouble(textBox1.Text) / 100;


            //    Func<double, string> Sum = (x)
            //       =>
            //    {
            //        string v = solidGauge1.Value.ToString("P1", CultureInfo.InvariantCulture);
            //        return v;
            //    };


               
            //    solidGauge1.LabelFormatter = new Func<double, string>(Sum);
                
            //}
            //else
            //    solidGauge1.Value = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
      
            if (ret)
                Text = string.Format("Tempo de Execução: {0}:{1}:{2}", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds);
            if (stopwatch.Elapsed.Seconds == 10)
            {
                label1.Text = "aplicação será encerrada em 10 segundos";
                if (!ret)
                {
                    notifyIcon1.ShowBalloonTip(200, "Teste titulo", "vamos encerrar a sessoa", ToolTipIcon.Info);
                    
                }
                ret = true;             
            }

            if (stopwatch.Elapsed.Seconds == 21)
            {
                Application.Exit()
;            }
        }

       
    }
}
