using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine{
    class TruthTable{
        // Create a list to store the truth table
        private List<List<bool>> truthTable = new List<List<bool>>();

        // The constructor
        public TruthTable(int column){
            // Create a row for each possibility
            for (int i = 0; i < Math.Pow(2, column); i++){
                // Create a row to store the objects
                List<bool> list = new List<bool>(); 
                // For each column
                for (int j = 0; j < column; j++){
                    list.Add(generatevalue(i, j));
                }

                // Add the row to the list
                truthTable.Add(list);
            }
        }

        public List<List<bool>> ToList(){
            return truthTable;
        }

        // The row number dividided by the number of rows is divisible by 2
        private bool generatevalue(int multiplier, int column){
            return (multiplier / (int)Math.Pow(2, column)) % 2 == 1;
        }

        public bool[][] ToArray(){
            // Create a list to return
            List<bool[]> truthTableList = new List<bool[]>();

            // Generate a row for each row
            foreach (List<bool> row in truthTable){
                truthTableList.Add(row.ToArray());
            }
            
            // return the array
            return truthTableList.ToArray();
        }

        public void print(){
            // For each row print the value
            foreach (List<bool> list in truthTable){
                foreach (bool truth in list)
                {
                    Console.Write((truth?"1":"0") + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
