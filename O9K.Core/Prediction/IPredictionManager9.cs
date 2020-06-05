namespace O9K.Core.Prediction
{
    using Data;

    public interface IPredictionManager9
    {
        PredictionOutput9 GetPrediction(PredictionInput9 input);

        PredictionOutput9 GetSimplePrediction(PredictionInput9 input);
    }
}