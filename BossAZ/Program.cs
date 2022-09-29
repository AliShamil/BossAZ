

namespace BossAZ;

using BossAZ.Helper;
using BossAZ.Visual_Helper;
using Microsoft.VisualBasic;
using System;
using System.Text.RegularExpressions;
using User;
using static Visual_Helper.Visual;

internal class Program
{

    static public bool CheckParse(string[] array)
    {
        foreach (var i in array)
        {
            if (!int.TryParse(i, out int result))
                return false;
        }
        return true;
    }


    static private void JSONSerializeMethod<T>(DirectoryInfo directory, List<T> data, string fileName)
    {
        var jsonString = System.Text.Json.JsonSerializer.Serialize(data);
        File.WriteAllText($@"{directory.FullName}\{fileName}.json", jsonString);
    }

    static private List<T> JSONDeserializeMethod<T>(string path)
    {

        using FileStream fs = new FileStream($"{path}", FileMode.Open);
        List<T> data = new();
        data = System.Text.Json.JsonSerializer.Deserialize<List<T>>(fs);
        return data;
    }

    static void Main(string[] args)
    {


        List<Worker> workers;
        List<Employer> employers;


        try
        {
            workers = JSONDeserializeMethod<Worker>("C:\\Users\\Admin\\source\\repos\\BossAZ\\BossAZ\\WorkerInfo\\Workers.json");
           
        }

        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Previous Workers file not found!");
            Console.ReadKey(false);
          
            workers = new();
        }
        
        try
        {
           employers = JSONDeserializeMethod<Employer>("C:\\Users\\Admin\\source\\repos\\BossAZ\\BossAZ\\EmployerInfo\\Employers.json");
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Previous Employers file not found!");
            Console.ReadKey(false);

            employers = new();
        }


        //Worker worker1 = new("Ali", "Shamil", Azerbaijan.Baku, "0517755590", 19);
        //Worker worker2 = new("Faiq", "Shamil", Azerbaijan.Baku, "0503534464", 21);

        //DirectoryInfo directoryInfo = new("C:\\Users\\Admin\\source\\repos\\BossAZ\\BossAZ\\WorkerInfo");

        //workers.Add(worker1);
        //workers.Add(worker2);

        //JSONSerializeMethod(directoryInfo, workers, "Workers");





        foreach (var worker in workers)
        {
            Console.WriteLine(worker);
        }

        bool BOSSAZ = false;
        string tempName, tempSurname;
        string[] StartMenu = { "Log In", "Sign Up" };
        string[] LogInMenu = { "Log In as Worker", "Log In as Employer" };
        string[] SignUpMenu = { "Sign Up as Worker", "Sign Up as Employer" };
        Worker currentWorker = null;
        Employer currentEmployer = null;
        ConsoleKeyInfo key;
        int index = 0;

        while (!BOSSAZ)
        {
            Console.Clear();
            ShowMenu(StartMenu, index);
            key = Console.ReadKey();
            Cursor(key, StartMenu.Length, ref index);
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    switch (index)
                    {
                        case 0:
                            bool checkLogin = false;
                            while (!checkLogin)
                            {
                                Console.Clear();
                                ShowMenu(LogInMenu, index);
                                key = Console.ReadKey();
                                Cursor(key, LogInMenu.Length, ref index);
                                switch (key.Key)
                                {
                                    case ConsoleKey.Enter:
                                        switch (index)
                                        {
                                            case 0:
                                                #region ValidateWorker

                                                bool checkWorkerLogin = false;
                                                while (!checkWorkerLogin)
                                                {
                                                    Console.Clear();
                                                    try
                                                    {
                                                        Console.Write("Enter Name: ");
                                                        tempName = Console.ReadLine();
                                                        if (string.IsNullOrWhiteSpace(tempName))
                                                            throw new ArgumentNullException("Name cannot be null or whitespace!");

                                                        Console.Write("Enter Surname: ");
                                                        tempSurname = Console.ReadLine();
                                                        if (string.IsNullOrWhiteSpace(tempSurname))
                                                            throw new ArgumentNullException("Surname cannot be null or whitespace!");


                                                        if (!workers.Any(worker => worker.Name==tempName && worker.Surname==tempSurname))
                                                            throw new ArgumentException("This worker not found !");


                                                        currentWorker = workers.Find(worker => worker.Name==tempName && worker.Surname==tempSurname);
                                                        checkWorkerLogin = true;

                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine(ex.Message);
                                                        Thread.Sleep(1500);

                                                    }

                                                }

                                                #endregion




                                                break;

                                            case 1:
                                                #region ValidateEmployer

                                                bool checkEmployerLogin = false;
                                                while (!checkEmployerLogin)
                                                {
                                                    Console.Clear();
                                                    try
                                                    {
                                                        Console.Write("Enter Name: ");
                                                        tempName = Console.ReadLine();
                                                        if (string.IsNullOrWhiteSpace(tempName))
                                                            throw new ArgumentNullException("Name cannot be null or whitespace!");

                                                        Console.Write("Enter Surname: ");
                                                        tempSurname = Console.ReadLine();
                                                        if (string.IsNullOrWhiteSpace(tempSurname))
                                                            throw new ArgumentNullException("Surname cannot be null or whitespace!");


                                                        if (!employers.Any(employer => employer.Name==tempName && employer.Surname==tempSurname))
                                                            throw new ArgumentException("This employer not found !");


                                                        currentEmployer = employers.Find(employer => employer.Name==tempName && employer.Surname==tempSurname);
                                                        checkEmployerLogin = true;

                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine(ex.Message);
                                                        Thread.Sleep(1500);

                                                    }

                                                }

                                                #endregion
                                                break;
                                        }
                                        break;
                                    case ConsoleKey.Backspace:
                                        checkLogin = true;
                                        break;
                                }
                            }
                            break;
                        case 1:
                            bool checkSignUp = false;
                            while (!checkSignUp)
                            {
                                Console.Clear();
                                ShowMenu(SignUpMenu, index);
                                key = Console.ReadKey();
                                Cursor(key, SignUpMenu.Length, ref index);
                                switch (key.Key)
                                {
                                    case ConsoleKey.Enter:
                                        break;
                                    case ConsoleKey.Backspace:
                                        checkSignUp = true;
                                        break;

                                }

                            }
                            break;
                    }
                    break;
                case ConsoleKey.Backspace:
                    BOSSAZ = true;
                    break;
            }





        }




    }
}