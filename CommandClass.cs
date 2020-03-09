using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotOM;
using SpecklePopup;
using SpeckleUiBase;
using SpeckleRobotClient.UI;

namespace SpeckleRobotClient
{
    // Created following the guide here: 
    // https://forums.autodesk.com/t5/robot-structural-analysis-forum/creating-dll-for-api-and-making-it-available-for-robot-menu/m-p/3359313

    [System.Runtime.InteropServices.ComVisibleAttribute(true), System.Runtime.InteropServices.Guid("D6A60BA1-1C40-4E5E-A6A3-293D325ACD17")]
    public class CommandClass : IRobotAddIn
    {
        private IRobotApplication iapp = null;
        public static SpeckleUiWindow SpeckleWindow;
        public static bool Launched = false;

        public bool Connect(RobotApplication robot_app, int add_in_id, bool first_time)
        {
            iapp = robot_app;
            return true;
        }

        public bool Disconnect()
        {
            iapp = null;
            return true;
        }

        public void DoCommand(int cmd_id)
        {
            //// open login page using winform
            //SpeckleRobotForm form = new SpeckleRobotForm();
            //form.Show();

            //// open account popup using SpecklePopup
            //var signInWindow = new SpecklePopup.SignInWindow(true);
            //signInWindow.ShowDialog();

            // Create a new speckle binding instance
            

            // Initialise the window
#if DEBUG
            try
            {
                var bindings = new SpeckleUiBindingsRobot(iapp);
                bindings.InitialiseBinding();
                SpeckleWindow = new SpeckleUiWindow(bindings, @"http://localhost:8080/");
            }
            catch (MissingMethodException e)
            {
                Console.WriteLine(e.Message);
            }

#else
            var bindings = new SpeckleUiBindingsRobot(iapp);
            bindings.InitialiseBinding();
            SpeckleWindow = new SpeckleUiWindow(bindings, @"https://matteo-dev.appui.speckle.systems/#/"); // On release, default to the latest ci-ed version from https://appui.speckle.systems
#endif
            SpeckleUiBindingsRobot.SpeckleWindow = SpeckleWindow;
            var helper = new System.Windows.Interop.WindowInteropHelper(SpeckleWindow);
            helper.Owner = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;

            // TODO: find a way to set the parent/owner of the speckle window so it minimises/maximises etc. together with the revit window.
            SpeckleWindow.Show();
            SpeckleWindow.Focus();
            Launched = true;
        }
        public double GetExpectedVersion()
        {
            return 10;
        }

        public int InstallCommands(RobotCmdList cmd_list)
        {
            cmd_list.New(1, "Account");
            return cmd_list.Count;
        }
    }
}
