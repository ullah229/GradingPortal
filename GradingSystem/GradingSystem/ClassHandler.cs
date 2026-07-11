using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradingSystem;

    internal class ClassHandler
    {
        List<Class> classList = new List<Class>();
        FileManager fmanager = new FileManager();

        public void Run()
        {
        try
        {
            classList = fmanager.Load();
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
        
        public void ListClasses()
        {
            if (classList.Count > 0)
            {
                AnsiConsole.MarkupLine("[bold]List of classes:[/]");
                foreach (Class klasse in classList.OrderBy(k => k.Name))
                {
                    AnsiConsole.MarkupLine($"- [cyan]{klasse.Name}[/]");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]No classes yet.[/]");
            }
        }
        private bool SelectOptions()
        {
            string option = AnsiConsole.Ask<string>(
    "Press [green]a[/] to add a class, [green]d[/] to delete a class, [green]s[/] to select a class, [red]b[/] to quit:");

            if (option == "a")
            {
                Class klasse = new Class();
                InputHandler ihandler = new InputHandler();
                ihandler.ConfigureClass(klasse);
                classList.Add(klasse);
                fmanager.Save(classList);
            }
            else if (option == "d")
            {
                if (classList.Count == 0)
                {
                    AnsiConsole.MarkupLine("[red]Nothing to delete.[/]");
                    AnsiConsole.Ask<string>("Press Enter to continue");
                    return false;
                }

                int classnr = AnsiConsole.Ask<int>("Which class number do you want to delete?");
                if (classnr >= 1 && classnr <= classList.Count)
                {
                    classList.RemoveAt(classnr - 1);
                    fmanager.Save(classList);
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Invalid number.[/]");
                    AnsiConsole.Ask<string>("Press Enter to continue");
                }
            }
            else if (option == "s")
            {
                if (classList.Count == 0)
                {
                    AnsiConsole.MarkupLine("[red]No classes to select.[/]");
                    AnsiConsole.Ask<string>("Press Enter to continue");
                    return false;
                }

                Class selectedClass = AnsiConsole.Prompt(
                    new SelectionPrompt<Class>()
                        .Title("Which class do you want to select?")
                        .UseConverter(k => k.Name)
                        .AddChoices(classList.OrderBy(k => k.Name)));

                StudentHandler shandler = new StudentHandler();
                shandler.Run(selectedClass);
                fmanager.Save(classList);
            }
            else if (option == "b")
            {
                return true;
            }
            return false;
        }
    }
