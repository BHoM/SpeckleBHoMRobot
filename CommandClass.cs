using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotOM;

namespace SpeckleRobotClient
{
    // Created following the guide here: 
    // https://forums.autodesk.com/t5/robot-structural-analysis-forum/creating-dll-for-api-and-making-it-available-for-robot-menu/m-p/3359313

    [System.Runtime.InteropServices.ComVisibleAttribute(true), System.Runtime.InteropServices.Guid("D6A60BA1-1C40-4E5E-A6A3-293D325ACD17")]
    public class CommandClass : IRobotAddIn
    {
        private IRobotApplication iapp = null;
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
            System.Windows.Forms.MessageBox.Show("Command " + cmd_id.ToString() + " executed.");
        }

        public double GetExpectedVersion()
        {
            return 10;
        }

        public int InstallCommands(RobotCmdList cmd_list)
        {
            cmd_list.New(1, "My Command 1");
            return cmd_list.Count;
        }
    }
}
