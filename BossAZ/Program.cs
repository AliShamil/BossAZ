

namespace BossAZ;

using BossAZ.Helper;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using User;

internal class Program
{
    static public bool ValidatePhone(string? phone) => Regex.IsMatch(phone!, @"^([0|\+[0-9]{1,5})?([0-9]{10})$");
    static public bool GitlINk(string? link) => Regex.IsMatch(link!, @"((http(s?)://)*([www])*\.|[linkedin])[linkedin/~\-]+\.[a-zA-Z0-9/~\-_,&=\?\.;]+[^\.,\s<]");
    static public bool CheckParse(string[] array)
    {
        foreach (var i in array)
        {
            if (!int.TryParse(i, out int result))
                return false;
        }
        return true;
    }
    static void Main(string[] args)
    {
        //while (true)
        //{

        //    try
        //    {
        //        Console.Clear();
        //        Console.WriteLine("Enter FirstDay of Working(year|month|day): ");
        //        string[] a = Console.ReadLine().Split('|');

        //        if (!CheckParse(a))
        //            throw new InvalidCastException();


        //        DateTime dateTime = new(int.Parse(a[0]), int.Parse(a[1]), int.Parse(a[2]));
        //        Console.WriteLine(dateTime);
        //        Console.ReadKey(false);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Clear();
        //        Console.WriteLine(ex.Message);
        //        Thread.Sleep(1500);
        //        continue;

        //    }
        //}


 
     
    }
}