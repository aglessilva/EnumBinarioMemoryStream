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
using Newtonsoft.Json;

namespace RatinhoMouse
{
    public partial class FormWhatsApp : Form
    {
        public FormWhatsApp()
        {
            InitializeComponent();
        }


        List<Perfil> perfil = null;
        private void button1_Click(object sender, EventArgs e)
        {
            //https://api.whatsapp.com/send?phone=5511971359795&text=teste%20de%20mensagem

            groupBox1.Enabled = perfil.Any(cb => cb.HasFlag((Perfil)groupBox1.Tag));
        }

        private void FormWhatsApp_Load(object sender, EventArgs e)
        {
            List<Perfil> lst = new List<Perfil>() {Perfil.Convidado, Perfil.Usuario, Perfil.Manutencao, Perfil.Proprietario, Perfil.SuperUsuario, Perfil.Admin, Perfil.Master};
            comboBox1.DataSource = lst;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            perfil = lst.Take(3).ToList();

            // CARREGAR DOCUMENTO PDF NA MEMORIA E ABRI
             //string caminho = @"C:\Tobamento DTI\70657230000786_18.pdf";
             //byte[] doc = File.ReadAllBytes(caminho);
             //MemoryStream ms = new MemoryStream(doc);
             //pdfViewer1.LoadFromStream(ms);

            // CARREGAR IMAGEM NA MEMORIA E ABIR
            string caminhoImage = @"C:\Users\x194262\Desktop\FotoApto\way-orquidario.jpg";
            byte[] imgs = File.ReadAllBytes(caminhoImage);
            MemoryStream msImagem = new MemoryStream(imgs);
            pictureBox1.Image = Image.FromStream(msImagem);


            Text = "Seu PErfil de Acesso: " + perfil[0].ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Tag = (Perfil)comboBox1.SelectedItem;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2( textBox1);
            form.Show();
        }
    }

    [Flags]
    enum Perfil : int
    {
        Convidado = 1,
        Usuario = 2,
        Manutencao = 4,
        Proprietario = 8,
        SuperUsuario = 16 | Convidado | Usuario,
        Admin =  32 | SuperUsuario | Proprietario,
        Master = 64 | Manutencao | Admin
    }
}
