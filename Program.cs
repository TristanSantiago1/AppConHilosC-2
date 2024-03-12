namespace AppConHilos2;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Thread currentThread= Thread.CurrentThread;
        currentThread.Name = "HiloPrioncipal";
        currentThread.Priority = ThreadPriority.Lowest;
        currentThread.IsBackground = false;

        Console.WriteLine("Thread id : {0}", currentThread.ManagedThreadId);
        Console.WriteLine("Thread Name : {0}", currentThread.Name);
        Console.WriteLine("Thread State : {0}", currentThread.ThreadState);
        Console.WriteLine("Thread es backgorund : {0}", currentThread.IsBackground);
         Console.WriteLine("Thread prioirdad : {0}", currentThread.Priority);
          Console.WriteLine("CUlture : {0}", currentThread.CurrentCulture.Name);
         Console.WriteLine("UI Culture: {0}", currentThread.CurrentUICulture.Name);

        Thread workerThread = new Thread(new ParameterizedThreadStart(Print));
        workerThread.Name = "hILO PRINT";
        CancellationTokenSource cts = new CancellationTokenSource();
        workerThread.Start(cts.Token);


        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"Pirncipal thread: {i}");
            Thread.Sleep(2000);
        }

        if(workerThread.IsAlive){
            cts.Cancel();
        }


        static void Print(object? obj){
            if(obj == null){
                return;
            }
            CancellationToken token = (CancellationToken) obj;

            Thread currentThread= Thread.CurrentThread;
            currentThread.Priority = ThreadPriority.Highest;
            currentThread.IsBackground = false;

            Console.WriteLine("Thread id : {0}", currentThread.ManagedThreadId);
            Console.WriteLine("Thread Name : {0}", currentThread.Name);
            Console.WriteLine("Thread State : {0}", currentThread.ThreadState);
            Console.WriteLine("Thread es backgorund : {0}", currentThread.IsBackground);
            Console.WriteLine("Thread prioirdad : {0}", currentThread.Priority);
            Console.WriteLine("CUlture : {0}", currentThread.CurrentCulture.Name);
            Console.WriteLine("UI Culture: {0}", currentThread.CurrentUICulture.Name);
            Console.WriteLine();
            for (int i = 0; i < 20; i++)
            {
                if(token.IsCancellationRequested){
                    Console.WriteLine("En la iteracion{0}, la cancelacion ha sidos solicitada...", i);
                    break;
                }
                Console.WriteLine($"Print thread: {i}");
                Thread.Sleep(1000);
            }
        }




    }
}