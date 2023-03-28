namespace WHILE_Berechenbarkeit
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Pruefen_btn = new Button();
            EingabeLbl = new Label();
            TerminalLbl = new Label();
            EingabeTb = new RichTextBox();
            TerminalTB = new RichTextBox();
            CancelBtn = new Button();
            menuStrip1 = new MenuStrip();
            MenuTS = new ToolStripMenuItem();
            OpenFileTS = new ToolStripMenuItem();
            FileSaveTS = new ToolStripMenuItem();
            beendenToolStripMenuItem = new ToolStripMenuItem();
            ExeTS = new ToolStripMenuItem();
            normalToolStripMenuItem = new ToolStripMenuItem();
            StepByStepTS = new ToolStripMenuItem();
            BspTS = new ToolStripMenuItem();
            BspAdditionTS = new ToolStripMenuItem();
            BspMultipTS = new ToolStripMenuItem();
            DivTS = new ToolStripMenuItem();
            FakultätTS = new ToolStripMenuItem();
            ExponentTS = new ToolStripMenuItem();
            hilfeToolStripMenuItem = new ToolStripMenuItem();
            SyntaxTS = new ToolStripMenuItem();
            InfoTS = new ToolStripMenuItem();
            EinstellungenTS = new ToolStripMenuItem();
            ErweiterteAusgabeTS = new ToolStripMenuItem();
            MultipErlaubenTS = new ToolStripMenuItem();
            DirektAssignmentTS = new ToolStripMenuItem();
            DoubleAssignmentTS = new ToolStripMenuItem();
            StepByStepBtn = new Button();
            inMsLbl = new Label();
            ToolTipMain = new ToolTip(components);
            TextTS = new ToolStrip();
            ZoomInTS = new ToolStripButton();
            ZoomOutTS = new ToolStripButton();
            ClearTS = new ToolStripButton();
            TextTS2 = new ToolStrip();
            ZoomInTS2 = new ToolStripButton();
            ZoomOutTS2 = new ToolStripButton();
            ClearTS2 = new ToolStripButton();
            MsPerExeCb = new ComboBox();
            menuStrip1.SuspendLayout();
            TextTS.SuspendLayout();
            TextTS2.SuspendLayout();
            SuspendLayout();
            // 
            // Pruefen_btn
            // 
            Pruefen_btn.Font = new Font("Consolas", 13F, FontStyle.Regular, GraphicsUnit.Point);
            Pruefen_btn.Location = new Point(498, 55);
            Pruefen_btn.Name = "Pruefen_btn";
            Pruefen_btn.Size = new Size(151, 51);
            Pruefen_btn.TabIndex = 1;
            Pruefen_btn.Text = "Ausführen";
            Pruefen_btn.UseVisualStyleBackColor = true;
            Pruefen_btn.Click += Pruefen_btn_Click;
            // 
            // EingabeLbl
            // 
            EingabeLbl.AutoSize = true;
            EingabeLbl.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            EingabeLbl.Location = new Point(14, 36);
            EingabeLbl.Name = "EingabeLbl";
            EingabeLbl.Size = new Size(70, 15);
            EingabeLbl.TabIndex = 4;
            EingabeLbl.Text = "Programm:";
            // 
            // TerminalLbl
            // 
            TerminalLbl.AutoSize = true;
            TerminalLbl.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            TerminalLbl.Location = new Point(14, 362);
            TerminalLbl.Name = "TerminalLbl";
            TerminalLbl.Size = new Size(63, 15);
            TerminalLbl.TabIndex = 5;
            TerminalLbl.Text = "Ausgabe:";
            TerminalLbl.MouseUp += TerminalLbl_MouseUp;
            // 
            // EingabeTb
            // 
            EingabeTb.AcceptsTab = true;
            EingabeTb.Font = new Font("Consolas", 14F, FontStyle.Regular, GraphicsUnit.Point);
            EingabeTb.Location = new Point(11, 55);
            EingabeTb.Name = "EingabeTb";
            EingabeTb.Size = new Size(482, 260);
            EingabeTb.TabIndex = 6;
            EingabeTb.Text = "";
            // 
            // TerminalTB
            // 
            TerminalTB.Font = new Font("Consolas", 11F, FontStyle.Regular, GraphicsUnit.Point);
            TerminalTB.Location = new Point(11, 381);
            TerminalTB.Name = "TerminalTB";
            TerminalTB.Size = new Size(482, 170);
            TerminalTB.TabIndex = 7;
            TerminalTB.Text = "";
            TerminalTB.KeyUp += TerminalTB_KeyUp_1;
            // 
            // CancelBtn
            // 
            CancelBtn.Font = new Font("Consolas", 13F, FontStyle.Regular, GraphicsUnit.Point);
            CancelBtn.Location = new Point(498, 263);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(151, 51);
            CancelBtn.TabIndex = 10;
            CancelBtn.Text = "Beenden";
            CancelBtn.UseVisualStyleBackColor = true;
            CancelBtn.Click += CancelBtn_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ControlLight;
            menuStrip1.Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            menuStrip1.Items.AddRange(new ToolStripItem[] { MenuTS, ExeTS, BspTS, EinstellungenTS, hilfeToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            menuStrip1.Size = new Size(656, 26);
            menuStrip1.TabIndex = 11;
            menuStrip1.Text = "MainMS";
            // 
            // MenuTS
            // 
            MenuTS.BackColor = SystemColors.ControlLight;
            MenuTS.DropDownItems.AddRange(new ToolStripItem[] { OpenFileTS, FileSaveTS, beendenToolStripMenuItem });
            MenuTS.Name = "MenuTS";
            MenuTS.Size = new Size(60, 22);
            MenuTS.Text = "Datei";
            // 
            // OpenFileTS
            // 
            OpenFileTS.BackColor = SystemColors.Menu;
            OpenFileTS.Name = "OpenFileTS";
            OpenFileTS.ShortcutKeys = Keys.Control | Keys.O;
            OpenFileTS.Size = new Size(252, 22);
            OpenFileTS.Text = "Datei öffnen";
            OpenFileTS.Click += OpenFileTS_Click;
            // 
            // FileSaveTS
            // 
            FileSaveTS.BackColor = SystemColors.Menu;
            FileSaveTS.Name = "FileSaveTS";
            FileSaveTS.ShortcutKeys = Keys.Control | Keys.S;
            FileSaveTS.Size = new Size(252, 22);
            FileSaveTS.Text = "Datei speichern";
            FileSaveTS.Click += FileSaveTS_Click;
            // 
            // beendenToolStripMenuItem
            // 
            beendenToolStripMenuItem.BackColor = SystemColors.Menu;
            beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            beendenToolStripMenuItem.Size = new Size(252, 22);
            beendenToolStripMenuItem.Text = "Beenden";
            beendenToolStripMenuItem.Click += BeendenToolStripMenuItem_Click;
            // 
            // ExeTS
            // 
            ExeTS.BackColor = SystemColors.ControlLight;
            ExeTS.DropDownItems.AddRange(new ToolStripItem[] { normalToolStripMenuItem, StepByStepTS });
            ExeTS.Name = "ExeTS";
            ExeTS.Size = new Size(92, 22);
            ExeTS.Text = "Ausführen";
            // 
            // normalToolStripMenuItem
            // 
            normalToolStripMenuItem.BackColor = SystemColors.Menu;
            normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            normalToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.R;
            normalToolStripMenuItem.Size = new Size(228, 22);
            normalToolStripMenuItem.Text = "Normal";
            normalToolStripMenuItem.Click += Pruefen_btn_Click;
            // 
            // StepByStepTS
            // 
            StepByStepTS.BackColor = SystemColors.Menu;
            StepByStepTS.Name = "StepByStepTS";
            StepByStepTS.ShortcutKeys = Keys.Control | Keys.D;
            StepByStepTS.Size = new Size(228, 22);
            StepByStepTS.Text = "Schrittweise";
            StepByStepTS.Click += StepByStep_Click;
            // 
            // BspTS
            // 
            BspTS.BackColor = SystemColors.ControlLight;
            BspTS.DropDownItems.AddRange(new ToolStripItem[] { BspAdditionTS, BspMultipTS, DivTS, FakultätTS, ExponentTS });
            BspTS.Name = "BspTS";
            BspTS.Size = new Size(92, 22);
            BspTS.Text = "Beispiele";
            // 
            // BspAdditionTS
            // 
            BspAdditionTS.BackColor = SystemColors.Menu;
            BspAdditionTS.Name = "BspAdditionTS";
            BspAdditionTS.ShortcutKeys = Keys.Control | Keys.D1;
            BspAdditionTS.Size = new Size(244, 22);
            BspAdditionTS.Text = "Addition";
            BspAdditionTS.Click += BspAdditionTS_Click;
            // 
            // BspMultipTS
            // 
            BspMultipTS.BackColor = SystemColors.Menu;
            BspMultipTS.Name = "BspMultipTS";
            BspMultipTS.ShortcutKeys = Keys.Control | Keys.D2;
            BspMultipTS.Size = new Size(244, 22);
            BspMultipTS.Text = "Multiplikation";
            BspMultipTS.Click += BspMultipTS_Click;
            // 
            // DivTS
            // 
            DivTS.BackColor = SystemColors.Menu;
            DivTS.Name = "DivTS";
            DivTS.ShortcutKeys = Keys.Control | Keys.D3;
            DivTS.Size = new Size(244, 22);
            DivTS.Text = "Dividieren";
            DivTS.Click += DivTS_Click;
            // 
            // FakultätTS
            // 
            FakultätTS.BackColor = SystemColors.Menu;
            FakultätTS.Name = "FakultätTS";
            FakultätTS.ShortcutKeys = Keys.Control | Keys.D4;
            FakultätTS.Size = new Size(244, 22);
            FakultätTS.Text = "Fakultät";
            FakultätTS.Click += FakultätTS_Click;
            // 
            // ExponentTS
            // 
            ExponentTS.BackColor = SystemColors.Menu;
            ExponentTS.Name = "ExponentTS";
            ExponentTS.ShortcutKeys = Keys.Control | Keys.D5;
            ExponentTS.Size = new Size(244, 22);
            ExponentTS.Text = "Exponent";
            ExponentTS.Click += ExpoTS_Click;
            // 
            // hilfeToolStripMenuItem
            // 
            hilfeToolStripMenuItem.BackColor = SystemColors.ControlLight;
            hilfeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { SyntaxTS, InfoTS });
            hilfeToolStripMenuItem.Name = "hilfeToolStripMenuItem";
            hilfeToolStripMenuItem.Size = new Size(60, 22);
            hilfeToolStripMenuItem.Text = "Hilfe";
            // 
            // SyntaxTS
            // 
            SyntaxTS.BackColor = SystemColors.Menu;
            SyntaxTS.Name = "SyntaxTS";
            SyntaxTS.Size = new Size(180, 22);
            SyntaxTS.Text = "Syntax";
            SyntaxTS.Click += SyntaxTS_Click;
            // 
            // InfoTS
            // 
            InfoTS.BackColor = SystemColors.Menu;
            InfoTS.Name = "InfoTS";
            InfoTS.Size = new Size(180, 22);
            InfoTS.Text = "Info";
            InfoTS.Click += InfoTS_Click;
            // 
            // EinstellungenTS
            // 
            EinstellungenTS.DropDownItems.AddRange(new ToolStripItem[] { ErweiterteAusgabeTS, MultipErlaubenTS, DirektAssignmentTS, DoubleAssignmentTS });
            EinstellungenTS.Name = "EinstellungenTS";
            EinstellungenTS.Size = new Size(124, 22);
            EinstellungenTS.Text = "Einstellungen";
            // 
            // ErweiterteAusgabeTS
            // 
            ErweiterteAusgabeTS.BackColor = SystemColors.Menu;
            ErweiterteAusgabeTS.Name = "ErweiterteAusgabeTS";
            ErweiterteAusgabeTS.Size = new Size(268, 22);
            ErweiterteAusgabeTS.Text = "Erweiterte Ausgabe";
            ErweiterteAusgabeTS.Click += ErweiterteAusgabeTS_Click;
            ErweiterteAusgabeTS.MouseEnter += ErweiterteAusgabeTS_MouseEnter;
            ErweiterteAusgabeTS.MouseLeave += ErweiterteAusgabeTS_MouseLeave;
            // 
            // MultipErlaubenTS
            // 
            MultipErlaubenTS.BackColor = SystemColors.Menu;
            MultipErlaubenTS.CheckOnClick = true;
            MultipErlaubenTS.Name = "MultipErlaubenTS";
            MultipErlaubenTS.Size = new Size(268, 22);
            MultipErlaubenTS.Text = "Multiplikation erlauben";
            MultipErlaubenTS.Click += MultipErlaubenTS_Click;
            MultipErlaubenTS.MouseEnter += MultipErlaubenTS_MouseEnter;
            MultipErlaubenTS.MouseLeave += MultipErlaubenTS_MouseLeave;
            // 
            // DirektAssignmentTS
            // 
            DirektAssignmentTS.BackColor = SystemColors.Menu;
            DirektAssignmentTS.CheckOnClick = true;
            DirektAssignmentTS.Name = "DirektAssignmentTS";
            DirektAssignmentTS.Size = new Size(268, 22);
            DirektAssignmentTS.Text = "Direktzuweisung erlauben";
            DirektAssignmentTS.Click += DirektAssignmentTS_Click;
            DirektAssignmentTS.MouseEnter += DirektAssignmentTS_MouseEnter;
            DirektAssignmentTS.MouseLeave += DirektAssignmentTS_MouseLeave;
            // 
            // DoubleAssignmentTS
            // 
            DoubleAssignmentTS.BackColor = SystemColors.Menu;
            DoubleAssignmentTS.CheckOnClick = true;
            DoubleAssignmentTS.Name = "DoubleAssignmentTS";
            DoubleAssignmentTS.Size = new Size(268, 22);
            DoubleAssignmentTS.Text = "Doppelzuweisung erlauben";
            DoubleAssignmentTS.Click += DoubleAssignmentTS_Click;
            DoubleAssignmentTS.MouseEnter += DoubleAssignmentTS_MouseEnter;
            DoubleAssignmentTS.MouseLeave += DoubleAssignmentTS_MouseLeave;
            // 
            // StepByStepBtn
            // 
            StepByStepBtn.Font = new Font("Consolas", 13F, FontStyle.Regular, GraphicsUnit.Point);
            StepByStepBtn.Location = new Point(498, 201);
            StepByStepBtn.Name = "StepByStepBtn";
            StepByStepBtn.Size = new Size(151, 56);
            StepByStepBtn.TabIndex = 12;
            StepByStepBtn.Text = "Schrittweise Ausführen";
            StepByStepBtn.UseVisualStyleBackColor = true;
            StepByStepBtn.Click += StepByStep_Click;
            // 
            // inMsLbl
            // 
            inMsLbl.AutoSize = true;
            inMsLbl.Font = new Font("Consolas", 12.75F, FontStyle.Regular, GraphicsUnit.Point);
            inMsLbl.Location = new Point(577, 128);
            inMsLbl.Name = "inMsLbl";
            inMsLbl.Size = new Size(72, 20);
            inMsLbl.TabIndex = 13;
            inMsLbl.Text = "in (ms)";
            // 
            // TextTS
            // 
            TextTS.BackColor = SystemColors.ControlLight;
            TextTS.Dock = DockStyle.None;
            TextTS.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TextTS.Items.AddRange(new ToolStripItem[] { ZoomInTS, ZoomOutTS, ClearTS });
            TextTS.Location = new Point(11, 318);
            TextTS.Name = "TextTS";
            TextTS.Size = new Size(81, 25);
            TextTS.Stretch = true;
            TextTS.TabIndex = 14;
            // 
            // ZoomInTS
            // 
            ZoomInTS.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ZoomInTS.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            ZoomInTS.Image = Properties.Resources.zoom_in_black;
            ZoomInTS.Name = "ZoomInTS";
            ZoomInTS.Size = new Size(23, 22);
            ZoomInTS.Click += ZoomInTS_Click;
            // 
            // ZoomOutTS
            // 
            ZoomOutTS.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ZoomOutTS.Image = Properties.Resources.zoom_out_black;
            ZoomOutTS.ImageTransparentColor = Color.Magenta;
            ZoomOutTS.Name = "ZoomOutTS";
            ZoomOutTS.Size = new Size(23, 22);
            ZoomOutTS.Click += ZoomOutTS_Click;
            // 
            // ClearTS
            // 
            ClearTS.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ClearTS.Image = Properties.Resources.bin_black;
            ClearTS.ImageTransparentColor = Color.Magenta;
            ClearTS.Name = "ClearTS";
            ClearTS.Size = new Size(23, 22);
            ClearTS.Click += ClearTS_Click;
            // 
            // TextTS2
            // 
            TextTS2.BackColor = SystemColors.ControlLight;
            TextTS2.Dock = DockStyle.None;
            TextTS2.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TextTS2.Items.AddRange(new ToolStripItem[] { ZoomInTS2, ZoomOutTS2, ClearTS2 });
            TextTS2.Location = new Point(11, 554);
            TextTS2.Name = "TextTS2";
            TextTS2.Size = new Size(81, 25);
            TextTS2.Stretch = true;
            TextTS2.TabIndex = 15;
            // 
            // ZoomInTS2
            // 
            ZoomInTS2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ZoomInTS2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            ZoomInTS2.Image = Properties.Resources.zoom_in_black;
            ZoomInTS2.Name = "ZoomInTS2";
            ZoomInTS2.Size = new Size(23, 22);
            ZoomInTS2.Click += ZoomInTS2_Click;
            // 
            // ZoomOutTS2
            // 
            ZoomOutTS2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ZoomOutTS2.Image = Properties.Resources.zoom_out_black;
            ZoomOutTS2.ImageTransparentColor = Color.Magenta;
            ZoomOutTS2.Name = "ZoomOutTS2";
            ZoomOutTS2.Size = new Size(23, 22);
            ZoomOutTS2.Click += ZoomOutTS2_Click;
            // 
            // ClearTS2
            // 
            ClearTS2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ClearTS2.Image = Properties.Resources.bin_black;
            ClearTS2.ImageTransparentColor = Color.Magenta;
            ClearTS2.Name = "ClearTS2";
            ClearTS2.Size = new Size(23, 22);
            ClearTS2.Click += ClearTS2_Click;
            // 
            // MsPerExeCb
            // 
            MsPerExeCb.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            MsPerExeCb.FormattingEnabled = true;
            MsPerExeCb.Items.AddRange(new object[] { "0", "300", "500", "800", "1000", "1500", "3000" });
            MsPerExeCb.Location = new Point(499, 122);
            MsPerExeCb.Name = "MsPerExeCb";
            MsPerExeCb.Size = new Size(74, 27);
            MsPerExeCb.TabIndex = 16;
            MsPerExeCb.Text = "0";
            MsPerExeCb.TextChanged += MsPerExeCb_TextChanged;
            MsPerExeCb.MouseHover += MsPerExeCb_MouseHover;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(656, 589);
            Controls.Add(MsPerExeCb);
            Controls.Add(TextTS2);
            Controls.Add(TextTS);
            Controls.Add(inMsLbl);
            Controls.Add(StepByStepBtn);
            Controls.Add(CancelBtn);
            Controls.Add(TerminalTB);
            Controls.Add(EingabeTb);
            Controls.Add(TerminalLbl);
            Controls.Add(EingabeLbl);
            Controls.Add(Pruefen_btn);
            Controls.Add(menuStrip1);
            Font = new Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "WHILE-Berechenbarkeit";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            TextTS.ResumeLayout(false);
            TextTS.PerformLayout();
            TextTS2.ResumeLayout(false);
            TextTS2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button Pruefen_btn;
        private Label EingabeLbl;
        private Label TerminalLbl;
        private RichTextBox EingabeTb;
        private RichTextBox TerminalTB;
        private Button CancelBtn;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem MenuTS;
        private Button StepByStepBtn;
        private Label inMsLbl;
        private ToolStripMenuItem beendenToolStripMenuItem;
        private ToolStripMenuItem BspTS;
        private ToolStripMenuItem BspAdditionTS;
        private ToolStripMenuItem BspMultipTS;
        private ToolStripMenuItem OpenFileTS;
        private ToolStripMenuItem FileSaveTS;
        private ToolStripMenuItem ExeTS;
        private ToolStripMenuItem normalToolStripMenuItem;
        private ToolStripMenuItem StepByStepTS;
        private ToolStripMenuItem FakultätTS;
        private ToolStripMenuItem ExponentTS;
        private ToolStripMenuItem DivTS;
        private ToolStripMenuItem hilfeToolStripMenuItem;
        private ToolStripMenuItem SyntaxTS;
        private ToolStripMenuItem InfoTS;
        private ToolTip ToolTipMain;
        private ToolStripMenuItem EinstellungenTS;
        private ToolStripMenuItem MultipErlaubenTS;
        private ToolStripMenuItem ErweiterteAusgabeTS;
        private ToolStrip TextTS;
        private ToolStripButton ZoomInTS;
        private ToolStripButton ZoomOutTS;
        private ToolStripButton ClearTS;
        private ToolStrip TextTS2;
        private ToolStripButton ZoomInTS2;
        private ToolStripButton ZoomOutTS2;
        private ToolStripButton ClearTS2;
        private ComboBox MsPerExeCb;
        private ToolStripMenuItem DirektAssignmentTS;
        private ToolStripMenuItem DoubleAssignmentTS;
    }
}