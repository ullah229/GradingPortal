using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradingSystem;
    internal class InputHandler
    {
        public void ConfigureClass(Class klasse)
        {
            klasse.Name = AskName();
        }

        public void ConfigureStudent(Student student)
        {
            student.Name = AskName();
        }

        public void ConfigureSubject(Subject subject)
        {
            subject.Name = AskSubjectName();
            subject.Grade = AskSubjectGrade();
        }
        private string AskName()
            {
                while (true)
                {
                    string name = AnsiConsole.Ask<string>("[yellow]Name[/]?");
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        return name;
                    }
                    Console.WriteLine("Please enter your name.");
                }
            }

        private string AskSubjectName()
        {
            while (true)
            {
                string subject = AnsiConsole.Ask<string>("[yellow]Subject[/] 1. Math, 2. German, 3. French?");
                if (!string.IsNullOrWhiteSpace(subject))
                {
                    return subject;
                }
                Console.WriteLine("Please enter your name.");
            }
        }

        private int AskSubjectGrade()
        {
            while (true)
            {
                int grade = AnsiConsole.Ask<int>("[yellow]Grade[/]?");
                return grade;
            }
        }
}
