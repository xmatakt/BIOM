using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BimSystRating
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var directory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Faces"));

            var content = Directory.GetDirectories(directory);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
