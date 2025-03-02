using Microsoft.Extensions.Caching.Memory;
using TFN_Validator.Server.Model;

namespace TFN_Validator.Server.Services
{
    public class TFNLinkedValidator : ITFNLinkedValidator
    {
        private List<LinkedValidation> _validationAttempts;
        private readonly int _linkedThreshold; 
        private readonly IMemoryCache _memoryCache;

        public TFNLinkedValidator(IMemoryCache memoryCache, int linkedThreshold = 3)
        {
            _validationAttempts = new List<LinkedValidation>();
            _memoryCache = memoryCache;
            _linkedThreshold = linkedThreshold;
        }

        public void AddTFN(string Tfn)
        {
            if (!_memoryCache.TryGetValue("ValidationAttempts", out _validationAttempts))
            {
                _validationAttempts = new List<LinkedValidation>();
            }
            _validationAttempts.Add(new LinkedValidation(Tfn));
            _validationAttempts.RemoveAll(attempt => (DateTime.UtcNow - attempt.Timestamp).TotalSeconds > 30);

            _memoryCache.Set("ValidationAttempts", _validationAttempts, TimeSpan.FromMinutes(5));

        }

        public bool IsTFNLinked(string Tfn)
        {
            AddTFN(Tfn);

            var substrings = GetFourDigitSubstrings(Tfn);
            var linkedAttempts = _validationAttempts
                //.Where(attempt => (DateTime.UtcNow - attempt.Timestamp).TotalSeconds <= 30)
                .Where(attempt => ContainsLinkedSubstring(attempt.Value, substrings))
                .ToList();

            return linkedAttempts.Count >= _linkedThreshold; 
        }

        private List<string> GetFourDigitSubstrings(string value)
        {
            var substrings = new List<string>();
            for (int i = 0; i <= value.Length - 4; i++)
            {
                substrings.Add(value.Substring(i, 4));
            }
            return substrings;
        }

        private bool ContainsLinkedSubstring(string attemptValue, List<string> substrings)
        {
            return substrings.Any(substring => attemptValue.Contains(substring));
        }

    }
}
