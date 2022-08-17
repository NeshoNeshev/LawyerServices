using Microsoft.ML.Data;

namespace LawyerServices.Ml.Services
{
    public class CaseModel
    {
        [LoadColumn(0)]
        public string? Category { get; set; }

        [LoadColumn(1)]
        public string? Content { get; set; }
    }
}
