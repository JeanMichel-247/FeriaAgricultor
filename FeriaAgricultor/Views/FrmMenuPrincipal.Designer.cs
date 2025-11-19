namespace FeriaAgricultor.Views
{
    partial class FrmMenuPrincipal
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
            menuStrip1 = new MenuStrip();
            catálogoComprarToolStripMenuItem = new ToolStripMenuItem();
            misReportesToolStripMenuItem = new ToolStripMenuItem();
            salirToolStripMenuItem = new ToolStripMenuItem();
            lblBienvenida = new Label();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { catálogoComprarToolStripMenuItem, misReportesToolStripMenuItem, salirToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(326, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // catálogoComprarToolStripMenuItem
            // 
            catálogoComprarToolStripMenuItem.Name = "catálogoComprarToolStripMenuItem";
            catálogoComprarToolStripMenuItem.Size = new Size(125, 20);
            catálogoComprarToolStripMenuItem.Text = "Catálogo / Comprar";
            catálogoComprarToolStripMenuItem.Click += menuCatalogo_Click;
            // 
            // misReportesToolStripMenuItem
            // 
            misReportesToolStripMenuItem.Name = "misReportesToolStripMenuItem";
            misReportesToolStripMenuItem.Size = new Size(84, 20);
            misReportesToolStripMenuItem.Text = "Mis reportes";
            misReportesToolStripMenuItem.Click += btnReportes_Click;
            // 
            // salirToolStripMenuItem
            // 
            salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            salirToolStripMenuItem.Size = new Size(41, 20);
            salirToolStripMenuItem.Text = "Salir";
            salirToolStripMenuItem.Click += menuSalir_Click;
            // 
            // lblBienvenida
            // 
            lblBienvenida.AutoSize = true;
            lblBienvenida.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBienvenida.Location = new Point(22, 55);
            lblBienvenida.Name = "lblBienvenida";
            lblBienvenida.Size = new Size(278, 21);
            lblBienvenida.TabIndex = 1;
            lblBienvenida.Text = "Bienvenido a la Feria del Agricultor";
            // 
            // FrmMenuPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(326, 112);
            Controls.Add(lblBienvenida);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FrmMenuPrincipal";
            Text = "FrmMenuPrincipal";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem catálogoComprarToolStripMenuItem;
        private ToolStripMenuItem misReportesToolStripMenuItem;
        private ToolStripMenuItem salirToolStripMenuItem;
        private Label lblBienvenida;
    }
}