using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine{
    public class TruthTableHelper{

        // remove rows if clause is invalid
        public static void RemoveRowsIfClauseInvalidRemoveRowsIfFalse(List<string> symbols, List<List<bool>> truthtable, Clause clause){
            int rowindex = 0;
            bool premiseTrue;
            while (rowindex < truthtable.Count()){
                premiseTrue = true;
                // for each conjunct in clause
                foreach (string conjunct in clause.Conjuncts){

                    int symbolIndex = symbols.IndexOf(conjunct);
                    // check if the conjunct is true
                    bool conjunctTrue = truthtable[rowindex][symbolIndex];
                    
                    // if not return false
                    if (!conjunctTrue){
                        premiseTrue = false;
                    }
                }

                // check if entails is true.
                bool entailsTrue = truthtable[rowindex][symbols.IndexOf(clause.Entails)];

                // if entails is not true and conjuncts arent true dont match entails is true return false
                if (!entailsTrue){
                    if ((premiseTrue != entailsTrue)){
                        truthtable.RemoveAt(rowindex);
                        rowindex--;
                    }
                }

                rowindex++;
            }
        }

        // remove rows if value false
        public static void RemoveRowsIfFalse(List<List<bool>> truthtable, int column){
            int i = 0;
            while (i < truthtable.Count){
                if (truthtable[i][column] == false){
                    truthtable.RemoveAt(i);
                    i--;
                }

                i++;
            }
        }
    }
}
