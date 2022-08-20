using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTimer;

internal class TimeTracker
{

    private DateTime Time1 = DateTime.Now; // fixed time (start or target)
    private DateTime Time2 = DateTime.Now; // changes as timer runs
    public TimeSpan Elapsed => Time2 - Time1;
    private bool IsRunning = true;

    public int Minutes => (int)Elapsed.TotalMinutes;
    public int Seconds => Elapsed.Seconds;

    public void Set(int seconds)
    {
        Time1 = Time2.AddSeconds(-seconds);
    }

    public void Stop()
    {
        IsRunning = false;
    }

    public void Start()
    {
        Time1 = DateTime.Now - Elapsed;
        Time2 = DateTime.Now;
        IsRunning = true;
    }

    public void Pause()
    {
        if (IsRunning)
            Stop();
        else
            Start();
    }

    public void Reset()
    {
        IsRunning = false;
        Time1 = DateTime.Now;
        Time2 = Time1;
    }

    public void Restart()
    {
        Reset();
        IsRunning = true;
    }

    public void UpdateTime()
    {
        if (IsRunning)
        {
            Time2 = DateTime.Now;
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append($"{Elapsed.Hours:00}:");
        sb.Append($"{Elapsed.Minutes:00}:");
        sb.Append($"{Elapsed.Seconds:00}.");
        sb.Append($"{Elapsed.Milliseconds / 10:00}");
        return sb.ToString();
    }
}
