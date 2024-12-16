using System;
using System.Threading;

// Delegate for event handling
public delegate void StopwatchEventHandler(string message);

public class Stopwatch
{
    // Fields
    private TimeSpan timeElapsed;
    private bool isRunning;
    private Timer timer;

    // Events
    public event StopwatchEventHandler OnStarted;
    public event StopwatchEventHandler OnStopped;
    public event StopwatchEventHandler OnReset;

    // Constructor
    public Stopwatch()
    {
        timeElapsed = TimeSpan.Zero;
        isRunning = false;
    }

    // Start method
    public void Start()
    {
        if (!isRunning)
        {
            isRunning = true;
            timer = new Timer(Tick, null, 0, 1000);
            OnStarted?.Invoke("Stopwatch Started!");
        }
    }

    // Stop method
    public void Stop()
    {
        if (isRunning)
        {
            isRunning = false;
            timer?.Dispose();
            OnStopped?.Invoke("Stopwatch Stopped!");
        }
    }

    // Reset method
    public void Reset()
    {
        Stop();
        timeElapsed = TimeSpan.Zero;
        OnReset?.Invoke("Stopwatch Reset!");
    }

    // Tick method to increment time
    private void Tick(object? state)
    {
        if (isRunning)
        {
            timeElapsed = timeElapsed.Add(TimeSpan.FromSeconds(1));
            Console.Clear();
            Console.WriteLine($"Time Elapsed: {timeElapsed}");
        }
    }
}

public class Program
{
    public static void Main()
    {
        Stopwatch stopwatch = new Stopwatch();

        // Subscribe to events
        stopwatch.OnStarted += message => Console.WriteLine(message);
        stopwatch.OnStopped += message => Console.WriteLine(message);
        stopwatch.OnReset += message => Console.WriteLine(message);

        Console.WriteLine("Stopwatch Application");
        Console.WriteLine("Commands: S to Start, T to Stop, R to Reset, Q to Quit");

        bool exit = false;

        while (!exit)
        {
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.S:
                    stopwatch.Start();
                    break;
                case ConsoleKey.T:
                    stopwatch.Stop();
                    break;
                case ConsoleKey.R:
                    stopwatch.Reset();
                    break;
                case ConsoleKey.Q:
                    exit = true;
                    stopwatch.Stop();
                    break;
                default:
                    Console.WriteLine("Invalid command. Use S, T, R, or Q.");
                    break;
            }
        }
    }
}
