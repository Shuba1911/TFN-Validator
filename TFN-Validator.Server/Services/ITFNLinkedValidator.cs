namespace TFN_Validator.Server.Services
{
    public interface ITFNLinkedValidator
    {
        public void AddTFN(string Tfn);

        public bool IsTFNLinked(string Tfn);

    }
}
