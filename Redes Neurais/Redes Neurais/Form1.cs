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
        Random _random = new Random();
        List<Especie> especie = new List<Especie>();
        List<TipoEspecie> tipoEspecie = new List<TipoEspecie>();
        List<Especie> treinamento = new List<Especie>();
        List<Especie> teste = new List<Especie>();

        CriterioAvaliacao IrisSetosa = new CriterioAvaliacao();
        CriterioAvaliacao _irisVirginica = new CriterioAvaliacao();
        int?[] peso = new int?[5];
        private double _dIrisSetosa;
        private double _dIrisVersicolor;
        private double _dIrisVirginica;
        private int _iTipo;

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
                var posicao = _random.Next(especie.Count);
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

        private void AvaliarIrisSetosa(List<Especie> verifica, bool bTreinamento)
        {
            bool bCorrecaoSoma = true;
            int iCount;
            int dTotalIrisSetosa = 0;
            List<CriterioAvaliacao> avaliacao = new List<CriterioAvaliacao>();

            verifica.ForEach(x =>
            {
                if (x.IDTipo == 1)
                    dTotalIrisSetosa++;
            });

            for (iCount = 0; iCount < 100; iCount++)
            {
                bool bCorrecaoPesos = false;
                int acerto = 0;
                List<Resultado> resultado = new List<Resultado>();

                verifica.ForEach(entradas =>
                {
                    double? soma = 0;
                    var x = entradas.Entradas.Split(',');
                    for (int i = 0; i < x.Length; i++)
                    {
                        soma += (Convert.ToDouble(x[i].Replace(".", ",")) * peso[i]);
                    }
                    soma += peso[4];

                    if (Math.Round((double)soma, 0) <= _dIrisSetosa)
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
                });

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
                    Bias = peso[4],
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
                        lvResultTeste.Items[lvResultTeste.Items.Count - 1].ForeColor = x.IDTipoResultado == x.IDTipoVerdadeira ? DefaultForeColor : Color.Red;
                        lvTeste.Update();
                        lvResultTeste.Update();
                    }
                });

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

                        if (!bCorrecaoPesos)
                        {
                            peso[0] = avaliacao[avaliacao.Count - 1].Peso1;
                            peso[1] = avaliacao[avaliacao.Count - 1].Peso2;
                            peso[2] = avaliacao[avaliacao.Count - 1].Peso3;
                            peso[3] = avaliacao[avaliacao.Count - 1].Peso4;
                            peso[4] = avaliacao[avaliacao.Count - 1].Bias;

                            LoadInformation((int)avaliacao[avaliacao.Count - 1].AcertoPorcent, (int)avaliacao[avaliacao.Count - 1].QtdAcerto, (int)avaliacao[avaliacao.Count - 1].ErroPorcent, (int)avaliacao[avaliacao.Count - 1].QtdErro, dTotalIrisSetosa, bTreinamento);
                            break;
                        }
                    }

                if (!bTreinamento)
                {
                    LoadInformation((int)avaliacao[0].AcertoPorcent, (int)avaliacao[0].QtdAcerto, (int)avaliacao[0].ErroPorcent, (int)avaliacao[0].QtdErro, dTotalIrisSetosa, bTreinamento);

                    if (lblAcertoTeste.Text.Equals(lblAcertoTrein.Text) && avaliacao[0].AcertoPorcent == 100
                        && lblErroTeste.Text.Equals(lblErroTrein.Text) && avaliacao[0].ErroPorcent == 0)
                    {
                        IrisSetosa.Peso1 = peso[0];
                        IrisSetosa.Peso2 = peso[1];
                        IrisSetosa.Peso3 = peso[2];
                        IrisSetosa.Peso4 = peso[3];
                        IrisSetosa.Bias = peso[4];
                        IrisSetosa.Soma = _dIrisSetosa;

                        EnabledFields(true);
                    }
                    break;
                }
            }

            if (iCount == 100)
            {
                LoadInformation((int)avaliacao[avaliacao.Count - 1].AcertoPorcent, (int)avaliacao[avaliacao.Count - 1].QtdAcerto, (int)avaliacao[avaliacao.Count - 1].ErroPorcent, (int)avaliacao[avaliacao.Count - 1].QtdErro, dTotalIrisSetosa, bTreinamento);
                MessageBox.Show(
                    "Por várias tentativas a I.A. não conseguiu formular um parâmetro de avalição das espécies.\r\nPor favor, tente novamente para seu critério de avaliação ser reiniciado.",
                    "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AvaliarIrisVersicolor(List<Especie> verifica, bool bTreinamento)
        {
            bool bCorrecaoSoma = true;
            int iCount;
            int dTotalIrisVersicolor = 0;
            List<CriterioAvaliacao> avaliacao = new List<CriterioAvaliacao>();

            verifica.ForEach(x =>
            {
                if (x.IDTipo == 2)
                    dTotalIrisVersicolor++;
            });

            for (iCount = 0; iCount < 100; iCount++)
            {
                bool bCorrecaoPesos = false;
                int acerto = 0;
                List<Resultado> resultado = new List<Resultado>();

                verifica.ForEach(entradas =>
                {
                    double? soma = 0;
                    var x = entradas.Entradas.Split(',');
                    for (int i = 0; i < x.Length; i++)
                    {
                        soma += (Convert.ToDouble(x[i].Replace(".", ",")) * peso[i]);
                    }
                    soma += peso[4];

                    if (Math.Round((double)soma, 0) > _dIrisSetosa && Math.Round((double)soma, 0) < _dIrisVersicolor)
                    {
                        if (entradas.IDTipo != 2 && _dIrisVersicolor == 100 && bTreinamento)
                            _dIrisVersicolor = Math.Round((double)soma, 0);

                        if (entradas.IDTipo != 2 && bTreinamento)
                        {
                            _dIrisVersicolor--;
                            bCorrecaoPesos = true;
                        }

                        resultado.Add(new Resultado
                        {
                            Entradas = entradas.Entradas,
                            IDTipoResultado = 2,
                            IDTipoVerdadeira = entradas.IDTipo
                        });
                    }
                    else
                    {
                        if (entradas.IDTipo == 2 && bTreinamento)
                        {
                            _dIrisVersicolor++;
                            bCorrecaoPesos = true;
                        }
                    }
                });

                resultado.ForEach(x =>
                {
                    if (x.IDTipoResultado == x.IDTipoVerdadeira)
                        acerto++;
                });

                avaliacao.Add(new CriterioAvaliacao
                {
                    AcertoPorcent = acerto == 0 ? 0 : (acerto * 100) / dTotalIrisVersicolor,
                    QtdAcerto = acerto,
                    ErroPorcent = 100 - (acerto == 0 ? 0 : (acerto * 100 / resultado.Count)),
                    QtdErro = resultado.Count - acerto,
                    Peso1 = peso[0],
                    Peso2 = peso[1],
                    Peso3 = peso[2],
                    Peso4 = peso[3],
                    Bias = peso[4],
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
                        lvResultTeste.Items[lvResultTeste.Items.Count - 1].ForeColor = x.IDTipoResultado == x.IDTipoVerdadeira ? DefaultForeColor : Color.Red;
                        lvTeste.Update();
                        lvResultTeste.Update();
                    }
                });

                if (avaliacao.Count >= 5)
                {
                    if (Math.Abs(Convert.ToInt32(avaliacao[avaliacao.Count - 1].QtdAcerto - dTotalIrisVersicolor)) < 10 && avaliacao[avaliacao.Count - 1].QtdErro < 10
                        && avaliacao[avaliacao.Count - 1].AcertoPorcent >= avaliacao[avaliacao.Count - 3].AcertoPorcent
                        && avaliacao[avaliacao.Count - 3].AcertoPorcent <= avaliacao[avaliacao.Count - 5].AcertoPorcent
                        && avaliacao[avaliacao.Count - 1].AcertoPorcent == avaliacao[avaliacao.Count - 5].AcertoPorcent)
                        bCorrecaoPesos = false;
                }

                if (!bCorrecaoPesos)
                {
                    peso[0] = avaliacao[avaliacao.Count - 1].Peso1;
                    peso[1] = avaliacao[avaliacao.Count - 1].Peso2;
                    peso[2] = avaliacao[avaliacao.Count - 1].Peso3;
                    peso[3] = avaliacao[avaliacao.Count - 1].Peso4;
                    peso[4] = avaliacao[avaliacao.Count - 1].Bias;

                    LoadInformation((int)avaliacao[avaliacao.Count - 1].AcertoPorcent, (int)avaliacao[avaliacao.Count - 1].QtdAcerto, (int)avaliacao[avaliacao.Count - 1].ErroPorcent, (int)avaliacao[avaliacao.Count - 1].QtdErro, dTotalIrisVersicolor, bTreinamento);
                    break;
                }

                if (!bTreinamento)
                {
                    LoadInformation((int)avaliacao[0].AcertoPorcent, (int)avaliacao[0].QtdAcerto, (int)avaliacao[0].ErroPorcent, (int)avaliacao[0].QtdErro, dTotalIrisVersicolor, bTreinamento);
                    break;
                }
            }

            if (iCount == 100)
            {
                LoadInformation((int)avaliacao[avaliacao.Count - 1].AcertoPorcent, (int)avaliacao[avaliacao.Count - 1].QtdAcerto, (int)avaliacao[avaliacao.Count - 1].ErroPorcent, (int)avaliacao[avaliacao.Count - 1].QtdErro, dTotalIrisVersicolor, bTreinamento);

                MessageBox.Show(
                    "Por várias tentativas a I.A. não conseguiu formular um parâmetro de avalição das espécies.\r\nPor favor, tente novamente para seu critério de avaliação ser reiniciado.",
                    "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AvaliarIrisVirginica(List<Especie> verifica, bool bTreinamento)
        {
            bool bCorrecaoSoma = true;
            int iCount;
            int dTotalIrisVirginica = 0;
            List<CriterioAvaliacao> avaliacao = new List<CriterioAvaliacao>();

            verifica.ForEach(x =>
            {
                if (x.IDTipo == 3)
                    dTotalIrisVirginica++;
            });

            for (iCount = 0; iCount < 100; iCount++)
            {
                bool bCorrecaoPesos = false;
                int acerto = 0;
                List<Resultado> resultado = new List<Resultado>();

                verifica.ForEach(entradas =>
                {
                    double? soma = 0;
                    var x = entradas.Entradas.Split(',');
                    for (int i = 0; i < x.Length; i++)
                    {
                        soma += (Convert.ToDouble(x[i].Replace(".", ",")) * peso[i]);
                    }
                    soma += peso[4];

                    if (Math.Round((double)soma, 0) >= _dIrisVirginica)
                    {
                        if (entradas.IDTipo != 3 && bTreinamento)
                        {
                            peso[0]--;
                            peso[1]--;
                            peso[2]--;
                            peso[3]--;
                            peso[4]--;
                            bCorrecaoPesos = true;
                        }

                        resultado.Add(new Resultado
                        {
                            Entradas = entradas.Entradas,
                            IDTipoResultado = 3,
                            IDTipoVerdadeira = entradas.IDTipo
                        });
                    }
                    else
                    {
                        if (entradas.IDTipo == 3 && bTreinamento)
                        {
                            peso[0]++;
                            peso[1]++;
                            peso[2]++;
                            peso[3]++;
                            peso[4]++;
                            bCorrecaoPesos = true;
                        }
                    }
                });

                resultado.ForEach(x =>
                {
                    if (x.IDTipoResultado == x.IDTipoVerdadeira)
                        acerto++;
                });

                avaliacao.Add(new CriterioAvaliacao
                {
                    AcertoPorcent = acerto == 0 ? 0 : (acerto * 100) / dTotalIrisVirginica,
                    QtdAcerto = acerto,
                    ErroPorcent = 100 - (acerto == 0 ? 0 : (acerto * 100 / resultado.Count)),
                    QtdErro = resultado.Count - acerto,
                    Peso1 = peso[0],
                    Peso2 = peso[1],
                    Peso3 = peso[2],
                    Peso4 = peso[3],
                    Bias = peso[4],
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
                        lvResultTeste.Items[lvResultTeste.Items.Count - 1].ForeColor = x.IDTipoResultado == x.IDTipoVerdadeira ? DefaultForeColor : Color.Red;
                        lvTeste.Update();
                        lvResultTeste.Update();
                    }
                });

                if (avaliacao.Count > 2 && bTreinamento)
                    if (avaliacao[avaliacao.Count - 1].AcertoPorcent.Equals(avaliacao[avaliacao.Count - 2].AcertoPorcent)
                        && avaliacao[avaliacao.Count - 1].ErroPorcent.Equals(avaliacao[avaliacao.Count - 2].ErroPorcent)
                        && avaliacao[avaliacao.Count - 1].QtdErro > 0)
                    {
                        if ((avaliacao[avaliacao.Count - 1].AcertoPorcent >= avaliacao[avaliacao.Count - 3].AcertoPorcent && bCorrecaoSoma)
                            || (avaliacao[avaliacao.Count - 1].AcertoPorcent < avaliacao[avaliacao.Count - 3].AcertoPorcent && !bCorrecaoSoma))
                        {
                            _dIrisVirginica++;
                            bCorrecaoSoma = true;
                        }
                        else
                        {
                            if ((avaliacao[avaliacao.Count - 1].AcertoPorcent < avaliacao[avaliacao.Count - 3].AcertoPorcent && bCorrecaoSoma)
                                || (avaliacao[avaliacao.Count - 1].AcertoPorcent >= avaliacao[avaliacao.Count - 3].AcertoPorcent && !bCorrecaoSoma))
                            {
                                _dIrisVirginica--;
                                bCorrecaoSoma = false;
                            }
                        }
                    }
                    else
                    {
                        if (avaliacao.Count >= 5)
                        {
                            if ((Math.Abs(Convert.ToInt32(avaliacao[avaliacao.Count - 1].QtdAcerto - dTotalIrisVirginica)) < 10 && avaliacao[avaliacao.Count - 1].QtdErro < 10
                                 && avaliacao[avaliacao.Count - 1].AcertoPorcent >= avaliacao[avaliacao.Count - 3].AcertoPorcent
                                 && avaliacao[avaliacao.Count - 3].AcertoPorcent <= avaliacao[avaliacao.Count - 5].AcertoPorcent
                                 && avaliacao[avaliacao.Count - 1].AcertoPorcent == avaliacao[avaliacao.Count - 5].AcertoPorcent))
                                bCorrecaoPesos = false;
                        }

                        if (!bCorrecaoPesos)
                        {
                            peso[0] = avaliacao[avaliacao.Count - 1].Peso1;
                            peso[1] = avaliacao[avaliacao.Count - 1].Peso2;
                            peso[2] = avaliacao[avaliacao.Count - 1].Peso3;
                            peso[3] = avaliacao[avaliacao.Count - 1].Peso4;
                            peso[4] = avaliacao[avaliacao.Count - 1].Bias;

                            LoadInformation((int)avaliacao[avaliacao.Count - 1].AcertoPorcent, (int)avaliacao[avaliacao.Count - 1].QtdAcerto, (int)avaliacao[avaliacao.Count - 1].ErroPorcent, (int)avaliacao[avaliacao.Count - 1].QtdErro, dTotalIrisVirginica, bTreinamento);
                            break;
                        }
                    }

                if (!bTreinamento)
                {
                    LoadInformation((int)avaliacao[0].AcertoPorcent, (int)avaliacao[0].QtdAcerto, (int)avaliacao[0].ErroPorcent, (int)avaliacao[0].QtdErro, dTotalIrisVirginica, bTreinamento);

                    if (lblAcertoTeste.Text.Equals(lblAcertoTrein.Text) && avaliacao[0].AcertoPorcent == 100
                        && lblErroTeste.Text.Equals(lblErroTrein.Text) && avaliacao[0].ErroPorcent == 0)
                    {
                        _irisVirginica.Peso1 = peso[0];
                        _irisVirginica.Peso2 = peso[1];
                        _irisVirginica.Peso3 = peso[2];
                        _irisVirginica.Peso4 = peso[3];
                        _irisVirginica.Bias = peso[4];
                        _irisVirginica.Soma = _dIrisVirginica;
                    }
                    break;
                }
            }

            if (iCount == 100)
            {
                LoadInformation((int)avaliacao[avaliacao.Count - 1].AcertoPorcent, (int)avaliacao[avaliacao.Count - 1].QtdAcerto, (int)avaliacao[avaliacao.Count - 1].ErroPorcent, (int)avaliacao[avaliacao.Count - 1].QtdErro, dTotalIrisVirginica, bTreinamento);

                MessageBox.Show(
                    "Por várias tentativas a I.A. não conseguiu formular um parâmetro de avalição das espécies.\r\nPor favor, tente novamente para seu critério de avaliação ser reiniciado.",
                    "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EnabledFields(bool bEnabled)
        {
            rdIrisVersicolor.Enabled = bEnabled;
            rdIrisVirginica.Enabled = bEnabled;
        }

        private void LoadPesos()
        {
            switch (_iTipo)
            {
                case 1:
                    for (int i = 0; i <= peso.Count() - 1; i++)
                        peso[i] = _random.Next(10);

                    _dIrisSetosa = _random.Next(100);

                    if (IrisSetosa.Peso1 != null && IrisSetosa.Soma != null)
                    {
                        if (MessageBox.Show("Deseja utilizar a mesma base de aprendizagem?", "Atenção!",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            peso[0] = IrisSetosa.Peso1;
                            peso[1] = IrisSetosa.Peso2;
                            peso[2] = IrisSetosa.Peso3;
                            peso[3] = IrisSetosa.Peso4;
                            peso[4] = IrisSetosa.Bias;
                            _dIrisSetosa = (double)IrisSetosa.Soma;
                        }
                    }

                    ShowValues((int)peso[0], (int)peso[1], (int)peso[2], (int)peso[3], (int)peso[4], _dIrisSetosa);
                    break;

                case 2:
                    _dIrisVersicolor = 100;
                    ShowValues((int)peso[0], (int)peso[1], (int)peso[2], (int)peso[3], (int)peso[4], _dIrisVersicolor);
                    break;

                case 3:
                    if (_irisVirginica.Peso1 != null && _irisVirginica.Soma != null)
                    {
                        peso[0] = _irisVirginica.Peso1;
                        peso[1] = _irisVirginica.Peso2;
                        peso[2] = _irisVirginica.Peso3;
                        peso[3] = _irisVirginica.Peso4;
                        peso[4] = _irisVirginica.Bias;
                        _dIrisVirginica = (double)_irisVirginica.Soma;
                    }
                    else
                    {
                        for (int i = 0; i <= peso.Count() - 1; i++)
                            peso[i] = _random.Next(10);

                        _dIrisVirginica = _random.Next(100);

                        if (IrisSetosa.Peso1 != null)
                            if (MessageBox.Show("Deseja utilizar base de aprendizagem da Iris Setosa?", "Atenção!",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                peso[0] = IrisSetosa.Peso1;
                                peso[1] = IrisSetosa.Peso2;
                                peso[2] = IrisSetosa.Peso3;
                                peso[3] = IrisSetosa.Peso4;
                                peso[4] = IrisSetosa.Bias;
                                _dIrisVirginica = (double)IrisSetosa.Soma;
                            }
                    }

                    ShowValues((int)peso[0], (int)peso[1], (int)peso[2], (int)peso[3], (int)peso[4], _dIrisVirginica);
                    break;
            }

        }

        private bool VerificaSelecao()
        {
            if (!rdIrisSetosa.Checked && !rdIrisVersicolor.Checked && !rdIrisVirginica.Checked)
                return false;

            return true;
        }

        private void LoadInformation(int acertoPorcent, int qtdAcerto, int ErroPorcent, int qtdErro, double total, bool treinamento)
        {
            var tipo = rdIrisSetosa.Checked ? rdIrisSetosa.Text : rdIrisVersicolor.Checked ? rdIrisVersicolor.Text : rdIrisVirginica.Text;

            if (treinamento)
            {
                lblAcertoTrein.Text = $"Porcentagem de acerto: {acertoPorcent}%";
                lblQtdAcertoTrein.Text = $"Quantidade: {qtdAcerto}";
                lblErroTrein.Text = $"Porcentagem de erro: {ErroPorcent}%";
                lblQtdErroTrein.Text = $"Quantidade: {qtdErro}";
                lblTotalTrein.Text = $"Total de {tipo}: {total}";
            }
            else
            {
                lblAcertoTeste.Text = $"Porcentagem de acerto: {acertoPorcent}%";
                lblQtdAcertoTeste.Text = $"Quantidade: {qtdAcerto}";
                lblErroTeste.Text = $"Porcentagem de erro: {ErroPorcent}%";
                lblQtdErroTeste.Text = $"Quantidade: {qtdErro}";
                lblTotalTeste.Text = $"Total de {tipo}: {total}";
            }
        }

        private void ShowValues(int p1, int p2, int p3, int p4, int bias, double soma)
        {
            lblPeso1.Text = $"Peso 1: {p1}";
            lblPeso2.Text = $"Peso 2: {p2}";
            lblPeso3.Text = $"Peso 3: {p3}";
            lblPeso4.Text = $"Peso 4: {p4}";
            lblBias.Text = $"Bias: {bias}";
            lblSoma.Text = $"Soma: {soma}";

            lblPeso1.Update();
            lblPeso2.Update();
            lblPeso3.Update();
            lblPeso4.Update();
            lblBias.Update();
            lblSoma.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (VerificaSelecao())
            {
                Cursor = Cursors.WaitCursor;
                LoadPesos();
                Separacao();

                switch (_iTipo)
                {
                    case 1:
                        AvaliarIrisSetosa(treinamento, true);
                        ShowValues((int) peso[0], (int) peso[1], (int) peso[2], (int) peso[3], (int) peso[4], _dIrisSetosa);
                        AvaliarIrisSetosa(teste, false);
                        break;

                    case 2:
                        AvaliarIrisVersicolor(treinamento, true);
                        ShowValues((int)peso[0], (int)peso[1], (int)peso[2], (int)peso[3], (int)peso[4], _dIrisVersicolor);
                        AvaliarIrisVersicolor(teste, false);
                        break;

                    case 3:
                        AvaliarIrisVirginica(treinamento, true);
                        ShowValues((int)peso[0], (int)peso[1], (int)peso[2], (int)peso[3], (int)peso[4], _dIrisVirginica);
                        AvaliarIrisVirginica(teste, false);
                        break;
                }

                Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show(
                    "Selecione uma espécie para realizar a aprendizagem!",
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
