using System;

internal class Program
{
    private static void Main(string[] args)
    {
        var type = typeof(MyReflectionClass);
        var method = type.GetMethod("MyMethod");
        var c = new MyReflectionClass();
        var result = (string)method.Invoke(c, null);
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