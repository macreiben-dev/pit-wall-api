using Newtonsoft.Json;
using NFluent;
using System.Diagnostics;
using System.Globalization;

namespace PitWallDataGatheringApi.Integration.Tests
{
    public static class TestHelper
    {
        public static void DisplayContent(HttpResponseMessage responseMessage)
        {
            using StreamReader reader = new StreamReader(responseMessage.Content.ReadAsStream());

            string response = reader.ReadToEnd();

            var x = JsonConvert.DeserializeObject(response);

            var formated = JsonConvert.SerializeObject(x, Formatting.Indented);

            Trace.WriteLine(formated.ToString());
        }

        public static void ExecuteAndAssert(Task<string> task1, object expected)
        {
            Task.WaitAll(task1);

            string intermediary = ReadResult(task1);

            var actual = Double.Parse(intermediary, CultureInfo.InvariantCulture);

            Check.That(actual).IsEqualTo(expected);
        }

        private static string ReadResult(Task<string> result)
        {
            if (result.Exception != null)
            {
                var inner = result.Exception.InnerExceptions.FirstOrDefault();

                if (inner != null)
                {
                    throw inner;
                }

                throw result.Exception;
            }

            string intermediary = result.Result;

            return intermediary;
        }
    }
}
