using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RatinhoMouse
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }


        public class Nanda
        {
            public Nanda()
            {
                CaracteristicaDefinidora = new List<string>();
                FatorRelacionado = new List<string>();
                FatorRisco = new List<string>();
            }
            public string Domnio { get; set; }
            public string DescrDominio { get; set; }
            public string Classe { get; set; }
            public string DesClasse { get; set; }
            public string Codigo { get; set; }
            public string Diagnostico { get; set; }
            public string Definicao { get; set; }
            public List<string> CaracteristicaDefinidora { get; set; }
            public List<string> FatorRelacionado { get; set; }
            public List<string> FatorRisco { get; set; }

        }

        private void Form3_Load(object sender, EventArgs e)
        {

            try
            {
                Nanda objNanda = null;
                List<Nanda> livro = new List<Nanda>();
                string pagina = string.Empty, line = string.Empty;
                List<string> _arrayLinha = null;
                int indice = 0;
                bool isDominio = false, isDefinicao = false, isDefinidora = false, isFatorRelacionado = false, isFatoRisco = false;

              
                using (StreamReader streamReader = new StreamReader(@"C:\!ZONA\NANDA\NANDA2018_2020.txt", true))
                {
                    objNanda = new Nanda();

                    while (!streamReader.EndOfStream)
                    {
                        try
                        {
                            line = streamReader.ReadLine();
                            _arrayLinha = line.Split(' ').ToList();

                            if (_arrayLinha.Any(k => k.Trim().Equals("#")))
                            {
                                isDefinidora = false;
                                isDefinicao = false;
                                isDominio = false;
                                isFatorRelacionado = false;
                                isFatoRisco = false;

                                continue;
                            }
                            if (_arrayLinha.Any(s => s.Trim().Equals("!!")))
                            {
                                isFatorRelacionado = false;
                                isDefinidora = false;
                                isDefinicao = false;
                                isDominio = true;
                                indice = _arrayLinha.FindIndex(i => i.Equals("•"));

                                if (!string.IsNullOrWhiteSpace(objNanda.Codigo))
                                {
                                    if (!livro.Any(l => l.Codigo.Equals(objNanda.Codigo)))
                                    {
                                        objNanda.Definicao = objNanda.Definicao.Trim();
                                        livro.Add(objNanda);
                                    }

                                    objNanda = new Nanda();
                                }

                                objNanda.DescrDominio = string.Join(" ", _arrayLinha.Skip(indice + 1).ToArray());
                                objNanda.Domnio = _arrayLinha[indice - 1];

                                continue;
                            }

                            if (_arrayLinha.Any(s => s.Trim().Equals("**")))
                            {
                                isFatorRelacionado = false;
                                isDefinidora = false;
                                isDominio = false;
                                isDefinicao = true;
                                indice = 0;
                                continue;
                            }

                            if (_arrayLinha.Any(s => s.Trim().Equals("$$")))
                            {
                                isDefinicao = false;
                                isFatorRelacionado = false;
                                isDominio = false;
                                isDefinidora = true;
                                continue;
                            }


                            if (_arrayLinha.Any(s => s.Trim().Equals("@@")))
                            {
                                isDefinidora = false;
                                isDefinicao = false;
                                isDominio = false;
                                isFatoRisco = false;
                                isFatorRelacionado = true;
                                continue;
                            }


                            if (_arrayLinha.Any(s => s.Trim().Equals("??")))
                            {
                                isDefinidora = false;
                                isDefinicao = false;
                                isDominio = false;
                                isFatorRelacionado = false;
                                isFatoRisco = true;
                                continue;
                            }

                            if (isDominio)
                            {
                                if (string.IsNullOrWhiteSpace(objNanda.DesClasse))
                                {
                                    indice = _arrayLinha.FindIndex(i => i.Equals("•"));
                                    objNanda.DesClasse = string.Join(" ", _arrayLinha.Skip(indice + 1).ToArray());
                                    objNanda.Classe = _arrayLinha[indice - 1];
                                    continue;
                                }

                                if (string.IsNullOrWhiteSpace(objNanda.Codigo))
                                {
                                    objNanda.Codigo = _arrayLinha.SingleOrDefault(c => Regex.IsMatch(c, @"^\d{5}$"));

                                    if(objNanda.Codigo ==  null)
                                    {
                                        isDefinidora = false;
                                        isDefinicao = false;
                                        isDominio = false;
                                        isFatorRelacionado = false;
                                        isFatoRisco = false;
                                        objNanda = new Nanda();
                                    }
                                    continue;
                                }

                                if (string.IsNullOrWhiteSpace(objNanda.Diagnostico))
                                {
                                    objNanda.Diagnostico = line;
                                    isDominio = false;
                                    continue;
                                }
                            }

                            if (isDefinicao)
                            {
                                objNanda.Definicao += line + " ";
                                continue;
                            }

                            if (isDefinidora)
                            {
                                objNanda.CaracteristicaDefinidora.Add(line);
                                continue;
                            }

                            if (isFatorRelacionado)
                            {
                                objNanda.FatorRelacionado.Add(line);
                                continue;
                            }

                            if (isFatoRisco)
                            {
                                objNanda.FatorRisco.Add(line);
                                continue;
                            }
                        }
                        catch (Exception exwhile)
                        {
                            int total = livro.Count;
                            throw exwhile;
                        }
                    }
                };

                if (!string.IsNullOrWhiteSpace(objNanda.Codigo))
                {
                    if (!livro.Any(l => l.Codigo.Equals(objNanda.Codigo)))
                    {
                        objNanda.Definicao = objNanda.Definicao.Trim();
                        livro.Add(objNanda);
                    }
                }
            }
            catch (Exception exNanda)
            {

                throw exNanda;
            }
        }
    }
}
