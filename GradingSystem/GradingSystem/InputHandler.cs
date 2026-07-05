using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradingSystem
{
    internal class InputHandler
    {
        public void Run(Class klasse)
        {
            klasse.Name = AskName();
        }

        public void RunStudent(Student student)
        {
            student.Name = AskName();
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
    }
}
