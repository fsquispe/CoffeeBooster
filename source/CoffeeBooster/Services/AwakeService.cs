using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace CoffeeBooster.Services
{
  public class AwakeService
  {
    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

    private const int KEYEVENTF_EXTENDEDKEY = 0x1;
    private const int KEYEVENTF_KEYUP = 0x2;
    private const byte VK_NUMLOCK = 0x90;
    private const int sleepTimeMilliseconds = 60000;

    private readonly Thread _workerThread;
    private readonly object _lockObject = new object();
    private bool _isActive;

    public AwakeService(bool isActive)
    {
      _workerThread = new Thread(WorkerLoop);
      _isActive = isActive;

      _workerThread.IsBackground = true;
      _workerThread.Start();
    }

    public bool isActive
    {
      get
      {
        lock (_lockObject)
        {
          return _isActive;
        }
      }
      set
      {
        lock (_lockObject)
        {
          _isActive = value;
        }
      }
    }

    private void WorkerLoop()
    {
      while (true)
      {
        lock (_lockObject)
        {
          if (_isActive)
          {
            KeepSystemAwake();
          }
        }

        Thread.Sleep(sleepTimeMilliseconds);
      }
    }

    private void KeepSystemAwake()
    {
      keybd_event(VK_NUMLOCK, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
      keybd_event(VK_NUMLOCK, 0x45, KEYEVENTF_KEYUP, 0);

      keybd_event(VK_NUMLOCK, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
      keybd_event(VK_NUMLOCK, 0x45, KEYEVENTF_KEYUP, 0);
    }
  }
}
