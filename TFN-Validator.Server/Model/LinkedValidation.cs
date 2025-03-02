namespace TFN_Validator.Server.Model
{
    public class LinkedValidation
    {
        public string Value { get; set; }
        public DateTime Timestamp { get; set; }

        public LinkedValidation(string value)
        {
            Value = value;
            Timestamp = DateTime.UtcNow;
        }
    }
}
