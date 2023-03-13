using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace InferenceEngine{
    public class InferenceLoader{
        // List of facts
        private List<string> facts;
        public string[] FactStatements { get { return facts.ToArray(); } }

        // List of clauses
        private List<string> clauses;
        public string[] ClauseStatements { get { return clauses.ToArray(); } }

        // The ask string
        private string ask;
        public string Query { get { return ask; } }

        // Constructor, initiate the lists
        public InferenceLoader(){
            facts = new List<string>();
            clauses = new List<string>();
        }



        // Uses TELL string to seperate facts and clauses
        public void AnalyseTellString(string tellStr){
            // Take spaces out
            tellStr = tellStr.Replace(" ", string.Empty);

            // Create an array of clauses
            string[] entries = Regex.Split(tellStr, ";");

            // For each entry
            foreach (string entry in entries){
                // If its a clause
                if (entry.Contains("=>")){
                    //Add as premise
                    clauses.Add(entry);
                }
                // If its a fact
                else{
                    //Add as fact
                    facts.Add(entry);
                }
            }
        }


        /* 
            Loads problem information from file
            - Identifies query based on ASK statement.
            - Generates statements for individual facts and clause based on TELL statement
        */
        public void LoadFromFile(string path){
            // Check file
            if (!System.IO.File.Exists(path)) throw new Exception("Invalid file path");
            
            // Create the reader
            System.IO.StreamReader objReader = new System.IO.StreamReader(path);

            // To store the data
            string line1;
            string line2;
            while ( (line1 = objReader.ReadLine()) !=null){
                // If its a tell string
                if (line1.ToUpper().Equals("TELL")){
                    line2 = objReader.ReadLine();
                    AnalyseTellString(line2);
                }else

                if (line1.ToUpper().Equals("ASK")){
                    line2 = objReader.ReadLine();
                    ask = line2;
                }

            }
         
        }


        public override string ToString(){
            string output="";

            output += "Fact Statements:" + System.Environment.NewLine;
            foreach (string fact in facts){
                output += fact + System.Environment.NewLine;
            }

            output += System.Environment.NewLine;

            output += "Clause Statements:" + System.Environment.NewLine;
            foreach (string clause in clauses){
                output += clause + System.Environment.NewLine;
            }

            output += System.Environment.NewLine;

            output += "Query: " + ask;

            return output;
        }
    }
}
