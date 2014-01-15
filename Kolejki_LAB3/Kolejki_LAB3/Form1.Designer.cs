namespace Kolejki_LAB3
{
    partial class FormQueueSystems
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
            this.groupBoxBasicData = new System.Windows.Forms.GroupBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.labelStreamApplicationIntensity = new System.Windows.Forms.Label();
            this.labelServiceIntensity = new System.Windows.Forms.Label();
            this.numericUpDownStreamApplicationIntensity = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownServiceIntensity = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.plikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zapiszModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wczytajModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zamknijToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dodajToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statystykiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statystykiToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.oProgramieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oProgramieToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProviderServiceTimeIntensity = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderStreamApplicationIntensity = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBoxComunicates = new System.Windows.Forms.GroupBox();
            this.labelComunicateCurrent = new System.Windows.Forms.Label();
            this.labeltimeCurrent = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.listBoxComunicates = new System.Windows.Forms.ListBox();
            this.backgroundWorkerUpdateInterface = new System.ComponentModel.BackgroundWorker();
            this.labelInputStream = new System.Windows.Forms.Label();
            this.labelOutputStream = new System.Windows.Forms.Label();
            this.groupBoxArchiveComunicates = new System.Windows.Forms.GroupBox();
            this.labelComunicateArchive = new System.Windows.Forms.Label();
            this.labelTimeArchive = new System.Windows.Forms.Label();
            this.buttonClearArchiveComunicates = new System.Windows.Forms.Button();
            this.listBoxArchiveComunicates = new System.Windows.Forms.ListBox();
            this.groupBoxBasicData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStreamApplicationIntensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownServiceIntensity)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderServiceTimeIntensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderStreamApplicationIntensity)).BeginInit();
            this.groupBoxComunicates.SuspendLayout();
            this.groupBoxArchiveComunicates.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxBasicData
            // 
            this.groupBoxBasicData.Controls.Add(this.buttonStart);
            this.groupBoxBasicData.Controls.Add(this.labelStreamApplicationIntensity);
            this.groupBoxBasicData.Controls.Add(this.labelServiceIntensity);
            this.groupBoxBasicData.Controls.Add(this.numericUpDownStreamApplicationIntensity);
            this.groupBoxBasicData.Controls.Add(this.numericUpDownServiceIntensity);
            this.groupBoxBasicData.Location = new System.Drawing.Point(1038, 32);
            this.groupBoxBasicData.Name = "groupBoxBasicData";
            this.groupBoxBasicData.Size = new System.Drawing.Size(337, 174);
            this.groupBoxBasicData.TabIndex = 1;
            this.groupBoxBasicData.TabStop = false;
            this.groupBoxBasicData.Text = "Podstawowe dane";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(41, 104);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(185, 64);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "START";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // labelStreamApplicationIntensity
            // 
            this.labelStreamApplicationIntensity.AutoSize = true;
            this.labelStreamApplicationIntensity.Location = new System.Drawing.Point(21, 67);
            this.labelStreamApplicationIntensity.Name = "labelStreamApplicationIntensity";
            this.labelStreamApplicationIntensity.Size = new System.Drawing.Size(185, 13);
            this.labelStreamApplicationIntensity.TabIndex = 3;
            this.labelStreamApplicationIntensity.Text = "Intensywność strumienia zgłoszeń: 1/";
            // 
            // labelServiceIntensity
            // 
            this.labelServiceIntensity.AutoSize = true;
            this.labelServiceIntensity.Location = new System.Drawing.Point(38, 41);
            this.labelServiceIntensity.Name = "labelServiceIntensity";
            this.labelServiceIntensity.Size = new System.Drawing.Size(168, 13);
            this.labelServiceIntensity.TabIndex = 2;
            this.labelServiceIntensity.Text = "Intensywność obsługi systemu: 1/";
            // 
            // numericUpDownStreamApplicationIntensity
            // 
            this.numericUpDownStreamApplicationIntensity.Location = new System.Drawing.Point(212, 65);
            this.numericUpDownStreamApplicationIntensity.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownStreamApplicationIntensity.Name = "numericUpDownStreamApplicationIntensity";
            this.numericUpDownStreamApplicationIntensity.Size = new System.Drawing.Size(46, 20);
            this.numericUpDownStreamApplicationIntensity.TabIndex = 1;
            this.numericUpDownStreamApplicationIntensity.ValueChanged += new System.EventHandler(this.numericUpDownStreamApplicationIntensity_ValueChanged);
            // 
            // numericUpDownServiceIntensity
            // 
            this.numericUpDownServiceIntensity.Location = new System.Drawing.Point(212, 39);
            this.numericUpDownServiceIntensity.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownServiceIntensity.Name = "numericUpDownServiceIntensity";
            this.numericUpDownServiceIntensity.Size = new System.Drawing.Size(46, 20);
            this.numericUpDownServiceIntensity.TabIndex = 0;
            this.numericUpDownServiceIntensity.ValueChanged += new System.EventHandler(this.numericUpDownServiceIntensity_ValueChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plikToolStripMenuItem,
            this.modelToolStripMenuItem,
            this.statystykiToolStripMenuItem,
            this.oProgramieToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1387, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // plikToolStripMenuItem
            // 
            this.plikToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zapiszModelToolStripMenuItem,
            this.wczytajModelToolStripMenuItem,
            this.zamknijToolStripMenuItem});
            this.plikToolStripMenuItem.Name = "plikToolStripMenuItem";
            this.plikToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.plikToolStripMenuItem.Text = "Plik";
            // 
            // zapiszModelToolStripMenuItem
            // 
            this.zapiszModelToolStripMenuItem.Name = "zapiszModelToolStripMenuItem";
            this.zapiszModelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.zapiszModelToolStripMenuItem.Text = "Zapisz model";
            this.zapiszModelToolStripMenuItem.Click += new System.EventHandler(this.zapiszModelToolStripMenuItem_Click);
            // 
            // wczytajModelToolStripMenuItem
            // 
            this.wczytajModelToolStripMenuItem.Name = "wczytajModelToolStripMenuItem";
            this.wczytajModelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.wczytajModelToolStripMenuItem.Text = "Wczytaj model";
            this.wczytajModelToolStripMenuItem.Click += new System.EventHandler(this.wczytajModelToolStripMenuItem_Click);
            // 
            // zamknijToolStripMenuItem
            // 
            this.zamknijToolStripMenuItem.Name = "zamknijToolStripMenuItem";
            this.zamknijToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.zamknijToolStripMenuItem.Text = "Zamknij";
            this.zamknijToolStripMenuItem.Click += new System.EventHandler(this.zamknijToolStripMenuItem_Click);
            // 
            // modelToolStripMenuItem
            // 
            this.modelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dodajToolStripMenuItem});
            this.modelToolStripMenuItem.Name = "modelToolStripMenuItem";
            this.modelToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.modelToolStripMenuItem.Text = "Model";
            // 
            // dodajToolStripMenuItem
            // 
            this.dodajToolStripMenuItem.Name = "dodajToolStripMenuItem";
            this.dodajToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.dodajToolStripMenuItem.Text = "Dodaj maszynę";
            this.dodajToolStripMenuItem.Click += new System.EventHandler(this.dodajToolStripMenuItem_Click);
            // 
            // statystykiToolStripMenuItem
            // 
            this.statystykiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statystykiToolStripMenuItem1});
            this.statystykiToolStripMenuItem.Name = "statystykiToolStripMenuItem";
            this.statystykiToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.statystykiToolStripMenuItem.Text = "Statystyki";
            // 
            // statystykiToolStripMenuItem1
            // 
            this.statystykiToolStripMenuItem1.Name = "statystykiToolStripMenuItem1";
            this.statystykiToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.statystykiToolStripMenuItem1.Text = "Statystyki";
            this.statystykiToolStripMenuItem1.Click += new System.EventHandler(this.statystykiToolStripMenuItem1_Click);
            // 
            // oProgramieToolStripMenuItem
            // 
            this.oProgramieToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oProgramieToolStripMenuItem1});
            this.oProgramieToolStripMenuItem.Name = "oProgramieToolStripMenuItem";
            this.oProgramieToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.oProgramieToolStripMenuItem.Text = "Opis";
            // 
            // oProgramieToolStripMenuItem1
            // 
            this.oProgramieToolStripMenuItem1.Name = "oProgramieToolStripMenuItem1";
            this.oProgramieToolStripMenuItem1.Size = new System.Drawing.Size(141, 22);
            this.oProgramieToolStripMenuItem1.Text = "O programie";
            this.oProgramieToolStripMenuItem1.Click += new System.EventHandler(this.oProgramieToolStripMenuItem1_Click);
            // 
            // errorProviderServiceTimeIntensity
            // 
            this.errorProviderServiceTimeIntensity.ContainerControl = this;
            // 
            // errorProviderStreamApplicationIntensity
            // 
            this.errorProviderStreamApplicationIntensity.ContainerControl = this;
            // 
            // groupBoxComunicates
            // 
            this.groupBoxComunicates.Controls.Add(this.labelComunicateCurrent);
            this.groupBoxComunicates.Controls.Add(this.labeltimeCurrent);
            this.groupBoxComunicates.Controls.Add(this.buttonClear);
            this.groupBoxComunicates.Controls.Add(this.listBoxComunicates);
            this.groupBoxComunicates.Location = new System.Drawing.Point(1038, 212);
            this.groupBoxComunicates.Name = "groupBoxComunicates";
            this.groupBoxComunicates.Size = new System.Drawing.Size(337, 150);
            this.groupBoxComunicates.TabIndex = 5;
            this.groupBoxComunicates.TabStop = false;
            this.groupBoxComunicates.Text = "Komunikaty aktualne";
            // 
            // labelComunicateCurrent
            // 
            this.labelComunicateCurrent.AutoSize = true;
            this.labelComunicateCurrent.Location = new System.Drawing.Point(58, 25);
            this.labelComunicateCurrent.Name = "labelComunicateCurrent";
            this.labelComunicateCurrent.Size = new System.Drawing.Size(98, 13);
            this.labelComunicateCurrent.TabIndex = 3;
            this.labelComunicateCurrent.Text = "Nazwa komunikatu";
            // 
            // labeltimeCurrent
            // 
            this.labeltimeCurrent.AutoSize = true;
            this.labeltimeCurrent.Location = new System.Drawing.Point(12, 25);
            this.labeltimeCurrent.Name = "labeltimeCurrent";
            this.labeltimeCurrent.Size = new System.Drawing.Size(30, 13);
            this.labeltimeCurrent.TabIndex = 2;
            this.labeltimeCurrent.Text = "Czas";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(256, 116);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 1;
            this.buttonClear.Text = "Wyczyść";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // listBoxComunicates
            // 
            this.listBoxComunicates.FormattingEnabled = true;
            this.listBoxComunicates.Location = new System.Drawing.Point(15, 41);
            this.listBoxComunicates.Name = "listBoxComunicates";
            this.listBoxComunicates.Size = new System.Drawing.Size(316, 69);
            this.listBoxComunicates.TabIndex = 0;
            // 
            // backgroundWorkerUpdateInterface
            // 
            this.backgroundWorkerUpdateInterface.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerUpdateInterface_DoWork);
            // 
            // labelInputStream
            // 
            this.labelInputStream.AutoSize = true;
            this.labelInputStream.Location = new System.Drawing.Point(13, 639);
            this.labelInputStream.Name = "labelInputStream";
            this.labelInputStream.Size = new System.Drawing.Size(99, 13);
            this.labelInputStream.TabIndex = 6;
            this.labelInputStream.Text = "Strumień wejściowy";
            // 
            // labelOutputStream
            // 
            this.labelOutputStream.AutoSize = true;
            this.labelOutputStream.Location = new System.Drawing.Point(905, 636);
            this.labelOutputStream.Name = "labelOutputStream";
            this.labelOutputStream.Size = new System.Drawing.Size(98, 13);
            this.labelOutputStream.TabIndex = 7;
            this.labelOutputStream.Text = "Strumień wyjściowy";
            // 
            // groupBoxArchiveComunicates
            // 
            this.groupBoxArchiveComunicates.Controls.Add(this.labelComunicateArchive);
            this.groupBoxArchiveComunicates.Controls.Add(this.labelTimeArchive);
            this.groupBoxArchiveComunicates.Controls.Add(this.buttonClearArchiveComunicates);
            this.groupBoxArchiveComunicates.Controls.Add(this.listBoxArchiveComunicates);
            this.groupBoxArchiveComunicates.Location = new System.Drawing.Point(1038, 368);
            this.groupBoxArchiveComunicates.Name = "groupBoxArchiveComunicates";
            this.groupBoxArchiveComunicates.Size = new System.Drawing.Size(337, 284);
            this.groupBoxArchiveComunicates.TabIndex = 8;
            this.groupBoxArchiveComunicates.TabStop = false;
            this.groupBoxArchiveComunicates.Text = "Komunikaty archiwalne";
            // 
            // labelComunicateArchive
            // 
            this.labelComunicateArchive.AutoSize = true;
            this.labelComunicateArchive.Location = new System.Drawing.Point(58, 29);
            this.labelComunicateArchive.Name = "labelComunicateArchive";
            this.labelComunicateArchive.Size = new System.Drawing.Size(98, 13);
            this.labelComunicateArchive.TabIndex = 4;
            this.labelComunicateArchive.Text = "Nazwa komunikatu";
            // 
            // labelTimeArchive
            // 
            this.labelTimeArchive.AutoSize = true;
            this.labelTimeArchive.Location = new System.Drawing.Point(15, 29);
            this.labelTimeArchive.Name = "labelTimeArchive";
            this.labelTimeArchive.Size = new System.Drawing.Size(30, 13);
            this.labelTimeArchive.TabIndex = 3;
            this.labelTimeArchive.Text = "Czas";
            // 
            // buttonClearArchiveComunicates
            // 
            this.buttonClearArchiveComunicates.Location = new System.Drawing.Point(256, 255);
            this.buttonClearArchiveComunicates.Name = "buttonClearArchiveComunicates";
            this.buttonClearArchiveComunicates.Size = new System.Drawing.Size(75, 23);
            this.buttonClearArchiveComunicates.TabIndex = 2;
            this.buttonClearArchiveComunicates.Text = "Wyczyść";
            this.buttonClearArchiveComunicates.UseVisualStyleBackColor = true;
            this.buttonClearArchiveComunicates.Click += new System.EventHandler(this.buttonClearArchiveComunicates_Click);
            // 
            // listBoxArchiveComunicates
            // 
            this.listBoxArchiveComunicates.FormattingEnabled = true;
            this.listBoxArchiveComunicates.Location = new System.Drawing.Point(15, 45);
            this.listBoxArchiveComunicates.Name = "listBoxArchiveComunicates";
            this.listBoxArchiveComunicates.Size = new System.Drawing.Size(316, 199);
            this.listBoxArchiveComunicates.TabIndex = 0;
            // 
            // FormQueueSystems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1387, 661);
            this.Controls.Add(this.groupBoxArchiveComunicates);
            this.Controls.Add(this.labelOutputStream);
            this.Controls.Add(this.labelInputStream);
            this.Controls.Add(this.groupBoxComunicates);
            this.Controls.Add(this.groupBoxBasicData);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FormQueueSystems";
            this.Text = "Systemy kolejkowe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormQueueSystems_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormQueueSystems_Paint);
            this.groupBoxBasicData.ResumeLayout(false);
            this.groupBoxBasicData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStreamApplicationIntensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownServiceIntensity)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderServiceTimeIntensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderStreamApplicationIntensity)).EndInit();
            this.groupBoxComunicates.ResumeLayout(false);
            this.groupBoxComunicates.PerformLayout();
            this.groupBoxArchiveComunicates.ResumeLayout(false);
            this.groupBoxArchiveComunicates.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxBasicData;
        private System.Windows.Forms.NumericUpDown numericUpDownStreamApplicationIntensity;
        private System.Windows.Forms.NumericUpDown numericUpDownServiceIntensity;
        private System.Windows.Forms.Label labelStreamApplicationIntensity;
        private System.Windows.Forms.Label labelServiceIntensity;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zapiszModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wczytajModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zamknijToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dodajToolStripMenuItem;
        private System.Windows.Forms.ErrorProvider errorProviderServiceTimeIntensity;
        private System.Windows.Forms.ErrorProvider errorProviderStreamApplicationIntensity;
        private System.Windows.Forms.GroupBox groupBoxComunicates;
        private System.Windows.Forms.ListBox listBoxComunicates;
        private System.Windows.Forms.Button buttonClear;
        private System.ComponentModel.BackgroundWorker backgroundWorkerUpdateInterface;
        private System.Windows.Forms.Label labelOutputStream;
        private System.Windows.Forms.Label labelInputStream;
        private System.Windows.Forms.ToolStripMenuItem statystykiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statystykiToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem oProgramieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oProgramieToolStripMenuItem1;
        private System.Windows.Forms.GroupBox groupBoxArchiveComunicates;
        private System.Windows.Forms.Button buttonClearArchiveComunicates;
        private System.Windows.Forms.ListBox listBoxArchiveComunicates;
        private System.Windows.Forms.Label labeltimeCurrent;
        private System.Windows.Forms.Label labelComunicateArchive;
        private System.Windows.Forms.Label labelTimeArchive;
        private System.Windows.Forms.Label labelComunicateCurrent;

    }
}

