using Corealate;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace CorealateApp
{
    class Program
    {
        private const string result1 = "result-1.txt";
        private const string result2 = "result-2.txt";
        private const string result4 = "result-4.txt";
        private const string result5 = "result-5.txt";
        private const string result6 = "result-6.txt";
        private const string batchFile = "input-file.txt";

        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            DisplayMenu();

            Console.Read();
        }

        private static void BuildMenu()
        {
            int.TryParse(Console.ReadLine(), out var menuOptions);

            switch (menuOptions)
            {
                case 1:
                    {
                        //TASK 1 REVERSE LINES - 15m
                        SaveFile(result1, DataContext.ReverseLines().ToArray());
                        File.WriteAllLines("result-1.txt", DataContext.ReverseLines());
                        Console.WriteLine($"Files was saved in {AppDomain.CurrentDomain.BaseDirectory}");

                        Console.Read();
                        DisplayMenu();
                        break;
                    }
                case 2:
                    {
                        //TASK 2 SORT BY TYPES 1h
                        var newLines = DataContext.CreateNewSortedFileByColumn("typ herbaty");
                        SaveFile(result2, newLines.ToArray());
                        Console.WriteLine($"Files was saved in {AppDomain.CurrentDomain.BaseDirectory}");

                        Console.Read();
                        DisplayMenu();
                        break;
                    }
                case 3:
                    {
                        //TASK 3 10s witch short break for tea
                        Console.WriteLine("Do nothing");
                        Console.Read();
                        DisplayMenu();
                        break;
                    }
                case 4:
                    {
                        //TASK 4 1,5h
                        string result = MakeTea();

                        Console.WriteLine(result);
                        Console.Read();
                        DisplayMenu();
                        break;
                    }
                case 5:
                    //TASK 5 10m
                    MakeSeveralTeas();

                    Console.Read();
                    DisplayMenu();
                    break;
                case 6:
                    //TASK 6 1,5h
                    MakeTouareg();

                    Console.Read();
                    DisplayMenu();
                    break;
                default:
                    {
                        DisplayMenu();
                        break;
                    }
            }
        }

        private static string MakeTea()
        {
            Console.Clear();
            Console.WriteLine("------------ Select tea to prepare ------------");

            DataContext.GetDataTableFromTextFile()
                .AsEnumerable()
                .Select(x => x[0])
                .ToList()
                .ForEach(Console.WriteLine);

            Console.WriteLine("Input tea name: ");

            ITea tea = new Tea(Console.ReadLine());

            Console.Clear();

            Console.WriteLine($"{tea.TeaName} temperature: ");

            double.TryParse(Console.ReadLine(), out double temp);
            Console.WriteLine($"Amount of time you want to spend brewing {tea.TeaName} (seconds): ");
            double.TryParse(Console.ReadLine(), out double time);

            var result = tea.PrepareTea(time, temp);
            SaveFile(result4, new []{result});
            return result;
        }

        private static void MakeTouareg()
        {
            Tea baseTea = null;

            foreach (var line in File.ReadAllLines(batchFile))
            {
                var teas = line.Split(new string[] { ", " }, StringSplitOptions.None);

                if (teas[1].ToUpper() == "WATER")
                {

                    baseTea = new Tea(teas[0])
                    {
                        UserBrewingTime = double.Parse(teas[3]),
                        UserBrewingTemp = double.Parse(teas[2])
                    };
                }
                else
                {
                    if (baseTea != null)
                    {
                        Touareg touareg = new Touareg(teas[0])
                        {
                            BaseOfTea = baseTea
                        };
                        var result = touareg.PrepareTea(double.Parse(teas[2]), double.Parse(teas[3]));
                        Console.WriteLine(result);

                        SaveFile(result6, new[]{result});
                        Console.WriteLine($"Files was saved in {AppDomain.CurrentDomain.BaseDirectory}");


                    }
                }

            }
        }

        private static void MakeSeveralTeas()
        {
            List<string> teaList = new List<string>();
            foreach (var lineTea in File.ReadAllLines(batchFile))
            {
                ITea tea = new Tea(lineTea.Split(new string[] { ", " }, StringSplitOptions.None)[0]);
                teaList
                    .Add(tea.TeaName + ", " + tea.PrepareTea(
                        temp: double.Parse(lineTea.Split(new string[] { ", " }, StringSplitOptions.None)[1]),
                        time: double.Parse(lineTea.Split(new string[] { ", " }, StringSplitOptions.None)[2])));
            }
            SaveFile(result5, teaList.ToArray());
            Console.WriteLine($"Files was saved in {AppDomain.CurrentDomain.BaseDirectory}");
        }

        private static void SaveFile(string fileName, String[] TextToSave)
        {
            try
            {
                File.WriteAllLines(fileName, TextToSave.ToArray());
            }
            catch (UnauthorizedAccessException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Which command do you want to run?");
            Console.WriteLine("1 - Reverse lines from text file");
            Console.WriteLine("2 - Sort lines by tea type");
            Console.WriteLine("4 - Prepare tea");
            Console.WriteLine("5 - Make several tea");
            Console.WriteLine("6 - Make a Touareg tea");
            BuildMenu();


        }
    }
}
