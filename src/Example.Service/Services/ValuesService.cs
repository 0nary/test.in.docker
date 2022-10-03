namespace Example.Service.Services
{
    public class ValuesService
    {
        public IEnumerable<string> GetValues() => new string[] { "Value1", "Value2", "Value3" };

        public string GetValue() => GetValues().First();
    }
}
