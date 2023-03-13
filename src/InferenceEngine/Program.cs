using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine
{
    class Program
    {
        // the reasoning method that the program will use, FC BC or TT.
        // Will use the ReasoningMethodHelper class that checks for a valid method
        static ReasoningMethod reasoningMethod;

        // the filename of the file to read. The filename is the second argument and is checked to be valid in InferenceLoader
        static string filename;


        /*
            Reads-in comman-line parameters 
        */
        public static void ReadCommandLineParameters(ref string[] arg){
            // Arguments
            // Args[0] == Reasoning
            // Args[1] == Filename
            //
            // check if there are two parameters for method (FC, BC, TT) and the filename
            if (arg.Length < 2) throw new Exception("Usage:InferenceEngine.exe <reasoning> <filename>");

            // command line arguments are validated at ReasoningMethodHelper and InferenceLoader
            // set the reasoning method to the first argument
            reasoningMethod = ReasoningMethodHelper.StrToReasoningMethod(arg[0]);
            
            filename = arg[1];
        }

        static void Main(string[] args)
        {
            try
            {
                //Read in commandline arguments
                ReadCommandLineParameters(ref args);


                //Create inferenceloader, handles facts, sentences, clauses and processing of file
                InferenceLoader inferenceLoader = new InferenceLoader();
                
                //Load problem from file, reads the file in and loads facts and statements
                inferenceLoader.LoadFromFile(filename);


                //Get problem info from inferenceloader.The facts and clauses are loaded from the inferenceLoader loadfromfile when reading in the file
                string[] factStatements = inferenceLoader.FactStatements;
                string[] clauseStatements = inferenceLoader.ClauseStatements;
                string query = inferenceLoader.Query;


                //Create knowledgebase. The knowledgebase is used to store and process the facts and clauses
                KnowledgeBase kb = new KnowledgeBase();
                kb = new KnowledgeBase();

                //Add facts to knowledgebase
                foreach (string statement in factStatements)
                    kb.AddFact(statement);

                //Clauses to knowledgebase
                foreach (string statement in clauseStatements)
                    kb.AddClause(statement);

                //Execute query against knowledgebase using reasoningMethod
                string result = kb.ExecuteQuery(query, reasoningMethod);


                //print query result
                Console.Out.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error: " + ex.Message);
            }


        }
    }
}
