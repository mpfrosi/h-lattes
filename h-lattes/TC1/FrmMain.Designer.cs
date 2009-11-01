namespace HLattes
{
    partial class FrmMain
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
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.novaBuscaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.porNomeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.porURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.porArquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdSelectFile = new System.Windows.Forms.OpenFileDialog();
            this.opçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.novaBuscaToolStripMenuItem,
            this.ajudaToolStripMenuItem,
            this.opçõesToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(916, 24);
            this.mnuMain.TabIndex = 1;
            this.mnuMain.Text = "menuStrip1";
            // 
            // novaBuscaToolStripMenuItem
            // 
            this.novaBuscaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.porNomeToolStripMenuItem,
            this.porURLToolStripMenuItem,
            this.porArquivoToolStripMenuItem});
            this.novaBuscaToolStripMenuItem.Name = "novaBuscaToolStripMenuItem";
            this.novaBuscaToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.novaBuscaToolStripMenuItem.Text = "Nova Busca";
            // 
            // porNomeToolStripMenuItem
            // 
            this.porNomeToolStripMenuItem.Name = "porNomeToolStripMenuItem";
            this.porNomeToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.porNomeToolStripMenuItem.Text = "Por Nome";
            this.porNomeToolStripMenuItem.Click += new System.EventHandler(this.porNomeToolStripMenuItem_Click);
            // 
            // porURLToolStripMenuItem
            // 
            this.porURLToolStripMenuItem.Name = "porURLToolStripMenuItem";
            this.porURLToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.porURLToolStripMenuItem.Text = "Por URL";
            this.porURLToolStripMenuItem.Click += new System.EventHandler(this.porURLToolStripMenuItem_Click);
            // 
            // porArquivoToolStripMenuItem
            // 
            this.porArquivoToolStripMenuItem.Name = "porArquivoToolStripMenuItem";
            this.porArquivoToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.porArquivoToolStripMenuItem.Text = "Por Arquivo";
            this.porArquivoToolStripMenuItem.Click += new System.EventHandler(this.porArquivoToolStripMenuItem_Click);
            // 
            // ajudaToolStripMenuItem
            // 
            this.ajudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sobreToolStripMenuItem});
            this.ajudaToolStripMenuItem.Name = "ajudaToolStripMenuItem";
            this.ajudaToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.ajudaToolStripMenuItem.Text = "Ajuda";
            this.ajudaToolStripMenuItem.Visible = false;
            // 
            // sobreToolStripMenuItem
            // 
            this.sobreToolStripMenuItem.Name = "sobreToolStripMenuItem";
            this.sobreToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.sobreToolStripMenuItem.Text = "Sobre";
            // 
            // ofdSelectFile
            // 
            this.ofdSelectFile.Filter = "Arquivos txt|*.txt";
            // 
            // opçõesToolStripMenuItem
            // 
            this.opçõesToolStripMenuItem.Name = "opçõesToolStripMenuItem";
            this.opçõesToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.opçõesToolStripMenuItem.Text = "Opções";
            this.opçõesToolStripMenuItem.Click += new System.EventHandler(this.opçõesToolStripMenuItem_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 491);
            this.Controls.Add(this.mnuMain);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuMain;
            this.Name = "FrmMain";
            this.Text = "FAAC";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem novaBuscaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem porNomeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem porURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem porArquivoToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog ofdSelectFile;
        private System.Windows.Forms.ToolStripMenuItem opçõesToolStripMenuItem;
    }
}