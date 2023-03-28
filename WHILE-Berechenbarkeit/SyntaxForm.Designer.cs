namespace WHILE_Berechenbarkeit
{
    partial class SyntaxForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyntaxForm));
            SyntaxLbl = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            SyntaxBNFLbl = new Label();
            ErklärungLbl = new Label();
            ErklärungTextLbl = new Label();
            CloseBtn = new Button();
            SyntaktischerZuckerLbl = new Label();
            SyntaktischerZuckerTextLbl = new Label();
            SuspendLayout();
            // 
            // SyntaxLbl
            // 
            SyntaxLbl.AutoSize = true;
            SyntaxLbl.Font = new Font("Comic Sans MS", 18F, FontStyle.Bold, GraphicsUnit.Point);
            SyntaxLbl.Location = new Point(15, 12);
            SyntaxLbl.Margin = new Padding(4, 0, 4, 0);
            SyntaxLbl.Name = "SyntaxLbl";
            SyntaxLbl.Size = new Size(96, 35);
            SyntaxLbl.TabIndex = 0;
            SyntaxLbl.Text = "Syntax";
            // 
            // SyntaxBNFLbl
            // 
            SyntaxBNFLbl.AutoSize = true;
            SyntaxBNFLbl.Location = new Point(15, 58);
            SyntaxBNFLbl.Name = "SyntaxBNFLbl";
            SyntaxBNFLbl.Size = new Size(559, 120);
            SyntaxBNFLbl.TabIndex = 2;
            SyntaxBNFLbl.Text = "WHILE-Programme haben folgende Syntax in modifizierter Backus-Naur-Form:\r\n\r\nP ::=   x := x + c\r\n      |    x := x -  c\r\n      |    P; P\r\n      |    WHILE x != 0 DO P END";
            // 
            // ErklärungLbl
            // 
            ErklärungLbl.AutoSize = true;
            ErklärungLbl.Font = new Font("Comic Sans MS", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            ErklärungLbl.Location = new Point(15, 226);
            ErklärungLbl.Margin = new Padding(4, 0, 4, 0);
            ErklärungLbl.Name = "ErklärungLbl";
            ErklärungLbl.Size = new Size(210, 27);
            ErklärungLbl.TabIndex = 3;
            ErklärungLbl.Text = "Erklärung der Syntax";
            // 
            // ErklärungTextLbl
            // 
            ErklärungTextLbl.AutoSize = true;
            ErklärungTextLbl.Location = new Point(12, 264);
            ErklärungTextLbl.Name = "ErklärungTextLbl";
            ErklärungTextLbl.Size = new Size(996, 240);
            ErklärungTextLbl.TabIndex = 4;
            ErklärungTextLbl.Text = resources.GetString("ErklärungTextLbl.Text");
            // 
            // CloseBtn
            // 
            CloseBtn.Font = new Font("Comic Sans MS", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            CloseBtn.Location = new Point(853, 766);
            CloseBtn.Name = "CloseBtn";
            CloseBtn.Size = new Size(161, 54);
            CloseBtn.TabIndex = 5;
            CloseBtn.Text = "Zurück";
            CloseBtn.UseVisualStyleBackColor = true;
            CloseBtn.Click += CloseBtn_Click;
            // 
            // SyntaktischerZuckerLbl
            // 
            SyntaktischerZuckerLbl.AutoSize = true;
            SyntaktischerZuckerLbl.Font = new Font("Comic Sans MS", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            SyntaktischerZuckerLbl.Location = new Point(12, 533);
            SyntaktischerZuckerLbl.Margin = new Padding(4, 0, 4, 0);
            SyntaktischerZuckerLbl.Name = "SyntaktischerZuckerLbl";
            SyntaktischerZuckerLbl.Size = new Size(211, 27);
            SyntaktischerZuckerLbl.TabIndex = 6;
            SyntaktischerZuckerLbl.Text = "Syntaktischer Zucker";
            // 
            // SyntaktischerZuckerTextLbl
            // 
            SyntaktischerZuckerTextLbl.AutoSize = true;
            SyntaktischerZuckerTextLbl.Location = new Point(9, 571);
            SyntaktischerZuckerTextLbl.Name = "SyntaktischerZuckerTextLbl";
            SyntaktischerZuckerTextLbl.Size = new Size(607, 220);
            SyntaktischerZuckerTextLbl.TabIndex = 7;
            SyntaktischerZuckerTextLbl.Text = resources.GetString("SyntaktischerZuckerTextLbl.Text");
            // 
            // SyntaxForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1026, 832);
            Controls.Add(SyntaktischerZuckerTextLbl);
            Controls.Add(SyntaktischerZuckerLbl);
            Controls.Add(CloseBtn);
            Controls.Add(ErklärungTextLbl);
            Controls.Add(ErklärungLbl);
            Controls.Add(SyntaxBNFLbl);
            Controls.Add(SyntaxLbl);
            Font = new Font("Comic Sans MS", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "SyntaxForm";
            Text = "Syntax";
            Load += SyntaxForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label SyntaxLbl;
        private PictureBox WhilePB;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label SyntaxBNFLbl;
        private Label ErklärungLbl;
        private Label ErklärungTextLbl;
        private Button CloseBtn;
        private Label SyntaktischerZuckerLbl;
        private Label SyntaktischerZuckerTextLbl;
    }
}