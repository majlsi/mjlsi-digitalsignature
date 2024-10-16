using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Services.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestNumberofSignaturePages()
        {
            int signPages = Convert.ToInt32((9 / 8));
            int reminder = 9 % 8;
            if (reminder > 0)
            {
                signPages = signPages + 1;
            }

        }

        [TestMethod]
        public void TestNumberofSignaturePagesLessthan8()
        {
            int signPages = Convert.ToInt32((7 / 8));
            int reminder = 7 % 8;
            if (reminder > 0)
            {
                signPages = signPages + 1;
            }

        }

        [TestMethod]
        public void TestNumberofSignaturePageswith8()
        {
            int signPages = Convert.ToInt32((8 / 8));
            int reminder = 8 % 8;
            if (reminder > 0)
            {
                signPages = signPages + 1;
            }

        }

        [TestMethod]
        public void TestPageNumberlessthan7()
        {
            int originalPages = 3 - 2;
            int pageReminder = 6 / 8;
            if (pageReminder > 0)
            {
                int pageNumber = originalPages + pageReminder + 1;

            }
            else
            {
                int pageNumber = originalPages + 1;
            }

        }

        [TestMethod]
        public void TestPageNumberwith7()
        {
            int originalPages = 3 - 2;
            int pageReminder = 7 / 8;
            if (pageReminder > 0)
            {
                int pageNumber = originalPages + pageReminder + 1;

            }
            else
            {
                int pageNumber = originalPages + 1;
            }

        }

        [TestMethod]
        public void TestPageNumberlargerthan7()
        {
            int originalPages = 3 - 2;
            int pageReminder = 8 / 8;
            if (pageReminder > 0)
            {
                int pageNumber = originalPages + pageReminder + 1;

            }
            else
            {
                int pageNumber = originalPages + 1;
            }

        }
    }
}
