using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetodosProyectoUno
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(semilla.Text) || semilla.Text.Any(Char.IsLetter) ||
       string.IsNullOrWhiteSpace(veces.Text) || veces.Text.Any(Char.IsLetter)
       )
            {
                MessageBox.Show("Favor de ingresar únicamente valores númericos");
            }
            else
            {
                dataGridView1.Rows.Clear();
                string[] rows = new string[300];
                int s = int.Parse(semilla.Text);
                int n = int.Parse(veces.Text);


                for (int a = 0; a < n; a++)
                {

                    rows[0] = (s + "^2").ToString();
                    string result = (s * s).ToString();
                    rows[1] = result;

                    while (result.Length < 4)
                    {
                     result = result.Insert(0, "0");
                    }
                    if (result.Length % 2 == 0)
                    {
                        while (result.Length != 4)
                        {
                            result = result.Remove(0, 1);
                            result = result.Remove(result.Length - 1, 1);
                        }
                    }
                    else
                    {
                        result = result.Insert(0, "0");
                        while (result.Length != 4)
                        {
                            result = result.Remove(0, 1);
                            result = result.Remove(result.Length - 1, 1);
                        }
                    }
                    rows[2] = result;
                    rows[3] = (float.Parse(result) / 10000).ToString();
                    dataGridView1.Rows.Add(rows);
                    s = Int32.Parse(result);

                }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
    }

