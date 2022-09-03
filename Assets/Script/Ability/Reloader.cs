using System.Threading.Tasks;
using UnityEngine;

public class Reloader
{
    private float Timer;

    private int TimerInMillisecond => Mathf.RoundToInt(Timer * 1000);

    public bool Reloaded { get; private set; } = true;
    
    public Reloader(float timer)
    {
        Timer = timer;
    }
    
    public async void StartReload()
    {
        Reloaded = false;
        await Task.Delay(TimerInMillisecond);
        Reloaded = true;
    }
}