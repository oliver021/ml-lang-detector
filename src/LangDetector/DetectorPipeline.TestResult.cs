using System;

namespace LangDetector
{

    public static partial class DetectorPipeline
    {
        public struct TestResult
        {
            public TestResult(float[] scores, string[] predictions)
            {
                Scores = scores ?? throw new ArgumentNullException(nameof(scores));
                Predictions = predictions ?? throw new ArgumentNullException(nameof(predictions));
            }

            public float[] Scores { get; }

            public string[] Predictions { get; }

        }
    }
}
