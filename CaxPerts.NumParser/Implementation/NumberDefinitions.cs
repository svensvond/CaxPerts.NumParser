using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaxPerts.NumParser.Model;

namespace CaxPerts.NumParser.Implementation
{
    static class NumberDefinitions
    {
        public static NumberDefinition GetNumberDefinitionByNumber(int num, List<NumberDefinition> numDefList)
        {
            NumberDefinition nd = new NumberDefinition();

            return numDefList.FirstOrDefault(n => n.Number == num);
        }

        public static List<NumberDefinition> CreateNumberDefinitions()
        {
            List<NumberDefinition> result = new List<NumberDefinition>();

            NumberDefinition nd = new NumberDefinition();
            nd.Number = 0;
            nd.lines.Add("-----");
            nd.lines.Add("|   |");
            nd.lines.Add("|   |");
            nd.lines.Add("|___|");
            result.Add(nd);

            nd = new NumberDefinition();
            nd.Number = 1;
            nd.lines.Add(" | ");
            nd.lines.Add(" | ");
            nd.lines.Add(" | ");
            nd.lines.Add(" | ");
            result.Add(nd);

            nd = new NumberDefinition();
            nd.Number = 2;
            nd.lines.Add("---");
            nd.lines.Add(" _|");
            nd.lines.Add("|  ");
            nd.lines.Add("---");
            result.Add(nd);

            nd = new NumberDefinition();
            nd.Number = 3;
            nd.lines.Add("---");
            nd.lines.Add(" / ");
            nd.lines.Add(@" \ ");
            nd.lines.Add("---");
            result.Add(nd);

            nd = new NumberDefinition();
            nd.Number = 4;
            nd.lines.Add("|   |");
            nd.lines.Add("|___|");
            nd.lines.Add("    |");
            nd.lines.Add("    |");
            result.Add(nd);

            nd = new NumberDefinition();
            nd.Number = 5;
            nd.lines.Add("-----");
            nd.lines.Add("|___ ");
            nd.lines.Add("    |");
            nd.lines.Add("____|");
            result.Add(nd);

            nd = new NumberDefinition();
            nd.Number = 6;
            nd.lines.Add("-----");
            nd.lines.Add("|___");
            nd.lines.Add("|   |");
            nd.lines.Add("|___|");
            result.Add(nd);

            nd = new NumberDefinition();
            nd.Number = 7;
            nd.lines.Add("-----");
            nd.lines.Add("   / ");
            nd.lines.Add("  /  ");
            nd.lines.Add(" /   ");
            result.Add(nd);

            nd = new NumberDefinition();
            nd.Number = 8;
            nd.lines.Add("-----");
            nd.lines.Add("|___|");
            nd.lines.Add("|   |");
            nd.lines.Add("|___|");
            result.Add(nd);

            nd = new NumberDefinition();
            nd.Number = 9;
            nd.lines.Add("-----");
            nd.lines.Add("|___| ");
            nd.lines.Add("    |");
            nd.lines.Add("____|");
            result.Add(nd);


            return result;

        }
    }
}
