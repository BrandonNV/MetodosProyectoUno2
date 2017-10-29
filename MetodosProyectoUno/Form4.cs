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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(semilla.Text) || semilla.Text.Any(Char.IsLetter) ||
                string.IsNullOrWhiteSpace(multiplicador.Text) || multiplicador.Text.Any(Char.IsLetter) ||
                string.IsNullOrWhiteSpace(modulo.Text) || modulo.Text.Any(Char.IsLetter) ||
                string.IsNullOrWhiteSpace(veces.Text) || veces.Text.Any(Char.IsLetter) ||  listBox2.SelectedIndex == -1
                )
            {
                MessageBox.Show("Favor de ingresar únicamente valores númericos y un nivel de significancia");

            }
            else {
                dataGridView1.Rows.Clear();
                string[] rows = new string[300];
                int s = int.Parse(semilla.Text);
                int mu = int.Parse(multiplicador.Text);
                int mo = int.Parse(modulo.Text);
                int n = int.Parse(veces.Text);
                double convert = 0;
                string temp;
                //KOLMOGROV
                double[] aleatorios = new double[300];
                double[] dplus = new double[300];
                double max = 0;
                double maxp = 0;
                double maxm = 0;
                double[] dminus = new double[300];
                double tabValue = 0;
                double[,] ktable = KolmogrovTable(n);


                //20%,10%,5%,1%
                //FIN KOLMOGROV
                if ((s >= 0 && mu >= 0 && mo >= 0) && (mo > mu && mo > s))
                {
                    for (int a = 0; a < n; a++)
                    {
                        rows[0] = s.ToString();
                        convert = Convert.ToDouble(mu * s);
                        temp = (convert / mo).ToString() + ".";
                        string[] operacion = temp.Split('.');
                        if (operacion[1] != "")
                        {
                            operacion[1] = " + ." + operacion[1];
                        }
                        rows[1] = operacion[0] + "" + operacion[1];
                        rows[2] = (convert % mo).ToString();
                        rows[3] = ((convert % mo) / mo).ToString();
                        //KOLMOGROV
                        aleatorios[a] = (convert % mo) / mo;
                        dataGridView1.Rows.Add(rows);
                        s = (mu * s) % mo;
                    }
                    double t;
                    for (int p = 0; p <= n - 2; p++)
                    {
                        for (int x = 0; x <= n - 2; x++)
                        {
                            if (aleatorios[x] > aleatorios[x + 1])
                            {
                                t = aleatorios[x + 1];
                                aleatorios[x + 1] = aleatorios[x];
                                aleatorios[x] = t;
                            }
                        }
                    }

                    Console.WriteLine("Sorted Elements");

                    for (int b = 0; b < n; b++)
                    {
                        Console.WriteLine(aleatorios[b]);
                        dplus[b] = ((b + 1) / n) - aleatorios[b];
                        dminus[b] = aleatorios[b] - (b / n);

                        if (b == 0)
                        {
                            maxp = dplus[b];
                            maxm = dminus[b];

                        }
                        else
                        {
                            if (dplus[b] > maxp) { maxp = dplus[b]; }
                            if (dminus[b] > maxm) { maxm = dminus[b]; }

                        }
                        //FIN KOLMOGROV


                    }
                    //KOLMOGROV
                    string sig = listBox2.SelectedItem.ToString();
                    int sigvalue = Int32.Parse(sig);
                    if (n <= 50)
                    {
                        if (sigvalue == 20) { tabValue = ktable[n, 0]; }
                        if (sigvalue == 10) { tabValue = ktable[n, 1]; }
                        if (sigvalue == 5) { tabValue = ktable[n, 2]; }
                        if (sigvalue == 2) { tabValue = ktable[n, 3]; }
                    }
                    else
                    {
                        if (sigvalue == 20) { tabValue = ktable[51, 0]; }
                        if (sigvalue == 10) { tabValue = ktable[51, 1]; }
                        if (sigvalue == 5) { tabValue = ktable[51, 2]; }
                        if (sigvalue == 2) { tabValue = ktable[51, 3]; }

                    }
                    if (maxp > maxm)
                    {
                        label9.Text = "MAX(D+,D-) " + maxp.ToString();
                        max = maxp;
                    }
                    else
                    {
                        label9.Text = "MAX(D+,D-) " + maxm.ToString();
                        max = maxm;
                    }
                    label10.Text = "Valor de la tabla " + tabValue.ToString();
                    if (max < tabValue)
                    {
                        label11.Text = "La hípótesis no es rechazada";
                    }
                    else { label11.Text = "La hípótesis  es rechazada"; }

                    //FIN KOLMOGROV
                }
                else {
                    MessageBox.Show("Valores númericos NO cumplen los requisitos:" +
                                    "\n\r -Semilla, Multiplicador, Módulo >= 0" +
                                    "\n\r -Módulo > Multiplicador" +
                                    "\n\r -Módulo > Semilla");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void semilla_TextChanged(object sender, EventArgs e)
        {

        }

        private void multiplicador_TextChanged(object sender, EventArgs e)
        {

        }

        private void modulo_TextChanged(object sender, EventArgs e)
        {

        }

        private void veces_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private double[,] KolmogrovTable(int n)
        {
            double[,] ktable = new double[52, 4];

            ktable[1, 0] = 0.90000;
            ktable[1, 1] = 0.95000;
            ktable[1, 2] = 0.97500;
            ktable[1, 3] = 0.99000;

            ktable[2, 0] = 0.68337;
            ktable[2, 1] = 0.77639;
            ktable[2, 2] = 0.84189;
            ktable[2, 3] = 0.90000;

            ktable[3, 0] = 0.56481;
            ktable[3, 1] = 0.63604;
            ktable[3, 2] = 0.70760;
            ktable[3, 3] = 0.78456;

            ktable[4, 0] = 0.49265;
            ktable[4, 1] = 0.56522;
            ktable[4, 2] = 0.62394;
            ktable[4, 3] = 0.68887;


            ktable[5, 0] = 0.44698;
            ktable[5, 1] = 0.50945;
            ktable[5, 2] = 0.56328;
            ktable[5, 3] = 0.62718;


            ktable[6, 0] = 0.41037;
            ktable[6, 1] = 0.46799;
            ktable[6, 2] = 0.51926;
            ktable[6, 3] = 0.57741;

            ktable[7, 0] = 0.38148;
            ktable[7, 1] = 0.43607;
            ktable[7, 2] = 0.48342;
            ktable[7, 3] = 0.53844;


            ktable[8, 0] = 0.35831;
            ktable[8, 1] = 0.40962;
            ktable[8, 2] = 0.45427;
            ktable[8, 3] = 0.50654;


            ktable[9, 0] = 0.33910;
            ktable[9, 1] = 0.38746;
            ktable[9, 2] = 0.43001;
            ktable[9, 3] = 0.47960;

            ktable[10, 0] = 0.32260;
            ktable[10, 1] = 0.36866;
            ktable[10, 2] = 0.40925;
            ktable[10, 3] = 0.45562;

            ktable[11, 0] = 0.30829;
            ktable[11, 1] = 0.35242;
            ktable[11, 2] = 0.39122;
            ktable[11, 3] = 0.43670;

            ktable[12, 0] = 0.29577;
            ktable[12, 1] = 0.33815;
            ktable[12, 2] = 0.37543;
            ktable[12, 3] = 0.41918;

            ktable[13, 0] = 0.28470;
            ktable[13, 1] = 0.32549;
            ktable[13, 2] = 0.36143;
            ktable[13, 3] = 0.40362;

            ktable[14, 0] = 0.27481;
            ktable[14, 1] = 0.31417;
            ktable[14, 2] = 0.34890;
            ktable[14, 3] = 0.38970;

            ktable[15, 0] = 0.26589;
            ktable[15, 1] = 0.30397;
            ktable[15, 2] = 0.33750;
            ktable[15, 3] = 0.37713;

            ktable[16, 0] = 0.25778;
            ktable[16, 1] = 0.29472;
            ktable[16, 2] = 0.32733;
            ktable[16, 3] = 0.36571;

            ktable[17, 0] = 0.25039;
            ktable[17, 1] = 0.28627;
            ktable[17, 2] = 0.31796;
            ktable[17, 3] = 0.35528;

            ktable[18, 0] = 0.24360;
            ktable[18, 1] = 0.27851;
            ktable[18, 2] = 0.30936;
            ktable[18, 3] = 0.34569;

            ktable[19, 0] = 0.23735;
            ktable[19, 1] = 0.27136;
            ktable[19, 2] = 0.30143;
            ktable[19, 3] = 0.33685;

            ktable[20, 0] = 0.23156;
            ktable[20, 1] = 0.26473;
            ktable[20, 2] = 0.29408;
            ktable[20, 3] = 0.32866;

            ktable[21, 0] = 0.22517;
            ktable[21, 1] = 0.25858;
            ktable[21, 2] = 0.28724;
            ktable[21, 3] = 0.32104;

            ktable[22, 0] = 0.22115;
            ktable[22, 1] = 0.25283;
            ktable[22, 2] = 0.28087;
            ktable[22, 3] = 0.31394;

            ktable[23, 0] = 0.21646;
            ktable[23, 1] = 0.24746;
            ktable[23, 2] = 0.2749;
            ktable[23, 3] = 0.30728;

            ktable[24, 0] = 0.21205;
            ktable[24, 1] = 0.24242;
            ktable[24, 2] = 0.26931;
            ktable[24, 3] = 0.30104;

            ktable[25, 0] = 0.20790;
            ktable[25, 1] = 0.23768;
            ktable[25, 2] = 0.26404;
            ktable[25, 3] = 0.29518;

            ktable[26, 0] = 0.20399;
            ktable[26, 1] = 0.23320;
            ktable[26, 2] = 0.25908;
            ktable[26, 3] = 0.28962;

            ktable[27, 0] = 0.20030;
            ktable[27, 1] = 0.22898;
            ktable[27, 2] = 0.25438;
            ktable[27, 3] = 0.28438;

            ktable[28, 0] = 0.19680;
            ktable[28, 1] = 0.22497;
            ktable[28, 2] = 0.24993;
            ktable[28, 3] = 0.27942;

            ktable[29, 0] = 0.19348;
            ktable[29, 1] = 0.22117;
            ktable[29, 2] = 0.24571;
            ktable[29, 3] = 0.27471;

            ktable[30, 0] = 0.19032;
            ktable[30, 1] = 0.21756;
            ktable[30, 2] = 0.24170;
            ktable[30, 3] = 0.27023;

            ktable[31, 0] = 0.18732;
            ktable[31, 1] = 0.21412;
            ktable[31, 2] = 0.23788;
            ktable[31, 3] = 0.26596;

            ktable[32, 0] = 0.18445;
            ktable[32, 1] = 0.21085;
            ktable[32, 2] = 0.23424;
            ktable[32, 3] = 0.26189;

            ktable[33, 0] = 0.18171;
            ktable[33, 1] = 0.20771;
            ktable[33, 2] = 0.23076;
            ktable[33, 3] = 0.25801;

            ktable[34, 0] = 0.17909;
            ktable[34, 1] = 0.21472;
            ktable[34, 2] = 0.22743;
            ktable[34, 3] = 0.25429;

            ktable[35, 0] = 0.17659;
            ktable[35, 1] = 0.20185;
            ktable[35, 2] = 0.22425;
            ktable[35, 3] = 0.25073;

            ktable[36, 0] = 0.17418;
            ktable[36, 1] = 0.19910;
            ktable[36, 2] = 0.22119;
            ktable[36, 3] = 0.24732;

            ktable[37, 0] = 0.17188;
            ktable[37, 1] = 0.19646;
            ktable[37, 2] = 0.21826;
            ktable[37, 3] = 0.24404;

            ktable[38, 0] = 0.16966;
            ktable[38, 1] = 0.19392;
            ktable[38, 2] = 0.21544;
            ktable[38, 3] = 0.24089;

            ktable[39, 0] = 0.16753;
            ktable[39, 1] = 0.19148;
            ktable[39, 2] = 0.21273;
            ktable[39, 3] = 0.23785;

            ktable[40, 0] = 0.16547;
            ktable[40, 1] = 0.18913;
            ktable[40, 2] = 0.21012;
            ktable[40, 3] = 0.23494;

            ktable[41, 0] = 0.16349;
            ktable[41, 1] = 0.18687;
            ktable[41, 2] = 0.20760;
            ktable[41, 3] = 0.23213;

            ktable[42, 0] = 0.16158;
            ktable[42, 1] = 0.18468;
            ktable[42, 2] = 0.20517;
            ktable[42, 3] = 0.22941;

            ktable[43, 0] = 0.15974;
            ktable[43, 1] = 0.18257;
            ktable[43, 2] = 0.20283;
            ktable[43, 3] = 0.22679;

            ktable[44, 0] = 0.15795;
            ktable[44, 1] = 0.18051;
            ktable[44, 2] = 0.20056;
            ktable[44, 3] = 0.22426;

            ktable[45, 0] = 0.15623;
            ktable[45, 1] = 0.17856;
            ktable[45, 2] = 0.19837;
            ktable[45, 3] = 0.22181;

            ktable[46, 0] = 0.15457;
            ktable[46, 1] = 0.17665;
            ktable[46, 2] = 0.19625;
            ktable[46, 3] = 0.21944;

            ktable[47, 0] = 0.15295;
            ktable[47, 1] = 0.17481;
            ktable[47, 2] = 0.19420;
            ktable[47, 3] = 0.21715;

            ktable[48, 0] = 0.15139;
            ktable[48, 1] = 0.17301;
            ktable[48, 2] = 0.19221;
            ktable[48, 3] = 0.21493;

            ktable[49, 0] = 0.14987;
            ktable[49, 1] = 0.17128;
            ktable[49, 2] = 0.19028;
            ktable[49, 3] = 0.21281;

            ktable[50, 0] = 0.14840;
            ktable[50, 1] = 0.16959;
            ktable[50, 2] = 0.18841;
            ktable[50, 3] = 0.21068;

            ktable[51, 0] = 1.07 / Math.Sqrt(n);
            ktable[51, 1] = 1.22 / Math.Sqrt(n);
            ktable[51, 2] = 1.36 / Math.Sqrt(n);
            ktable[51, 3] = 1.52 / Math.Sqrt(n);



            return ktable;

        }
    }
}
