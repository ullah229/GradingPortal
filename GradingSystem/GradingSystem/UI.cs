using Microsoft.VisualBasic.FileIO;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradingSystem
{
    internal class UI
    {
        public void ListClass(List<Class> classes)
        {
            if (classes.Count > 0)
            {
                foreach (Class cls in classes.OrderBy(k => k.Name))
                {
                    AnsiConsole.MarkupLine($"- [cyan]{cls.Name}[/]");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]No classes yet.[/]");
            }
        }

        public void ListStudents(List<Student> students)
        {
            if (students.Count > 0)
            {
                foreach (Student student in students.OrderBy(k => k.Name))
                {
                    AnsiConsole.MarkupLine($"- [cyan]{student.Name}[/]");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]No Students yet.[/]");
            }
        }

        public void ListSubjects(List<Subject> subjects)
        {
            int result = 0;
            if (subjects.Count > 0)
            {
                foreach (Subject subject in subjects)
                {
                    AnsiConsole.MarkupLine($"- [cyan]{subject.Name} | {subject.Grade} [/]");
                    result +=subject.Grade;
                }
                double durchschnitt = (double)result / subjects.Count;
                AnsiConsole.WriteLine("Durchschnitt : " + durchschnitt.ToString());
            }
            else
            {
                AnsiConsole.MarkupLine("[red]No subjects/ grades yet.[/]");
            }
        }
        public Class SelectClass(List<Class> classes)
        {
                Class selectedClass = AnsiConsole.Prompt(
                new SelectionPrompt<Class>()
                .Title("Which class do you want to select?")
                .UseConverter(k => k.Name)
                .AddChoices(classes.OrderBy(k => k.Name)));

            return selectedClass;
        }

        public Student SelectStudent(Class SelectedClass)
        {
            Student selectedStudent = AnsiConsole.Prompt(
                new SelectionPrompt<Student>()
                    .Title("Which Student do you want to select?")
                    .UseConverter(k => k.Name)
                    .AddChoices(SelectedClass.Students.OrderBy(k => k.Name)));
            return selectedStudent;
        }

        public string SelectOptions()
        {
            string option = AnsiConsole.Ask<string>(
    "Press [green]a[/] to add, [green]d[/] to delete, [green]s[/] to select, [red]b[/] to quit:");

            return option;
        }
    }
}
