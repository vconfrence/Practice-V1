using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practice;
using Practice.Controllers;

namespace Practice.Tests.Controllers
{
    [TestClass]
    public class RecordsControllerTest
    {
        //validating get method with no arguments defualt sort functionality, lastname, firstname sort
        [TestMethod]
        public void Get()
        {
            // Arrange
            RecordsController controller = new RecordsController();
            RecordsController.pdir.Plist = new List<ClassLibrary.PersonInfo>();
            controller.Post("smith, edward, male, yellow, 5/20/2000");
            controller.Post("thomas, edward, male, purple, 2/16/2001");
            controller.Post("fox, sue, female, green, 1/18/1990");
            controller.Post("fox, kelly, female, brown, 8/05/1992");
            controller.Post("laflin, stephen, male, burgandy, 09/02/2000");

            List<string> expctd = new List<string>
            {
                "fox, kelly, female, brown, 8/5/1992",
                "fox, sue, female, green, 1/18/1990",
                "laflin, stephen, male, burgandy, 9/2/2000",
                "smith, edward, male, yellow, 5/20/2000",
                "thomas, edward, male, purple, 2/16/2001"
            };


            // Act
            List<string> result = controller.Get("name").ToList();

            bool match = true;
            if (expctd.Count != result.Count)
                match = false;

            if (match)
            {
                for (int cntr = 0; cntr < expctd.Count; cntr++)
                {
                    if (expctd[cntr] != result[cntr])
                    {
                        match = false;
                        break;
                    }
                }
            }

            // Assert
            Assert.AreEqual(true, match);
        }

        //validating get method sort based on gender
        [TestMethod]
        public void GetById()
        {
            // Arrange
            RecordsController controller = new RecordsController();
            RecordsController.pdir.Plist = new List<ClassLibrary.PersonInfo>();
            controller.Post("smith, edward, male, yellow, 5/20/2000");
            controller.Post("thomas, edward, male, purple, 2/16/2001");
            controller.Post("fox, sue, female, green, 1/18/1990");
            controller.Post("fox, kelly, female, brown, 8/05/1992");
            controller.Post("laflin, stephen, male, burgandy, 09/02/2000");

            List<string> expctd = new List<string>
            {
                "fox, sue, female, green, 1/18/1990",
                "fox, kelly, female, brown, 8/5/1992",
                "smith, edward, male, yellow, 5/20/2000",
                "thomas, edward, male, purple, 2/16/2001",
                "laflin, stephen, male, burgandy, 9/2/2000"
            };

            // Act
            List<string> result = controller.Get("gender").ToList();

            bool match = true;
            if (expctd.Count != result.Count)
                match = false;

            if (match)
            {
                for (int cntr = 0; cntr < expctd.Count; cntr++)
                {
                    if (expctd[cntr] != result[cntr])
                    {
                        match = false;
                        break;
                    }
                }
            }

            // Assert
            Assert.AreEqual(true, match);
        }

        //validating get method sort based on birthdate
        [TestMethod]
        public void GetById2()
        {
            // Arrange
            RecordsController controller = new RecordsController();
            RecordsController.pdir.Plist = new List<ClassLibrary.PersonInfo>();
            controller.Post("smith, edward, male, yellow, 5/20/2000");
            controller.Post("thomas, edward, male, purple, 2/16/2001");
            controller.Post("fox, sue, female, green, 1/18/1990");
            controller.Post("fox, kelly, female, brown, 8/05/1992");
            controller.Post("laflin, stephen, male, burgandy, 09/02/2000");

            List<string> expctd = new List<string>
            {
                "fox, sue, female, green, 1/18/1990",
                "fox, kelly, female, brown, 8/5/1992",
                "smith, edward, male, yellow, 5/20/2000",
                "laflin, stephen, male, burgandy, 9/2/2000",
                "thomas, edward, male, purple, 2/16/2001"
                
            };

            // Act
            List<string> result = controller.Get("birthdate").ToList();

            bool match = true;
            if (expctd.Count != result.Count)
                match = false;

            if (match)
            {
                for (int cntr = 0; cntr < expctd.Count; cntr++)
                {
                    if (expctd[cntr] != result[cntr])
                    {
                        match = false;
                        break;
                    }
                }
            }

            // Assert
            Assert.AreEqual(true, match);
        }

        //validating get method sort based on lastname, firstname
        [TestMethod]
        public void GetById3()
        {
            // Arrange
            RecordsController controller = new RecordsController();
            RecordsController.pdir.Plist = new List<ClassLibrary.PersonInfo>();
            controller.Post("smith, edward, male, yellow, 5/20/2000");
            controller.Post("thomas, edward, male, purple, 2/16/2001");
            controller.Post("fox, sue, female, green, 1/18/1990");
            controller.Post("fox, kelly, female, brown, 8/05/1992");
            controller.Post("laflin, stephen, male, burgandy, 09/02/2000");

            List<string> expctd = new List<string>
            {
                "fox, kelly, female, brown, 8/5/1992",
                "fox, sue, female, green, 1/18/1990",
                "laflin, stephen, male, burgandy, 9/2/2000",
                "smith, edward, male, yellow, 5/20/2000",
                "thomas, edward, male, purple, 2/16/2001"

            };

            // Act
            List<string> result = controller.Get("name").ToList();

            bool match = true;
            if (expctd.Count != result.Count)
                match = false;

            if (match)
            {
                for (int cntr = 0; cntr < expctd.Count; cntr++)
                {
                    if (expctd[cntr] != result[cntr])
                    {
                        match = false;
                        break;
                    }
                }
            }

            // Assert
            Assert.AreEqual(true, match);
        }

        //validating post method functionality based on comma delimited data
        [TestMethod]
        public void Post()
        {
            // Arrange
            RecordsController controller = new RecordsController();
            RecordsController.pdir.Plist = new List<ClassLibrary.PersonInfo>();

            // Act
            string value = "fox, kelly, female, brown, 8/5/1992";
            controller.Post(value);
            List<string> result = controller.Get("gender").ToList();

            bool match = true;
            if (result.Count != 1 || result[0] != value)
            {
                match = false;
            }

            // Assert
            Assert.AreEqual(true, match);
        }

        //validating post method based on pipe delimited data
        [TestMethod]
        public void Post2()
        {
            // Arrange
            RecordsController controller = new RecordsController();
            RecordsController.pdir.Plist = new List<ClassLibrary.PersonInfo>();

            // Act
            string value = "fox| kelly| female| brown| 8/5/1992";
            string expctd = "fox, kelly, female, brown, 8/5/1992";
            controller.Post(value);
            List<string> result = controller.Get("gender").ToList();

            bool match = true;
            if (result.Count != 1 || result[0] != expctd)
            {
                match = false;
            }

            // Assert
            Assert.AreEqual(true, match);
        }

        //validating post method based on space delimited data
        [TestMethod]
        public void Post3()
        {
            // Arrange
            RecordsController controller = new RecordsController();
            RecordsController.pdir.Plist = new List<ClassLibrary.PersonInfo>();

            // Act
            string value = "fox kelly female brown 8/5/1992";
            string expctd = "fox, kelly, female, brown, 8/5/1992";
            controller.Post(value);
            List<string> result = controller.Get("gender").ToList();

            bool match = true;
            if (result.Count != 1 || result[0] != expctd)
            {
                match = false;
            }

            // Assert
            Assert.AreEqual(true, match);
        }

        /*
        [TestMethod]
        public void Put()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            controller.Put(5, "value");

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            controller.Delete(5);

            // Assert
        }
        */
    }
}
