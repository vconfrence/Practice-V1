using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //user input and instructions
            Console.WriteLine("Please enter the folder path for csv files");
            Console.WriteLine("Note that comma seperated file should be named cfile.csv");
            Console.WriteLine("Pipe seperated file should be named pfile.csv");
            Console.WriteLine("Space seperated file should be named sfile.csv");
            Console.WriteLine();
            Console.WriteLine("Input Example");
            Console.WriteLine("c:\\users\\user\\desktop");

            int cntr = 0;
            while (true)
            {
                string input = Console.ReadLine();

                //if the files have been processed and user hasn't pressed exit
                if (cntr == 1 && input != "exit")
                    continue;
                //if the files have been processed and user pressed exit
                if (cntr == 1 && input == "exit")
                    break;
                //folder path check
                if (Directory.Exists(input) == false)
                {
                    Console.WriteLine("Please enter a valid path");
                    continue;
                }
                List<string> files = new List<string>
                {
                    input + "\\cfile.csv",
                    input + "\\pfile.csv",
                    input + "\\sfile.csv"
                };

                //checking for at least one valid file name within provided folder
                bool contains = false;
                foreach (string file in files)
                {
                    if (File.Exists(file))
                    {
                        contains = true;
                        break;
                    }
                }

                if (contains == false)
                {
                    Console.WriteLine("Please include at least one valid file in the specified folder");
                    continue;
                }

                Console.Clear();
                PersonDir pdir = new PersonDir();
                //processing each input file while updating the user
                foreach (string file in files)
                {
                    if (File.Exists(file))
                    {
                        int proc_code = pdir.FetchData(file, true);

                        if (proc_code == 2)
                        {
                            Console.WriteLine("All records in file " + file + " were included!");
                        }
                        else if (proc_code == 1)
                        {
                            Console.WriteLine("Some records in file " + file + " were included!");
                        }
                        else
                        {
                            Console.WriteLine("No records in file " + file + " were included!");
                        }
                    }
                }

                Console.WriteLine("------------------------------------------------------------------");
                Console.WriteLine();
                
                //sort orders
                List<string> sorders = new List<string>
                {
                    "gendername",
                    "birthdate",
                    "lname"
                };

                //sorting processed files in different sort orders and displaying on the console
                foreach (string sorder in sorders)
                {
                    List<PersonInfo> pinfos = pdir.SortData(sorder);
                    Console.WriteLine("Output ordered by " + sorder);
                    Console.WriteLine();
                    Console.WriteLine("{0,-16} {1,-16} {2,-8} {3,-10} {4,-10} ", "LastName", "FirstName", "Gender", "FaveColor", "Birth");
                    foreach (PersonInfo pinfo in pinfos)
                    {
                        Console.WriteLine("{0,-16} {1,-16} {2,-8} {3,-10} {4,-10} ", pinfo.Lname, pinfo.Fname, pinfo.Gendr, pinfo.Color, pinfo.Dob.ToString("M/d/yyyy"));
                    }
                    Console.WriteLine("---------------------------------------------------------------");
                    Console.WriteLine();
                }

                Console.WriteLine("Finished processing, enter 'exit' to close the program");
                cntr++;

            }
        }
    }
}
