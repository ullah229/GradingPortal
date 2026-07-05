using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradingSystem
{
    internal class StudentHandler
    {

        public void Run(Class SelectedClass) 
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.MarkupLine($"Welcome [yellow]Martin[/] zum {SelectedClass.Name} portal");
                AnsiConsole.WriteLine();

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

                AnsiConsole.WriteLine();
                string option = AnsiConsole.Ask<string>(
                    "Press [green]a[/] to add a student, [green]d[/] to delete a student, [green]s[/] to select a student, [red]b[/] to go back:");

                if (option == "a")
                {
                    Student student = new Student();
                    InputHandler ihandler = new InputHandler();
                    ihandler.RunStudent(student);
                    SelectedClass.Students.Add(student);
                }
                else if (option == "d")
                {
                    if (SelectedClass.Students.Count == 0)
                    {
                        AnsiConsole.MarkupLine("[red]Nothing to delete.[/]");
                        AnsiConsole.Ask<string>("Press Enter to continue");
                        continue;
                    }

                    int classnr = AnsiConsole.Ask<int>("Which class number do you want to delete?");
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
                    if (SelectedClass.Students.Count == 0)
                    {
                        AnsiConsole.MarkupLine("[red]No classes to select.[/]");
                        AnsiConsole.Ask<string>("Press Enter to continue");
                        continue;
                    }

                    Student selectedStudent = AnsiConsole.Prompt(
                        new SelectionPrompt<Student>()
                            .Title("Which Student do you want to select?")
                            .UseConverter(k => k.Name)
                            .AddChoices(SelectedClass.Students.OrderBy(k => k.Name)));
                }
                else if(option == "b")
                {
                    return;
                }
            }
        }
    }
}
