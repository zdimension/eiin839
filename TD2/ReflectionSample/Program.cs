using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Type type = typeof(MyReflectionClass);
        MethodInfo method = type.GetMethod("MyMethod");
        MyReflectionClass c = new MyReflectionClass();
        string result = (string) method.Invoke(c, null);
        Console.WriteLine(result);
        Console.ReadLine();

    }
}

public class MyReflectionClass
{
    public string MyMethod()
    {
        Console.WriteLine("Call MyMethod 1");
        return "Call MyMethod 2";
    }
}