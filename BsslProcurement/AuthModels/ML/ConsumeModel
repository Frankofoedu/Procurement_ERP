using System;
using Microsoft.ML;

namespace BsslProcurement
{
public class ConsumeModel
    {
         string _modelPath;
        public ConsumeModel(string modelPath)
        {
            _modelPath = modelPath;
        }
        // For more info on consuming ML.NET models, visit https://aka.ms/mlnet-consume
        // Method for consuming model in your app
        public ModelOutput Predict(ModelInput input)
        {
           Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictionEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(CreatePredictionEngine);

           ModelOutput result = PredictionEngine.Value.Predict(input);
            return result;
        }

        public PredictionEngine<ModelInput, ModelOutput> CreatePredictionEngine()
        {
            // Create new MLContext
            MLContext mlContext = new MLContext();

            // Load model & create prediction engine          

            ITransformer mlModel = mlContext.Model.Load(_modelPath, out _);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            return predEngine;
        }
    }
}
