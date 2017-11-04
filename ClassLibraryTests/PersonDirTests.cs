using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClassLibrary.Tests
{
    [TestClass()]
    public class PersonDirTests
    {
        //constructor validation
        [TestMethod()]
        public void PersonDirTest()
        {
            //act 
            PersonDir prsn = new PersonDir();
            Assert.IsNotNull(prsn.Plist, "Error");
        }

        //adding one row of information validation
        [TestMethod()]
        public void FetchDataTest()
        {
            //Arrange
            string data = "smith, edward, male, yellow, 5/20/2000";
            //Act
            PersonDir person = new PersonDir();
            //Assert
            Assert.AreEqual(person.FetchData(data, false), 2);
        }

        //adding one row of information with invalid date
        [TestMethod()]
        public void FetchDataTest2()
        {
            //Arrange
            string data = "smith,edward,male,yellow,5/20/";
            //Act
            PersonDir person = new PersonDir();
            //Assert
            Assert.AreEqual(person.FetchData(data, false), 0);
        }

        //adding one row of information with more than one specific delimeter, invalid
        [TestMethod()]
        public void FetchDataTest3()
        {
            //Arrange
            string data = "smith| edward male, yellow, 5/20/2000";
            //Act
            PersonDir person = new PersonDir();
            //Assert
            Assert.AreEqual(person.FetchData(data, false), 0);
        }

        //adding one row of inormation with digits in the name, invalid
        [TestMethod()]
        public void FetchDataTest4()
        {
            //Arrange
            string data = "smith, edward1, male, yellow, 5/20/2000";
            //Act
            PersonDir person = new PersonDir();
            //Assert
            Assert.AreEqual(person.FetchData(data, false), 0);
        }

        //adding one row of information with gender upper and lower case, valid
        [TestMethod()]
        public void FetchDataTest5()
        {
            //Arrange
            string data = "smith, edward, Male, yellow, 5/20/2000";
            //Act
            PersonDir person = new PersonDir();
            //Assert
            Assert.AreEqual(person.FetchData(data, false), 2);
        }

        //adding records from a comma seperated file with all valid rows
        [TestMethod()]
        public void FetchDataTest6()
        {
            //Arrange
            string data = "cfile.csv";
            //Act
            PersonDir person = new PersonDir();
            //Assert
            Assert.AreEqual(person.FetchData(data, true), 2);
        }

        //adding records from a pipe seperated file with all valid rows
        [TestMethod()]
        public void FetchDataTest7()
        {
            //Arrange
            string data = "pfile.csv";
            //Act
            PersonDir person = new PersonDir();
            //Assert
            Assert.AreEqual(person.FetchData(data, true), 2);
        }

        //adding records from a space seperated file with all valid rows
        [TestMethod()]
        public void FetchDataTest8()
        {
            //Arrange
            string data = "sfile.csv";
            //Act
            PersonDir person = new PersonDir();
            //Assert
            Assert.AreEqual(person.FetchData(data, true), 2);
        }

        //adding records from a comma seperated file with all invalid records
        [TestMethod()]
        public void FetchDataTest9()
        {
            //Arrange
            string data = "fldr\\cfile.csv";
            //Act
            PersonDir person = new PersonDir();
            //Assert
            Assert.AreEqual(person.FetchData(data, true), 0);
        }

        //adding records from a pipe seperated file with some invalid records
        [TestMethod()]
        public void FetchDataTest10()
        {
            //Arrange
            string data = "fldr\\pfile.csv";
            //Act
            PersonDir person = new PersonDir();
            //Assert
            Assert.AreEqual(person.FetchData(data, true), 1);
        }

        //sort functionality validation based on gender then lastname
        [TestMethod()]
        public void SortDataTest()
        {
            //Arrange
            List<PersonInfo> pinfos = new List<PersonInfo>();
            PersonInfo pinfo = new PersonInfo
            {
                Lname = "smith",
                Fname = "edward",
                Gendr = "male",
                Color = "yellow",
                Dob = DateTime.Parse("5/20/2000")
            };
            pinfos.Add(pinfo);

            pinfo = new PersonInfo
            {
                Lname = "thomas",
                Fname = "edward",
                Gendr = "male",
                Color = "purple",
                Dob = DateTime.Parse("2/16/2001")
            };
            pinfos.Add(pinfo);

            pinfo = new PersonInfo
            {
                Lname = "fox",
                Fname = "kelly",
                Gendr = "female",
                Color = "green",
                Dob = DateTime.Parse("1/18/1990")
            };
            pinfos.Add(pinfo);

            pinfo = new PersonInfo
            {
                Lname = "fox",
                Fname = "sandy",
                Gendr = "female",
                Color = "brown",
                Dob = DateTime.Parse("8/05/1992")
            };
            pinfos.Add(pinfo);

            pinfo = new PersonInfo
            {
                Lname = "laflin",
                Fname = "stephen",
                Gendr = "male",
                Color = "burgandy",
                Dob = DateTime.Parse("09/02/2000")
            };
            pinfos.Add(pinfo);

            PersonDir pdir = new PersonDir
            {
                Plist = pinfos
            };
            //Act
            List<PersonInfo> srtd_info = pdir.SortData("gendername");

            List<string> name = new List<string>
            {
                "foxkelly",
                "foxsandy",
                "laflinstephen",
                "smithedward",
                "thomasedward"
            };

            bool match = true;
            for (int cntr = 0; cntr < name.Count; cntr++)
            {
                if (name[cntr] != srtd_info[cntr].Lname + srtd_info[cntr].Fname)
                {
                    match = false;
                    break;
                }
            }


            //Assert
            Assert.AreEqual(match, true);
        }

        //sort functionality validation based on lastname, firstname
        [TestMethod()]
        public void SortDataTest2()
        {
            //Arrange
            List<PersonInfo> pinfos = new List<PersonInfo>();
            PersonInfo pinfo = new PersonInfo
            {
                Lname = "smith",
                Fname = "edward",
                Gendr = "male",
                Color = "yellow",
                Dob = DateTime.Parse("5/20/2000")
            };
            pinfos.Add(pinfo);

            pinfo = new PersonInfo
            {
                Lname = "thomas",
                Fname = "edward",
                Gendr = "male",
                Color = "purple",
                Dob = DateTime.Parse("2/16/2001")
            };
            pinfos.Add(pinfo);

            pinfo = new PersonInfo
            {
                Lname = "fox",
                Fname = "sue",
                Gendr = "female",
                Color = "green",
                Dob = DateTime.Parse("1/18/1990")
            };
            pinfos.Add(pinfo);

            pinfo = new PersonInfo
            {
                Lname = "fox",
                Fname = "kelly",
                Gendr = "female",
                Color = "brown",
                Dob = DateTime.Parse("8/05/1992")
            };
            pinfos.Add(pinfo);

            pinfo = new PersonInfo
            {
                Lname = "laflin",
                Fname = "stephen",
                Gendr = "male",
                Color = "burgandy",
                Dob = DateTime.Parse("09/02/2000")
            };
            pinfos.Add(pinfo);

            PersonDir pdir = new PersonDir
            {
                Plist = pinfos
            };
            //Act
            List<PersonInfo> srtd_info = pdir.SortData("name");

            List<string> name = new List<string>
            {
                "foxkelly",
                "foxsue",
                "laflinstephen",
                "smithedward",
                "thomasedward"
            };

            bool match = true;
            for (int cntr = 0; cntr < name.Count; cntr++)
            {
                if (name[cntr] != srtd_info[cntr].Lname + srtd_info[cntr].Fname)
                {
                    match = false;
                    break;
                }
            }


            //Assert
            Assert.AreEqual(match, true);
        }

        //sort functionality validation based on birthdate
        [TestMethod()]
        public void SortDataTest3()
        {
            //Arrange
            List<PersonInfo> pinfos = new List<PersonInfo>();
            PersonInfo pinfo = new PersonInfo
            {
                Lname = "smith",
                Fname = "edward",
                Gendr = "male",
                Color = "yellow",
                Dob = DateTime.Parse("5/20/2000")
            };
            pinfos.Add(pinfo);

            pinfo = new PersonInfo
            {
                Lname = "thomas",
                Fname = "edward",
                Gendr = "male",
                Color = "purple",
                Dob = DateTime.Parse("2/16/2001")
            };
            pinfos.Add(pinfo);

            pinfo = new PersonInfo
            {
                Lname = "fox",
                Fname = "kelly",
                Gendr = "female",
                Color = "green",
                Dob = DateTime.Parse("1/18/1990")
            };
            pinfos.Add(pinfo);

            pinfo = new PersonInfo
            {
                Lname = "fox",
                Fname = "sandy",
                Gendr = "female",
                Color = "brown",
                Dob = DateTime.Parse("8/05/1992")
            };
            pinfos.Add(pinfo);

            pinfo = new PersonInfo
            {
                Lname = "laflin",
                Fname = "stephen",
                Gendr = "male",
                Color = "burgandy",
                Dob = DateTime.Parse("09/02/2000")
            };
            pinfos.Add(pinfo);

            PersonDir pdir = new PersonDir
            {
                Plist = pinfos
            };
            //Act
            List<PersonInfo> srtd_info = pdir.SortData("birthdate");

            List<string> name = new List<string>
            {
                "foxkelly",
                "foxsandy",
                "smithedward",
                "laflinstephen",
                "thomasedward"
            };

            bool match = true;
            for (int cntr = 0; cntr < name.Count; cntr++)
            {
                if (name[cntr] != srtd_info[cntr].Lname + srtd_info[cntr].Fname)
                {
                    match = false;
                    break;
                }
            }


            //Assert
            Assert.AreEqual(match, true);
        }

        //sort functionality validation based on lastname descnding
        [TestMethod()]
        public void SortDataTest4()
        {
            //Arrange
            List<PersonInfo> pinfos = new List<PersonInfo>();
            PersonInfo pinfo = new PersonInfo
            {
                Lname = "smith",
                Fname = "edward",
                Gendr = "male",
                Color = "yellow",
                Dob = DateTime.Parse("5/20/2000")
            };
            pinfos.Add(pinfo);

            pinfo = new PersonInfo
            {
                Lname = "thomas",
                Fname = "edward",
                Gendr = "male",
                Color = "purple",
                Dob = DateTime.Parse("2/16/2001")
            };
            pinfos.Add(pinfo);

            pinfo = new PersonInfo
            {
                Lname = "fox",
                Fname = "kelly",
                Gendr = "female",
                Color = "green",
                Dob = DateTime.Parse("1/18/1990")
            };
            pinfos.Add(pinfo);

            pinfo = new PersonInfo
            {
                Lname = "fox",
                Fname = "sandy",
                Gendr = "female",
                Color = "brown",
                Dob = DateTime.Parse("8/05/1992")
            };
            pinfos.Add(pinfo);

            pinfo = new PersonInfo
            {
                Lname = "laflin",
                Fname = "stephen",
                Gendr = "male",
                Color = "burgandy",
                Dob = DateTime.Parse("09/02/2000")
            };
            pinfos.Add(pinfo);

            PersonDir pdir = new PersonDir
            {
                Plist = pinfos
            };
            //Act
            List<PersonInfo> srtd_info = pdir.SortData("lname");

            List<string> name = new List<string>
            {
                "thomasedward",
                "smithedward",
                "laflinstephen",
                "foxkelly",
                "foxsandy"
            };

            bool match = true;
            for (int cntr = 0; cntr < name.Count; cntr++)
            {
                if (name[cntr] != srtd_info[cntr].Lname + srtd_info[cntr].Fname)
                {
                    match = false;
                    break;
                }
            }


            //Assert
            Assert.AreEqual(match, true);
        }

        //sort functionality validation based on gender
        [TestMethod()]
        public void SortDataTest5()
        {
            //Arrange
            List<PersonInfo> pinfos = new List<PersonInfo>();
            PersonInfo pinfo = new PersonInfo
            {
                Lname = "smith",
                Fname = "edward",
                Gendr = "male",
                Color = "yellow",
                Dob = DateTime.Parse("5/20/2000")
            };
            pinfos.Add(pinfo);

            pinfo = new PersonInfo
            {
                Lname = "thomas",
                Fname = "edward",
                Gendr = "male",
                Color = "purple",
                Dob = DateTime.Parse("2/16/2001")
            };
            pinfos.Add(pinfo);

            pinfo = new PersonInfo
            {
                Lname = "fox",
                Fname = "kelly",
                Gendr = "female",
                Color = "green",
                Dob = DateTime.Parse("1/18/1990")
            };
            pinfos.Add(pinfo);

            pinfo = new PersonInfo
            {
                Lname = "fox",
                Fname = "sandy",
                Gendr = "female",
                Color = "brown",
                Dob = DateTime.Parse("8/05/1992")
            };
            pinfos.Add(pinfo);

            pinfo = new PersonInfo
            {
                Lname = "laflin",
                Fname = "stephen",
                Gendr = "male",
                Color = "burgandy",
                Dob = DateTime.Parse("09/02/2000")
            };
            pinfos.Add(pinfo);

            PersonDir pdir = new PersonDir
            {
                Plist = pinfos
            };
            //Act
            List<PersonInfo> srtd_info = pdir.SortData("gender");

            List<string> name = new List<string>
            {
                "foxkelly",
                "foxsandy",
                "smithedward",
                "thomasedward",
                "laflinstephen"
            };

            bool match = true;
            for (int cntr = 0; cntr < name.Count; cntr++)
            {
                if (name[cntr] != srtd_info[cntr].Lname + srtd_info[cntr].Fname)
                {
                    match = false;
                    break;
                }
            }


            //Assert
            Assert.AreEqual(match, true);
        }
    }
}