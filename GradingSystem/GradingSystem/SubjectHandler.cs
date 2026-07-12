using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradingSystem;
    internal class SubjectHandler
    {
        UI ui = new UI();
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
        
        private void ListSubjects(Student SelectedStudent)
        {
            ui.ListSubjects(SelectedStudent.Subjects);
        }

        private bool SelectOptions(Student SelectedStudent)
        {
            string option = ui.SelectOptions();

            if (option == "a")
            {
                AddSubject(SelectedStudent);
            }
            else if (option == "d")
            {
                DeleteSubject(SelectedStudent);
            }
            else if (option == "b")
            {
                return true;
            }
            return false;
        }
        
        private void AddSubject(Student SelectedStudent)
        {
            Subject subject = new Subject();
            InputHandler ihandler = new InputHandler();
            ihandler.ConfigureSubject(subject);
            SelectedStudent.Subjects.Add(subject);
        }
        
        private void DeleteSubject(Student SelectedStudent)
        {
            if (SelectedStudent.Subjects.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No subjects to select.[/]");
                AnsiConsole.Ask<string>("Press Enter to continue");
                return;
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
    }
