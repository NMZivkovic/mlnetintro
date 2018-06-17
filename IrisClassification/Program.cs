using IrisClassification.Helpers;
using System;
using Microsoft.ML.Data;

namespace IrisClassification
{
    class Program
    {
        static void Main(string[] args)
        {
            var trainingDataLocation = @"Data/iris-data_training.csv";
            var testDataLocation = @"Data/iris-data_test.csv";

            // Building and evaluating model.
            var modelBuilder = new ModelBuilder(trainingDataLocation);
            var model = modelBuilder.BuildAndTrain();
            var accuracy = modelBuilder.Evaluate(model, testDataLocation);

            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*        Accuracy of the model : {accuracy * 100}%    *");
            Console.WriteLine($"*************************************************");

            // Visualising the results.
            var testDataObjects = new IrisCsvReader().GetIrisDataFromCsv(testDataLocation);
            foreach (var iris in testDataObjects)
            {
                var prediction = model.Predict(iris);
                Console.WriteLine($"-------------------------------------------------");
                Console.WriteLine($"Predicted type : {prediction.PredictedLabels}");
                Console.WriteLine($"Actual type :    {iris.Label}");
                Console.WriteLine($"-------------------------------------------------");
            }

            Console.ReadLine();
        }
    }
}
