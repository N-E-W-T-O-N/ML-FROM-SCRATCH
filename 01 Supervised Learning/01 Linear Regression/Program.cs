namespace Program
{
    public class TestClass
    {
        public static void Main(String[] args)
        {
            var lr = new LinearRegression();

            //Read from CSV file
            lr.ReadCSV("data.csv");
            //Train to Find Weight
            lr.TrainModel();
            //Calcuate Error using Root Mean Square(RMS)
            lr.CalculateError();
            Console.WriteLine($"RMS := {lr.RMS}");
            //Create the Chart
            lr.PlotChart();
        }
    }
}