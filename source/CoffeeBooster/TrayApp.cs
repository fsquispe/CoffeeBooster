using System;
using System.Drawing;
using System.Windows.Forms;
using CoffeeBooster.Services;
using CoffeeBooster.Properties;

namespace CoffeeBooster
{
  public class TrayApp : ApplicationContext
  {
    private Icon _activeIcon;
    private Icon _inactiveIcon;
    private NotifyIcon _trayIcon;
    private bool _isActive;

    private readonly UtilService _utilService;
    private readonly AwakeService _awakeService;

    public TrayApp(UtilService utilService, AwakeService awakeService)
    {
      _utilService = utilService;
      _awakeService = awakeService;
      _isActive = false;

      OnLoad();
    }

    private void OnLoad()
    {
      _activeIcon = _utilService.ByteArrayToIcon(Resources.cup_active);
      _inactiveIcon = _utilService.ByteArrayToIcon(Resources.cup_empty);

      _trayIcon = new NotifyIcon()
      {
        Text = "CoffeeBooster",
        Icon = _activeIcon,
        ContextMenu = new ContextMenu(
            new MenuItem[] {
              new MenuItem("", ToggleState),
              new MenuItem("Exit", Exit)
            }
          ),
        Visible = true,
      };

      _trayIcon.DoubleClick += new EventHandler(ToggleState);

      ToggleState(null, null);
    }

    private void ToggleState(object sender, EventArgs e)
    {
      _isActive = !_isActive;
      _trayIcon.Icon = _isActive ? _activeIcon : _inactiveIcon;
      _trayIcon.Text = $"CoffeeBooster: {(_isActive ? "Active" : "Idle")}";
      var menuItem = _trayIcon.ContextMenu.MenuItems[0];
      menuItem.Text = _isActive ? "Disable" : "Enable";
    }

    private void Exit(object sender, EventArgs e)
    {
      _trayIcon.Visible = false;
      Application.Exit();
    }
  }
}
