using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTimer;

internal class TimeTracker
{

    private DateTime TimeStart = DateTime.Now; // to show a rising time FROM a start point
    private DateTime TimeNow = DateTime.Now; // changes as timer runs
    private DateTime TimeEnd = DateTime.Now; // to show a falling time TO a target time
    public TimeSpan TimeOnClock => CountingUpward ? TimeNow - TimeStart : TimeEnd - TimeNow;
    private bool IsRunning = true;

    private bool _upward = true;
    public bool CountingUpward
    {
        get => _upward;
        set
        {
            _upward = value;
            if (_upward)
            {
                // used to be counting down (toward a future date)
                // keep the same time on the clock
                TimeSpan timeOnClock = TimeEnd - TimeNow;
                TimeStart = TimeNow - timeOnClock;
            }
            else
            {
                // used to be counting up (from a past date)
                TimeSpan timeOnClock = TimeNow - TimeStart;
                TimeEnd = TimeNow + timeOnClock;
            }
        }
    }

    public int Minutes => (int)TimeOnClock.TotalMinutes;
    public int Seconds => TimeOnClock.Seconds;

    public void Set(int seconds)
    {
        if (CountingUpward)
            TimeStart = TimeNow.AddSeconds(-seconds);
        else
            TimeEnd = TimeNow.AddSeconds(seconds);
    }

    public void Stop()
    {
        IsRunning = false;
    }

    public void Start()
    {
        if (CountingUpward)
        {
            TimeStart = DateTime.Now - TimeOnClock;
            TimeNow = DateTime.Now;
        }
        else
        {
            TimeEnd = DateTime.Now + TimeOnClock;
            TimeNow = DateTime.Now;
        }
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
        TimeNow = DateTime.Now;
        TimeStart = TimeNow;
        TimeEnd = TimeNow;
    }

    public void Restart()
    {
        Reset();
        IsRunning = true;
    }

    public void UpdateTime()
    {
        if (IsRunning)
            TimeNow = DateTime.Now;
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        if (TimeOnClock.TotalSeconds < 0)
            sb.Append($"-");
        sb.Append($"{Math.Abs(TimeOnClock.Hours):00}:");
        sb.Append($"{Math.Abs(TimeOnClock.Minutes):00}:");
        sb.Append($"{Math.Abs(TimeOnClock.Seconds):00}.");
        sb.Append($"{Math.Abs(TimeOnClock.Milliseconds / 10):00}");
        return sb.ToString();
    }
}
