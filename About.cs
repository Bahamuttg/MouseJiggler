using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace MouseJiggler
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            List<AssemblyName> Assys = new List<AssemblyName>();
            foreach (Assembly Assy in AppDomain.CurrentDomain.GetAssemblies().OrderBy(X => X.FullName))
            {
                try
                {
                    AssemblyName Name = AssemblyName.GetAssemblyName(Assy.Location);
                    if (!Name.Name.StartsWith("System"))
                        Assys.Add(Name);
                }
                catch { }
            }
            dataGridView1.DataSource = Assys;
            lblVersion.Text = $"Version: { Assys.First(X => X.FullName == this.GetType().Assembly.FullName).Version.ToString()}";
        }

        private void About_Load(object sender, EventArgs e)
        {

        }

        private void llGPL3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                llGPL3.LinkVisited = true;
                System.Diagnostics.Process.Start(llGPL3.Text);
            }
            catch { }
        }
    }
}
