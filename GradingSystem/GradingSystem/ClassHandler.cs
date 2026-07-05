using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradingSystem
{
    internal class ClassHandler
    {
        List<Class> listclass = new List<Class>();

        public void Run()
        {
            FileManager fmanager = new FileManager();
            listclass = fmanager.Load();
            while (true)
            {
                Console.Clear();
                AnsiConsole.MarkupLine("Welcome [yellow]Martin[/] to Class portal");
                AnsiConsole.WriteLine();

                if (listclass.Count > 0)
                {
                    AnsiConsole.MarkupLine("[bold]List of classes:[/]");
                    foreach (Class klasse in listclass.OrderBy(k => k.Name))
                    {
                        AnsiConsole.MarkupLine($"- [cyan]{klasse.Name}[/]");
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]No classes yet.[/]");
                }

                AnsiConsole.WriteLine();
                string option = AnsiConsole.Ask<string>(
                    "Press [green]a[/] to add a class, [green]d[/] to delete a class, [green]s[/] to select a class, [red]b[/] to quit:");

                if (option == "a")
                {
                    Class klasse = new Class();
                    InputHandler ihandler = new InputHandler();
                    ihandler.Run(klasse);
                    listclass.Add(klasse);
                    fmanager.Save(listclass);
                }
                else if (option == "d")
                {
                    if (listclass.Count == 0)
                    {
                        AnsiConsole.MarkupLine("[red]Nothing to delete.[/]");
                        AnsiConsole.Ask<string>("Press Enter to continue");
                        continue;
                    }

                    int classnr = AnsiConsole.Ask<int>("Which class number do you want to delete?");
                    if (classnr >= 1 && classnr <= listclass.Count)
                    {
                        listclass.RemoveAt(classnr - 1);
                        fmanager.Save(listclass);
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Invalid number.[/]");
                        AnsiConsole.Ask<string>("Press Enter to continue");
                    }
                }
                else if (option == "s")
                {
                    if (listclass.Count == 0)
                    {
                        AnsiConsole.MarkupLine("[red]No classes to select.[/]");
                        AnsiConsole.Ask<string>("Press Enter to continue");
                        continue;
                    }

                    Class selectedClass = AnsiConsole.Prompt(
                        new SelectionPrompt<Class>()
                            .Title("Which class do you want to select?")
                            .UseConverter(k => k.Name)
                            .AddChoices(listclass.OrderBy(k => k.Name)));

                    StudentHandler shandler = new StudentHandler();
                    shandler.Run(selectedClass);
                    fmanager.Save(listclass);
                }
                else if (option == "b")
                {
                    return;
                }
            }
        }
    }
}
