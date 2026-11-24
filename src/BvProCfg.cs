using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ezx6tools
{
    public partial class BvProCfg : Form
    {
        private static bool opened = false;
        public BvProCfg()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        public static void OpenCfg()
        {
            if (opened) return;
            BvProCfg bvProCfg = new();
            bvProCfg.Show();
            opened = true;
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            opened = false;
        }
    }
}
