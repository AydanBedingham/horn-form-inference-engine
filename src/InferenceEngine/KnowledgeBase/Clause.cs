using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace InferenceEngine{
    public class Clause{
        private string premise;
        public string Premise{get{return premise;}}

        private List<string> conjuncts;
        public string[] Conjuncts {get {return conjuncts.ToArray();}}

        private string entails;
        public string Entails { get { return entails; } }

        public Clause(string clause){
            conjuncts = new List<string>();
            // remove the spaces
            clause = clause.Replace(" ", string.Empty);

            // split by =>
            string[] temp = Regex.Split(clause, "=>").ToArray();
            premise = temp[0]; //first part
            entails = temp[1]; //second part

            // conjuncts
            string[] conjunctsArray = Regex.Split(premise, "&");
            conjuncts.AddRange(conjunctsArray);

        }

        public Boolean InPremise(String p){
            // check if conjuncts contains the premise p
            return conjuncts.Contains(p);
        }

        public override string ToString(){
            string output = premise + "=>" + entails;
            return output;
        }

    }
}
