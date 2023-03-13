using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InferenceEngine
{
    public static class ReasoningMethodHelper
    {

        /*
            Identifies reasoning method (enumeration) from string 
        */

        public static ReasoningMethod StrToReasoningMethod(string reasoningMethodStr)
        {
            if (reasoningMethodStr.ToUpper().Equals("FC"))
                return ReasoningMethod.ForwardChaining;
            else if (reasoningMethodStr.ToUpper().Equals("BC"))
                return ReasoningMethod.BackwardChaining;
            else if (reasoningMethodStr.ToUpper().Equals("TT"))
                return ReasoningMethod.TruthTable;
            else
                throw new Exception("Invalid reasoning method");
        }


    }
}
