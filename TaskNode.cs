using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp22
{
    internal class TaskNode
    {
        public Task Task { get; set; }
        public TaskNode Next { get; set; }

        public TaskNode(Task task)
        {
            Task = task;
            Next = null;
        }
    }
}
