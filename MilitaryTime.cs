using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'timeConversion' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts STRING s as parameter.
     */

    public static string timeConversion(string s)
    {
        int hh, mm, ss;
        bool am = true;
        int length = s.ToString().Length;
        if(s.ToString().Substring(length - 2).Equals("PM"))
            am = false;
        hh = Convert.ToInt32(s.ToString().Substring(0, 2));
        mm = Convert.ToInt32(s.ToString().Substring(3, 2));
        ss = Convert.ToInt32(s.ToString().Substring(6, 2));
        if(!am && (hh != 12))
            hh += 12;
        if(am && (hh == 12))
            hh -= 12;
        String militaryTime = String.Empty;
        militaryTime = String.Format("{0,2:D2}:{1,2:D2}:{2,2:D2}", hh,mm,ss);
        return militaryTime;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string s = Console.ReadLine();

        string result = Result.timeConversion(s);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
