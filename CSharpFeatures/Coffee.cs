namespace CSharpFeatures
{
    internal class Coffee
    {
        public string CoffeType { get; set; }
        public Coffee()
        {
        }

        public Coffee(string type)
        {
            CoffeType = type;
        }

        public override string ToString()
        {
            return CoffeType;
        }
    }
}