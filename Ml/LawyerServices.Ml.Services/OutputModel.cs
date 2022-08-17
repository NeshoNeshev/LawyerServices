using Microsoft.ML.Data;

namespace LawyerServices.Ml.Services
{
    public class OutputModel
    {
        [ColumnName("PredictedLabel")]
        public string Category { get; set; }

        public float[] Score { get; set; }
    }
}
