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
        int linha = 10;
        int coluna = 10;
        int bandeiras = 0;
        List<int> bomba = new List<int>();

        public Form1()
        {
            InitializeComponent();
            dataGridView1.Columns.Add("columUm", "");
            dataGridView1.Columns.Add("columDois", "");
            dataGridView1.Columns.Add("columTres", "");
            dataGridView1.Columns.Add("columQuatro", "");
            dataGridView1.Columns.Add("columCinco", "");
            dataGridView1.Columns.Add("columseis", "");
            dataGridView1.Columns.Add("columSete", "");
            dataGridView1.Columns.Add("columOito", "");
            dataGridView1.Columns.Add("columNove", "");
            dataGridView1.Columns.Add("columDez", "");
            inicializaCampoMinado();
        }

        private void inicializaCampoMinado()
        {
            bomba.Add(24);
            bomba.Add(17);
            bomba.Add(3);

            dataGridView1.Enabled = true;
            dataGridView1.Rows.Clear();
            bandeiras = 0;
            lblBandeiras.Text = bandeiras.ToString();
            dataGridView1.Rows.Add("            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ");
            dataGridView1.Rows.Add("            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ");
            dataGridView1.Rows.Add("            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ");
            dataGridView1.Rows.Add("            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ");
            dataGridView1.Rows.Add("            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ");
            dataGridView1.Rows.Add("            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ");
            dataGridView1.Rows.Add("            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ");
            dataGridView1.Rows.Add("            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ");
            dataGridView1.Rows.Add("            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ");
            dataGridView1.Rows.Add("            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ");
            dataGridView1.Rows.Add("            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ", "            ");
        }

        private int contaBandeira()
        {
            int totalBandeira = 0;
            for (int a=0;a<linha; a++)
            {
                for (int b = 0; b < coluna; b++)
                {
                    if (dataGridView1.Rows[a].Cells[b].Value.Equals("         Bandeira")) totalBandeira++;
                }
            }
            return totalBandeira;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.Equals("            "))
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "         Bandeira";
                bandeiras++;
                lblBandeiras.Text = contaBandeira().ToString();
            }
            
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {     
            int contador = 0;

            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "              " + calculaQtdBomba(e.RowIndex, e.ColumnIndex).ToString() ;
            lblBandeiras.Text = contaBandeira().ToString().ToString();
        
                

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

        private void btnJogar_Click(object sender, EventArgs e)
        {
            inicializaCampoMinado();
        }

        public int verificaBomba(int posi)//1para bomba && 0para nao bomba
        {
            foreach(int bom in bomba)
            {
                if (posi == bom) return 1;
            }
            return 0;
        }

        private int calculaQtdBomba(int posiLinha, int posiColuna)
        {
            int contador = 0;
            int qntdBomba = 0;
            for (int a=0; a < linha; a++)
            {
                for(int b = 0; b < coluna; b++)
                {
                    if ((a == posiLinha - 1 && b == posiColuna - 1) && verificaBomba(contador) == 1) qntdBomba++;
                    if ((a == posiLinha - 1 && b == posiColuna) && verificaBomba(contador) == 1) qntdBomba++;
                    if ((a == posiLinha - 1 && b == posiColuna + 1) && verificaBomba(contador) == 1) qntdBomba++;

                    if ((a == posiLinha && b == posiColuna - 1) && verificaBomba(contador) == 1) qntdBomba++;
                    if ((a == posiLinha && b == posiColuna + 1) && verificaBomba(contador) == 1) qntdBomba++;

                    if ((a == posiLinha + 1 && b == posiColuna - 1) && verificaBomba(contador) == 1) qntdBomba++;
                    if ((a == posiLinha + 1 && b == posiColuna) && verificaBomba(contador) == 1) qntdBomba++;
                    if ((a == posiLinha + 1 && b == posiColuna + 1) && verificaBomba(contador) == 1) qntdBomba++;
                    contador++;
                }
            }
            return qntdBomba;
        }
    }
}
