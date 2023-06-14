using System.Text.RegularExpressions;

namespace RoslynSamples.RegularExpressions
{
    public static partial class Demo
    {
        const string good_address = "matt.gallant@virtekvision.com";
        const string bad_address = "This is not@a.valid.email";

        private static Regex _emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        private static partial Regex GeneratedEmailRegex();

        public static void Run()
        {
            Console.WriteLine("The old fashioned way:");
            Console.WriteLine($"Is {good_address} valid? {_emailRegex.IsMatch(good_address)}");
            Console.WriteLine($"Is {bad_address} valid? {_emailRegex.IsMatch(bad_address)}");

            Console.WriteLine();

            var generatedRegex = GeneratedEmailRegex();
            Console.WriteLine("The new fashioned way:");
            Console.WriteLine($"Is {good_address} valid? {generatedRegex.IsMatch(good_address)}");
            Console.WriteLine($"Is {bad_address} valid? {generatedRegex.IsMatch(bad_address)}");
        }
    }
}
