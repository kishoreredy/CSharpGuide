﻿using System.Threading;

class NumbersCounter
{
  public void CountUp()
  {
    try
    {
      Console.WriteLine("Count Up started");
      Thread.Sleep(1000);

      //i = 1 to 100
      for (int i = 1; i <= 100; i++)
      {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"i = {i}, ");
        Thread.Sleep(100); //1000 milliseconds = 1 sec
      }
      Thread.Sleep(1000);
      Console.WriteLine("Count Up completed");
    }
    catch (ThreadInterruptedException ex)
    {
      Console.WriteLine("Count-Up Thread interrupted");
    }

  }


  public void CountDown()
  {
    Console.WriteLine("Count Down started");
    Thread.Sleep(1000);

    //j = 100 to 1
    for (int j = 100; j >= 1; j--)
    {
      System.Console.ForegroundColor = ConsoleColor.Red;
      Console.Write($"j = {j}, ");
      Thread.Sleep(100); //1000 milliseconds = 1 sec
    }

    Thread.Sleep(1000);
    Console.WriteLine("Count Down completed");
  }
}

class Program
{
  static void Main()
  {
    //Get main thread
    Thread mainThread = Thread.CurrentThread;
    mainThread.Name = "Main thread";
    Console.WriteLine(mainThread.Name + " started"); //Main thread

    //Object of NumbersCounter
    NumbersCounter numbersCounter = new NumbersCounter();

    //Create first thread
    ThreadStart threadStart1 = new ThreadStart(numbersCounter.CountUp);
    Thread thread1 = new Thread(threadStart1) { Name = "Count-Up Thread", Priority = ThreadPriority.Highest };

    //Invoke CountUp
    thread1.Start();
    Console.WriteLine($"{thread1.Name} ({thread1.ManagedThreadId}) is {thread1.ThreadState.ToString()}"); //Running



    //Create second thread
    ThreadStart threadStart2 = new ThreadStart(numbersCounter.CountDown);
    Thread thread2 = new Thread(threadStart2) { Name = "Count-Down Thread", Priority = ThreadPriority.BelowNormal };

    //Invoke CountUp
    thread2.Start();
    Console.WriteLine($"{thread2.Name} ({thread2.ManagedThreadId}) is {thread2.ThreadState.ToString()}"); //Running


    //Join
    //thread1.Join();
    //thread2.Join();

    Thread.Sleep(2000); //2 sec delay
    thread1.Interrupt(); //Interrupts the thread1

    Console.WriteLine(mainThread.Name + " completed");
    Console.ReadKey();
  }
}
