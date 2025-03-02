namespace TFN_Validator.Server.Services
{
    public interface ITFNValidatorAlgorithm
    {
        public string NineDigitTFNValidator(string TFN);

        public string EightDigitTFNValidator(string TFN);
    }
}
