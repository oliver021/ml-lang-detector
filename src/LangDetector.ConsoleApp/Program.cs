using Microsoft.Extensions.CommandLineUtils;
using System;

namespace LangDetector.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();

            app.Command("train", config =>
            {
                config.Description = "Train from a datasets and save result in file";

                // set arguments
                var pathFile = config.Argument("datset","A path of dataset file.");
                var pathDest = config.Argument("out","The path to save result of train.");

                // execute command
                config.OnExecute(delegate 
                {
                    var path = pathFile.Value;
                    var save = pathDest.Value;

                    // save data
                    DetectorPipeline.TrainAndSave(DetectorPipeline.MLContext, path, save);

                    return 0;
                });
            });

            app.Execute(args);
        }
    }
}
