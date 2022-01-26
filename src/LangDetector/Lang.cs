using Microsoft.ML.Data;

namespace LangDetector
{
    public class Lang
    {
        public const string LangKeyLabel = "PredictedLabel";

        [ColumnName(LangKeyLabel)]
        public string Language { get; set; }
    }
}
