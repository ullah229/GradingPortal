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
                foreach (Class cls in classes)
                {
                    AnsiConsole.MarkupLine($"- [cyan]{cls.Name}[/]");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]No subjects/ grades yet.[/]");
            }
        }

        public void ListStudents(List<Student> students)
        {
            if (students.Count > 0)
            {
                foreach (Student student in students)
                {
                    AnsiConsole.MarkupLine($"- [cyan]{student.Name}[/]");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]No subjects/ grades yet.[/]");
            }
        }

        public void ListSubjects(List<Subject> subjects)
        {
            if (subjects.Count > 0)
            {
                foreach (Subject subject in subjects)
                {
                    AnsiConsole.MarkupLine($"- [cyan]{subject.Name} | {subject.Grade} [/]");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]No subjects/ grades yet.[/]");
            }
        }

        public string SelectOptions()
        {
            string option = AnsiConsole.Ask<string>(
    "Press [green]a[/] to add, [green]d[/] to delete, [green]s[/] to select, [red]b[/] to quit:");

            return option;
        }
    }
}
