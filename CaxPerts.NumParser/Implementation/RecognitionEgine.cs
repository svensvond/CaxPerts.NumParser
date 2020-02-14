using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaxPerts.NumParser.Model;

namespace CaxPerts.NumParser.Implementation
{
    class RecognitionEgine
    {

        List<NumberDefinition> NumberDefinitionList;

        public ArrayList Content = new ArrayList();


        List<FindResult> GetNumbersInDrawing(List<string> searchStringList, NumberDefinition numDef)
        {
            List<FindResult> results = new List<FindResult>();

            var firstLinehits = new List<Tuple<int, int>>();

            for (int i = 0; i < searchStringList.Count; i++)
            {
                //search matches inline, add to init-check-position (firstLineHits)
                foreach (int charPos in searchStringList[i].AllIndexesOf((string)numDef.lines[0]).ToList())
                {
                    firstLinehits.Add(new Tuple<int, int>(i, charPos));
                }
            }

            foreach (var firstLine in firstLinehits)
            {
                //try to get followups
                int lineNumber = firstLine.Item1;
                int charNumber = firstLine.Item2;
                bool found = false;
                if (searchStringList.Count >= lineNumber + numDef.lines.Count)
                {
                    found = true;
                    for (int i = 1; i < numDef.lines.Count; i++)
                    {
                        //followup lines are too short -> skip
                        if (searchStringList[lineNumber + i].Length >= charNumber + numDef.lines[i].Length)
                        {
                            if (searchStringList[lineNumber + i].Substring(charNumber, numDef.lines[i].Length) != numDef.lines[i])
                            {
                                found = false;
                            }
                        } else
                        {
                            found = false;
                        }

                    }
                }
                if (found)
                {
                    results.Add(new FindResult() { Number = numDef.Number, Line = lineNumber, CharPos = charNumber });
                }

            }

            return results;

        }



        public RecognitionEgine(List<string> searchStringList)
        {
            NumberDefinitionList = NumberDefinitions.CreateNumberDefinitions();

            var numberFindResults = new List<FindResult>();

            Content.Add("");

            foreach (NumberDefinition nd in NumberDefinitionList)
            {
                numberFindResults.AddRange(GetNumbersInDrawing(searchStringList, nd));
            }

            var lineNum = 0;
            foreach (var nfr in numberFindResults.OrderBy(s => s.Line).ThenBy(x => x.CharPos))
            {
                bool skip = false;
                if (nfr.Line != lineNum)
                {
                    //condition: numbers need to be 4 lines in height!
                    if (nfr.Line > lineNum + 3)
                    {
                        //Console.WriteLine();
                        Content.Add("");
                        lineNum = nfr.Line;
                    } else
                    {
                        skip = true;
                    }
                }
                if (!skip)
                {
                    //Content.Add(nfr.Number);
                    Content[Content.Count - 1] += nfr.Number.ToString();
                    //Console.Write(nfr.Number);
                }
                
            }

        }
    }
}
