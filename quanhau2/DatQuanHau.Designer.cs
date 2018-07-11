namespace quanhau2
{
    partial class DatQuanHau
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbnTuanTu = new System.Windows.Forms.RadioButton();
            this.rbnDatHien = new System.Windows.Forms.RadioButton();
            this.pnlBan_Co = new System.Windows.Forms.Panel();
            this.lblThongBao = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox1.Controls.Add(this.rbnTuanTu);
            this.groupBox1.Controls.Add(this.rbnDatHien);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(5, 121);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 93);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chế độ";
            // 
            // rbnTuanTu
            // 
            this.rbnTuanTu.AutoSize = true;
            this.rbnTuanTu.Location = new System.Drawing.Point(6, 61);
            this.rbnTuanTu.Name = "rbnTuanTu";
            this.rbnTuanTu.Size = new System.Drawing.Size(108, 22);
            this.rbnTuanTu.TabIndex = 0;
            this.rbnTuanTu.TabStop = true;
            this.rbnTuanTu.Text = "Đặt tuần tự";
            this.rbnTuanTu.UseVisualStyleBackColor = true;
            // 
            // rbnDatHien
            // 
            this.rbnDatHien.AutoSize = true;
            this.rbnDatHien.Location = new System.Drawing.Point(6, 27);
            this.rbnDatHien.Name = "rbnDatHien";
            this.rbnDatHien.Size = new System.Drawing.Size(166, 22);
            this.rbnDatHien.TabIndex = 0;
            this.rbnDatHien.TabStop = true;
            this.rbnDatHien.Text = "Đặt một ô hiện 7 ô";
            this.rbnDatHien.UseVisualStyleBackColor = true;
            // 
            // pnlBan_Co
            // 
            this.pnlBan_Co.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlBan_Co.Location = new System.Drawing.Point(211, 1);
            this.pnlBan_Co.Name = "pnlBan_Co";
            this.pnlBan_Co.Size = new System.Drawing.Size(640, 640);
            this.pnlBan_Co.TabIndex = 3;
            // 
            // lblThongBao
            // 
            this.lblThongBao.AutoSize = true;
            this.lblThongBao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblThongBao.ForeColor = System.Drawing.Color.Red;
            this.lblThongBao.Location = new System.Drawing.Point(3, 13);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Size = new System.Drawing.Size(14, 20);
            this.lblThongBao.TabIndex = 5;
            this.lblThongBao.Text = ".";
            // 
            // comboBox2
            // 
            this.comboBox2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.comboBox2.ForeColor = System.Drawing.Color.Red;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Đặt 8 quân hậu",
            "Game 2 quân hậu"});
            this.comboBox2.Location = new System.Drawing.Point(27, 12);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 24);
            this.comboBox2.TabIndex = 20;
            this.comboBox2.Text = "Đặt 8 quân hậu";
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Controls.Add(this.lblThongBao);
            this.panel2.Location = new System.Drawing.Point(857, 123);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(202, 47);
            this.panel2.TabIndex = 21;
            // 
            // DatQuanHau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkViolet;
            this.ClientSize = new System.Drawing.Size(1068, 641);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlBan_Co);
            this.Name = "DatQuanHau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DatQuanHau";
            this.Load += new System.EventHandler(this.DatQuanHau_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbnTuanTu;
        private System.Windows.Forms.RadioButton rbnDatHien;
        private System.Windows.Forms.Panel pnlBan_Co;
        private System.Windows.Forms.Label lblThongBao;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Panel panel2;
    }
}