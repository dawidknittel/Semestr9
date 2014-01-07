﻿namespace Kolejki_LAB3
{
    partial class StatisticsWindow
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
            this.comboBoxMachinesName = new System.Windows.Forms.ComboBox();
            this.labelMachine = new System.Windows.Forms.Label();
            this.grpStatitics = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAbsoluteSystemAbility = new System.Windows.Forms.TextBox();
            this.textBoxRelativeSystemAbility = new System.Windows.Forms.TextBox();
            this.textBoxMeanTimeApplicationInQueue = new System.Windows.Forms.TextBox();
            this.textBoxMeanNumberOfApplicationInQueue = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.grpStatitics.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxMachinesName
            // 
            this.comboBoxMachinesName.FormattingEnabled = true;
            this.comboBoxMachinesName.Location = new System.Drawing.Point(70, 29);
            this.comboBoxMachinesName.Name = "comboBoxMachinesName";
            this.comboBoxMachinesName.Size = new System.Drawing.Size(241, 21);
            this.comboBoxMachinesName.TabIndex = 0;
            this.comboBoxMachinesName.SelectedValueChanged += new System.EventHandler(this.comboBoxMachinesName_SelectedValueChanged);
            // 
            // labelMachine
            // 
            this.labelMachine.AutoSize = true;
            this.labelMachine.Location = new System.Drawing.Point(12, 32);
            this.labelMachine.Name = "labelMachine";
            this.labelMachine.Size = new System.Drawing.Size(52, 13);
            this.labelMachine.TabIndex = 1;
            this.labelMachine.Text = "Maszyna:";
            // 
            // grpStatitics
            // 
            this.grpStatitics.Controls.Add(this.label4);
            this.grpStatitics.Controls.Add(this.label3);
            this.grpStatitics.Controls.Add(this.label2);
            this.grpStatitics.Controls.Add(this.label1);
            this.grpStatitics.Controls.Add(this.textBoxAbsoluteSystemAbility);
            this.grpStatitics.Controls.Add(this.textBoxRelativeSystemAbility);
            this.grpStatitics.Controls.Add(this.textBoxMeanTimeApplicationInQueue);
            this.grpStatitics.Controls.Add(this.textBoxMeanNumberOfApplicationInQueue);
            this.grpStatitics.Location = new System.Drawing.Point(15, 67);
            this.grpStatitics.Name = "grpStatitics";
            this.grpStatitics.Size = new System.Drawing.Size(296, 129);
            this.grpStatitics.TabIndex = 2;
            this.grpStatitics.TabStop = false;
            this.grpStatitics.Text = "Statystyki maszyny";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Bezwzględna zdolność systemu:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Względna zdolność systemu:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Średni czas przebywania w kolejce:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Średnia liczba zgłoszeń w kolejce:";
            // 
            // textBoxAbsoluteSystemAbility
            // 
            this.textBoxAbsoluteSystemAbility.Location = new System.Drawing.Point(190, 95);
            this.textBoxAbsoluteSystemAbility.Name = "textBoxAbsoluteSystemAbility";
            this.textBoxAbsoluteSystemAbility.ReadOnly = true;
            this.textBoxAbsoluteSystemAbility.Size = new System.Drawing.Size(100, 20);
            this.textBoxAbsoluteSystemAbility.TabIndex = 3;
            // 
            // textBoxRelativeSystemAbility
            // 
            this.textBoxRelativeSystemAbility.Location = new System.Drawing.Point(190, 70);
            this.textBoxRelativeSystemAbility.Name = "textBoxRelativeSystemAbility";
            this.textBoxRelativeSystemAbility.ReadOnly = true;
            this.textBoxRelativeSystemAbility.Size = new System.Drawing.Size(100, 20);
            this.textBoxRelativeSystemAbility.TabIndex = 2;
            // 
            // textBoxMeanTimeApplicationInQueue
            // 
            this.textBoxMeanTimeApplicationInQueue.Location = new System.Drawing.Point(190, 45);
            this.textBoxMeanTimeApplicationInQueue.Name = "textBoxMeanTimeApplicationInQueue";
            this.textBoxMeanTimeApplicationInQueue.ReadOnly = true;
            this.textBoxMeanTimeApplicationInQueue.Size = new System.Drawing.Size(100, 20);
            this.textBoxMeanTimeApplicationInQueue.TabIndex = 1;
            // 
            // textBoxMeanNumberOfApplicationInQueue
            // 
            this.textBoxMeanNumberOfApplicationInQueue.Location = new System.Drawing.Point(190, 18);
            this.textBoxMeanNumberOfApplicationInQueue.Name = "textBoxMeanNumberOfApplicationInQueue";
            this.textBoxMeanNumberOfApplicationInQueue.ReadOnly = true;
            this.textBoxMeanNumberOfApplicationInQueue.Size = new System.Drawing.Size(100, 20);
            this.textBoxMeanNumberOfApplicationInQueue.TabIndex = 0;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(236, 202);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // StatisticsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 237);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.grpStatitics);
            this.Controls.Add(this.labelMachine);
            this.Controls.Add(this.comboBoxMachinesName);
            this.Name = "StatisticsWindow";
            this.Text = "Statystyki";
            this.grpStatitics.ResumeLayout(false);
            this.grpStatitics.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxMachinesName;
        private System.Windows.Forms.Label labelMachine;
        private System.Windows.Forms.GroupBox grpStatitics;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox textBoxAbsoluteSystemAbility;
        private System.Windows.Forms.TextBox textBoxRelativeSystemAbility;
        private System.Windows.Forms.TextBox textBoxMeanTimeApplicationInQueue;
        private System.Windows.Forms.TextBox textBoxMeanNumberOfApplicationInQueue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}