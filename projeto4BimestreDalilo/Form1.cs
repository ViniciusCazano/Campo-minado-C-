using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projeto4BimestreDalilo
{
    public partial class Form1 : Form
    {
        int linha = 5;
        int coluna = 5;
        int bandeiras = 0;
        int[,] matriz = new int[2, 2];
        public Form1()
        {
            InitializeComponent();
            dataGridView1.Columns.Add("columUm", "");
            dataGridView1.Columns.Add("columDois", "");
            dataGridView1.Columns.Add("columTres", "");
            dataGridView1.Columns.Add("columQuatro", "");
            dataGridView1.Columns.Add("columCinco", "");

            dataGridView1.Rows.Add("            ", "        ", "       ", "3", "          ");
            dataGridView1.Rows.Add("5", "6", "7", "8", "9");
            dataGridView1.Rows.Add("10", "11", "12", "13", "14");
            dataGridView1.Rows.Add("15", "16", "17", "18", "19");
            dataGridView1.Rows.Add("20", "21", "22", "23", " ");
        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (! dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.Equals("              S"))
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "         Bandeira";
                bandeiras++;
                lblBandeiras.Text = bandeiras.ToString();
            }
            
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //int contador = 0;
            //for (int a = 0; a < linha; a++)
            //{
            //    for (int b = 0; b < coluna; b++)
            //    {
            //        Console.Write(contador+" ");
            //        contador++;
            //    }
            //    Console.WriteLine("");
            //}
            List<int> bomba = new List<int>();
            bomba.Add(24);
            bomba.Add(17);
            bomba.Add(3);
            int contador = 0;
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.Equals("         Bandeira"))
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "              S";
                bandeiras-=2;
                lblBandeiras.Text = bandeiras.ToString();
            }
                

            for (int a = 0; a < linha; a++)
            {
                for (int b = 0; b < coluna; b++)
                {
                    //verifica se a bomba esta na celula clicada
                    foreach (int bomb in bomba)
                    {
                        if (contador == bomb)
                        {
                            if (e.RowIndex == a && e.ColumnIndex == b)
                            {//Se estiver o mesmo mostra onde esta todas as bombas e acaba o jogo
                                foreach (int bombEstorado in bomba)
                                {
                                    int contEstorado = 0;
                                    for (int aEstorado = 0; aEstorado < linha; aEstorado++)
                                    {
                                        for (int bEstorado = 0; bEstorado < coluna; bEstorado++)
                                        {
                                            if (contEstorado == bombEstorado)
                                            {
                                                dataGridView1.Rows[aEstorado].Cells[bEstorado].Value = "              B";
                                            }
                                            contEstorado++;
                                        }
                                    }
                                }
                                dataGridView1.Enabled = false;
                            }

                        }

                    }
                    contador++;
                }
            }//fim dos for 
        }
    }
}
