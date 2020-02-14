using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaxPerts.NumParser.Model;

namespace CaxPerts.NumParser.Implementation
{
    //Definition what numbers are converted to drawing
    class NumberCreationLines: List<List<int>>
    {
        public void Add(int[] numbers)
        {
            Add(numbers.ToList<int>());
        }

    }


    class CreationEngine
    {

        NumberCreationLines creationLines;

        List<NumberDefinition> numberDefinitions;

        public int numSpaces;

        public List<string> GetDrawing()
        {
            var results = new List<string>();

            foreach (var cr in creationLines.ToList())
            {
                //create empty lines
                int numberHeight = 4;
                for (int i = 0; i <= numberHeight; i++) { results.Add(""); }

                // Number from one line in creationList - all numbers in cr needs to be in one string line
                foreach (int num in cr.ToList()) {

                    //get string drawing for number
                    var drawing = NumberDefinitions.GetNumberDefinitionByNumber(num, NumberDefinitions.CreateNumberDefinitions());

                    //if null, numberdefinition not found
                    if (drawing != null)
                    {

                        //drawing number max length
                        int maxLength = drawing.GetMaxLength() + numSpaces; //horizontal spaces here                    

                        // for (int drawingLine = drawing.lines.Count - 1; drawingLine >= 0; drawingLine--)
                        for (int drawingLine = 0; drawingLine <= drawing.lines.Count - 1; drawingLine++)
                        {
                            string s = drawing.lines[drawingLine];

                            if (s.Length < maxLength)
                            {
                                while (s.Length < maxLength)
                                {
                                    s += " ";
                                }
                            }

                            results[results.Count - 1 - (drawing.lines.Count) + drawingLine] = results[results.Count - 1 - (drawing.lines.Count) + drawingLine] + s;

                        }
                    }

                }

            }

            return results;
        }

        public CreationEngine(NumberCreationLines cLines, List<NumberDefinition> numDefList, int spaces = 4)
        {
            creationLines = cLines;
            numberDefinitions = numDefList;
            numSpaces = spaces;

        }
    }
}
