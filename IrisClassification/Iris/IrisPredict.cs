using Microsoft.ML.Runtime.Api;

namespace IrisClassification.Iris
{
    public class IrisPredict
    {
        [ColumnName("PredictedLabel")]
        public string PredictedLabels;
    }
}
