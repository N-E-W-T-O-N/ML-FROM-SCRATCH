using CsvHelper.Configuration;
using CsvHelper;
using ScottPlot;
using System.Globalization;
using CsvHelper.Configuration.Attributes;

namespace Program
{
    public class LinearRegression
    {
        List<Info> data_set;
        List<float> weight, bias;
        public float RMS { get; private set; }
        readonly float alpha, epsilon;
        int iteration { get; }

        public LinearRegression()
        {
            data_set = new();
            weight = new() { 0, 1 };
            bias = new() { 0, 1 };
            alpha = 0.001f;
            epsilon = 0.0001f;
            iteration = 200;
        }

        public void ReadCSV(string fileLocation)
        {
            try
            {
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                };
                using var st = new StreamReader(fileLocation);
                using (var csv = new CsvReader(st, csvConfig))
                {
                    data_set = csv.GetRecords<Info>().ToList();
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void TrainModel()
        {
            if (data_set.Count == 0)
            {
                throw new ArgumentNullException();
            }

            float X = 0.0f, XX = 0.0f, Y = 0f, XY = 0.0f;
            float total = data_set.Count;

            foreach (var user in data_set)
            {
                //Console.WriteLine(user.Salary + " " + user.YearsExperience);

                //Independent Variable
                X += user.X;
                //Dependend Variable
                Y += user.Y;
                XX += user.X * user.X;
                XY += user.X * user.Y;
            }
            long i = 0;
            //or you can use Abs(weight[^1] - weight[^2])>=epsilon) 
            //Same as weight[weight.count-1] -weight[weight.count-2]
            while (i <= iteration)
            {
                float weight_derv = (weight.Last() * XX + bias.Last() * X - XY);
                float bias_derv = (weight.Last() * X + bias.Last() - Y);

                weight.Add(weight.Last() - 0.5f * alpha * weight_derv / total);
                bias.Add(bias.Last() - 0.5f * alpha * bias_derv / total);
                Console.WriteLine($" Iteration {++i} Weight:{weight.Last()} Bias:{bias.Last()}");
            }
            Console.WriteLine($"Iteration {i}");
            #region Comment
            //The same thing can be implement using LINQ
            //var X = data_set.Select(x => x.Salary).Sum();
            //var XX = data_set.Select(x => x.Salary * x.Salary).Sum();
            //var Y = data_set.Select(x => x.YearsExperience).Sum();
            //var XY = data_set.Select(x => x.Salary * x.YearsExperience).Sum();
            #endregion
        }

        public void CalculateError()
        {
            if (data_set.Count == 0)
                throw new NotImplementedException();
            double totalsum = 0;
            for (var i = 0; i < data_set.Count; i++)
            {
                totalsum += Math.Pow(data_set[i].Y - RegressionLine(data_set[i].X), 2);
            }
            RMS = (float)Math.Sqrt(totalsum / data_set.Count);
        }

        public void PlotChart()
        {
            if (weight.Count == 0 || bias.Count == 0)
            {
                throw new NotImplementedException("First train the model");
            }

            Plot myPlot = new();

            var xs = data_set.Select(x => x.X).ToArray();
            var ys = data_set.Select(x => x.Y).ToArray();

            var xmin = xs.Min();
            var xmax = xs.Max();
            //myPlot.Add.Scatter(xs, ys,Colors.Blue);
            for (int i = 0; i < xs.Count(); i++)
            {
                myPlot.Add.Marker(xs[i], ys[i], shape: MarkerShape.Asterisk, color: Colors.Blue);
            }
            var line = myPlot.Add.Line(
                xmin, xmin * weight.Last() + bias.Last(),
                xmax, xmax * weight.Last() + bias.Last());
            line.LineColor = Colors.Red;
            line.LineWidth = 2;

            myPlot.XLabel("X  -->");
            myPlot.YLabel("Y  -->");

            List<LegendItem> legendItems = new()
            {
                new LegendItem()
                {
                    Label = "Data",
                    Marker= new MarkerStyle() {Shape = MarkerShape.Asterisk},
                    MarkerColor = Colors.Blue,
                    Line = LineStyle.None
                },
                new LegendItem()
                {
                    Label = "Regression Line",
                    LineWidth=2,
                    LineColor= Colors.Red,
                    Marker = MarkerStyle.None
                },
                new LegendItem()
                {
                    Label = $" RMS := {RMS}",
                    Marker=MarkerStyle.None,
                    Line=LineStyle.None,
                }
            };

            myPlot.ShowLegend(legendItems, Alignment.LowerRight);

            myPlot.SavePng("../../../LinearRegression.png", 600, 500);
        }

        public float RegressionLine(float x)
        {
            if (weight.Count == 0)
                throw new NotImplementedException("First Train the Model");
            else return weight.Last() * x + bias.Last();
        }
    }

    public class Info
    {
        [Index(0)]
        public float X { get; set; }
        [Index(1)]
        public float Y { get; set; }
    }
}