namespace HLattes
{
    partial class FrmEnterURL
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
            this.btnOk = new System.Windows.Forms.Button();
            this.tbxUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlURL = new System.Windows.Forms.Panel();
            this.lblArtigo = new System.Windows.Forms.Label();
            this.lblAutor = new System.Windows.Forms.Label();
            this.pnlURL.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(416, 22);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tbxUrl
            // 
            this.tbxUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxUrl.Location = new System.Drawing.Point(7, 25);
            this.tbxUrl.Name = "tbxUrl";
            this.tbxUrl.Size = new System.Drawing.Size(398, 20);
            this.tbxUrl.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "URL para Currículo Lattes a ser pesquisado:";
            // 
            // ToolTips
            // 
            this.ToolTips.Tag = "";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(7, 57);
            this.progressBar.Maximum = 100000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(398, 23);
            this.progressBar.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(416, 57);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlURL
            // 
            this.pnlURL.Controls.Add(this.label1);
            this.pnlURL.Controls.Add(this.tbxUrl);
            this.pnlURL.Controls.Add(this.btnOk);
            this.pnlURL.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlURL.Location = new System.Drawing.Point(0, 0);
            this.pnlURL.Name = "pnlURL";
            this.pnlURL.Size = new System.Drawing.Size(503, 51);
            this.pnlURL.TabIndex = 6;
            // 
            // lblArtigo
            // 
            this.lblArtigo.AutoSize = true;
            this.lblArtigo.Location = new System.Drawing.Point(4, 32);
            this.lblArtigo.Name = "lblArtigo";
            this.lblArtigo.Size = new System.Drawing.Size(98, 13);
            this.lblArtigo.TabIndex = 4;
            this.lblArtigo.Text = "Buscando curriculo";
            // 
            // lblAutor
            // 
            this.lblAutor.AutoSize = true;
            this.lblAutor.Location = new System.Drawing.Point(4, 15);
            this.lblAutor.Name = "lblAutor";
            this.lblAutor.Size = new System.Drawing.Size(35, 13);
            this.lblAutor.TabIndex = 7;
            this.lblAutor.Text = "label2";
            // 
            // FrmEnterURL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 93);
            this.Controls.Add(this.lblAutor);
            this.Controls.Add(this.lblArtigo);
            this.Controls.Add(this.pnlURL);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.progressBar);
            this.Location = new System.Drawing.Point(30, 30);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(511, 127);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(511, 127);
            this.Name = "FrmEnterURL";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Endereço do Currículo Lattes";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmEnterURL_FormClosed);
            this.pnlURL.ResumeLayout(false);
            this.pnlURL.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox tbxUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip ToolTips;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlURL;
        private System.Windows.Forms.Label lblArtigo;
        private System.Windows.Forms.Label lblAutor;
    }
}