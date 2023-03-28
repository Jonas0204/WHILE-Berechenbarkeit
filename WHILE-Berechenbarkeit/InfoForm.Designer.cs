namespace WHILE_Berechenbarkeit
{
    partial class InfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoForm));
            InfoTb = new RichTextBox();
            InfoPb = new PictureBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)InfoPb).BeginInit();
            SuspendLayout();
            // 
            // InfoTb
            // 
            InfoTb.Location = new Point(107, 27);
            InfoTb.Name = "InfoTb";
            InfoTb.ReadOnly = true;
            InfoTb.Size = new Size(468, 169);
            InfoTb.TabIndex = 1;
            InfoTb.Text = "";
            // 
            // InfoPb
            // 
            InfoPb.Image = Properties.Resources.infoICO;
            InfoPb.Location = new Point(12, 27);
            InfoPb.Name = "InfoPb";
            InfoPb.Size = new Size(72, 75);
            InfoPb.SizeMode = PictureBoxSizeMode.StretchImage;
            InfoPb.TabIndex = 2;
            InfoPb.TabStop = false;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(465, 202);
            button1.Name = "button1";
            button1.Size = new Size(110, 38);
            button1.TabIndex = 3;
            button1.Text = "Zurück";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // InfoForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(603, 254);
            Controls.Add(button1);
            Controls.Add(InfoPb);
            Controls.Add(InfoTb);
            Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "InfoForm";
            Text = "InfoForm";
            Load += InfoForm_Load;
            ((System.ComponentModel.ISupportInitialize)InfoPb).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private RichTextBox InfoTb;
        private PictureBox InfoPb;
        private Button button1;
    }
}