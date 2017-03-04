namespace BimSystRating
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
            this.createDataSetsButton = new System.Windows.Forms.Button();
            this.fmrFnmrgraphsButton = new System.Windows.Forms.Button();
            this.rocGraphButton = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.minThresholdNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.maxThresholdNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.threshStepNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.xValCountNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.minThresholdNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxThresholdNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threshStepNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xValCountNumUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // createDataSetsButton
            // 
            this.createDataSetsButton.Location = new System.Drawing.Point(12, 23);
            this.createDataSetsButton.Name = "createDataSetsButton";
            this.createDataSetsButton.Size = new System.Drawing.Size(176, 40);
            this.createDataSetsButton.TabIndex = 0;
            this.createDataSetsButton.Text = "Generate graphs";
            this.createDataSetsButton.UseVisualStyleBackColor = true;
            this.createDataSetsButton.Click += new System.EventHandler(this.createDataSetsButton_Click);
            // 
            // fmrFnmrgraphsButton
            // 
            this.fmrFnmrgraphsButton.Enabled = false;
            this.fmrFnmrgraphsButton.Location = new System.Drawing.Point(12, 69);
            this.fmrFnmrgraphsButton.Name = "fmrFnmrgraphsButton";
            this.fmrFnmrgraphsButton.Size = new System.Drawing.Size(176, 40);
            this.fmrFnmrgraphsButton.TabIndex = 1;
            this.fmrFnmrgraphsButton.Text = "FMR/FNMR graphs";
            this.fmrFnmrgraphsButton.UseVisualStyleBackColor = true;
            this.fmrFnmrgraphsButton.Click += new System.EventHandler(this.createRecognizerButton_Click);
            // 
            // rocGraphButton
            // 
            this.rocGraphButton.Enabled = false;
            this.rocGraphButton.Location = new System.Drawing.Point(12, 115);
            this.rocGraphButton.Name = "rocGraphButton";
            this.rocGraphButton.Size = new System.Drawing.Size(176, 40);
            this.rocGraphButton.TabIndex = 2;
            this.rocGraphButton.Text = "ROC curve graph";
            this.rocGraphButton.UseVisualStyleBackColor = true;
            this.rocGraphButton.Click += new System.EventHandler(this.rocGraphButton_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(222, 26);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(167, 21);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "LBPH face recognizer";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(222, 53);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(167, 21);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.Text = "Eigen face recognizer";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // minThresholdNumUpDown
            // 
            this.minThresholdNumUpDown.Location = new System.Drawing.Point(315, 80);
            this.minThresholdNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.minThresholdNumUpDown.Name = "minThresholdNumUpDown";
            this.minThresholdNumUpDown.Size = new System.Drawing.Size(74, 22);
            this.minThresholdNumUpDown.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(219, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Min. T:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Max T:";
            // 
            // maxThresholdNumUpDown
            // 
            this.maxThresholdNumUpDown.Location = new System.Drawing.Point(315, 108);
            this.maxThresholdNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.maxThresholdNumUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxThresholdNumUpDown.Name = "maxThresholdNumUpDown";
            this.maxThresholdNumUpDown.Size = new System.Drawing.Size(74, 22);
            this.maxThresholdNumUpDown.TabIndex = 7;
            this.maxThresholdNumUpDown.Value = new decimal(new int[] {
            700,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(219, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Step for T:";
            // 
            // threshStepNumUpDown
            // 
            this.threshStepNumUpDown.Location = new System.Drawing.Point(315, 136);
            this.threshStepNumUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.threshStepNumUpDown.Name = "threshStepNumUpDown";
            this.threshStepNumUpDown.Size = new System.Drawing.Size(74, 22);
            this.threshStepNumUpDown.TabIndex = 9;
            this.threshStepNumUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(219, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "xVal count:";
            // 
            // xValCountNumUpDown
            // 
            this.xValCountNumUpDown.Location = new System.Drawing.Point(315, 164);
            this.xValCountNumUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.xValCountNumUpDown.Name = "xValCountNumUpDown";
            this.xValCountNumUpDown.Size = new System.Drawing.Size(74, 22);
            this.xValCountNumUpDown.TabIndex = 11;
            this.xValCountNumUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "(* T means threshold)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 202);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.xValCountNumUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.threshStepNumUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.maxThresholdNumUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.minThresholdNumUpDown);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.rocGraphButton);
            this.Controls.Add(this.fmrFnmrgraphsButton);
            this.Controls.Add(this.createDataSetsButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.minThresholdNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxThresholdNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threshStepNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xValCountNumUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createDataSetsButton;
        private System.Windows.Forms.Button fmrFnmrgraphsButton;
        private System.Windows.Forms.Button rocGraphButton;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.NumericUpDown minThresholdNumUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown maxThresholdNumUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown threshStepNumUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown xValCountNumUpDown;
        private System.Windows.Forms.Label label5;
    }
}

