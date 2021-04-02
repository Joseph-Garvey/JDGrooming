using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDGrooming.Classes
{
    public class Absence : INotifyPropertyChanged
    {
        public Absence(int staffID, string name, DateTime startTime, DateTime endTime, string description)
        {
            StaffID = staffID;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            StartTime = startTime;
            EndTime = endTime;
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        private int staffID { get; set; }
        public int StaffID
        {
            get { return staffID; }
            set
            {
                if (staffID == value) return;
                staffID = value;
                this.NotifyPropertyChanged("StaffID");
            }
        }
        private string name;
        public String Name
        {
            get { return name; }
            set
            {
                if (name == value) return;
                name = value;
                this.NotifyPropertyChanged("Name");
            }
        }
        private DateTime startime;
        public DateTime StartTime
        {
            get { return startime; }
            set
            {
                if (startime == value) return;
                startime = value;
                this.NotifyPropertyChanged("StartTime");
            }
        }
        public TimeSpan StartTime_TimeofDay
        {
            get {  return StartTime.TimeOfDay; }
            set
            {
                DateTime newvalue = new DateTime(StartTime.Year, StartTime.Month, StartTime.Day, value.Hours, value.Minutes, value.Seconds);
                if (StartTime == newvalue) return;
                StartTime = newvalue;
                this.NotifyPropertyChanged("StartTime_TimeofDay");
            }
        }
        private DateTime endtime;
        public DateTime EndTime
        {
            get { return endtime; }
            set
            {
                if (endtime == value) return;
                endtime = value;
                this.NotifyPropertyChanged("EndTime");
            }
        }
        public TimeSpan EndTime_TimeofDay
        {
            get { return EndTime.TimeOfDay; }
            set
            {
                DateTime newvalue = new DateTime(EndTime.Year, EndTime.Month, EndTime.Day, value.Hours, value.Minutes, value.Seconds);
                if (EndTime == newvalue) return;
                EndTime = newvalue;
                this.NotifyPropertyChanged("EndTime_TimeofDay");
            }
        }
        private string description;
        public String Description
        {
            get { return description; }
            set
            {
                if (description == value) return;
                description = value;
                this.NotifyPropertyChanged("Description");
            }
        }

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
