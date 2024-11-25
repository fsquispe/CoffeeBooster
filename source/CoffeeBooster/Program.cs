using System;
using System.Diagnostics;
using System.Windows.Forms;
using CoffeeBooster.Services;

namespace CoffeeBooster
{
  internal static class Program
  {
    [STAThread]
    static void Main()
    {
      string processName = Process.GetCurrentProcess().ProcessName;
      if (Process.GetProcessesByName(processName).Length > 1)
        return;

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      UtilService utilService = new UtilService();
      AwakeService awakeService = new AwakeService(false);
      Application.Run(new TrayApp(utilService, awakeService));
    }
  }
}
