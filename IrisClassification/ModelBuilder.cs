using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Models;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using IrisClassification.Iris;

namespace IrisClassification
{
    public sealed class ModelBuilder
    {
        private readonly string _trainingDataLocation;

        public ModelBuilder(string trainingDataLocation)
        {
            _trainingDataLocation = trainingDataLocation;
        }

        /// <summary>
        /// Using training data location that is passed trough constructor this method is building
        /// and training machine learning model.
        /// </summary>
        /// <returns>Trained machine learning model.</returns>
        public PredictionModel<IrisFlower, IrisPredict> BuildAndTrain()
        {
            var pipeline = new LearningPipeline();
            pipeline.Add(new TextLoader(_trainingDataLocation).CreateFrom<IrisFlower>(useHeader: true, separator: ','));
            pipeline.Add(new Dictionarizer("Label"));
            pipeline.Add(new ColumnConcatenator("Features", "SepalLength", "SepalWidth", "PetalLength", "PetalWidth"));
            pipeline.Add(new StochasticDualCoordinateAscentClassifier());
            pipeline.Add(new PredictedLabelColumnOriginalValueConverter() { PredictedLabelColumn = "PredictedLabel" });

            return pipeline.Train<IrisFlower, IrisPredict>();
        }

        /// <summary>
        /// Ussing passed testing data and model, it calculates model's accuracy.
        /// </summary>
        /// <returns>Accuracy of the model.</returns>
        public double Evaluate(PredictionModel<IrisFlower, IrisPredict> model, string testDataLocation)
        {
            var testData = new TextLoader(testDataLocation).CreateFrom<IrisFlower>(useHeader: true, separator: ',');
            var metrics = new ClassificationEvaluator().Evaluate(model, testData);
            return metrics.AccuracyMacro;
        }
    }
}
