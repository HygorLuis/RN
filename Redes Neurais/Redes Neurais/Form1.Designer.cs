namespace Redes_Neurais
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAprender = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lvEspecies = new System.Windows.Forms.ListView();
            this.especies = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.especies1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblTotalTrein = new System.Windows.Forms.Label();
            this.lvTreinamento = new System.Windows.Forms.ListView();
            this.treina = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.treina1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblTotalTeste = new System.Windows.Forms.Label();
            this.lvTeste = new System.Windows.Forms.ListView();
            this.avaliacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.avaliacao1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lblQtdErroTrein = new System.Windows.Forms.Label();
            this.lblErroTrein = new System.Windows.Forms.Label();
            this.lblQtdAcertoTrein = new System.Windows.Forms.Label();
            this.lblAcertoTrein = new System.Windows.Forms.Label();
            this.lvResultTreinamento = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.lblQtdErroTeste = new System.Windows.Forms.Label();
            this.lblErroTeste = new System.Windows.Forms.Label();
            this.lblQtdAcertoTeste = new System.Windows.Forms.Label();
            this.lblAcertoTeste = new System.Windows.Forms.Label();
            this.lvResultTeste = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rdIrisSetosa = new System.Windows.Forms.RadioButton();
            this.rdIrisVersicolor = new System.Windows.Forms.RadioButton();
            this.rdIrisVirginica = new System.Windows.Forms.RadioButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAprender
            // 
            this.btnAprender.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAprender.Location = new System.Drawing.Point(352, 192);
            this.btnAprender.Name = "btnAprender";
            this.btnAprender.Size = new System.Drawing.Size(87, 23);
            this.btnAprender.TabIndex = 0;
            this.btnAprender.Text = "Aprender";
            this.btnAprender.UseVisualStyleBackColor = true;
            this.btnAprender.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabControl1.Location = new System.Drawing.Point(31, 59);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(205, 354);
            this.tabControl1.TabIndex = 8;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lvEspecies);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(197, 328);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Espécies";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lvEspecies
            // 
            this.lvEspecies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.especies,
            this.especies1});
            this.lvEspecies.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvEspecies.Location = new System.Drawing.Point(-4, 0);
            this.lvEspecies.Name = "lvEspecies";
            this.lvEspecies.Size = new System.Drawing.Size(226, 329);
            this.lvEspecies.TabIndex = 0;
            this.lvEspecies.UseCompatibleStateImageBehavior = false;
            this.lvEspecies.View = System.Windows.Forms.View.Details;
            // 
            // especies
            // 
            this.especies.Text = "Entradas";
            this.especies.Width = 100;
            // 
            // especies1
            // 
            this.especies1.Text = "Tipo Espécie";
            this.especies1.Width = 100;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblTotalTrein);
            this.tabPage2.Controls.Add(this.lvTreinamento);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(197, 328);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Treinamento";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblTotalTrein
            // 
            this.lblTotalTrein.AutoSize = true;
            this.lblTotalTrein.Location = new System.Drawing.Point(27, 20);
            this.lblTotalTrein.Name = "lblTotalTrein";
            this.lblTotalTrein.Size = new System.Drawing.Size(52, 13);
            this.lblTotalTrein.TabIndex = 2;
            this.lblTotalTrein.Text = "Total de :";
            // 
            // lvTreinamento
            // 
            this.lvTreinamento.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.treina,
            this.treina1});
            this.lvTreinamento.Location = new System.Drawing.Point(-4, 51);
            this.lvTreinamento.Name = "lvTreinamento";
            this.lvTreinamento.Size = new System.Drawing.Size(226, 278);
            this.lvTreinamento.TabIndex = 1;
            this.lvTreinamento.UseCompatibleStateImageBehavior = false;
            this.lvTreinamento.View = System.Windows.Forms.View.Details;
            // 
            // treina
            // 
            this.treina.Text = "Entradas";
            this.treina.Width = 100;
            // 
            // treina1
            // 
            this.treina1.Text = "Tipo Espécie";
            this.treina1.Width = 100;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lblTotalTeste);
            this.tabPage3.Controls.Add(this.lvTeste);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(197, 328);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Teste";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lblTotalTeste
            // 
            this.lblTotalTeste.AutoSize = true;
            this.lblTotalTeste.Location = new System.Drawing.Point(27, 20);
            this.lblTotalTeste.Name = "lblTotalTeste";
            this.lblTotalTeste.Size = new System.Drawing.Size(52, 13);
            this.lblTotalTeste.TabIndex = 2;
            this.lblTotalTeste.Text = "Total de :";
            // 
            // lvTeste
            // 
            this.lvTeste.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.avaliacao,
            this.avaliacao1});
            this.lvTeste.Location = new System.Drawing.Point(-4, 51);
            this.lvTeste.Name = "lvTeste";
            this.lvTeste.Size = new System.Drawing.Size(226, 278);
            this.lvTeste.TabIndex = 1;
            this.lvTeste.UseCompatibleStateImageBehavior = false;
            this.lvTeste.View = System.Windows.Forms.View.Details;
            // 
            // avaliacao
            // 
            this.avaliacao.Text = "Entradas";
            this.avaliacao.Width = 100;
            // 
            // avaliacao1
            // 
            this.avaliacao1.Text = "Tipo Espécie";
            this.avaliacao1.Width = 100;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabControl2.Location = new System.Drawing.Point(559, 59);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(308, 354);
            this.tabControl2.TabIndex = 9;
            this.tabControl2.SelectedIndexChanged += new System.EventHandler(this.tabControl2_SelectedIndexChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lblQtdErroTrein);
            this.tabPage4.Controls.Add(this.lblErroTrein);
            this.tabPage4.Controls.Add(this.lblQtdAcertoTrein);
            this.tabPage4.Controls.Add(this.lblAcertoTrein);
            this.tabPage4.Controls.Add(this.lvResultTreinamento);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(300, 328);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Treinamento";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lblQtdErroTrein
            // 
            this.lblQtdErroTrein.AutoSize = true;
            this.lblQtdErroTrein.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblQtdErroTrein.ForeColor = System.Drawing.Color.Red;
            this.lblQtdErroTrein.Location = new System.Drawing.Point(160, 30);
            this.lblQtdErroTrein.Name = "lblQtdErroTrein";
            this.lblQtdErroTrein.Size = new System.Drawing.Size(68, 13);
            this.lblQtdErroTrein.TabIndex = 11;
            this.lblQtdErroTrein.Text = "Quantidade: ";
            // 
            // lblErroTrein
            // 
            this.lblErroTrein.AutoSize = true;
            this.lblErroTrein.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblErroTrein.ForeColor = System.Drawing.Color.Red;
            this.lblErroTrein.Location = new System.Drawing.Point(160, 7);
            this.lblErroTrein.Name = "lblErroTrein";
            this.lblErroTrein.Size = new System.Drawing.Size(112, 13);
            this.lblErroTrein.TabIndex = 10;
            this.lblErroTrein.Text = "Porcentagem de erro: ";
            // 
            // lblQtdAcertoTrein
            // 
            this.lblQtdAcertoTrein.AutoSize = true;
            this.lblQtdAcertoTrein.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblQtdAcertoTrein.ForeColor = System.Drawing.Color.Green;
            this.lblQtdAcertoTrein.Location = new System.Drawing.Point(6, 30);
            this.lblQtdAcertoTrein.Name = "lblQtdAcertoTrein";
            this.lblQtdAcertoTrein.Size = new System.Drawing.Size(71, 13);
            this.lblQtdAcertoTrein.TabIndex = 9;
            this.lblQtdAcertoTrein.Text = "Quantidade:  ";
            // 
            // lblAcertoTrein
            // 
            this.lblAcertoTrein.AutoSize = true;
            this.lblAcertoTrein.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblAcertoTrein.ForeColor = System.Drawing.Color.Green;
            this.lblAcertoTrein.Location = new System.Drawing.Point(6, 7);
            this.lblAcertoTrein.Name = "lblAcertoTrein";
            this.lblAcertoTrein.Size = new System.Drawing.Size(124, 13);
            this.lblAcertoTrein.TabIndex = 8;
            this.lblAcertoTrein.Text = "Porcentagem de acerto: ";
            // 
            // lvResultTreinamento
            // 
            this.lvResultTreinamento.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvResultTreinamento.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvResultTreinamento.Location = new System.Drawing.Point(-1, 51);
            this.lvResultTreinamento.Name = "lvResultTreinamento";
            this.lvResultTreinamento.Size = new System.Drawing.Size(321, 278);
            this.lvResultTreinamento.TabIndex = 2;
            this.lvResultTreinamento.UseCompatibleStateImageBehavior = false;
            this.lvResultTreinamento.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Entradas";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Tipo Resultado";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Tipo Verdadeiro";
            this.columnHeader3.Width = 100;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.lblQtdErroTeste);
            this.tabPage5.Controls.Add(this.lblErroTeste);
            this.tabPage5.Controls.Add(this.lblQtdAcertoTeste);
            this.tabPage5.Controls.Add(this.lblAcertoTeste);
            this.tabPage5.Controls.Add(this.lvResultTeste);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(300, 328);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Teste";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // lblQtdErroTeste
            // 
            this.lblQtdErroTeste.AutoSize = true;
            this.lblQtdErroTeste.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblQtdErroTeste.ForeColor = System.Drawing.Color.Red;
            this.lblQtdErroTeste.Location = new System.Drawing.Point(160, 30);
            this.lblQtdErroTeste.Name = "lblQtdErroTeste";
            this.lblQtdErroTeste.Size = new System.Drawing.Size(68, 13);
            this.lblQtdErroTeste.TabIndex = 9;
            this.lblQtdErroTeste.Text = "Quantidade: ";
            // 
            // lblErroTeste
            // 
            this.lblErroTeste.AutoSize = true;
            this.lblErroTeste.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblErroTeste.ForeColor = System.Drawing.Color.Red;
            this.lblErroTeste.Location = new System.Drawing.Point(160, 7);
            this.lblErroTeste.Name = "lblErroTeste";
            this.lblErroTeste.Size = new System.Drawing.Size(112, 13);
            this.lblErroTeste.TabIndex = 8;
            this.lblErroTeste.Text = "Porcentagem de erro: ";
            // 
            // lblQtdAcertoTeste
            // 
            this.lblQtdAcertoTeste.AutoSize = true;
            this.lblQtdAcertoTeste.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblQtdAcertoTeste.ForeColor = System.Drawing.Color.Green;
            this.lblQtdAcertoTeste.Location = new System.Drawing.Point(6, 30);
            this.lblQtdAcertoTeste.Name = "lblQtdAcertoTeste";
            this.lblQtdAcertoTeste.Size = new System.Drawing.Size(71, 13);
            this.lblQtdAcertoTeste.TabIndex = 7;
            this.lblQtdAcertoTeste.Text = "Quantidade:  ";
            // 
            // lblAcertoTeste
            // 
            this.lblAcertoTeste.AutoSize = true;
            this.lblAcertoTeste.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblAcertoTeste.ForeColor = System.Drawing.Color.Green;
            this.lblAcertoTeste.Location = new System.Drawing.Point(6, 7);
            this.lblAcertoTeste.Name = "lblAcertoTeste";
            this.lblAcertoTeste.Size = new System.Drawing.Size(124, 13);
            this.lblAcertoTeste.TabIndex = 6;
            this.lblAcertoTeste.Text = "Porcentagem de acerto: ";
            // 
            // lvResultTeste
            // 
            this.lvResultTeste.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lvResultTeste.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvResultTeste.Location = new System.Drawing.Point(-1, 51);
            this.lvResultTeste.Name = "lvResultTeste";
            this.lvResultTeste.Size = new System.Drawing.Size(321, 282);
            this.lvResultTeste.TabIndex = 2;
            this.lvResultTeste.UseCompatibleStateImageBehavior = false;
            this.lvResultTeste.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Entradas";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Tipo Resultado";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Tipo Verdadeiro";
            this.columnHeader6.Width = 100;
            // 
            // rdIrisSetosa
            // 
            this.rdIrisSetosa.AutoSize = true;
            this.rdIrisSetosa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdIrisSetosa.Location = new System.Drawing.Point(272, 149);
            this.rdIrisSetosa.Name = "rdIrisSetosa";
            this.rdIrisSetosa.Size = new System.Drawing.Size(74, 17);
            this.rdIrisSetosa.TabIndex = 10;
            this.rdIrisSetosa.Text = "Iris Setosa";
            this.rdIrisSetosa.UseVisualStyleBackColor = true;
            this.rdIrisSetosa.CheckedChanged += new System.EventHandler(this.rdIrisSetosa_CheckedChanged);
            // 
            // rdIrisVersicolor
            // 
            this.rdIrisVersicolor.AutoSize = true;
            this.rdIrisVersicolor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdIrisVersicolor.Location = new System.Drawing.Point(352, 149);
            this.rdIrisVersicolor.Name = "rdIrisVersicolor";
            this.rdIrisVersicolor.Size = new System.Drawing.Size(87, 17);
            this.rdIrisVersicolor.TabIndex = 11;
            this.rdIrisVersicolor.Text = "Iris Versicolor";
            this.rdIrisVersicolor.UseVisualStyleBackColor = true;
            this.rdIrisVersicolor.CheckedChanged += new System.EventHandler(this.rdIrisVersicolor_CheckedChanged);
            // 
            // rdIrisVirginica
            // 
            this.rdIrisVirginica.AutoSize = true;
            this.rdIrisVirginica.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdIrisVirginica.Location = new System.Drawing.Point(445, 149);
            this.rdIrisVirginica.Name = "rdIrisVirginica";
            this.rdIrisVirginica.Size = new System.Drawing.Size(81, 17);
            this.rdIrisVirginica.TabIndex = 12;
            this.rdIrisVirginica.Text = "Iris Virginica";
            this.rdIrisVirginica.UseVisualStyleBackColor = true;
            this.rdIrisVirginica.CheckedChanged += new System.EventHandler(this.rdIrisVirginica_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 439);
            this.Controls.Add(this.rdIrisVirginica);
            this.Controls.Add(this.rdIrisVersicolor);
            this.Controls.Add(this.rdIrisSetosa);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnAprender);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Redes Neurais";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAprender;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView lvEspecies;
        private System.Windows.Forms.ColumnHeader especies;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ColumnHeader especies1;
        private System.Windows.Forms.ListView lvTreinamento;
        private System.Windows.Forms.ColumnHeader treina;
        private System.Windows.Forms.ColumnHeader treina1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView lvTeste;
        private System.Windows.Forms.ColumnHeader avaliacao;
        private System.Windows.Forms.ColumnHeader avaliacao1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView lvResultTreinamento;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ListView lvResultTeste;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label lblQtdErroTrein;
        private System.Windows.Forms.Label lblErroTrein;
        private System.Windows.Forms.Label lblQtdAcertoTrein;
        private System.Windows.Forms.Label lblAcertoTrein;
        private System.Windows.Forms.Label lblQtdErroTeste;
        private System.Windows.Forms.Label lblErroTeste;
        private System.Windows.Forms.Label lblQtdAcertoTeste;
        private System.Windows.Forms.Label lblAcertoTeste;
        private System.Windows.Forms.RadioButton rdIrisSetosa;
        private System.Windows.Forms.RadioButton rdIrisVersicolor;
        private System.Windows.Forms.RadioButton rdIrisVirginica;
        private System.Windows.Forms.Label lblTotalTrein;
        private System.Windows.Forms.Label lblTotalTeste;
    }
}

