using System;
using ConsoleApp22;

namespace ConsoleApp22
{
    class Program
    {
        private static Taskmanager manager = new Taskmanager();
        private static int taskIdCounter = 1;

        static void Main()
        {
            Console.WriteLine("=== WELCOME TO TASK MANAGER ===");

            while (true)
            {
                ShowMenu();
                Console.Write("\nChoose an option: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateTask();
                        break;
                    case "2":
                        MoveToInProgress();
                        break;
                    case "3":
                        MarkAsCompleted();
                        break;
                    case "4":
                        manager.ShowToDoTasks();
                        break;
                    case "5":
                        manager.ShowInProgressTasks();
                        break;
                    case "6":
                        manager.ShowCompletedTasks();
                        break;
                    case "7":
                        ShowAll();
                        break;
                    case "8":
                        Console.WriteLine("Exiting... Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                PauseAndClear();
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\n======== MENU ========");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Start Task");
            Console.WriteLine("3. Complete Task");
            Console.WriteLine("4. View To-Do List");
            Console.WriteLine("5. View In-Progress List");
            Console.WriteLine("6. View Completed Tasks");
            Console.WriteLine("7. View All Tasks");
            Console.WriteLine("8. Exit");
            Console.WriteLine("======================");
        }

        static void CreateTask()
        {
            Console.WriteLine("\n--- CREATE NEW TASK ---");

            Console.Write("Task Name: ");
            string name = Console.ReadLine();

            Console.Write("Task Description: ");
            string desc = Console.ReadLine();

            DateTime dueDate;
            while (true)
            {
                Console.Write("Due Date (YYYY-MM-DD): ");
                string dateInput = Console.ReadLine();
                if (DateTime.TryParse(dateInput, out dueDate)) break;
                Console.WriteLine("Invalid date format. Try again.");
            }
            
            Task task = new Task(taskIdCounter++, taskName, desc, dueDate, "To-Do");

             
            manager.AddToDoTask(task);

            Console.WriteLine($"Task '{name}' added with ID #{task.TaskId}");
        }

        static void MoveToInProgress()
        {
            Console.WriteLine("\n--- START A TASK ---");
            manager.ShowToDoTasks();

            if (!manager.AnyToDoTasks())
            {
                Console.WriteLine("No tasks available to start.");
                return;
            }

            Console.Write("\nEnter Task ID to start: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (!manager.StartTask(id))
                    Console.WriteLine("Couldn't find task with that ID.");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }

        static void MarkAsCompleted()
        {
            Console.WriteLine("\n--- COMPLETE TASK ---");
            manager.ShowInProgressTasks();

            if (!manager.AnyInProgressTasks())
            {
                Console.WriteLine("No active tasks to complete.");
                return;
            }

            Console.Write("Confirm complete latest task? (y/n): ");
            string confirm = Console.ReadLine().Trim().ToLower();

            if (confirm == "y" || confirm == "yes")
            {
                if (!manager.FinishTask())
                    Console.WriteLine("Error completing task.");
            }
            else
            {
                Console.WriteLine("Canceled.");
            }
        }

        static void ShowAll()
        {
            Console.WriteLine("\n--- ALL TASKS ---");
            manager.ShowToDoTasks();
            manager.ShowInProgressTasks();
            manager.ShowCompletedTasks();
        }

        static void PauseAndClear()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}

