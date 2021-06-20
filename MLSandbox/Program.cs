using System;
using MLSandboxML.Model;

namespace MLSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var nextTest = true;

            while (nextTest)
            {
                Console.WriteLine($"Type your comment (or type 'q' to quit):");

                var comment = Console.ReadLine();

                if (comment.Equals("q") || comment.Equals("quit"))
                {
                    nextTest = false;
                    Console.WriteLine("Ending program");
                }

                else if (string.IsNullOrEmpty(comment))
                {
                    Console.WriteLine();
                }

                else
                {
                    // Add input data
                    var input = new ModelInput() { Comment = comment };

                    // Load model and predict output of sample data
                    ModelOutput result = ConsumeModel.Predict(input);

                    var sentiment = result.Prediction == "1" ? "Positive" : "Negative";
                    Console.WriteLine($"Text: {input.Comment}\nSentiment: {sentiment}\n");
                }
            }
        }
    }
}
