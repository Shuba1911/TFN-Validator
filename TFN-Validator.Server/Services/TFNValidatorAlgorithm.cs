namespace TFN_Validator.Server.Services
{
    public class TFNValidatorAlgorithm : ITFNValidatorAlgorithm
    {
        private readonly IConfiguration _configuration;
        public TFNValidatorAlgorithm(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string NineDigitTFNValidator(string TFN)
        {
            int[] tfnArray = TFN.Where(char.IsDigit) 
                                .Select(c => c - '0')
                                .ToArray();
            int[] weightingFactor = _configuration.GetSection("TFNWeightingFactor:NineDigitTFN").Get<int[]>(); ;
            int denominator = _configuration.GetSection("TFNWeightingFactor:TFNDenominator").Get<int>();
            int tfnLength = tfnArray.Length;
            int total = 0;
            
            for(int i=0; i < tfnLength; i++)
            {
                total = total + (tfnArray[i] * weightingFactor[i]);
            }

            int remainder = total % denominator;

            if(remainder == 0)
            {
                return "Valid TFN";

            }
            else
            {
                return "Invalid TFN";

            }
        }

        public string EightDigitTFNValidator(string TFN)
        {
            int[] tfnArray = TFN.Where(char.IsDigit) 
                               .Select(c => c - '0')
                               .ToArray();
            int[] weightingFactor = _configuration.GetSection("TFNWeightingFactor:EightDigitTFN").Get<int[]>(); ;
            int denominator = _configuration.GetSection("TFNWeightingFactor:TFNDenominator").Get<int>();
            int tfnLength = tfnArray.Length;
            int total = 0;

            for (int i = 0; i < tfnLength; i++)
            {
                total = total + (tfnArray[i] * weightingFactor[i]);
            }

            int remainder = total % denominator;

            if (remainder == 0)
            {
                return "Valid TFN";
            }
            else
            {
                return "Invalid TFN";
            }
        }
    }
}
