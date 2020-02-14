using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaxPerts.NumParser.Model;
using CaxPerts.NumParser.Implementation;
using System.Diagnostics;
using CommandLine;

namespace CaxPerts.NumParser
{
    /*
     * Synopsis: Console App for reading and creating numbers out of ASCII Images (locally)
     * Usage, creating: CaxPerts.NumParser.exe -f "output.txt" -t write -n "93743" -s 2
     *  create new file with content "93743", numbers have 2 spaces
     * Usage, creating: CaxPerts.NumParser.exe -f "output.txt" -t write 
     *  create new file with content DateTime, numbers have 4 spaces (default)
     * Usage: reading: CaxPerts.NumParser.exe -f "output.txt" -t read 
     *  read file "output.txt" and display output in recognized numbers
     * Date: 2020.02.13
     * Dev: SG"
    */

    class Options
    {
        [Option('f', "", Required = true, HelpText = "file")]
        public string cmdOptionFile { get; set; }

        [Option('t', "task", Required = true, HelpText = "read or write")]
        public string cmdOptionTask { get; set; }

        [Option('n', "numbers", Required = false, HelpText = "numbers to convert to text file")]
        public string cmdOptionNumbers { get; set; }

        [Option('s', "spaces", Required = false, HelpText = "spaces between numbers, affects write only")]
        public int cmdOptionSpaces { get; set; }

    }


    class NumParserConsoleApp
    {

        static List<string> GetFileContent(string filePath)
        {
            try
            {                
                string dataFilePath = null;

                if (File.Exists(dataFilePath = filePath) || File.Exists(dataFilePath = Directory.GetCurrentDirectory() + @"\" + filePath))
                {
                    return File.ReadLines(dataFilePath).ToList();
                }

            } catch { Console.WriteLine("Error loading datafile"); }

            return null;

        }


        static void Main(string[] args)
        {
            string task = null;
            string inputFile = null;
            string inputNumbers = null;
            int spaces = 0;

            List<string> content = null;

            CommandLine.Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
            {
                task = o.cmdOptionTask;
                inputFile = o.cmdOptionFile;
                inputNumbers = o.cmdOptionNumbers;
                spaces = o.cmdOptionSpaces;
            });


            switch (task)
            {
                case "read":
                    
                    if ((content = GetFileContent(inputFile)) != null)
                    {
                        RecognitionEgine numParser = new RecognitionEgine(content);
                        ArrayList results = numParser.Content;
                        results.ToArray().ToList().ForEach(s => Console.WriteLine(s));
                        Console.ReadKey();
                    }
                    break;
                
                case "write":
                    
                    var ncls = new NumberCreationLines();


                    if (inputNumbers != null)
                    {
                        //Use Number Input
                        List<int> nclsInts = new List<int>();
                        inputNumbers.ToList().ForEach(s => nclsInts.Add(Int16.Parse(s.ToString())));
                        //numbers.ToString().ToList().ForEach(s => nclsInts.Add(Int16.Parse(s.ToString())));                        
                        ncls.Add(nclsInts);

                    }
                    else
                    {
                        //Use current dateTime

                        List<int> nclsInts = new List<int>();
                        string str = DateTime.Now.ToString("ddMMyyyy");
                        str.ToList().ForEach(s => nclsInts.Add(int.Parse(s.ToString())));
                        ncls.Add(nclsInts);

                        nclsInts = new List<int>();
                        str = DateTime.Now.ToString("hhmm");
                        str.ToList().ForEach(s => nclsInts.Add(int.Parse(s.ToString())));
                        ncls.Add(nclsInts);

                    }

                    CreationEngine creationEngine;

                    //spaces = spaces between drawn numbers
                    if (spaces != 0)
                    {
                        creationEngine = new CreationEngine(ncls, NumberDefinitions.CreateNumberDefinitions(), spaces);

                    } else
                    {
                        creationEngine = new CreationEngine(ncls, NumberDefinitions.CreateNumberDefinitions());
                    }
                    
                    List<string> drawing = creationEngine.GetDrawing();

                    try
                    {

                        using (StreamWriter writer = new StreamWriter(inputFile))
                        {
                            foreach (string s in drawing)
                            {
                                writer.WriteLine(s);
                            }
                        }

                    } catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        inputFile = null;
                    }

                    if (inputFile != null)
                    {
                        Process.Start(inputFile);
                    }
                    
                    break;
            
            }
            
        
        }


    }
}
