using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradingSystem;

    internal class ClassHandler
    {
        List<Class> classList = new List<Class>();
        UI ui = new UI();

        public void Run()
        {
        try
        {
            classList = FileManager.Load();
        }
        catch
        {
            AnsiConsole.MarkupLine($"[red]Could not load JSON file[/]");
            AnsiConsole.MarkupLine("[yellow]Starting with an empty list. Saving will overwrite the old file![/]");
            AnsiConsole.Ask<string>("Press Enter to continue");
            classList = new List<Class>();
        }
            while (true)
            {
                Console.Clear();
                AnsiConsole.MarkupLine("Welcome [yellow]Martin[/] to Class portal");
                AnsiConsole.WriteLine();


                ListClasses();
                AnsiConsole.WriteLine();
                bool GoBack = SelectOptions();
                if (GoBack)
                {
                    return;
                }

            }
        }
        
        private void ListClasses()
        {
            ui.ListClass(classList);
        }
        private bool SelectOptions()
        {
            string option = ui.SelectOptions();
            if (option == "a")
            {
                AddClass();
            }
            else if (option == "d")
            {
                DeleteClass();
            }
            else if (option == "s")
            {
                SelectClass();
            }
            else if (option == "b")
            {
                return true;
            }
            return false;
        }
        
        private void AddClass()
        {
            Class klasse = new Class();
            InputHandler ihandler = new InputHandler();
            ihandler.ConfigureClass(klasse);
            classList.Add(klasse);
            FileManager.Save(classList);
        }

        private void DeleteClass()
        {
            if (classList.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]Nothing to delete.[/]");
                AnsiConsole.Ask<string>("Press Enter to continue");
                return;
            }

            int classnr = AnsiConsole.Ask<int>("Which class number do you want to delete?");
            if (classnr >= 1 && classnr <= classList.Count)
            {
                classList.RemoveAt(classnr - 1);
                FileManager.Save(classList);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Invalid number.[/]");
                AnsiConsole.Ask<string>("Press Enter to continue");
            }
        }

        private void SelectClass()
        {
            if (classList.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No classes to select.[/]");
                AnsiConsole.Ask<string>("Press Enter to continue");
                return;
            }

            StudentHandler shandler = new StudentHandler();
            shandler.Run(ui.SelectClass(classList));
            FileManager.Save(classList);
    }
}
