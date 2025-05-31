using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp22
{
    internal class Task
    {
        public string Taskname { get; set; }
        public int TaskId { get; set; }
        public string Discription { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public Task(string taskname, int taskId, string discription, DateTime date, string status)
        {
            Taskname = taskname;
            TaskId = taskId;
            Discription = discription;
            Date = date;
            Status = status;
        }
        public override string ToString()
        {
            return $"Task ID: {TaskId}, Name: {Taskname}, Description: {Discription}, Date: {Date.ToShortDateString()}, Status: {Status}";
        }
    }
}
