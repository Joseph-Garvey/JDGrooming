using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using JDGrooming.Classes.Database_Management;
using JDGrooming.Classes;
using JDGrooming;
using System.Collections.ObjectModel;
using System.Collections.Generic;

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
            db.Connect();
            if (db.Rdr is null) { }
            else
            {
                db.Rdr.Close();
            }
            DateTime t = DateTime.Today;
            ObservableCollection<Staff> staff = new ObservableCollection<Staff>(query.GetShifts());
            ObservableCollection<Schedule> Schedules = query.GetSchedules(t, staff);
            int blockstart = 5;
            int blockcount = 3;
            int itemindex = 0;
            for (int i = blockstart; i < blockcount + blockstart - 1; i++) // fix change on click  /// either get item or get grid
            {
                if (Schedules[itemindex].time[i] == false) return; // databind this
            }
            for (int i = blockstart; i < blockcount + blockstart - 1; i++)
            {
                //((Schedule)(data_Availability.SelectedCells[0].Item)).time[i] = false;
                Schedules[itemindex].time[i] = false;
            }
            Schedules[0].time[2] = false;
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
