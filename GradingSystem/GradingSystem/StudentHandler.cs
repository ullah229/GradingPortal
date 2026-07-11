using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradingSystem;
    internal class StudentHandler
    {

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
            if (SelectedClass.Students.Count > 0)
            {
                AnsiConsole.MarkupLine("[bold]List of students:[/]");
                foreach (Student student in SelectedClass.Students.OrderBy(k => k.Name))
                {
                    AnsiConsole.MarkupLine($"- [cyan]{student.Name}[/]");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]No students yet.[/]");
            }
        }

        private bool SelectOptions(Class SelectedClass)
        {
                string option = AnsiConsole.Ask<string>(
        "Press [green]a[/] to add a student, [green]d[/] to delete a student, [green]s[/] to select a student, [red]b[/] to go back:");

                if (option == "a")
                {
                    //Add
                    Student student = new Student();
                    InputHandler ihandler = new InputHandler();
                    ihandler.ConfigureStudent(student);
                    SelectedClass.Students.Add(student);
                }
                else if (option == "d")
                {
                    //Delete
                    if (SelectedClass.Students.Count == 0)
                    {
                        AnsiConsole.MarkupLine("[red]Nothing to delete.[/]");
                        AnsiConsole.Ask<string>("Press Enter to continue");
                        return false;
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
                else if (option == "s")
                {
                    //Select
                    if (SelectedClass.Students.Count == 0)
                    {
                        AnsiConsole.MarkupLine("[red]No classes to select.[/]");
                        AnsiConsole.Ask<string>("Press Enter to continue");
                        return false;
                    }

                    Student selectedStudent = AnsiConsole.Prompt(
                        new SelectionPrompt<Student>()
                            .Title("Which Student do you want to select?")
                            .UseConverter(k => k.Name)
                            .AddChoices(SelectedClass.Students.OrderBy(k => k.Name)));
                    SubjectHandler shandler = new SubjectHandler();
                    shandler.Run(selectedStudent);
                }
                else if (option == "b")
                {
                    //Go Back
                    return true;
                }
                return false;
            }
    }
