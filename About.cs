/*
    Mouse Jiggler
    Copyright © 2018 Thomas George

    This file is part of Mouse Jiggler.

    Mouse Jiggler is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Mouse Jiggler is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Mouse Jiggler.  If not, see <http://www.gnu.org/licenses/>.
*/
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
