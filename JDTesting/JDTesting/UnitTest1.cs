using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using JDGrooming.Classes.Database_Management;
using JDGrooming.Classes;
using JDGrooming;

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
            DateTime t = DateTime.Today;
            String s = $"{t:yyyy-MM+1-01}";
        }
        //[TestMethod]
        //public void InsertShifts()
        //{
        //    bool worked = false;
        //    if (db.Connect())
        //    {
        //        query.QueryDatabase("DELETE FROM [Shift]");
        //        foreach(Staff s in query.GetStaff())
        //        {
        //            DateTime startime = DateTime.Today.AddHours(9);
        //            DateTime endtime = startime.AddHours(8);
        //            if(s.Name == "Jane")
        //            {
        //                endtime.AddHours(-4);
        //            }
        //            for(int i=1; i<=5; i++)
        //            {
        //                String str = $"INSERT INTO [Shift] ([StaffID], [Day], [StartTime], [EndTime]) VALUES ({s.ID}, {i}, '{startime.TimeOfDay}', '{endtime.TimeOfDay}');";
        //                query.QueryDatabase(str);
        //            }

        //        }
        //        //write code to generate shifts
        //        worked = true;
        //    }
        //    Assert.IsTrue(worked);
        //}
    }
}
