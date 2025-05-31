using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp22
{
    internal class Taskmanager
    {
        private TaskNode toDoFirst;
        private TaskNode inProgressTop;
        private TaskNode completedFirst;
        private TaskNode completedLast;

        public Taskmanager()
        {
            toDoFirst = null;
            inProgressTop = null;
            completedFirst = null;
            completedLast = null;
        }

        public void AddToDoTask(Task task)
        {
            TaskNode newNode = new TaskNode(task);

            if (toDoFirst == null || task.Date < toDoFirst.Task.Date)
            {
                newNode.Next = toDoFirst;
                toDoFirst = newNode;
                return;
            }

            TaskNode current = toDoFirst;
            while (current.Next != null && current.Next.Task.Date <= task.Date)
            {
                current = current.Next;
            }

            newNode.Next = current.Next;
            current.Next = newNode;
        }

        public bool StartTask(int taskId)
        {
            Task task = RemoveFromToDoList(taskId);
            if (task == null)
            {
                Console.WriteLine($"No task found with ID: {taskId}");
                return false;
            }

            task.Status = "In Progress";
            TaskNode newNode = new TaskNode(task)
            {
                Next = inProgressTop
            };
            inProgressTop = newNode;

            Console.WriteLine($"Task '{task.Taskname}' is now in progress.");
            return true;
        }

        public bool FinishTask()
        {
            if (inProgressTop == null)
            {
                Console.WriteLine("No tasks are currently in progress.");
                return false;
            }

            Task task = inProgressTop.Task;
            inProgressTop = inProgressTop.Next;

            task.Status = "Completed";
            TaskNode newNode = new TaskNode(task);

            if (completedFirst == null)
            {
                completedFirst = newNode;
                completedLast = newNode;
            }
            else
            {
                completedLast.Next = newNode;
                completedLast = newNode;
            }

            Console.WriteLine($"Task '{task.Taskname}' marked as completed.");
            return true;
        }

        private Task RemoveFromToDoList(int taskId)
        {
            if (toDoFirst == null) return null;

            if (toDoFirst.Task.TaskId == taskId)
            {
                Task task = toDoFirst.Task;
                toDoFirst = toDoFirst.Next;
                return task;
            }

            TaskNode current = toDoFirst;
            while (current.Next != null && current.Next.Task.TaskId != taskId)
            {
                current = current.Next;
            }

            if (current.Next != null)
            {
                Task task = current.Next.Task;
                current.Next = current.Next.Next;
                return task;
            }

            return null;
        }

        public void ShowToDoTasks()
        {
            Console.WriteLine("\n--- TO-DO LIST ---");
            PrintTasks(toDoFirst);
        }

        public void ShowInProgressTasks()
        {
            Console.WriteLine("\n--- TASKS IN PROGRESS (Stack) ---");
            PrintTasks(inProgressTop);
        }

        public void ShowCompletedTasks()
        {
            Console.WriteLine("\n--- COMPLETED TASKS (Queue) ---");
            PrintTasks(completedFirst);
        }

        private void PrintTasks(TaskNode head)
        {
            if (head == null)
            {
                Console.WriteLine("No tasks available.");
                return;
            }

            TaskNode current = head;
            while (current != null)
            {
                Console.WriteLine(current.Task.ToString());
                current = current.Next;
            }
        }

        public bool AnyToDoTasks() => toDoFirst != null;
        public bool AnyInProgressTasks() => inProgressTop != null;
    }
}
