using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
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
        List<Treinamento> treinamento = new List<Treinamento>();
        List<Teste> teste = new List<Teste>();
        List<Avaliacao> avaliacao = new List<Avaliacao>();
        List<Resultado> resultado = new List<Resultado>();
        int[] peso = { 5, 3, 6, 0, 2 };

        private void loadTipoEspecie()
        {
            tipoEspecie.Add(new TipoEspecie { IDTipo = 1, Especie = "Iris-setosa" });
            tipoEspecie.Add(new TipoEspecie { IDTipo = 2, Especie = "Iris-versicolor" });
            tipoEspecie.Add(new TipoEspecie { IDTipo = 3, Especie = "Iris-virginica" });
        }

        private void loadEspecie()
        {
            var caminho = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "\\Especies.txt";
            StreamReader arquivo = new StreamReader(caminho);

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
        }

        private void separacao()
        {
            List<int> verifica = new List<int>();
            for (int i = 1; i <= especie.Count / 2; i++)
            {
                var posicao = random.Next(especie.Count);
                if (verifica.AsEnumerable().ToList().Where(x => x.Equals(posicao)).ToList().Count > 0)
                {
                    i--;
                    continue;
                }

                treinamento.Add(new Treinamento
                {
                    IDTipo = especie[posicao].IDTipo,
                    Entradas = especie[posicao].Entradas
                });
                verifica.Add(posicao);
            }

            for (int i = 0; i < especie.Count; i++)
            {
                if (verifica.AsEnumerable().ToList().Where(x => x.Equals(i)).ToList().Count == 0)
                    teste.Add(new Teste { IDTipo = especie[i].IDTipo, Entradas = especie[i].Entradas });
            }
        }

        private void avaliar(List<Avaliacao> verifica)
        {
            while (true)
            {
                foreach (var entradas in verifica)
                {
                    double soma = 0;
                    var x = entradas.Entradas.Split(',');
                    for (int i = 0; i < x.Length; i++)
                    {
                        soma += (Convert.ToDouble(x[i].Replace(".", ",")) * peso[i]);
                    }
                    soma += peso[4];


                    var tpt = tipoEspecie.Where(a => a.IDTipo.Equals(entradas.IDTipo));

                    if (Math.Round(soma, 0) <= 46)
                    {
                        //var especieVerdadeira = tipoEspecie.FirstOrDefault(a => a.IDTipo.Equals(entradas.IDTipo))?.Especie;
                        resultado.Add(new Resultado
                        {
                            Entradas = entradas.Entradas,
                            IDTipoResultado = 1,
                            IDTipoVerdadeira = Convert.ToInt32(tipoEspecie.FirstOrDefault(a => a.IDTipo.Equals(entradas.IDTipo))?.Especie)
                        });
                    }
                    else
                    {
                        if (entradas.IDTipo == 1)
                        {
                            peso[0] -= 1;
                            peso[1] -= 1;
                            peso[2] -= 1;
                            peso[3] -= 1;
                            peso[4] -= 1;
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadTipoEspecie();
            loadEspecie();
            separacao();
            treinamento.ForEach(x => avaliacao.Add(new Avaliacao { IDTipo = x.IDTipo, Entradas = x.Entradas }));
            avaliar(avaliacao);


        }
    }
}
