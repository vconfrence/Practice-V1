using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClassLibrary
{
    public class PersonDir
    {
        private List<PersonInfo> plist;

        public List<PersonInfo> Plist { get => plist; set => plist = value; }

        //constructor
        public PersonDir()
        {
            Plist = new List<PersonInfo>();
        }

        /*
         * FetchData method retrieves a file in comma, pipe or space delimited format or a single row
         * of information, then it calls a private method to validate the data and inserts the data 
         * into the personinfo collection if passes validation.
         * Return value of 2 indicates all the records were inserted into the collection, 1 indicates
         * some records were inserted and 0 means no record was inserted which depends on the validation module.
         * 
         */ 
        public int FetchData(string source, bool isfile)
        {
            char delimtr = '0';
            int proc_cnt = 0;
            int insrt_cnt = 0;
            //input is a file
            if (isfile)
            {
                string fname = "";
                if (File.Exists(source))
                {
                    fname = source.Substring(source.LastIndexOf("\\") + 1);
                }
                //determining input delimeter based on the file name
                switch (fname)
                {
                    case "cfile.csv":
                        delimtr = ',';
                        break;
                    case "pfile.csv":
                        delimtr = '|';
                        break;
                    case "sfile.csv":
                        delimtr = ' ';
                        break;
                }

                if (delimtr != '0')
                {
                    string line;
                    StreamReader sr = new StreamReader(source);
                    while ((line = sr.ReadLine()) != null && line != "")
                    {
                        string[] row = line.Split(delimtr);
                        //checking data validity
                        if(ValidateData(row))
                        {
                            AddPerson(row);
                            insrt_cnt++;
                        }
                        proc_cnt++;
                    }
                    sr.Close();
                }
            }
            else //input is one row of information as a string
            {
                int cntr = 0; 
                if (cntr == 0 && (cntr = source.Count(x => x == ',')) == 4)
                {
                    delimtr = ',';
                }
                else if (cntr == 0 && (cntr = source.Count(x => x == '|')) == 4)
                {
                    delimtr = '|';
                }
                else if (cntr == 0 && (cntr = source.Count(x => x == ' ')) == 4)
                {
                    delimtr = ' ';
                }

                if (delimtr != '0')
                {
                    string[] row = source.Split(delimtr);
                    //checking data validity
                    if (ValidateData(row))
                    {
                        AddPerson(row);
                        insrt_cnt++;
                    }
                    proc_cnt++;
                }
            }

            if (insrt_cnt == 0)
                // no records inserted
                return 0;
            else if (proc_cnt == insrt_cnt)
                // all records inserted
                return 2;
            else
                // some records inserted
                return 1;
        }

        /*
         * sorting the data based on various sort options
         */ 
        public List<PersonInfo> SortData(string sortby)
        {
            List<PersonInfo> srtd = new List<PersonInfo>();
            switch (sortby)
            {
                case "gendername":
                    srtd = Plist.OrderBy(x => x.Gendr).ThenBy(x => x.Lname).ToList();
                    break;
                case "name":
                    srtd = Plist.OrderBy(x => x.Lname).ThenBy(x => x.Fname).ToList();
                    break;
                case "birthdate":
                    srtd = Plist.OrderBy(x => x.Dob).ToList();
                    break;
                case "lname":
                    srtd = Plist.OrderByDescending(x => x.Lname).ToList();
                    break;
                case "gender":
                    srtd = Plist.OrderBy(x => x.Gendr).ToList();
                    break;
            }
            return srtd;
        }

        /*
         * validating data, names can't contain numbers, dates have to be valid
         * gender have to be a coplete male or female word, not case sensitive
         */ 
        private bool ValidateData(string[] row)
        {
            if (row.Length != 5)
                return false;
            for (int cntr = 0; cntr < 2; cntr++)
            {
                if (row[cntr].Any(char.IsDigit))
                {
                    return false;
                }
            }
            if (string.Equals(row[2].Trim(), "male", StringComparison.OrdinalIgnoreCase) == false &&
                string.Equals(row[2].Trim(), "female", StringComparison.OrdinalIgnoreCase) == false)
            {
                return false;
            }
            if (DateTime.TryParse(row[4].Trim(), out DateTime dt) == false)
            {
                return false;
            }
            return true;
        }

        /*
         * adding the person to the collection
         */ 
        private void AddPerson(string[] row)
        {
            PersonInfo pinfo = new PersonInfo
            {
                Lname = row[0].Trim(),
                Fname = row[1].Trim(),
                Gendr = row[2].Trim().ToLower(),
                Color = row[3].Trim(),
                Dob = DateTime.Parse(row[4].Trim())
            };
            Plist.Add(pinfo);
        }
    }
}
