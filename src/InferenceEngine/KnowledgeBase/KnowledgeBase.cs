using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace InferenceEngine{


    public class KnowledgeBase{
        // List of facts
        private List<Fact> facts;
        public Fact[] Facts {get {return facts.ToArray();}}

        // List of clauses
        private List<Clause> clauses;
        public Clause[] Clauses { get { return clauses.ToArray(); } }

        // List of entailed for producing the trace
        private List<string> entailed;
        public String[] Entailed { get { return entailed.ToArray(); } }

        // The count of premises for each clause
        private Dictionary<string, int> count;

        // Construct the lists
        public KnowledgeBase(){
            facts = new List<Fact>();
            clauses = new List<Clause>();
            entailed = new List<String>();
            count = new Dictionary<string,int>();
        }


        // Adds a new fact
        public void AddFact(string statement){
            Fact newFact = new Fact(statement);
            if (newFact.Subject != ""){
                facts.Add(newFact);
            }
        }


        // Adds a new clause
        public void AddClause(string statement){
            Clause newClause = new Clause(statement);
            clauses.Add(newClause);
            // Add to the count
            count.Add(newClause.Entails,newClause.Conjuncts.Count());
        }

        // Execute query using a given reasoning method 
        public string ExecuteQuery(string query, ReasoningMethod reasoningMethod){
           
            string result = "";
            
            if (reasoningMethod == ReasoningMethod.BackwardChaining)
                result = ExecuteBackwardChainingQuery(query);
            else
            if (reasoningMethod == ReasoningMethod.ForwardChaining)
                result = ExecuteForwardChainingQuery(query);
            else if (reasoningMethod == ReasoningMethod.TruthTable)
                result = ExecuteTruthTableQuery(query);
            else throw new Exception("KnowledgeBase cannot utilise this reasoning method");

            return result;
        }
        
        // Truth table
        private string ExecuteTruthTableQuery(string query){
            List<string> symbol = new List<string>();
            
            // add all symbols to symbol
            foreach (Fact f in facts){
                if (!symbol.Contains(f.Subject))
                symbol.Add(f.Subject);
            }

            foreach (Clause c in clauses){
                foreach (String conjunct in c.Conjuncts){
                    if (!symbol.Contains(conjunct))
                    symbol.Add(conjunct);
                }

                if (!symbol.Contains(c.Entails))
                symbol.Add(c.Entails);
            }

            // Create truth table
            TruthTable truthTable = new TruthTable(symbol.Count());
            
            List<List<bool>> truthTableList = truthTable.ToList();

            // Remove the rows where the clauses arent true
            foreach(Clause clause in Clauses){
                TruthTableHelper.RemoveRowsIfClauseInvalidRemoveRowsIfFalse(symbol, truthTableList, clause);
            }

            // remove the rows where the facts arent true
            foreach (Fact f in facts){
                TruthTableHelper.RemoveRowsIfFalse(truthTableList, symbol.IndexOf(f.Subject));
            }

            // remove the rows where the query isnt true
            TruthTableHelper.RemoveRowsIfFalse(truthTableList, symbol.IndexOf(query));
            
            // if the row count then give "YES" plus the number of rows
            if (truthTableList.Count > 0){
                return "YES: " + truthTableList.Count;
            }
            else return "No";
        }

        // Backward chaining
        private string ExecuteBackwardChainingQuery(string query){
            string result = "";
            // If it entails print Yes plus the number of entailed
            if (BCEntail(query)){
                result += "YES: ";
                result += entailed.Aggregate((i, j) => i + ", " + j);
            }
            else{
                result += "NO";
            }
    

            return result;
        }

        // Forward chaining
        private string ExecuteForwardChainingQuery(string query){
            string result = "";
            if (FCEntail(query)){
                result += "YES: ";
                result += entailed.Aggregate((i, j) => i + ", " + j);
            }
            else{
                result += "NO";
            }


            return result;
        }

        private bool BCEntail(string query){

            List<String> agenda = new List<string>();
            foreach (Fact f in facts){
                agenda.Add(f.Subject);
            }

            while (agenda.Count() != 0){
                // take the first item and process it
                String currentAgenda = agenda.ElementAt(agenda.Count() - 1);
                agenda.RemoveAt(agenda.Count() - 1);
                //get first, then remove. Emulates a pop

                // add to entailed
                entailed.Add(currentAgenda);
                // for each of the clauses....
                foreach (Clause c in clauses){
                    // .... that contain currentAgenda in its premise
                    if (c.InPremise(currentAgenda)){
                        //find it in the count dictionary
                        if (count.ContainsKey(c.Entails)){
                            int newCountValue = --count[c.Entails];
                            if (newCountValue == 0){
                                string head = c.Entails;
                                if (head == query){
                                    entailed.Add(head);
                                    return true;
                                }
                                agenda.Add(head);
                            }
                        }
                    }
                }
            }
            // if we arrive here then ask cannot be entailed
            return false;
        }

        private bool FCEntail(string query){
            List<String> agenda = new List<string>();
            foreach (Fact f in facts){
                agenda.Add(f.Subject);
            }
            
            while(agenda.Count() != 0){
		        // take the first item and process it
	 	        String currentAgenda = agenda.ElementAt(0);
                agenda.RemoveAt(0);
                //get first, then remove. Emulates a pop

		        // add to entailed
		        entailed.Add(currentAgenda);
		        // for each of the clauses....
                foreach (Clause c in clauses){
			        // .... that contain currentAgenda in its premise
			        if (c.InPremise(currentAgenda)){
                        //find it in the count dictionary
                        if (count.ContainsKey(c.Entails)){
                            int newCountValue = --count[c.Entails];
                            if (newCountValue == 0){
                                string head = c.Entails;
                                if (head == query){
                                    entailed.Add(head);
                                    return true;
                                }
                                agenda.Add(head);
                            }
                        }
			        }
		        }
	        }
	        // if we arrive here then ask cannot be entailed
            return false;
        }



    }
}
