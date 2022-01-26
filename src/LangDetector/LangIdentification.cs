using Microsoft.ML.Data;

namespace LangDetector
{
    public class LangIdentification
    {
        [LoadColumn(0)]
        public string Sentence { get; set; }

        [LoadColumn(1)]
        public string Language { get; set; }
    }
}
