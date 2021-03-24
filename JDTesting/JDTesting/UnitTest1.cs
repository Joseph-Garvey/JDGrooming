using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using JDGrooming.Classes.Database_Management;

namespace JDTesting
{
    [TestClass]
    public class UnitTest1
    {
        public ProjectDBAccess query = new ProjectDBAccess();
        public Database db
        {
            get { return query.db; }
            set { query.db = value; }
        }
        [TestMethod]
        public void TestMethod()
        {
        }
        [TestMethod]
        public void InsertShifts()
        {
            bool worked = false;
            if (db.Connect())
            {
                // write code to generate shifts
            }
            Assert.IsTrue(worked);
        }
    }
}
