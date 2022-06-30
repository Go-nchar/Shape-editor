using System;
using System.Windows.Forms;

namespace Editor
{
  internal static class Program
  {
    public static Form1 MainForm;

    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      MainForm = new Form1();
      Application.Run(MainForm);
    }
  }
}
