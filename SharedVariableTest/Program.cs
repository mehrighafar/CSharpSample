string result = String.Empty;
var thread1 = new Thread(SharedVariableTest.SetSharedVariable) { Name = "It's Thread1" };
var thread2 = new Thread(() => { result = SharedVariableTest.GetSharedVariable(); }) { Name = "It's Thread2" };

thread1.Start();
thread2.Start();
thread2.Join();// thread2 must complete before print.

Console.WriteLine(result);

public static class SharedVariableTest
{
    public static string sharedVariable = "Default";
    public static object __sharedVariableLock = new object();

    public static void SetSharedVariable()
    {
        lock (__sharedVariableLock)
            sharedVariable = Thread.CurrentThread.Name == null ? "" : Thread.CurrentThread.Name; // or read the data from cache.
    }

    public static string GetSharedVariable()
    {
        lock (__sharedVariableLock)
            return sharedVariable;
    }
}

