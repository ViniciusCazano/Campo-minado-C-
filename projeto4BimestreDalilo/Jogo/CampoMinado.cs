using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projeto4BimestreDalilo.Jogo
{
    public partial class CampoMinado : Form
    {
        int linha = 10;
        int coluna = 10;
        int bandeiras = 0;
        int nivel = 30;//facil=30 medio=40 dificil=50
        List<int> bomba = new List<int>();

        public CampoMinado()
        {
            InitializeComponent();
            inicializaCampoMinado();
        }

        private void CampoMinado_Load(object sender, EventArgs e)
        {

        }

        private void randomBombas()
        {
            int bombaUnitaria;
            int total = 0;
            Random random = new Random();
            while (total<nivel)
            {
                bombaUnitaria = random.Next(0, 100);
                Console.WriteLine(bombaUnitaria);
                bomba.Add(bombaUnitaria);
                total++;
            }
            
        }

        private void inicializaCampoMinado()
        {
            //bomba.Add(24);
            //bomba.Add(17);
            //bomba.Add(3);
            bomba.Clear();
            randomBombas();

            dataGridView1.Enabled = true;
            dataGridView1.Rows.Clear();
            bandeiras = 0;
            lblBandeiras.Text = bandeiras.ToString();
            dataGridView1.Rows.Add(" ", " ", " ", " ", " ", " ", " ", " ", " ", " ");
            dataGridView1.Rows.Add(" ", " ", " ", " ", " ", " ", " ", " ", " ", " ");
            dataGridView1.Rows.Add(" ", " ", " ", " ", " ", " ", " ", " ", " ", " ");
            dataGridView1.Rows.Add(" ", " ", " ", " ", " ", " ", " ", " ", " ", " ");
            dataGridView1.Rows.Add(" ", " ", " ", " ", " ", " ", " ", " ", " ", " ");
            dataGridView1.Rows.Add(" ", " ", " ", " ", " ", " ", " ", " ", " ", " ");
            dataGridView1.Rows.Add(" ", " ", " ", " ", " ", " ", " ", " ", " ", " ");
            dataGridView1.Rows.Add(" ", " ", " ", " ", " ", " ", " ", " ", " ", " ");
            dataGridView1.Rows.Add(" ", " ", " ", " ", " ", " ", " ", " ", " ", " ");
            dataGridView1.Rows.Add(" ", " ", " ", " ", " ", " ", " ", " ", " ", " ");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int contador = 0;

            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "      " + calculaQtdBomba(e.RowIndex, e.ColumnIndex).ToString();
            lblBandeiras.Text = contaBandeira().ToString().ToString();

            for (int a = 0; a < linha; a++)
            {
                for (int b = 0; b < coluna; b++)
                {
                    //verifica se a bomba esta na celula clicada
                    if ((verificaBomba(contador) == 1) && (a == e.RowIndex && b == e.ColumnIndex))
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
                                        dataGridView1.Rows[aEstorado].Cells[bEstorado].Value = "BOMBA";
                                    }
                                    contEstorado++;
                                }
                            }
                        }
                        dataGridView1.Enabled = false;
                    }
                    contador++;
                }
            }//fim dos for 
            testaVitoria();
        }

        private int contaBandeira()
        {
            int totalBandeira = 0;
            for (int a = 0; a < linha; a++)
            {
                for (int b = 0; b < coluna; b++)
                {
                    if (dataGridView1.Rows[a].Cells[b].Value.Equals("Bandeira")) totalBandeira++;
                }
            }
            bandeiras = totalBandeira;
            return totalBandeira;
        }

        public int verificaBomba(int posi)//1 para bomba && 0 para nao bomba
        {
            foreach (int bom in bomba)
            {
                if (posi == bom) return 1;
            }
            return 0;
        }

        private int calculaQtdBomba(int posiLinha, int posiColuna)
        {
            int contador = 0;
            int qntdBomba = 0;
            for (int a = 0; a < linha; a++)
            {
                for (int b = 0; b < coluna; b++)
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

        private void btnJogar_Click(object sender, EventArgs e)
        {
            inicializaCampoMinado();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.Equals(" ")) && bandeiras < 3)
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "Bandeira";
                bandeiras++;
                lblBandeiras.Text = contaBandeira().ToString();
            }
            testaVitoria();
        }

        private int testaVitoria()
        {
            int contadorPosicao = 0;
            for (int a=0; a < linha; a++)
            {
                for (int b = 0; b < coluna; b++)
                {
                    if( dataGridView1.Rows[a].Cells[b].Value.Equals(" "))
                    {
                        return 0;
                    }
                    contadorPosicao++;
                }
            }
            MessageBox.Show("Você ganhou");
            dataGridView1.Enabled = false;
            return 1;
        }
    }
}
