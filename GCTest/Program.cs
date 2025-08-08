using System;

public class SomeGarbage
{
    public static string name = "SomeGarbage";
}

public class FooClass : IDisposable
{
    private bool disposed = false;

    public static string name = "FooClass";

    public int age { get; set; }

    public List<int> array { get; set; }

    public SomeGarbage garbage { get; set; }

    public FooClass()
    {
        age = 42;
        array = new List<int> { 1, 2, 3 };
        garbage = new SomeGarbage();
    }

    public void Dispose()
    {
        array.Clear();
        garbage = null;
        disposed = true;
        GC.SuppressFinalize(this);
    }

    ~FooClass()
    {
        Console.WriteLine("In the finalizer");
        if (!disposed)
        {
            Dispose();
        }
    }

    public string GetName()
    {
        return name;
    }
}

public class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        CreateFoo();

        Console.WriteLine("Forcing garbage collection...");
        GC.Collect();
        GC.WaitForPendingFinalizers();

        Console.WriteLine("Garbage collection completed.");
    }

    public static void CreateFoo()
    {
        using (var foo = new FooClass())
        {
            Console.WriteLine(foo.age);
            Console.WriteLine(foo.GetName());
            Console.WriteLine(foo.garbage);
        }
    }
}