

namespace BossAZ;

using BossAZ.Helper;
using BossAZ.Visual_Helper;
using Microsoft.VisualBasic;
using Serilog;
using Serilog.Settings.Configuration;
using System;
using System.Reflection.Emit;
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

    public static Vacancy CreateVacancy()
    {
        Vacancy tempVacancy = null;
        string tempProfession;
        string[] tempKnowledge;
        string[] tempLimit;
        (byte min, byte max) tempAge = default;
        double tempSalary;

        bool check = false;

        while (!check)
        {
            Console.Clear();
            try
            {
                Console.Write("Enter Profession: ");
                tempProfession = Console.ReadLine();

                Console.Write("Enter Age Limit(min|max): ");
                tempLimit = Console.ReadLine().Split("|");

                if (!byte.TryParse(tempLimit[0], out tempAge.min) && byte.TryParse(tempLimit[1], out tempAge.max))
                    throw new InvalidCastException("Invalid Age");
                else
                {
                    tempAge.min = byte.Parse(tempLimit[0]);
                    tempAge.max = byte.Parse(tempLimit[1]);
                }

                Console.Write("Enter Salary: ");
                if (!double.TryParse(Console.ReadLine(), out tempSalary))
                    throw new InvalidCastException("Invalid Salary");

                Console.Write("Enter Required Knowledges (use '|' for multiple entering): ");
                tempKnowledge = Console.ReadLine().Split("|");

                tempVacancy = new(tempProfession, tempAge, tempSalary, tempKnowledge.ToList());
                check = true;

            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Thread.Sleep(1500);

            }
        }
        return tempVacancy;

    }

    static public void DeleteVacancy(ref Employer employer)
    {

        string VacancyID;

        bool check = false;
        while (!check)
        {
            try
            {
                Console.Write("Enter Vacancy ID: ");
                VacancyID = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(VacancyID))
                    throw new ArgumentException("Invalid ID");

                if (!employer.Vacancies.Any(v => v.Id.ToString()== VacancyID))
                    throw new ArgumentException("This Vacancy not Found");

                else
                {
                    employer.Vacancies.Remove(employer.Vacancies.Find(v => v.Id.ToString()== VacancyID));
                    Console.Clear();
                    Console.WriteLine("Vacancy Removed Successfully !");
                    Thread.Sleep(1500);
                    check = true;
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Thread.Sleep(1500);
            }

        }

    }
    static void DeleteCV(ref Worker worker)
    {
        string CVId;

        bool check = false;
        while (!check)
        {
            try
            {
                Console.Write("Enter CV ID: ");
                CVId = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(CVId))
                    throw new ArgumentException("Invalid ID");

                if (!worker.WorkerCV.Any(v => v.Id.ToString()== CVId))
                    throw new ArgumentException("This CV not Found");

                else
                {
                    worker.WorkerCV.Remove(worker.WorkerCV.Find(v => v.Id.ToString()== CVId));
                    Console.Clear();
                    Console.WriteLine("CV Removed Successfully !");
                    Thread.Sleep(1500);
                    check = true;
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Thread.Sleep(1500);
            }

        }
    }

    public static CV CreateCV()
    {
        CV tempCv = null;
        string tempProfession, tempSchool, tempgitlink, tempLinkedIn, accRate;
        double rate;
        string[] skills;
        bool hasDiplom = default;
        List<WorkHistory> workHistories = new();
        List<Language> languages = new();



        bool check = false;

        while (!check)
        {
            try
            {
                Console.Write("Enter Profession: ");
                tempProfession = Console.ReadLine();

                Console.Write("Enter School: ");
                tempSchool = Console.ReadLine();

                Console.Write("Enter Acceptance rate: ");
                accRate = Console.ReadLine();
                if (!double.TryParse(accRate, out rate))
                    throw new InvalidCastException("Invalid rate");
                rate = double.Parse(accRate);

                Console.Write("Enter Skills(use '|' for multiple entering): ");
                skills = Console.ReadLine().Split("|");

                Console.Write("Enter Gitlink: ");
                tempgitlink = Console.ReadLine();

                Console.Write("Enter LinkedIn: ");
                tempLinkedIn = Console.ReadLine();

                Console.WriteLine("Do you want to add WorkHistory?(y or n): ");
                switch (Console.ReadLine().ToLower())
                {
                    case "y":
                        bool addHistory = false;

                        while (!addHistory)
                        {
                            WorkHistory workHistory = createWorkHistory();
                            workHistories.Add(workHistory);
                            Console.WriteLine("Do you want add another?(y or n): ");
                            switch (Console.ReadLine())
                            {
                                case "y":
                                    continue;
                                case "n":
                                    addHistory = true;
                                    break;
                            }
                        }
                        break;
                    case "n":
                        break;

                }

                Console.WriteLine("Do you want to add Language?(y or n): ");
                switch (Console.ReadLine().ToLower())
                {
                    case "y":
                        bool addLanguage = false;

                        while (!addLanguage)
                        {
                            Language lang = createLanguage();
                            languages.Add(lang);
                            Console.WriteLine("Do you want add another?(y or n): ");
                            switch (Console.ReadLine())
                            {
                                case "y":
                                    continue;
                                case "n":
                                    addLanguage = true;
                                    break;
                            }
                        }
                        break;
                    case "n":
                        break;

                }

                if (languages.Count == 0 && workHistories.Count == 0)
                {
                    tempCv = new(tempProfession, tempSchool, rate, skills.ToList(), hasDiplom, tempgitlink, tempLinkedIn);

                }
                else
                {
                    tempCv = new(tempProfession, tempSchool, rate, skills.ToList(), workHistories, languages, hasDiplom, tempgitlink, tempLinkedIn);
                }
                check = true;
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Thread.Sleep(1500);

            }
        }
        return tempCv;


    }


    static public WorkHistory createWorkHistory()
    {
        WorkHistory tempHistory = null;
        string? company;
        (DateTime FirstDay, DateTime LastDay) workedTimes;
        string firstDate;
        string EndDate;
        bool check = false;

        while (!check)
        {
            Console.Clear();
            try
            {

                Console.Write("Enter Company Name: ");
                company = Console.ReadLine();

                Console.Write("Enter First Job Day(year.month.day): ");
                firstDate = Console.ReadLine();
                if (!DateTime.TryParse(firstDate, out workedTimes.FirstDay))
                    throw new InvalidCastException();

                Console.Write("Enter Last Job Day(year.month.day): ");
                EndDate = Console.ReadLine();

                if (!DateTime.TryParse(EndDate, out workedTimes.LastDay))
                    throw new InvalidCastException();

                workedTimes.FirstDay = DateTime.Parse(firstDate);
                workedTimes.LastDay = DateTime.Parse(EndDate);

                tempHistory = new(company, workedTimes);
                check = true;
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Thread.Sleep(1500);
            }
        }
        return tempHistory;


    }


    public static Language createLanguage()
    {
        Language language = null;
        string name, languageL;
        LanguageLevels level = default;
        bool check = false;

        while (!check)
        {
            try
            {
                Console.Write("Enter Language name: ");
                name = Console.ReadLine();

                Console.Write("Enter Level: ");
                languageL = Console.ReadLine();

                if (!Enum.TryParse(languageL, out level))
                    throw new InvalidCastException("Incorrect Level!");

                level = Enum.Parse<LanguageLevels>(languageL);

                language = new(name, level);

                check = true;
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Thread.Sleep(1500);

            }


        }
        return language;


    }





    static string format = @"[{Timestamp:HH:mm:ss} {Level:u3}] {Message} {Exception} {MachineName} {NewLine}";



    //    Log.Information("InfoMessage1");
    //Log.Information("InfoMessage2");
    //Log.Warning("WarningMessage");
    //Log.Error(new NullReferenceException("ExMess"), "ErrorMessage");
    //Log.Fatal("FatalMessage");


    static void Main12(string[] args)
    {
        WorkHistory wh = createWorkHistory();
        Console.WriteLine(wh);


    }



    static void Main()
    {
        Log.Logger = new LoggerConfiguration()
.WriteTo.File("C:\\Users\\Admin\\source\\repos\\BossAZ\\BossAZ\\MyLog\\myLog.json", outputTemplate: format)
.WriteTo.Console(outputTemplate: format)
.Enrich.WithMachineName()
.CreateLogger();


        List<Worker> workers;
        List<Worker> jobSeekers;
        List<Employer> employers;



        Log.Information("Program started");
        try
        {
            workers = JSONDeserializeMethod<Worker>("C:\\Users\\Admin\\source\\repos\\BossAZ\\BossAZ\\WorkerInfo\\Workers.json");

        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Previous Workers file not found!");
            Log.Warning(ex.Message);
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
            Log.Warning(ex.Message);
            Console.ReadKey(false);

            employers = new();
        }

        try
        {
            jobSeekers = JSONDeserializeMethod<Worker>("C:\\Users\\Admin\\source\\repos\\BossAZ\\BossAZ\\JobSeekersInfo\\JobSeekers.json");
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Previous Job Seekers file not found!");
            Log.Warning(ex.Message);
            Console.ReadKey(false);

            jobSeekers = new();
        }



        DirectoryInfo workerDirectoryInfo = new("C:\\Users\\Admin\\source\\repos\\BossAZ\\BossAZ\\WorkerInfo");
        DirectoryInfo employerDirectoryInfo = new("C:\\Users\\Admin\\source\\repos\\BossAZ\\BossAZ\\EmployerInfo");
        DirectoryInfo jobSeekerDirectoryInfo = new("C:\\Users\\Admin\\source\\repos\\BossAZ\\BossAZ\\JobSeekersInfo");









        bool BOSSAZ = false;
        string tempName, tempSurname;
        string[] StartMenu = { "Log In", "Sign Up" };
        string[] LogInMenu = { "Log In as Worker", "Log In as Employer" };
        string[] SignUpMenu = { "Sign Up as Worker", "Sign Up as Employer" };
        string[] WorkerMenu = { "Show Vacancies", "Show Job Seekers", "Join Job Seekers", "Show own Cv", "Add CV", "Delete Cv", "Clear Cv" };
        string[] EmployerMenu = { "Show Vacancies", "Show Job Seekers", "Show own Vacancies", "Add Vacancies", "Delete owm Vacancies", "Clear Vacancies" };


        Worker currentWorker = null;
        Employer currentEmployer = null;
        ConsoleKeyInfo key;
        int index = 0;
        int MenuIndex = 0;
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
                                        Log.Information("User entered Login Menu");
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
                                                        Log.Information($"{currentWorker.Name} entered succesfully!");
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine(ex.Message);
                                                        Thread.Sleep(1500);

                                                    }

                                                }

                                                #endregion
                                                
                                                bool checkWorkerMenu = false;


                                                while (!checkWorkerMenu)
                                                {
                                                    Console.Clear();
                                                    ShowMenu(WorkerMenu, index);
                                                    key = Console.ReadKey();
                                                    Cursor(key, WorkerMenu.Length, ref index);
                                                    
                                                    switch (key.Key)
                                                    {
                                                        case ConsoleKey.Enter:
                                                            switch (index)
                                                            {
                                                                case 0:
                                                                    Console.Clear();

                                                                    foreach (Employer employer in employers)
                                                                    {
                                                                        if (employer.Vacancies.Count != 0)
                                                                            Console.WriteLine(employer);
                                                                    }
                                                                    Console.ReadKey(false);
                                                                    Log.Information($"{currentWorker.Name} showned vacancies!");
                                                                    break;
                                                                case 1:
                                                                    Console.Clear();
                                                                    if (jobSeekers.Count != 0)
                                                                    {
                                                                        foreach (Worker js in jobSeekers)
                                                                        {
                                                                            Console.WriteLine(js);
                                                                        }
                                                                    }
                                                                    else
                                                                        Console.WriteLine("Empty!");
                                                                    Console.ReadKey(false);
                                                                    Log.Information($"{currentWorker.Name} showned job seekers!");
                                                                    break;
                                                                case 2:
                                                                    Console.Clear();
                                                                    if (!jobSeekers.Any(js => js.Name==currentWorker.Name && js.Surname==currentWorker.Surname))
                                                                    {
                                                                        jobSeekers.Add(currentWorker);
                                                                        JSONSerializeMethod(jobSeekerDirectoryInfo, jobSeekers, "JobSeekers");
                                                                        Console.Clear();
                                                                        Console.WriteLine("You are jobSeeker now!");
                                                                        Log.Information($"{currentWorker.Name} joined job seekers!");
                                                                        Thread.Sleep(1500);
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.Clear();
                                                                        Console.WriteLine("You already jobSeekers");
                                                                        Log.Warning($"{currentWorker.Name} already job seekers!");
                                                                        Thread.Sleep(1500);
                                                                    }
                                                                    break;
                                                                case 3:
                                                                    Console.Clear();
                                                                    if (currentWorker.WorkerCV.Count!=0)
                                                                        currentWorker.WorkerCV.ForEach(cv => Console.WriteLine(cv));
                                                                    else
                                                                        Console.WriteLine("Empty!");

                                                                    Console.ReadKey(false);
                                                                    Log.Information($"{currentWorker.Name} showned cv!");
                                                                    break;
                                                                case 4:
                                                                    CV tempCV = CreateCV();
                                                                    currentWorker.WorkerCV.Add(tempCV);
                                                                    JSONSerializeMethod(workerDirectoryInfo, workers, "Workers");
                                                                    Console.WriteLine("New CV added successfuly!");
                                                                    Thread.Sleep(1500);
                                                                    Log.Information($"{currentWorker.Name} added CV!");
                                                                    break;
                                                                case 5:
                                                                    Console.Clear();
                                                                    DeleteCV(ref currentWorker);
                                                                    Log.Information($"{currentWorker.Name} delete CV successfuly!");
                                                                    break;
                                                                case 6:
                                                                    currentWorker.WorkerCV.Clear();
                                                                    Console.Clear();
                                                                    JSONSerializeMethod(workerDirectoryInfo, workers, "Workers");
                                                                    Console.WriteLine("Your CV clear successfuly!");
                                                                    Log.Information($"{currentWorker.Name} clear CV succesfully!");
                                                                    Thread.Sleep(1500);
                                                                    break;

                                                            }
                                                            break;
                                                        case ConsoleKey.Backspace:
                                                            checkWorkerMenu = true;
                                                            Log.Information($"{currentWorker.Name} went to back Menu!");
                                                            break;
                                                    }

                                                }


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
                                                        Log.Information($"{currentEmployer.Name} entered own Profile!");
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine(ex.Message);
                                                        Thread.Sleep(1500);

                                                    }

                                                }

                                                #endregion

                                                bool checkEmployerMenu = false;


                                                while (!checkEmployerMenu)
                                                {
                                                    Console.Clear();
                                                    ShowMenu(EmployerMenu, index);
                                                    key = Console.ReadKey();
                                                    Cursor(key, EmployerMenu.Length, ref index);

                                                    switch (key.Key)
                                                    {
                                                        case ConsoleKey.Enter:
                                                            switch (index)
                                                            {
                                                                case 0:
                                                                    Console.Clear();

                                                                    foreach (Employer employer in employers)
                                                                    {
                                                                        if (employer.Vacancies.Count != 0)
                                                                            Console.WriteLine(employer);
                                                                    }
                                                                    Console.ReadKey(false);
                                                                    Log.Information($"{currentEmployer.Name} shown Vacancies!");
                                                                    break;
                                                                case 1:
                                                                    Console.Clear();
                                                                    if (jobSeekers.Count != 0)
                                                                    {
                                                                        foreach (Worker js in jobSeekers)
                                                                        {
                                                                            Console.WriteLine(js);
                                                                        }
                                                                    }
                                                                    else
                                                                        Console.WriteLine("Empty!");
                                                                    Console.ReadKey(false);
                                                                    Log.Information($"{currentEmployer.Name} shown job seekers!");
                                                                    break;
                                                                case 2:
                                                                    Console.Clear();
                                                                    if (currentEmployer.Vacancies.Count!=0)
                                                                        currentEmployer.Vacancies.ForEach(v => Console.WriteLine(v));
                                                                    else
                                                                        Console.WriteLine("Empty!");

                                                                    Console.ReadKey(false);
                                                                    Log.Information($"{currentEmployer.Name} shown own Vacancies!");
                                                                    break;
                                                                case 3:
                                                                    Vacancy tempVacancy = CreateVacancy();
                                                                    currentEmployer?.Vacancies.Add(tempVacancy);
                                                                    Console.Clear();
                                                                    JSONSerializeMethod(employerDirectoryInfo, employers, "Employers");
                                                                    Console.WriteLine("New Vacancy added successfuly!");
                                                                    Thread.Sleep(1500);
                                                                    Log.Information($"{currentEmployer.Name} added new Vacancy!");
                                                                    break;
                                                                case 4:
                                                                    Console.Clear();
                                                                    DeleteVacancy(ref currentEmployer);
                                                                    break;
                                                                case 5:
                                                                    currentEmployer.Vacancies.Clear();
                                                                    Console.Clear();
                                                                    JSONSerializeMethod(employerDirectoryInfo, employers, "Employers");
                                                                    Console.WriteLine("Your Vacancies clear successfuly!");
                                                                    Log.Information($"{currentEmployer.Name} cleared all Vacancies!");
                                                                    Thread.Sleep(1500);
                                                                    break;

                                                            }
                                                            break;
                                                        case ConsoleKey.Backspace:
                                                            checkEmployerMenu = true;
                                                            Log.Information($"{currentEmployer.Name} went back Menu!");
                                                            break;
                                                    }

                                                }
                                                break;
                                        }
                                        break;
                                    case ConsoleKey.Backspace:
                                        Log.Information("User back to Start Menu");
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
                                string tempPhone, tempAge;
                                Azerbaijan tempCity = default;
                                switch (key.Key)
                                {
                                    case ConsoleKey.Enter:
                                        switch (index)
                                        {
                                            case 0:
                                                bool checkWorkerSignUp = false;
                                                while (!checkWorkerSignUp)
                                                {
                                                    Console.Clear();
                                                    Worker newWorker = null;
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

                                                        Console.Write("Enter City: ");
                                                        if (!Enum.TryParse(Console.ReadLine(), out tempCity))
                                                            throw new ArgumentException("Wrong City Choice pls look our city list");

                                                        Console.Write("Enter Phone number: ");
                                                        tempPhone = Console.ReadLine();


                                                        Console.Write("Enter Age: ");
                                                        tempAge = Console.ReadLine();
                                                        if (!byte.TryParse(tempAge, out byte result))
                                                            throw new InvalidCastException("Invalid Cast");

                                                        newWorker = new(tempName, tempSurname, tempCity, tempPhone, result);


                                                        if (workers.Any(w => w.Name == newWorker.Name && w.Surname== newWorker.Surname))
                                                        {
                                                            checkWorkerSignUp = true;
                                                            checkSignUp = true;
                                                            throw new ArgumentException("You already have an account! Pls Login!");
                                                        }
                                                        else
                                                        {
                                                            Console.Clear();
                                                            checkWorkerSignUp = true;
                                                            checkSignUp = true;
                                                            workers.Add(newWorker);
                                                            JSONSerializeMethod(workerDirectoryInfo, workers, "Workers");
                                                            Console.WriteLine("New Accaunt successfuly created!");
                                                            Thread.Sleep(1500);
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine(ex.Message);
                                                        Thread.Sleep(1500);
                                                    }
                                                }
                                                break;
                                            case 1:
                                                bool checkEmployerSignUp = false;
                                                while (!checkEmployerSignUp)
                                                {
                                                    Console.Clear();
                                                    Employer newEmployer = null;
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

                                                        Console.Write("Enter City: ");
                                                        if (!Enum.TryParse(Console.ReadLine(), out tempCity))
                                                            throw new ArgumentException("Wrong City Choice pls look our city list");


                                                        Console.Write("Enter Phone number: ");
                                                        tempPhone = Console.ReadLine();


                                                        Console.Write("Enter Age: ");
                                                        tempAge = Console.ReadLine();
                                                        if (!byte.TryParse(tempAge, out byte result))
                                                            throw new InvalidCastException("Invalid Cast");

                                                        newEmployer = new(tempName, tempSurname, tempCity, tempPhone, result);


                                                        if (employers.Any(e => e.Name == newEmployer.Name && e.Surname== newEmployer.Surname))
                                                        {
                                                            checkWorkerSignUp = true;
                                                            checkSignUp = true;
                                                            throw new ArgumentException("You already have an account! Pls Login!");
                                                        }
                                                        else
                                                        {
                                                            Console.Clear();
                                                            checkEmployerSignUp = true;
                                                            checkSignUp = true;
                                                            employers.Add(newEmployer);
                                                            JSONSerializeMethod(employerDirectoryInfo, employers, "Employers");
                                                            Console.WriteLine("New Accaunt successfuly created!");
                                                            Thread.Sleep(1500);
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine(ex.Message);
                                                        Thread.Sleep(1500);
                                                    }
                                                }

                                                break;
                                        }
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
                    Console.Clear();
                    Console.WriteLine("GOOD BYE!");
                    Thread.Sleep(1500);
                    break;
            }





        }




    }
}