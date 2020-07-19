using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RatinhoMouse
{
    public partial class Form2 : Form
    {
        TextBox form;
        Form frm;
        public Form2(TextBox _form)
        {
            InitializeComponent();

            frm = _form.FindForm();

            form = _form;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            form.Text = textBoxForm2.Text;
           
        }
    }
}