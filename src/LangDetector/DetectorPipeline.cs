using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangDetector
{

    public static partial class DetectorPipeline
    {
        private const string OutputLabel = "Label";
        private const string LabelSentenceFeaturized = "SentenceFeaturized";

        /// <summary>
        /// New frsh context with zero as seed.
        /// </summary>
        public static MLContext MLContext => new(seed: 0);

        /// <summary>
        /// Make the train process
        /// </summary>
        /// <param name="context"></param>
        public static IEstimator<ITransformer> BuildPipeline(MLContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // create the pipeline
            return context.Transforms.Conversion.MapValueToKey(inputColumnName: nameof(LangIdentification.Language), outputColumnName: OutputLabel)
                .Append(context.Transforms.Text.FeaturizeText(inputColumnName: nameof(LangIdentification.Sentence), outputColumnName: LabelSentenceFeaturized))
                .Append(context.Transforms.Concatenate("Features", inputColumnNames: LabelSentenceFeaturized))
                // chose SDCA maximiun entropy
                .Append(context.MulticlassClassification.Trainers.NaiveBayes(OutputLabel, "Features"))
                .Append(context.Transforms.Conversion.MapKeyToValue(Lang.LangKeyLabel));
        }

        /// <summary>
        /// Get data from a path.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="pathData"></param>
        /// <returns></returns>
        public static IDataView GetDataFrom(MLContext context, string pathData)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (string.IsNullOrEmpty(pathData) || string.IsNullOrWhiteSpace(pathData))
            {
                throw new ArgumentException($"'{nameof(pathData)}' cannot be null, empty or whitespace.", nameof(pathData));
            }

            // from passed path file
            return context.Data.LoadFromTextFile<LangIdentification>(pathData, hasHeader: false);
        }

        /// <summary>
        /// Train and save at the time.
        /// </summary>
        /// <param name="ml"></param>
        /// <param name="dataPath"></param>
        /// <param name="saveModel"></param>
        public static void TrainAndSave(MLContext ml, string dataPath, string saveModel)
        {
            IDataView trainerData;
            ITransformer model;
            Train(dataPath, ml, out trainerData, out model);

            // save
            ml.Model.Save(model, trainerData.Schema, saveModel);
        }

        /// <summary>
        /// Do train by passed path and get out <see cref="IDataView"/> and <see cref="ITransformer"/> build.
        /// </summary>
        /// <param name="dataPath"></param>
        /// <param name="ml"></param>
        /// <param name="trainerData"></param>
        /// <param name="model"></param>
        public static void Train(string dataPath, MLContext ml, out IDataView trainerData, out ITransformer model)
        {
            trainerData = GetDataFrom(ml, dataPath);
            var pipeline = BuildPipeline(ml);

            // train
            model = pipeline.Fit(trainerData);
        }

        /// <summary>
        /// Make a test and get a result about it.
        /// </summary>
        /// <param name="testPath"></param>
        /// <param name="ml"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TestResult TestData(string testPath, MLContext ml, ITransformer model)
        {
            var testData = GetDataFrom(ml, testPath);
            
            // make group predictions
            IDataView transformed = model.Transform(testData);

            // takes result
            return new TestResult(scores: transformed.GetColumn<float>("Score").ToArray(),
                                  predictions: transformed.GetColumn<string>(Lang.LangKeyLabel).ToArray());
        }
    }
}
