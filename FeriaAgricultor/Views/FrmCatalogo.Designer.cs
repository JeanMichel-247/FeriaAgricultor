namespace FeriaAgricultor.Views
{
    partial class FrmCatalogo
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
            lblCatalogo = new Label();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            dgvProductos = new DataGridView();
            numCantidad = new NumericUpDown();
            lblCantidad = new Label();
            btnAgregar = new Button();
            dgvCarrito = new DataGridView();
            lblCarrito = new Label();
            lblTotal = new Label();
            lblDireccion = new Label();
            txtDireccion = new TextBox();
            btnPagar = new Button();
            lblProductor = new Label();
            cmbProductores = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCantidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvCarrito).BeginInit();
            SuspendLayout();
            // 
            // lblCatalogo
            // 
            lblCatalogo.AutoSize = true;
            lblCatalogo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCatalogo.Location = new Point(142, 21);
            lblCatalogo.Name = "lblCatalogo";
            lblCatalogo.Size = new Size(79, 21);
            lblCatalogo.TabIndex = 0;
            lblCatalogo.Text = "Catálogo";
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(36, 64);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(97, 15);
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "Buscar producto:";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(147, 61);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(165, 23);
            txtBuscar.TabIndex = 2;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // dgvProductos
            // 
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Location = new Point(36, 140);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.ReadOnly = true;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Size = new Size(276, 171);
            dgvProductos.TabIndex = 3;
            // 
            // numCantidad
            // 
            numCantidad.Location = new Point(203, 317);
            numCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCantidad.Name = "numCantidad";
            numCantidad.Size = new Size(68, 23);
            numCantidad.TabIndex = 4;
            numCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(142, 319);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(55, 15);
            lblCantidad.TabIndex = 5;
            lblCantidad.Text = "Cantidad";
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(118, 362);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(121, 23);
            btnAgregar.TabIndex = 6;
            btnAgregar.Text = "Agregar al carrito";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // dgvCarrito
            // 
            dgvCarrito.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCarrito.Location = new Point(461, 140);
            dgvCarrito.Name = "dgvCarrito";
            dgvCarrito.Size = new Size(276, 171);
            dgvCarrito.TabIndex = 7;
            // 
            // lblCarrito
            // 
            lblCarrito.AutoSize = true;
            lblCarrito.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCarrito.Location = new Point(576, 21);
            lblCarrito.Name = "lblCarrito";
            lblCarrito.Size = new Size(62, 21);
            lblCarrito.TabIndex = 8;
            lblCarrito.Text = "Carrito";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(461, 319);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(51, 15);
            lblTotal.TabIndex = 9;
            lblTotal.Text = "Total: ₡0";
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.Location = new Point(461, 43);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(116, 15);
            lblDireccion.TabIndex = 10;
            lblDireccion.Text = "Dirección de Entrega";
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(461, 61);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(276, 23);
            txtDireccion.TabIndex = 11;
            // 
            // btnPagar
            // 
            btnPagar.Location = new Point(563, 362);
            btnPagar.Name = "btnPagar";
            btnPagar.Size = new Size(75, 23);
            btnPagar.TabIndex = 12;
            btnPagar.Text = "Pagar";
            btnPagar.UseVisualStyleBackColor = true;
            btnPagar.Click += btnPagar_Click;
            // 
            // lblProductor
            // 
            lblProductor.AutoSize = true;
            lblProductor.Location = new Point(36, 101);
            lblProductor.Name = "lblProductor";
            lblProductor.Size = new Size(120, 15);
            lblProductor.TabIndex = 13;
            lblProductor.Text = "Filtrar por productor: ";
            // 
            // cmbProductores
            // 
            cmbProductores.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProductores.FormattingEnabled = true;
            cmbProductores.Location = new Point(162, 98);
            cmbProductores.Name = "cmbProductores";
            cmbProductores.Size = new Size(150, 23);
            cmbProductores.TabIndex = 14;
            cmbProductores.SelectedIndexChanged += cmbProductores_SelectedIndexChanged;
            // 
            // FrmCatalogo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(cmbProductores);
            Controls.Add(lblProductor);
            Controls.Add(btnPagar);
            Controls.Add(txtDireccion);
            Controls.Add(lblDireccion);
            Controls.Add(lblTotal);
            Controls.Add(lblCarrito);
            Controls.Add(dgvCarrito);
            Controls.Add(btnAgregar);
            Controls.Add(lblCantidad);
            Controls.Add(numCantidad);
            Controls.Add(dgvProductos);
            Controls.Add(txtBuscar);
            Controls.Add(lblBuscar);
            Controls.Add(lblCatalogo);
            Name = "FrmCatalogo";
            Text = "FrmCatalogo";
            Load += FrmCatalogo_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCantidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvCarrito).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCatalogo;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private DataGridView dgvProductos;
        private NumericUpDown numCantidad;
        private Label lblCantidad;
        private Button btnAgregar;
        private DataGridView dgvCarrito;
        private Label lblCarrito;
        private Label lblTotal;
        private Label lblDireccion;
        private TextBox txtDireccion;
        private Button btnPagar;
        private Label lblProductor;
        private ComboBox cmbProductores;
    }
}