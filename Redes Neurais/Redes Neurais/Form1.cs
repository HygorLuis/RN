using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Redes_Neurais
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Random random = new Random();
        List<Especie> especie = new List<Especie>();
        List<TipoEspecie> tipoEspecie = new List<TipoEspecie>();
        List<Especie> treinamento = new List<Especie>();
        List<Especie> teste = new List<Especie>();
        
        CriterioAvaliacao IrisSetosa = new CriterioAvaliacao();
        //List<CriterioAvaliacao> IrisVersicolor = new List<CriterioAvaliacao>();
        //List<CriterioAvaliacao> IrisVirginica = new List<CriterioAvaliacao>();
        int?[] peso = new int?[5];
        private double _dIrisSetosa;
        private int _iTipo;
        //int[] peso = { 5, 3, 6, 0, 2 };
        //private double dIrisSetosa = 46;

        private void LoadTipoEspecie()
        {
            tipoEspecie.Clear();
            tipoEspecie.Add(new TipoEspecie { IDTipo = 1, Especie = "Iris-setosa" });
            tipoEspecie.Add(new TipoEspecie { IDTipo = 2, Especie = "Iris-versicolor" });
            tipoEspecie.Add(new TipoEspecie { IDTipo = 3, Especie = "Iris-virginica" });
        }

        private void LoadEspecie()
        {
            var caminho = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "\\Especies.txt";
            StreamReader arquivo = new StreamReader(caminho);
            especie.Clear();

            var especies = arquivo.ReadToEnd().Replace("\r\n", "*").Split('*');
            foreach (var linha in especies)
            {
                especie.Add(new Especie
                {
                    IDTipo = linha.Contains("Iris-setosa") ? 1 :
                             linha.Contains("Iris-versicolor") ? 2 : 3,

                    Entradas = linha.Substring(0, 15)
                });
            }

            lvEspecies.Items.Clear();
            especie.ForEach(x =>
            {
                string[] item = { x.Entradas, tipoEspecie.FirstOrDefault(t => t.IDTipo.Equals(x.IDTipo))?.Especie };
                var items = new ListViewItem(item);
                lvEspecies.Items.Add(items);
            });

            lvEspecies.Update();
        }

        private void Separacao()
        {
            List<int> verifica = new List<int>();       
            treinamento.Clear();
            teste.Clear();
            lvTreinamento.Items.Clear();
            lvTeste.Items.Clear();

            for (int i = 1; i <= especie.Count / 2; i++)
            {
                var posicao = random.Next(especie.Count);
                if (verifica.AsEnumerable().ToList().Where(x => x.Equals(posicao)).ToList().Count > 0)
                {
                    i--;
                    continue;
                }

                treinamento.Add(new Especie
                {
                    IDTipo = especie[posicao].IDTipo,
                    Entradas = especie[posicao].Entradas
                });
                verifica.Add(posicao);
            }

            for (int i = 0; i < especie.Count; i++)
            {
                if (verifica.AsEnumerable().ToList().Where(x => x.Equals(i)).ToList().Count == 0)
                    teste.Add(new Especie { IDTipo = especie[i].IDTipo, Entradas = especie[i].Entradas });
            }

            treinamento.ForEach(x =>
            {
                string[] item = { x.Entradas, tipoEspecie.FirstOrDefault(t => t.IDTipo.Equals(x.IDTipo))?.Especie };
                var items = new ListViewItem(item);
                lvTreinamento.Items.Add(items);
            });
            
            teste.ForEach(x =>
            {
                string[] item = { x.Entradas, tipoEspecie.FirstOrDefault(t => t.IDTipo.Equals(x.IDTipo))?.Especie };
                var items = new ListViewItem(item);
                lvTeste.Items.Add(items);
            });          
        }

        private void Avaliar(List<Especie> verifica, bool bTreinamento)
        {
            bool bCorrecaoSoma = true;
            int iCount;
            int dTotalIrisSetosa = 0;
            List<CriterioAvaliacao> avaliacao = new List<CriterioAvaliacao>();
            List<Resultado> resultado = new List<Resultado>(); 

            verifica.ForEach(x =>
            {
                if (x.IDTipo == 1)
                    dTotalIrisSetosa++;
            });

            for (iCount = 0; iCount < 250; iCount++)
            {
                bool bCorrecaoPesos = false;
                int acerto = 0;
                foreach (var entradas in verifica)
                {
                    double? soma = 0;
                    var x = entradas.Entradas.Split(',');
                    for (int i = 0; i < x.Length; i++)
                    {
                        soma += (Convert.ToDouble(x[i].Replace(".", ",")) * peso[i]);
                    }
                    soma += peso[4];

                    if (Math.Round((double) soma, 0) <= _dIrisSetosa)
                    {
                        if (entradas.IDTipo != 1 && bTreinamento)
                        {
                            peso[0] += 1;
                            peso[1] += 1;
                            peso[2] += 1;
                            peso[3] += 1;
                            peso[4] += 1;
                            bCorrecaoPesos = true;
                        }

                        resultado.Add(new Resultado
                        {
                            Entradas = entradas.Entradas,
                            IDTipoResultado = 1,
                            IDTipoVerdadeira = entradas.IDTipo
                        });
                    }
                    else
                    {
                        if (entradas.IDTipo == 1 && bTreinamento)
                        {
                            peso[0] -= 1;
                            peso[1] -= 1;
                            peso[2] -= 1;
                            peso[3] -= 1;
                            peso[4] -= 1;
                            bCorrecaoPesos = true;
                        }
                    }
                }

                resultado.ForEach(x =>
                {
                    if (x.IDTipoResultado == x.IDTipoVerdadeira)
                        acerto++;
                });

                avaliacao.Add(new CriterioAvaliacao
                {
                    AcertoPorcent = acerto == 0 ? 0 : (acerto * 100) / dTotalIrisSetosa,
                    QtdAcerto = acerto,
                    ErroPorcent = 100 - (acerto == 0 ? 0 : (acerto * 100 / resultado.Count)),
                    QtdErro = resultado.Count - acerto,
                    Peso1 = peso[0],
                    Peso2 = peso[1],
                    Peso3 = peso[2],
                    Peso4 = peso[3],
                    Baias = peso[4],
                });

                if (bTreinamento)
                    lvResultTreinamento.Items.Clear();
                else
                    lvResultTeste.Items.Clear();

                resultado.ForEach(x =>
                {
                    string[] item = { x.Entradas, tipoEspecie.FirstOrDefault(t => t.IDTipo.Equals(x.IDTipoResultado))?.Especie, tipoEspecie.FirstOrDefault(t => t.IDTipo.Equals(x.IDTipoVerdadeira))?.Especie };
                    var items = new ListViewItem(item);

                    if (bTreinamento)
                    {
                        tabControl1.SelectedTab = tabPage2;
                        tabControl2.SelectedTab = tabPage4;
                        lvResultTreinamento.Items.Add(items);
                        lvResultTreinamento.Items[lvResultTreinamento.Items.Count - 1].ForeColor = x.IDTipoResultado == x.IDTipoVerdadeira ? DefaultForeColor : Color.Red;
                        lvTreinamento.Update();
                        lvResultTreinamento.Update();
                    }
                    else
                    {
                        tabControl1.SelectedTab = tabPage3;
                        tabControl2.SelectedTab = tabPage5;
                        lvResultTeste.Items.Add(items);
                        lvResultTeste.Items[lvResultTeste.Items.Count-1].ForeColor = x.IDTipoResultado == x.IDTipoVerdadeira ? DefaultForeColor : Color.Red;
                        lvTeste.Update();
                        lvResultTeste.Update();
                    }
                });
                resultado.Clear();

                if (avaliacao.Count > 2 && bTreinamento)
                    if (avaliacao[avaliacao.Count - 1].AcertoPorcent.Equals(avaliacao[avaliacao.Count - 2].AcertoPorcent)
                        && avaliacao[avaliacao.Count - 1].ErroPorcent.Equals(avaliacao[avaliacao.Count - 2].ErroPorcent)
                        && avaliacao[avaliacao.Count - 1].QtdErro > 0)
                    {
                        if ((avaliacao[avaliacao.Count - 1].AcertoPorcent >= avaliacao[avaliacao.Count - 3].AcertoPorcent && bCorrecaoSoma)
                            || (avaliacao[avaliacao.Count - 1].AcertoPorcent < avaliacao[avaliacao.Count - 3].AcertoPorcent && !bCorrecaoSoma))
                        {
                            _dIrisSetosa++;
                            bCorrecaoSoma = true;
                        }
                        else
                        {
                            if ((avaliacao[avaliacao.Count - 1].AcertoPorcent < avaliacao[avaliacao.Count - 3].AcertoPorcent && bCorrecaoSoma)
                                || (avaliacao[avaliacao.Count - 1].AcertoPorcent >= avaliacao[avaliacao.Count - 3].AcertoPorcent && !bCorrecaoSoma))
                            {
                                _dIrisSetosa--;
                                bCorrecaoSoma = false;
                            }
                        }
                    }
                    else
                    {
                        if (avaliacao.Count >= 5)
                        {
                            if (avaliacao[avaliacao.Count - 1].AcertoPorcent > avaliacao[avaliacao.Count - 3].AcertoPorcent
                                && avaliacao[avaliacao.Count - 3].AcertoPorcent < avaliacao[avaliacao.Count - 5].AcertoPorcent
                                && avaliacao[avaliacao.Count - 1].AcertoPorcent == avaliacao[avaliacao.Count - 5].AcertoPorcent
                                && avaliacao[avaliacao.Count - 1].AcertoPorcent >= 90)
                                bCorrecaoPesos = false;
                        }

                        if (/*porcentagemAcerto[porcentagemAcerto.Count - 1].AcertoPorcent <= porcentagemAcerto[porcentagemAcerto.Count - 2].AcertoPorcent
                            && porcentagemAcerto[porcentagemAcerto.Count - 1].ErroPorcent <= porcentagemAcerto[porcentagemAcerto.Count - 2].ErroPorcent
                            && */!bCorrecaoPesos)
                        {
                            peso[0] = avaliacao[avaliacao.Count - 2].Peso1;
                            peso[1] = avaliacao[avaliacao.Count - 2].Peso2;
                            peso[2] = avaliacao[avaliacao.Count - 2].Peso3;
                            peso[3] = avaliacao[avaliacao.Count - 2].Peso4;
                            peso[4] = avaliacao[avaliacao.Count - 2].Baias;

                            var tipo = rdIrisSetosa.Checked ? rdIrisSetosa.Text : rdIrisVersicolor.Checked ? rdIrisVersicolor.Text : rdIrisVirginica.Text;
                            lblAcertoTrein.Text = $"Porcentagem de acerto: {avaliacao[avaliacao.Count - 2].AcertoPorcent}%";
                            lblQtdAcertoTrein.Text = $"Quantidade: {avaliacao[avaliacao.Count - 2].QtdAcerto}";
                            lblErroTrein.Text = $"Porcentagem de erro: {avaliacao[avaliacao.Count - 2].ErroPorcent}%";
                            lblQtdErroTrein.Text = $"Quantidade: {avaliacao[avaliacao.Count - 2].QtdErro}";
                            lblTotalTrein.Text = $"Total de {tipo}: {dTotalIrisSetosa}";
                            break;
                        }
                    }

                /*if (porcentagemAcerto.Count > 1 && treinamento)
                {
                    if (porcentagemAcerto[porcentagemAcerto.Count - 1].AcertoPorcent.Equals(porcentagemAcerto[porcentagemAcerto.Count - 2].AcertoPorcent)
                        && porcentagemAcerto[porcentagemAcerto.Count - 1].ErroPorcent.Equals(porcentagemAcerto[porcentagemAcerto.Count - 2].ErroPorcent)
                        && porcentagemAcerto[porcentagemAcerto.Count - 1].AcertoPorcent < 100
                        && porcentagemAcerto[porcentagemAcerto.Count - 1].ErroPorcent != 0
                        && (porcentagemAcerto[porcentagemAcerto.Count - 1].QtdErro > porcentagemAcerto[porcentagemAcerto.Count - 1].QtdAcerto))
                        dIrisSetosa--;
                    else
                    {
                        if (porcentagemAcerto[porcentagemAcerto.Count - 1].AcertoPorcent <= porcentagemAcerto[porcentagemAcerto.Count - 2].AcertoPorcent
                            && porcentagemAcerto[porcentagemAcerto.Count - 1].ErroPorcent <= porcentagemAcerto[porcentagemAcerto.Count - 2].ErroPorcent
                            && Math.Abs(porcentagemAcerto[porcentagemAcerto.Count - 1].AcertoPorcent - porcentagemAcerto[porcentagemAcerto.Count - 2].AcertoPorcent) < 90)
                        {
                            dIrisSetosa++;
                        }
                        else
                        {
                            if (Math.Abs(porcentagemAcerto[porcentagemAcerto.Count - 1].AcertoPorcent - porcentagemAcerto[porcentagemAcerto.Count - 1].ErroPorcent) == 100
                                && !bCorrecao)
                            {
                                peso[0] = porcentagemAcerto[porcentagemAcerto.Count - 2].Peso1;
                                peso[1] = porcentagemAcerto[porcentagemAcerto.Count - 2].Peso2;
                                peso[2] = porcentagemAcerto[porcentagemAcerto.Count - 2].Peso3;
                                peso[3] = porcentagemAcerto[porcentagemAcerto.Count - 2].Peso4;
                                peso[4] = porcentagemAcerto[porcentagemAcerto.Count - 2].baias;

                                lblAcerto.Text = $"Porcentagem de acerto: {porcentagemAcerto[porcentagemAcerto.Count - 2].AcertoPorcent}%";
                                lblQtdAcerto.Text = $"Quantidade: {porcentagemAcerto[porcentagemAcerto.Count - 2].QtdAcerto}";
                                lblErro.Text = $"Porcentagem de erro: {porcentagemAcerto[porcentagemAcerto.Count - 2].ErroPorcent}%";
                                lblQtdErro.Text = $"Quantidade: {porcentagemAcerto[porcentagemAcerto.Count - 2].QtdErro}";
                                break;
                            }
                        }
                    }

                }*/


                /*if (porcentagemAcerto.Count > 1 && treinamento)
                    if (porcentagemAcerto[porcentagemAcerto.Count - 1].AcertoPorcent.Equals(porcentagemAcerto[porcentagemAcerto.Count - 2].AcertoPorcent)
                        && porcentagemAcerto[porcentagemAcerto.Count - 1].ErroPorcent.Equals(porcentagemAcerto[porcentagemAcerto.Count - 2].ErroPorcent)
                        && porcentagemAcerto[porcentagemAcerto.Count - 1].QtdErro > 0)
                    {
                        if (porcentagemAcerto[porcentagemAcerto.Count - 1].AcertoPorcent >
                            porcentagemAcerto[porcentagemAcerto.Count - 2].AcertoPorcent
                            || porcentagemAcerto[porcentagemAcerto.Count - 1].AcertoPorcent < 90)
                        {
                            dIrisSetosa++;
                            bCorrecaoSoma = false;
                        }
                        else if (porcentagemAcerto[porcentagemAcerto.Count - 1].AcertoPorcent <
                                 porcentagemAcerto[porcentagemAcerto.Count - 3].AcertoPorcent
                                 || porcentagemAcerto[porcentagemAcerto.Count - 1].ErroPorcent > 5)
                        {
                            dIrisSetosa--;
                            bCorrecaoSoma = true;
                        }
                    }
                    else
                    {
                        if (porcentagemAcerto[porcentagemAcerto.Count - 1].AcertoPorcent <= porcentagemAcerto[porcentagemAcerto.Count - 2].AcertoPorcent
                            && porcentagemAcerto[porcentagemAcerto.Count - 1].ErroPorcent >= porcentagemAcerto[porcentagemAcerto.Count - 2].ErroPorcent)
                        {
                            peso[0] = porcentagemAcerto[porcentagemAcerto.Count - 2].Peso1;
                            peso[1] = porcentagemAcerto[porcentagemAcerto.Count - 2].Peso2;
                            peso[2] = porcentagemAcerto[porcentagemAcerto.Count - 2].Peso3;
                            peso[3] = porcentagemAcerto[porcentagemAcerto.Count - 2].Peso4;
                            peso[4] = porcentagemAcerto[porcentagemAcerto.Count - 2].baias;

                            lblAcerto.Text = $"Porcentagem de acerto: {porcentagemAcerto[porcentagemAcerto.Count - 2].AcertoPorcent}%";
                            lblQtdAcerto.Text = $"Quantidade: {porcentagemAcerto[porcentagemAcerto.Count - 2].QtdAcerto}";
                            lblErro.Text = $"Porcentagem de erro: {porcentagemAcerto[porcentagemAcerto.Count - 2].ErroPorcent}%";
                            lblQtdErro.Text = $"Quantidade: {porcentagemAcerto[porcentagemAcerto.Count - 2].QtdErro}";
                            break;
                        }
                    }*/

                if (!bTreinamento)
                {
                    var tipo = rdIrisSetosa.Checked ? rdIrisSetosa.Text : rdIrisVersicolor.Checked ? rdIrisVersicolor.Text : rdIrisVirginica.Text;
                    lblAcertoTeste.Text = $"Porcentagem de acerto: {avaliacao[0].AcertoPorcent}%";
                    lblQtdAcertoTeste.Text = $"Quantidade: {avaliacao[0].QtdAcerto}";
                    lblErroTeste.Text = $"Porcentagem de erro: {avaliacao[0].ErroPorcent}%";
                    lblQtdErroTeste.Text = $"Quantidade: {avaliacao[0].QtdErro}";
                    lblTotalTeste.Text = $"Total de {tipo}: {dTotalIrisSetosa}";

                    if (lblAcertoTeste.Text.Equals(lblAcertoTrein.Text) && avaliacao[0].AcertoPorcent == 100
                        && lblErroTeste.Text.Equals(lblErroTrein.Text) && avaliacao[0].ErroPorcent == 0)
                    {
                        IrisSetosa.Peso1 = peso[0];
                        IrisSetosa.Peso2 = peso[1];
                        IrisSetosa.Peso3 = peso[2];
                        IrisSetosa.Peso4 = peso[3];
                        IrisSetosa.Baias = peso[4];
                        IrisSetosa.Soma = _dIrisSetosa;
                    }
                    break;
                }

                //resultado.Clear();
            }

            if (iCount == 250)
            {
                var tipo = rdIrisSetosa.Checked ? rdIrisSetosa.Text : rdIrisVersicolor.Checked ? rdIrisVersicolor.Text : rdIrisVirginica.Text;
                lblAcertoTrein.Text = $"Porcentagem de acerto: {avaliacao[avaliacao.Count - 2].AcertoPorcent}%";
                lblQtdAcertoTrein.Text = $"Quantidade: {avaliacao[avaliacao.Count - 2].QtdAcerto}";
                lblErroTrein.Text = $"Porcentagem de erro: {avaliacao[avaliacao.Count - 2].ErroPorcent}%";
                lblQtdErroTrein.Text = $"Quantidade: {avaliacao[avaliacao.Count - 2].QtdErro}";
                lblTotalTrein.Text = $"Total de {tipo}: {dTotalIrisSetosa}";

                MessageBox.Show(
                    "Por várias tentativas a I.A. não conseguiu formular um parâmetro de avalição das espécies.\r\nPor favor, tente novamente para seu critério de avaliação ser reiniciado.",
                    "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadPesos()
        {
            switch (_iTipo)
            {
                case 1:
                    if (IrisSetosa.Peso1 != null && IrisSetosa.Soma != null)
                    {
                        peso[0] = IrisSetosa.Peso1;
                        peso[1] = IrisSetosa.Peso2;
                        peso[2] = IrisSetosa.Peso3;
                        peso[3] = IrisSetosa.Peso4;
                        peso[4] = IrisSetosa.Baias;
                        _dIrisSetosa = (double) IrisSetosa.Soma;
                    }
                    else
                    {
                        for (int i = 0; i <= peso.Count() - 1; i++)
                            peso[i] = random.Next(10);

                        _dIrisSetosa = random.Next(100);
                    }             
                    break;

                case 2:
                    break;

                case 3:
                    break;
            }
            
        }

        private bool VerificaSelecao()
        {
            if (!rdIrisSetosa.Checked && !rdIrisVersicolor.Checked && !rdIrisVirginica.Checked)
                return false;

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //treinamento.ForEach(x => avaliacao.Add(new Especie { IDTipo = x.IDTipo, Entradas = x.Entradas }));
            if (VerificaSelecao())
            {
                Cursor = Cursors.WaitCursor;
                LoadPesos();
                Separacao();
                Avaliar(treinamento, true);
                Avaliar(teste, false);
                Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show(
                    "Selecione alguma espécie para realizar a aprendizagem!",
                    "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            LoadTipoEspecie();
            LoadEspecie();
        }

        private void rdIrisSetosa_CheckedChanged(object sender, EventArgs e)
        {
            _iTipo = 1;
        }

        private void rdIrisVersicolor_CheckedChanged(object sender, EventArgs e)
        {
            _iTipo = 2;
        }

        private void rdIrisVirginica_CheckedChanged(object sender, EventArgs e)
        {
            _iTipo = 3;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 1:
                    tabControl2.SelectedTab = tabPage4;
                    break;

                case 2:
                    tabControl2.SelectedTab = tabPage5;
                    break;
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl2.SelectedIndex)
            {
                case 0:
                    tabControl1.SelectedTab = tabPage2;
                    break;

                case 1:
                    tabControl1.SelectedTab = tabPage3;
                    break;
            }
        }
    }
}
