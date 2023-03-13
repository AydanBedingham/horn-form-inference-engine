using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine{
    public class Fact{
        private string subject;
        public string Subject { get { return subject; } }


        public Fact(string factSubject){
            subject = factSubject;
        }



        public override string ToString(){
            return subject;
        }




    }
}
