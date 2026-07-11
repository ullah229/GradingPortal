using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradingSystem;
    internal class SubjectHandler
    {
        public void Run(Student SelectedStudent)
        {

            SelectedStudent.Subjects ??= new List<Subject>();
            while (true)
            {
                Console.Clear();

                AnsiConsole.MarkupLine($"{SelectedStudent.Name}");
                ListSubjects(SelectedStudent);
                bool GoBack = SelectOptions(SelectedStudent);
                if (GoBack)
                {
                    return;
                }
            }
        }
        
        public void ListSubjects(Student SelectedStudent)
        {
            if (SelectedStudent.Subjects.Count > 0)
            {
                foreach (Subject subject in SelectedStudent.Subjects.OrderBy(k => k.Name))
                {
                    AnsiConsole.MarkupLine($"- [cyan]{subject.Name} | {subject.Grade} [/]");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]No subjects/ grades yet.[/]");
            }
        }

        public bool SelectOptions(Student SelectedStudent)
        {
            string option = AnsiConsole.Ask<string>(
        "Press [green]a[/] to add a subject/ grading, [green]d[/] to delete a subject/grading, [red]b[/] to go back:");

            if (option == "a")
            {
                Subject subject = new Subject();
                InputHandler ihandler = new InputHandler();
                ihandler.ConfigureSubject(subject);
                SelectedStudent.Subjects.Add(subject);
            }
            else if (option == "d")
            {
                if (SelectedStudent.Subjects.Count == 0)
                {
                    AnsiConsole.MarkupLine("[red]No subjects to select.[/]");
                    AnsiConsole.Ask<string>("Press Enter to continue");
                    return false;
                }
                int subjectnr = AnsiConsole.Ask<int>("Which subject/ grading do you want to delete?");
                if (subjectnr >= 1 && subjectnr <= SelectedStudent.Subjects.Count)
                {
                    SelectedStudent.Subjects.RemoveAt(subjectnr - 1);
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Invalid number.[/]");
                    AnsiConsole.Ask<string>("Press Enter to continue");
                }
            }
            else if (option == "s")
            {
            }
            else if (option == "b")
            {
                return true;
            }
            return false;
        }
    }
