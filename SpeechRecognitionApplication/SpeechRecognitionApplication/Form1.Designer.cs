namespace SpeechRecognitionApplication
{
    partial class Form1
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
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonListen = new System.Windows.Forms.Button();
            this.labelDoubleDot = new System.Windows.Forms.Label();
            this.labelResultText = new System.Windows.Forms.Label();
            this.labelRowStart = new System.Windows.Forms.Label();
            this.labelColumnStart = new System.Windows.Forms.Label();
            this.labelColumnDestination = new System.Windows.Forms.Label();
            this.labelRowDestination = new System.Windows.Forms.Label();
            this.labelPiece = new System.Windows.Forms.Label();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.labelResult = new System.Windows.Forms.Label();
            this.textBoxMove = new System.Windows.Forms.TextBox();
            this.comboBoxPiece = new System.Windows.Forms.ComboBox();
            this.buttonPiece = new System.Windows.Forms.Button();
            this.labelChosenPiece = new System.Windows.Forms.Label();
            this.groupBoxStartUp = new System.Windows.Forms.GroupBox();
            this.groupBoxDestination = new System.Windows.Forms.GroupBox();
            this.groupBoxStartUp.SuspendLayout();
            this.groupBoxDestination.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(197, 99);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 1;
            this.buttonClear.Text = "Wyczyść";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonListen
            // 
            this.buttonListen.Location = new System.Drawing.Point(116, 99);
            this.buttonListen.Name = "buttonListen";
            this.buttonListen.Size = new System.Drawing.Size(75, 23);
            this.buttonListen.TabIndex = 2;
            this.buttonListen.Text = "Sluchaj";
            this.buttonListen.UseVisualStyleBackColor = true;
            this.buttonListen.Click += new System.EventHandler(this.buttonListen_Click);
            // 
            // labelDoubleDot
            // 
            this.labelDoubleDot.AutoSize = true;
            this.labelDoubleDot.Location = new System.Drawing.Point(48, 9);
            this.labelDoubleDot.Name = "labelDoubleDot";
            this.labelDoubleDot.Size = new System.Drawing.Size(10, 13);
            this.labelDoubleDot.TabIndex = 3;
            this.labelDoubleDot.Text = ":";
            // 
            // labelResultText
            // 
            this.labelResultText.AutoSize = true;
            this.labelResultText.Location = new System.Drawing.Point(12, 9);
            this.labelResultText.Name = "labelResultText";
            this.labelResultText.Size = new System.Drawing.Size(37, 13);
            this.labelResultText.TabIndex = 4;
            this.labelResultText.Text = "Wynik";
            // 
            // labelRowStart
            // 
            this.labelRowStart.AutoSize = true;
            this.labelRowStart.Location = new System.Drawing.Point(6, 24);
            this.labelRowStart.Name = "labelRowStart";
            this.labelRowStart.Size = new System.Drawing.Size(42, 13);
            this.labelRowStart.TabIndex = 5;
            this.labelRowStart.Text = "Wiersz:";
            // 
            // labelColumnStart
            // 
            this.labelColumnStart.AutoSize = true;
            this.labelColumnStart.Location = new System.Drawing.Point(6, 46);
            this.labelColumnStart.Name = "labelColumnStart";
            this.labelColumnStart.Size = new System.Drawing.Size(51, 13);
            this.labelColumnStart.TabIndex = 6;
            this.labelColumnStart.Text = "Kolumna:";
            // 
            // labelColumnDestination
            // 
            this.labelColumnDestination.AutoSize = true;
            this.labelColumnDestination.Location = new System.Drawing.Point(7, 42);
            this.labelColumnDestination.Name = "labelColumnDestination";
            this.labelColumnDestination.Size = new System.Drawing.Size(51, 13);
            this.labelColumnDestination.TabIndex = 8;
            this.labelColumnDestination.Text = "Kolumna:";
            // 
            // labelRowDestination
            // 
            this.labelRowDestination.AutoSize = true;
            this.labelRowDestination.Location = new System.Drawing.Point(7, 20);
            this.labelRowDestination.Name = "labelRowDestination";
            this.labelRowDestination.Size = new System.Drawing.Size(42, 13);
            this.labelRowDestination.TabIndex = 7;
            this.labelRowDestination.Text = "Wiersz:";
            // 
            // labelPiece
            // 
            this.labelPiece.AutoSize = true;
            this.labelPiece.Location = new System.Drawing.Point(15, 99);
            this.labelPiece.Name = "labelPiece";
            this.labelPiece.Size = new System.Drawing.Size(39, 13);
            this.labelPiece.TabIndex = 9;
            this.labelPiece.Text = "Figura:";
            // 
            // buttonCheck
            // 
            this.buttonCheck.Location = new System.Drawing.Point(116, 182);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(75, 23);
            this.buttonCheck.TabIndex = 10;
            this.buttonCheck.Text = "Sprawdź";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Location = new System.Drawing.Point(56, 9);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(0, 13);
            this.labelResult.TabIndex = 11;
            // 
            // textBoxMove
            // 
            this.textBoxMove.Location = new System.Drawing.Point(15, 157);
            this.textBoxMove.Name = "textBoxMove";
            this.textBoxMove.Size = new System.Drawing.Size(176, 20);
            this.textBoxMove.TabIndex = 12;
            this.textBoxMove.TextChanged += new System.EventHandler(this.textBoxMove_TextChanged);
            this.textBoxMove.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxMove_KeyDown);
            // 
            // comboBoxPiece
            // 
            this.comboBoxPiece.FormattingEnabled = true;
            this.comboBoxPiece.Location = new System.Drawing.Point(197, 157);
            this.comboBoxPiece.Name = "comboBoxPiece";
            this.comboBoxPiece.Size = new System.Drawing.Size(75, 21);
            this.comboBoxPiece.TabIndex = 13;
            // 
            // buttonPiece
            // 
            this.buttonPiece.Location = new System.Drawing.Point(198, 182);
            this.buttonPiece.Name = "buttonPiece";
            this.buttonPiece.Size = new System.Drawing.Size(75, 23);
            this.buttonPiece.TabIndex = 14;
            this.buttonPiece.Text = "Wstaw";
            this.buttonPiece.UseVisualStyleBackColor = true;
            this.buttonPiece.Click += new System.EventHandler(this.buttonPiece_Click);
            // 
            // labelChosenPiece
            // 
            this.labelChosenPiece.AutoSize = true;
            this.labelChosenPiece.Location = new System.Drawing.Point(56, 99);
            this.labelChosenPiece.Name = "labelChosenPiece";
            this.labelChosenPiece.Size = new System.Drawing.Size(0, 13);
            this.labelChosenPiece.TabIndex = 15;
            // 
            // groupBoxStartUp
            // 
            this.groupBoxStartUp.Controls.Add(this.labelRowStart);
            this.groupBoxStartUp.Controls.Add(this.labelColumnStart);
            this.groupBoxStartUp.Location = new System.Drawing.Point(12, 25);
            this.groupBoxStartUp.Name = "groupBoxStartUp";
            this.groupBoxStartUp.Size = new System.Drawing.Size(109, 66);
            this.groupBoxStartUp.TabIndex = 16;
            this.groupBoxStartUp.TabStop = false;
            this.groupBoxStartUp.Text = "Początkowe";
            // 
            // groupBoxDestination
            // 
            this.groupBoxDestination.Controls.Add(this.labelRowDestination);
            this.groupBoxDestination.Controls.Add(this.labelColumnDestination);
            this.groupBoxDestination.Location = new System.Drawing.Point(150, 29);
            this.groupBoxDestination.Name = "groupBoxDestination";
            this.groupBoxDestination.Size = new System.Drawing.Size(106, 62);
            this.groupBoxDestination.TabIndex = 17;
            this.groupBoxDestination.TabStop = false;
            this.groupBoxDestination.Text = "Końcowe";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 217);
            this.Controls.Add(this.groupBoxDestination);
            this.Controls.Add(this.groupBoxStartUp);
            this.Controls.Add(this.labelChosenPiece);
            this.Controls.Add(this.buttonPiece);
            this.Controls.Add(this.comboBoxPiece);
            this.Controls.Add(this.textBoxMove);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.labelPiece);
            this.Controls.Add(this.labelResultText);
            this.Controls.Add(this.labelDoubleDot);
            this.Controls.Add(this.buttonListen);
            this.Controls.Add(this.buttonClear);
            this.Name = "Form1";
            this.Text = "SpeechRecognition";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxStartUp.ResumeLayout(false);
            this.groupBoxStartUp.PerformLayout();
            this.groupBoxDestination.ResumeLayout(false);
            this.groupBoxDestination.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonListen;
        private System.Windows.Forms.Label labelDoubleDot;
        private System.Windows.Forms.Label labelResultText;
        private System.Windows.Forms.Label labelRowStart;
        private System.Windows.Forms.Label labelColumnStart;
        private System.Windows.Forms.Label labelColumnDestination;
        private System.Windows.Forms.Label labelRowDestination;
        private System.Windows.Forms.Label labelPiece;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.TextBox textBoxMove;
        private System.Windows.Forms.ComboBox comboBoxPiece;
        private System.Windows.Forms.Button buttonPiece;
        private System.Windows.Forms.Label labelChosenPiece;
        private System.Windows.Forms.GroupBox groupBoxStartUp;
        private System.Windows.Forms.GroupBox groupBoxDestination;
    }
}

