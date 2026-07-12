using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradingSystem;
    internal class StudentHandler
    {
        UI ui = new UI();

        public void Run(Class SelectedClass) 
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.MarkupLine($"Welcome [yellow]Martin[/] zum {SelectedClass.Name} portal");
                AnsiConsole.WriteLine();

                ListStudents(SelectedClass);
                AnsiConsole.WriteLine();
                bool GoBack = SelectOptions(SelectedClass);

                if (GoBack) 
                {
                    return;
                }
            }
        }

        private void ListStudents(Class SelectedClass)
        {
            ui.ListStudents(SelectedClass.Students);
        }

        private bool SelectOptions(Class SelectedClass)
        {
                string option = ui.SelectOptions();

                if (option == "a")
                {
                    //Add
                    AddStudent(SelectedClass);
                }
                else if (option == "d")
                {
                    //Delete
                    DeleteStudent(SelectedClass);
                }
                else if (option == "s")
                {
                    //Select
                    SelectStudent(SelectedClass);
                }
                else if (option == "b")
                {
                    //Go Back
                    return true;
                }
                return false;
            }
        private void AddStudent(Class SelectedClass)
        {
            Student student = new Student();
            InputHandler ihandler = new InputHandler();
            ihandler.ConfigureStudent(student);
            SelectedClass.Students.Add(student);
        }
        private void SelectStudent(Class SelectedClass)
        {
            //Select
            if (SelectedClass.Students.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No classes to select.[/]");
                AnsiConsole.Ask<string>("Press Enter to continue");
                return;
            }

            Student selectedStudent = AnsiConsole.Prompt(
                new SelectionPrompt<Student>()
                    .Title("Which Student do you want to select?")
                    .UseConverter(k => k.Name)
                    .AddChoices(SelectedClass.Students.OrderBy(k => k.Name)));
            SubjectHandler shandler = new SubjectHandler();
            shandler.Run(selectedStudent);
        }
        private void DeleteStudent(Class SelectedClass)
        {
            //Delete
            if (SelectedClass.Students.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]Nothing to delete.[/]");
                AnsiConsole.Ask<string>("Press Enter to continue");
                return;
            }

            int classnr = AnsiConsole.Ask<int>("Which student do you want to delete?");
            if (classnr >= 1 && classnr <= SelectedClass.Students.Count)
            {
                SelectedClass.Students.RemoveAt(classnr - 1);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Invalid number.[/]");
                AnsiConsole.Ask<string>("Press Enter to continue");
            }
        }
}
