using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using S7PROSIMLib;

namespace PumpContol
{
    public partial class Form1 : Form
    {
        S7ProSim sim = new S7ProSim();
        Object bitData = new object();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sim.Connect();

            sim.BeginScanNotify();
            sim.SetScanMode(ScanModeConstants.ContinuousScan);

            //write inputs
            bitData = checkBox_Input_0_0.Checked;
            sim.WriteInputPoint(0, 0, bitData);
            bitData = checkBox_Input_0_1.Checked;
            sim.WriteInputPoint(0, 1, bitData);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //read info
            label_CPUState.Text = sim.GetState();
            lable_scan_mode.Text = sim.GetScanMode().ToString();
            label_connect_status.Text = sim.GetState() != "ERROR"? "Connected": "Disconnected";

            //read outputs
            bitData = false;
            sim.ReadOutputPoint(0, 0, PointDataTypeConstants.S7_Bit, ref bitData);
            checkBox_Output_0_0.Checked = (bool)bitData;
            sim.ReadOutputPoint(0, 1, PointDataTypeConstants.S7_Bit, ref bitData);
            checkBox_Output_0_1.Checked = (bool)bitData;

            //images
            pictureBox1.Image = checkBox_Input_0_0.Checked ? Properties.Resources.pipelineOn : Properties.Resources.pipelineOff2;
            pictureBox2.Image = checkBox_Input_0_1.Checked ? Properties.Resources.pipelineOn : Properties.Resources.pipelineOff2;

            pictureBox3.Image = checkBox_Output_0_0.Checked ? Properties.Resources.NormalWork : Properties.Resources.Alarm;
            pictureBox3.Image = checkBox_Output_0_1.Checked ? Properties.Resources.Alarm : Properties.Resources.NormalWork;
        }

        private void checkBox_Input_0_0_CheckedChanged(object sender, EventArgs e)
        {
            bitData = checkBox_Input_0_0.Checked;
            sim.WriteInputPoint(0, 0, bitData);
        }

        private void checkBox_Input_0_1_CheckedChanged(object sender, EventArgs e)
        {
            bitData = checkBox_Input_0_1.Checked;
            sim.WriteInputPoint(0, 1, bitData);
        }
    }
}
