namespace Kolejki_LAB3.AddMachine
{
    partial class AddMachine
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
            this.numericUpDownServicePlacesNumber = new System.Windows.Forms.NumericUpDown();
            this.labelNewMachine = new System.Windows.Forms.Label();
            this.labelMM = new System.Windows.Forms.Label();
            this.labeLNumerOfitems = new System.Windows.Forms.Label();
            this.labelSlash = new System.Windows.Forms.Label();
            this.labelAlgorithm = new System.Windows.Forms.Label();
            this.labelMachineNumber = new System.Windows.Forms.Label();
            this.comboBoxAlgorithm = new System.Windows.Forms.ComboBox();
            this.labelQueueLenght = new System.Windows.Forms.Label();
            this.numericUpDownQueueLenght = new System.Windows.Forms.NumericUpDown();
            this.labelSelectedAlgorithm = new System.Windows.Forms.Label();
            this.labelSlash2 = new System.Windows.Forms.Label();
            this.labelQueueLenghtPattern = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxInput = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxOutput = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownInput = new System.Windows.Forms.NumericUpDown();
            this.labelInputPercent = new System.Windows.Forms.Label();
            this.numericUpDownOutput = new System.Windows.Forms.NumericUpDown();
            this.labelOutput = new System.Windows.Forms.Label();
            this.listBoxInputs = new System.Windows.Forms.ListBox();
            this.listBoxOutput = new System.Windows.Forms.ListBox();
            this.buttonInput = new System.Windows.Forms.Button();
            this.buttonOutput = new System.Windows.Forms.Button();
            this.errorProviderInput = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderOutput = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownServicePlacesNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQueueLenght)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOutput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownServicePlacesNumber
            // 
            this.numericUpDownServicePlacesNumber.Location = new System.Drawing.Point(120, 60);
            this.numericUpDownServicePlacesNumber.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownServicePlacesNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownServicePlacesNumber.Name = "numericUpDownServicePlacesNumber";
            this.numericUpDownServicePlacesNumber.Size = new System.Drawing.Size(43, 20);
            this.numericUpDownServicePlacesNumber.TabIndex = 0;
            this.numericUpDownServicePlacesNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownServicePlacesNumber.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // labelNewMachine
            // 
            this.labelNewMachine.AutoSize = true;
            this.labelNewMachine.Location = new System.Drawing.Point(12, 21);
            this.labelNewMachine.Name = "labelNewMachine";
            this.labelNewMachine.Size = new System.Drawing.Size(82, 13);
            this.labelNewMachine.TabIndex = 1;
            this.labelNewMachine.Text = "Nowa maszyna:";
            // 
            // labelMM
            // 
            this.labelMM.AutoSize = true;
            this.labelMM.Location = new System.Drawing.Point(100, 21);
            this.labelMM.Name = "labelMM";
            this.labelMM.Size = new System.Drawing.Size(35, 13);
            this.labelMM.TabIndex = 2;
            this.labelMM.Text = "M/M/";
            // 
            // labeLNumerOfitems
            // 
            this.labeLNumerOfitems.AutoSize = true;
            this.labeLNumerOfitems.Location = new System.Drawing.Point(131, 21);
            this.labeLNumerOfitems.Name = "labeLNumerOfitems";
            this.labeLNumerOfitems.Size = new System.Drawing.Size(13, 13);
            this.labeLNumerOfitems.TabIndex = 3;
            this.labeLNumerOfitems.Text = "1";
            // 
            // labelSlash
            // 
            this.labelSlash.AutoSize = true;
            this.labelSlash.Location = new System.Drawing.Point(141, 21);
            this.labelSlash.Name = "labelSlash";
            this.labelSlash.Size = new System.Drawing.Size(12, 13);
            this.labelSlash.TabIndex = 4;
            this.labelSlash.Text = "/";
            // 
            // labelAlgorithm
            // 
            this.labelAlgorithm.AutoSize = true;
            this.labelAlgorithm.Location = new System.Drawing.Point(12, 92);
            this.labelAlgorithm.Name = "labelAlgorithm";
            this.labelAlgorithm.Size = new System.Drawing.Size(88, 13);
            this.labelAlgorithm.TabIndex = 5;
            this.labelAlgorithm.Text = "Algorytm obsługi:";
            // 
            // labelMachineNumber
            // 
            this.labelMachineNumber.AutoSize = true;
            this.labelMachineNumber.Location = new System.Drawing.Point(12, 62);
            this.labelMachineNumber.Name = "labelMachineNumber";
            this.labelMachineNumber.Size = new System.Drawing.Size(102, 13);
            this.labelMachineNumber.TabIndex = 6;
            this.labelMachineNumber.Text = "Ilość miejsc obłusgi:";
            // 
            // comboBoxAlgorithm
            // 
            this.comboBoxAlgorithm.FormattingEnabled = true;
            this.comboBoxAlgorithm.Location = new System.Drawing.Point(120, 89);
            this.comboBoxAlgorithm.Name = "comboBoxAlgorithm";
            this.comboBoxAlgorithm.Size = new System.Drawing.Size(80, 21);
            this.comboBoxAlgorithm.TabIndex = 7;
            this.comboBoxAlgorithm.SelectedValueChanged += new System.EventHandler(this.comboBoxAlgorithm_SelectedValueChanged);
            // 
            // labelQueueLenght
            // 
            this.labelQueueLenght.AutoSize = true;
            this.labelQueueLenght.Location = new System.Drawing.Point(12, 121);
            this.labelQueueLenght.Name = "labelQueueLenght";
            this.labelQueueLenght.Size = new System.Drawing.Size(84, 13);
            this.labelQueueLenght.TabIndex = 8;
            this.labelQueueLenght.Text = "Długość kolejki:";
            // 
            // numericUpDownQueueLenght
            // 
            this.numericUpDownQueueLenght.Location = new System.Drawing.Point(120, 119);
            this.numericUpDownQueueLenght.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownQueueLenght.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownQueueLenght.Name = "numericUpDownQueueLenght";
            this.numericUpDownQueueLenght.Size = new System.Drawing.Size(43, 20);
            this.numericUpDownQueueLenght.TabIndex = 9;
            this.numericUpDownQueueLenght.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownQueueLenght.ValueChanged += new System.EventHandler(this.numericUpDownQueueLenght_ValueChanged);
            // 
            // labelSelectedAlgorithm
            // 
            this.labelSelectedAlgorithm.AutoSize = true;
            this.labelSelectedAlgorithm.Location = new System.Drawing.Point(150, 21);
            this.labelSelectedAlgorithm.Name = "labelSelectedAlgorithm";
            this.labelSelectedAlgorithm.Size = new System.Drawing.Size(46, 13);
            this.labelSelectedAlgorithm.TabIndex = 10;
            this.labelSelectedAlgorithm.Text = "algorytm";
            // 
            // labelSlash2
            // 
            this.labelSlash2.AutoSize = true;
            this.labelSlash2.Location = new System.Drawing.Point(202, 21);
            this.labelSlash2.Name = "labelSlash2";
            this.labelSlash2.Size = new System.Drawing.Size(12, 13);
            this.labelSlash2.TabIndex = 11;
            this.labelSlash2.Text = "/";
            // 
            // labelQueueLenghtPattern
            // 
            this.labelQueueLenghtPattern.AutoSize = true;
            this.labelQueueLenghtPattern.Location = new System.Drawing.Point(220, 21);
            this.labelQueueLenghtPattern.Name = "labelQueueLenghtPattern";
            this.labelQueueLenghtPattern.Size = new System.Drawing.Size(13, 13);
            this.labelQueueLenghtPattern.TabIndex = 12;
            this.labelQueueLenghtPattern.Text = "1";
            this.labelQueueLenghtPattern.UseWaitCursor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(174, 322);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(106, 23);
            this.buttonOK.TabIndex = 13;
            this.buttonOK.Text = "Dodaj maszynę";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(291, 322);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 14;
            this.buttonCancel.Text = "Anuluj";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboBoxInput
            // 
            this.comboBoxInput.FormattingEnabled = true;
            this.comboBoxInput.Location = new System.Drawing.Point(120, 148);
            this.comboBoxInput.Name = "comboBoxInput";
            this.comboBoxInput.Size = new System.Drawing.Size(80, 21);
            this.comboBoxInput.TabIndex = 16;
            this.comboBoxInput.SelectedValueChanged += new System.EventHandler(this.comboBoxInput_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Wejście:";
            // 
            // comboBoxOutput
            // 
            this.comboBoxOutput.FormattingEnabled = true;
            this.comboBoxOutput.Location = new System.Drawing.Point(274, 148);
            this.comboBoxOutput.Name = "comboBoxOutput";
            this.comboBoxOutput.Size = new System.Drawing.Size(80, 21);
            this.comboBoxOutput.TabIndex = 18;
            this.comboBoxOutput.SelectedValueChanged += new System.EventHandler(this.comboBoxOutput_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Wyjście:";
            // 
            // numericUpDownInput
            // 
            this.numericUpDownInput.Location = new System.Drawing.Point(120, 175);
            this.numericUpDownInput.Name = "numericUpDownInput";
            this.numericUpDownInput.Size = new System.Drawing.Size(43, 20);
            this.numericUpDownInput.TabIndex = 20;
            // 
            // labelInputPercent
            // 
            this.labelInputPercent.AutoSize = true;
            this.labelInputPercent.Location = new System.Drawing.Point(12, 177);
            this.labelInputPercent.Name = "labelInputPercent";
            this.labelInputPercent.Size = new System.Drawing.Size(68, 13);
            this.labelInputPercent.TabIndex = 19;
            this.labelInputPercent.Text = "Strumień [%]:";
            // 
            // numericUpDownOutput
            // 
            this.numericUpDownOutput.Location = new System.Drawing.Point(311, 175);
            this.numericUpDownOutput.Name = "numericUpDownOutput";
            this.numericUpDownOutput.Size = new System.Drawing.Size(43, 20);
            this.numericUpDownOutput.TabIndex = 22;
            // 
            // labelOutput
            // 
            this.labelOutput.AutoSize = true;
            this.labelOutput.Location = new System.Drawing.Point(220, 177);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(65, 13);
            this.labelOutput.TabIndex = 21;
            this.labelOutput.Text = "Strumień[%]:";
            // 
            // listBoxInputs
            // 
            this.listBoxInputs.FormattingEnabled = true;
            this.listBoxInputs.Location = new System.Drawing.Point(52, 240);
            this.listBoxInputs.Name = "listBoxInputs";
            this.listBoxInputs.Size = new System.Drawing.Size(148, 56);
            this.listBoxInputs.TabIndex = 23;
            // 
            // listBoxOutput
            // 
            this.listBoxOutput.FormattingEnabled = true;
            this.listBoxOutput.Location = new System.Drawing.Point(210, 240);
            this.listBoxOutput.Name = "listBoxOutput";
            this.listBoxOutput.Size = new System.Drawing.Size(144, 56);
            this.listBoxOutput.TabIndex = 24;
            // 
            // buttonInput
            // 
            this.buttonInput.Location = new System.Drawing.Point(125, 211);
            this.buttonInput.Name = "buttonInput";
            this.buttonInput.Size = new System.Drawing.Size(75, 23);
            this.buttonInput.TabIndex = 25;
            this.buttonInput.Text = "Dodaj";
            this.buttonInput.UseVisualStyleBackColor = true;
            this.buttonInput.Click += new System.EventHandler(this.buttonInput_Click);
            // 
            // buttonOutput
            // 
            this.buttonOutput.Location = new System.Drawing.Point(279, 211);
            this.buttonOutput.Name = "buttonOutput";
            this.buttonOutput.Size = new System.Drawing.Size(75, 23);
            this.buttonOutput.TabIndex = 26;
            this.buttonOutput.Text = "Dodaj";
            this.buttonOutput.UseVisualStyleBackColor = true;
            this.buttonOutput.Click += new System.EventHandler(this.buttonOutput_Click);
            // 
            // errorProviderInput
            // 
            this.errorProviderInput.ContainerControl = this;
            // 
            // errorProviderOutput
            // 
            this.errorProviderOutput.ContainerControl = this;
            // 
            // AddMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 357);
            this.Controls.Add(this.buttonOutput);
            this.Controls.Add(this.buttonInput);
            this.Controls.Add(this.listBoxOutput);
            this.Controls.Add(this.listBoxInputs);
            this.Controls.Add(this.numericUpDownOutput);
            this.Controls.Add(this.labelOutput);
            this.Controls.Add(this.numericUpDownInput);
            this.Controls.Add(this.labelInputPercent);
            this.Controls.Add(this.comboBoxOutput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelQueueLenghtPattern);
            this.Controls.Add(this.labelSlash2);
            this.Controls.Add(this.labelSelectedAlgorithm);
            this.Controls.Add(this.numericUpDownQueueLenght);
            this.Controls.Add(this.labelQueueLenght);
            this.Controls.Add(this.comboBoxAlgorithm);
            this.Controls.Add(this.labelMachineNumber);
            this.Controls.Add(this.labelAlgorithm);
            this.Controls.Add(this.labelSlash);
            this.Controls.Add(this.labeLNumerOfitems);
            this.Controls.Add(this.labelMM);
            this.Controls.Add(this.labelNewMachine);
            this.Controls.Add(this.numericUpDownServicePlacesNumber);
            this.Name = "AddMachine";
            this.Text = "Dodawanie maszyny";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownServicePlacesNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQueueLenght)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOutput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderOutput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownServicePlacesNumber;
        private System.Windows.Forms.Label labelNewMachine;
        private System.Windows.Forms.Label labelMM;
        private System.Windows.Forms.Label labeLNumerOfitems;
        private System.Windows.Forms.Label labelSlash;
        private System.Windows.Forms.Label labelAlgorithm;
        private System.Windows.Forms.Label labelMachineNumber;
        private System.Windows.Forms.ComboBox comboBoxAlgorithm;
        private System.Windows.Forms.Label labelQueueLenght;
        private System.Windows.Forms.NumericUpDown numericUpDownQueueLenght;
        private System.Windows.Forms.Label labelSelectedAlgorithm;
        private System.Windows.Forms.Label labelSlash2;
        private System.Windows.Forms.Label labelQueueLenghtPattern;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownInput;
        private System.Windows.Forms.Label labelInputPercent;
        private System.Windows.Forms.NumericUpDown numericUpDownOutput;
        private System.Windows.Forms.Label labelOutput;
        private System.Windows.Forms.ListBox listBoxInputs;
        private System.Windows.Forms.ListBox listBoxOutput;
        private System.Windows.Forms.Button buttonInput;
        private System.Windows.Forms.Button buttonOutput;
        private System.Windows.Forms.ErrorProvider errorProviderInput;
        private System.Windows.Forms.ErrorProvider errorProviderOutput;
    }
}