using LawyerServices.Ml.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;

namespace LaweyrServices.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MlCaseController : ControllerBase
    {
        private readonly PredictionEnginePool<CaseModel, OutputModel> _predictionEnginePool;
        public MlCaseController(PredictionEnginePool<CaseModel, OutputModel> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }

        [HttpGet("FindCaseCategory")]
        public async Task<string> FindCaseCategory( string? input)
        {
            var caseModel = new CaseModel() { Content = input };
            var output = this._predictionEnginePool.Predict(caseModel);

            return output.Category;
        }
    }
}
