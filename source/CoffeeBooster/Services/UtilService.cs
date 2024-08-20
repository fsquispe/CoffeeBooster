using System;
using System.Drawing;
using System.IO;

namespace CoffeeBooster.Services
{
  public class UtilService
  {
    public Icon ByteArrayToIcon(byte[] byteArray)
    {
      using (MemoryStream ms = new MemoryStream(byteArray))
      {
        Bitmap bitmap = new Bitmap(ms);
        IntPtr iconHandle = bitmap.GetHicon();
        return Icon.FromHandle(iconHandle);
      }
    }
  }
}
