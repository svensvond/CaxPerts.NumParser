using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaxPerts.NumParser.Model
{
    //number drawing definition
    class NumberDefinition
    {
        public List<string> lines = new List<string>();
        public int Number;

        public int GetMaxLength()
        {
            int maxLength = 0;

            foreach (string l in lines)
            {
                maxLength = maxLength < l.Length ? l.Length : maxLength;          
            }

            return maxLength;
        }
    }
}
