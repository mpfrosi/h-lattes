namespace HLattes
{
    partial class FrmResult
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.tituloDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.citacoesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.datCurriculos1 = new HLattes.datCurriculos();
            this.lblHindexTitle = new System.Windows.Forms.Label();
            this.lblHindex = new System.Windows.Forms.Label();
            this.lblFiltro = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblGindexTitle = new System.Windows.Forms.Label();
            this.lblGindex = new System.Windows.Forms.Label();
            this.lblAutor = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCitTotal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCitHindex = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCitGindex = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datCurriculos1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoGenerateColumns = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tituloDataGridViewTextBoxColumn,
            this.citacoesDataGridViewTextBoxColumn});
            this.dataGridView.DataSource = this.bindingSource;
            this.dataGridView.Location = new System.Drawing.Point(12, 71);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(838, 336);
            this.dataGridView.TabIndex = 0;
            // 
            // tituloDataGridViewTextBoxColumn
            // 
            this.tituloDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tituloDataGridViewTextBoxColumn.DataPropertyName = "Titulo";
            this.tituloDataGridViewTextBoxColumn.HeaderText = "Titulo";
            this.tituloDataGridViewTextBoxColumn.Name = "tituloDataGridViewTextBoxColumn";
            this.tituloDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // citacoesDataGridViewTextBoxColumn
            // 
            this.citacoesDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.citacoesDataGridViewTextBoxColumn.DataPropertyName = "Citacoes";
            this.citacoesDataGridViewTextBoxColumn.FillWeight = 20F;
            this.citacoesDataGridViewTextBoxColumn.HeaderText = "Citações";
            this.citacoesDataGridViewTextBoxColumn.Name = "citacoesDataGridViewTextBoxColumn";
            this.citacoesDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bindingSource
            // 
            this.bindingSource.DataMember = "Publicacao";
            this.bindingSource.DataSource = this.datCurriculos1;
            // 
            // datCurriculos1
            // 
            this.datCurriculos1.DataSetName = "datCurriculos";
            this.datCurriculos1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lblHindexTitle
            // 
            this.lblHindexTitle.AutoSize = true;
            this.lblHindexTitle.Location = new System.Drawing.Point(165, 24);
            this.lblHindexTitle.Name = "lblHindexTitle";
            this.lblHindexTitle.Size = new System.Drawing.Size(46, 13);
            this.lblHindexTitle.TabIndex = 1;
            this.lblHindexTitle.Text = "H-index:";
            // 
            // lblHindex
            // 
            this.lblHindex.AutoSize = true;
            this.lblHindex.Location = new System.Drawing.Point(217, 24);
            this.lblHindex.Name = "lblHindex";
            this.lblHindex.Size = new System.Drawing.Size(17, 13);
            this.lblHindex.TabIndex = 2;
            this.lblHindex.Text = "xx";
            // 
            // lblFiltro
            // 
            this.lblFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFiltro.AutoSize = true;
            this.lblFiltro.Location = new System.Drawing.Point(12, 416);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(32, 13);
            this.lblFiltro.TabIndex = 3;
            this.lblFiltro.Text = "Filtro:";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(50, 413);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(800, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblGindexTitle
            // 
            this.lblGindexTitle.AutoSize = true;
            this.lblGindexTitle.Location = new System.Drawing.Point(311, 24);
            this.lblGindexTitle.Name = "lblGindexTitle";
            this.lblGindexTitle.Size = new System.Drawing.Size(47, 13);
            this.lblGindexTitle.TabIndex = 5;
            this.lblGindexTitle.Text = "G-Index:";
            // 
            // lblGindex
            // 
            this.lblGindex.AutoSize = true;
            this.lblGindex.Location = new System.Drawing.Point(371, 24);
            this.lblGindex.Name = "lblGindex";
            this.lblGindex.Size = new System.Drawing.Size(17, 13);
            this.lblGindex.TabIndex = 6;
            this.lblGindex.Text = "xx";
            // 
            // lblAutor
            // 
            this.lblAutor.AutoSize = true;
            this.lblAutor.Location = new System.Drawing.Point(12, 7);
            this.lblAutor.Name = "lblAutor";
            this.lblAutor.Size = new System.Drawing.Size(42, 13);
            this.lblAutor.TabIndex = 7;
            this.lblAutor.Text = "lblAutor";
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.Location = new System.Drawing.Point(775, 11);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 8;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // sfdSaveFile
            // 
            this.sfdSaveFile.DefaultExt = "cvs";
            this.sfdSaveFile.Filter = "Arquivos CSV|*.csv";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Total Citações:";
            // 
            // lblCitTotal
            // 
            this.lblCitTotal.AutoSize = true;
            this.lblCitTotal.Location = new System.Drawing.Point(97, 24);
            this.lblCitTotal.Name = "lblCitTotal";
            this.lblCitTotal.Size = new System.Drawing.Size(17, 13);
            this.lblCitTotal.TabIndex = 10;
            this.lblCitTotal.Text = "xx";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(120, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Citações H-Index:";
            // 
            // lblCitHindex
            // 
            this.lblCitHindex.AutoSize = true;
            this.lblCitHindex.Location = new System.Drawing.Point(217, 46);
            this.lblCitHindex.Name = "lblCitHindex";
            this.lblCitHindex.Size = new System.Drawing.Size(17, 13);
            this.lblCitHindex.TabIndex = 12;
            this.lblCitHindex.Text = "xx";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(267, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Citações G-Index:";
            // 
            // lblCitGindex
            // 
            this.lblCitGindex.AutoSize = true;
            this.lblCitGindex.Location = new System.Drawing.Point(371, 46);
            this.lblCitGindex.Name = "lblCitGindex";
            this.lblCitGindex.Size = new System.Drawing.Size(17, 13);
            this.lblCitGindex.TabIndex = 14;
            this.lblCitGindex.Text = "xx";
            // 
            // FrmResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 438);
            this.Controls.Add(this.lblCitGindex);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblCitHindex);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCitTotal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.lblAutor);
            this.Controls.Add(this.lblGindex);
            this.Controls.Add(this.lblGindexTitle);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblFiltro);
            this.Controls.Add(this.lblHindex);
            this.Controls.Add(this.lblHindexTitle);
            this.Controls.Add(this.dataGridView);
            this.Name = "FrmResult";
            this.ShowIcon = false;
            this.Text = "Resultado da pesquisa";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datCurriculos1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label lblHindexTitle;
        private System.Windows.Forms.Label lblHindex;
        private System.Windows.Forms.BindingSource bindingSource;
        private datCurriculos datCurriculos1;
        private System.Windows.Forms.DataGridViewTextBoxColumn tituloDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn citacoesDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label lblFiltro;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblGindexTitle;
        private System.Windows.Forms.Label lblGindex;
        private System.Windows.Forms.Label lblAutor;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.SaveFileDialog sfdSaveFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCitTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCitHindex;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCitGindex;
    }
}