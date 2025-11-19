namespace FeriaAgricultor.Views
{
    partial class FrmFactura
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
            label1 = new Label();
            lblNumeroOrden = new Label();
            lblCliente = new Label();
            lblFecha = new Label();
            lblDetalle = new Label();
            dgvDetalleFactura = new DataGridView();
            lblTotalFactura = new Label();
            btnCerrar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDetalleFactura).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(136, 9);
            label1.Name = "label1";
            label1.Size = new Size(107, 30);
            label1.TabIndex = 0;
            label1.Text = "FACTURA";
            // 
            // lblNumeroOrden
            // 
            lblNumeroOrden.AutoSize = true;
            lblNumeroOrden.Location = new Point(105, 57);
            lblNumeroOrden.Name = "lblNumeroOrden";
            lblNumeroOrden.Size = new Size(0, 15);
            lblNumeroOrden.TabIndex = 2;
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(105, 101);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(0, 15);
            lblCliente.TabIndex = 4;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(105, 140);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(0, 15);
            lblFecha.TabIndex = 6;
            // 
            // lblDetalle
            // 
            lblDetalle.AutoSize = true;
            lblDetalle.Location = new Point(32, 208);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(43, 15);
            lblDetalle.TabIndex = 7;
            lblDetalle.Text = "Detalle";
            // 
            // dgvDetalleFactura
            // 
            dgvDetalleFactura.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalleFactura.Location = new Point(32, 226);
            dgvDetalleFactura.Name = "dgvDetalleFactura";
            dgvDetalleFactura.Size = new Size(336, 335);
            dgvDetalleFactura.TabIndex = 8;
            // 
            // lblTotalFactura
            // 
            lblTotalFactura.AutoSize = true;
            lblTotalFactura.Location = new Point(37, 574);
            lblTotalFactura.Name = "lblTotalFactura";
            lblTotalFactura.Size = new Size(10, 15);
            lblTotalFactura.TabIndex = 9;
            lblTotalFactura.Text = " ";
            // 
            // btnCerrar
            // 
            btnCerrar.Location = new Point(159, 608);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(75, 23);
            btnCerrar.TabIndex = 10;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // FrmFactura
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(398, 643);
            Controls.Add(btnCerrar);
            Controls.Add(lblTotalFactura);
            Controls.Add(dgvDetalleFactura);
            Controls.Add(lblDetalle);
            Controls.Add(lblFecha);
            Controls.Add(lblCliente);
            Controls.Add(lblNumeroOrden);
            Controls.Add(label1);
            Name = "FrmFactura";
            Text = "FrmFactura";
            Load += FrmFactura_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDetalleFactura).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lblNumeroOrden;
        private Label lblCliente;
        private Label lblFecha;
        private Label lblDetalle;
        private DataGridView dgvDetalleFactura;
        private Label lblTotalFactura;
        private Button btnCerrar;
    }
}