using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public sealed class Screensaver
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string argument = null, windowHandleArgument = null;
            int windowHandle;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Parse command line arguments
            if (args.Length > 0)
            {
                argument = args[0].ToLower().Trim();

                // handle colon cases ex. /c:123 or /p:123
                if (argument.Length == 2)
                {
                    argument = argument.Substring(1);
                    if (args.Length > 1)
                    {
                        // get window handle not passed in colon format
                        windowHandleArgument = args[1];
                    }
                }
                else if (argument.Length > 2)
                {
                    windowHandleArgument = argument.Substring(3).Trim();
                    argument = argument.Substring(1, 2);
                }

                // Try for the window handle else just pass the argument
                if (int.TryParse(windowHandleArgument, out windowHandle))
                    new Screensaver(argument[0], windowHandle);
                else
                    new Screensaver(argument[0]);
            }
            else
            {
                new Screensaver();
            }
        }

        public Screensaver(char? argument = 'c', long? windowHandle = 0)
        {
            switch (argument)
            {
                case 'c':
                    // Show the screensaver configuration dialog box
                    MessageBox.Show(String.Format("Configuration not implemented"),
                        "Screen Saver", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 'p':
                    // Show the screensaver in the screensaver selection dialog box
                    if (windowHandle == 0)
                    {
                        MessageBox.Show(String.Format("Expected window handle"),
                            "Screen Saver", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }

                    IntPtr previewWindowHandle = new IntPtr(windowHandle.Value);
                    Application.Run(new GameOfLifeForm(previewWindowHandle));
                    break;
                case 's':
                    // Show the screensaver full-screen 
                    ShowScreenSaver();
                    Application.Run();
                    break;
                default:
                    // invalid argument show error message
                    MessageBox.Show(String.Format("Invalid argument {0}"),
                        "Screen Saver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }
        }

        private void ShowScreenSaver()
        {
            foreach (Screen screen in Screen.AllScreens)
                new GameOfLifeForm(screen.Bounds).Show();
        }

    }
}
