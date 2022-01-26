using NUnit.Framework;
using System;
using System.IO;

namespace LangDetector.Testing
{
    public class Tests
    {
        public readonly string PathModel = Path.Combine(Environment.CurrentDirectory, "dataset-train.txt");
        public readonly string PathModelTest = Path.Combine(Environment.CurrentDirectory, "dataset-test.txt");
        public readonly string PathData = Path.Combine("build", "langDetector.zip");

        public string[] Samples => new string[]
        {
            "Hello friend",
            "Hi brother",
            "Il m'intrigue.",
            "They play guitar",
            "That's right.",
            "Mi name is Luis",
            "Il est rêveur.",
            "Il a deux chats.",
            "It is rain",
            "Wow, that is amazing!!",
            "Il m'a frappée par deux fois.",
            "Это ужасно.",
            "Это лучше всего.",
            "Менты!",
            "He is a painter",
            "He's faster"
        };

        /// <summary>
        /// Simple show test
        /// </summary>
        [Test]
        public void TestPrediction()
        {
            var ml = DetectorPipeline.MLContext;

            DetectorPipeline.Train(PathModel, ml, out var schema, out var model);

            var engine = ml.Model.CreatePredictionEngine<LangIdentification, Lang>(model);

            foreach (var item in Samples)
            {
                var result = engine.Predict(new LangIdentification { Sentence = item});
                Console.WriteLine("{0} => {1} -- {2}", item, result.Language, LangDetector.LangMap[int.Parse(result.Language)]);
            }
        }
    }
}