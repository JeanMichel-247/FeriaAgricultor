namespace FeriaAgricultor.Views
{
    partial class FrmLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtCorreo = new TextBox();
            lblCorreo = new Label();
            label1 = new Label();
            label2 = new Label();
            txtClave = new TextBox();
            btnIngresar = new Button();
            lblMensaje = new Label();
            SuspendLayout();
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(192, 83);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(177, 23);
            txtCorreo.TabIndex = 0;
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Location = new Point(81, 86);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(46, 15);
            lblCorreo.TabIndex = 1;
            lblCorreo.Text = "Correo:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(153, 33);
            label1.Name = "label1";
            label1.Size = new Size(128, 21);
            label1.TabIndex = 2;
            label1.Text = "Inicio de sesión";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(81, 138);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 3;
            label2.Text = "Contraseña";
            // 
            // txtClave
            // 
            txtClave.Location = new Point(192, 138);
            txtClave.Name = "txtClave";
            txtClave.Size = new Size(177, 23);
            txtClave.TabIndex = 4;
            txtClave.UseSystemPasswordChar = true;
            // 
            // btnIngresar
            // 
            btnIngresar.Location = new Point(177, 196);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(75, 23);
            btnIngresar.TabIndex = 5;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = true;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.AutoSize = true;
            lblMensaje.Location = new Point(192, 166);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(0, 15);
            lblMensaje.TabIndex = 7;
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(445, 231);
            Controls.Add(lblMensaje);
            Controls.Add(btnIngresar);
            Controls.Add(txtClave);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblCorreo);
            Controls.Add(txtCorreo);
            Name = "FrmLogin";
            Text = "Feria del Agricultor";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCorreo;
        private Label lblCorreo;
        private Label label1;
        private Label label2;
        private TextBox txtClave;
        private Button btnIngresar;
        private Label lblMensaje;
    }
}
