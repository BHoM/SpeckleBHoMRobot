using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace SpeckleRobotClient
{
    public partial class SpeckleRobotForm : Form
    {
        public SpeckleRobotForm()
        {
            InitializeComponent();

            SpecklePlugin.InitializeCef();
            SpecklePlugin.InitializeChromium();

            this.Controls.Add(SpecklePlugin.Browser);
        }

        private void SpeckleRobotForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cef.Shutdown();
        }

        private void SpeckleRobotForm_Load(object sender, EventArgs e)
        {

        }
    }
}
