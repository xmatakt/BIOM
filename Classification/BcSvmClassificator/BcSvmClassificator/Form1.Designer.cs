namespace BcSvmClassificator
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sVMKernelTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rbfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chi2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.zedGraphControl = new ZedGraph.ZedGraphControl();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.dataLabel = new System.Windows.Forms.Label();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.trackLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.calPrecision = new System.Windows.Forms.Button();
            this.svmCheckBox = new System.Windows.Forms.CheckBox();
            this.bayesCheckBox = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(862, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadDataToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadDataToolStripMenuItem
            // 
            this.loadDataToolStripMenuItem.Name = "loadDataToolStripMenuItem";
            this.loadDataToolStripMenuItem.Size = new System.Drawing.Size(151, 26);
            this.loadDataToolStripMenuItem.Text = "Load data";
            this.loadDataToolStripMenuItem.Click += new System.EventHandler(this.loadDataToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sVMKernelTypeToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // sVMKernelTypeToolStripMenuItem
            // 
            this.sVMKernelTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rbfToolStripMenuItem,
            this.chi2ToolStripMenuItem});
            this.sVMKernelTypeToolStripMenuItem.Name = "sVMKernelTypeToolStripMenuItem";
            this.sVMKernelTypeToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.sVMKernelTypeToolStripMenuItem.Text = "SVM kernel type";
            // 
            // rbfToolStripMenuItem
            // 
            this.rbfToolStripMenuItem.Checked = true;
            this.rbfToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rbfToolStripMenuItem.Name = "rbfToolStripMenuItem";
            this.rbfToolStripMenuItem.Size = new System.Drawing.Size(113, 26);
            this.rbfToolStripMenuItem.Text = "Rbf";
            this.rbfToolStripMenuItem.Click += new System.EventHandler(this.rbfToolStripMenuItem_Click);
            // 
            // chi2ToolStripMenuItem
            // 
            this.chi2ToolStripMenuItem.Name = "chi2ToolStripMenuItem";
            this.chi2ToolStripMenuItem.Size = new System.Drawing.Size(113, 26);
            this.chi2ToolStripMenuItem.Text = "Chi2";
            this.chi2ToolStripMenuItem.Click += new System.EventHandler(this.chi2ToolStripMenuItem_Click);
            // 
            // zedGraphControl
            // 
            this.zedGraphControl.Location = new System.Drawing.Point(0, 40);
            this.zedGraphControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.zedGraphControl.Name = "zedGraphControl";
            this.zedGraphControl.ScrollGrace = 0D;
            this.zedGraphControl.ScrollMaxX = 0D;
            this.zedGraphControl.ScrollMaxY = 0D;
            this.zedGraphControl.ScrollMaxY2 = 0D;
            this.zedGraphControl.ScrollMinX = 0D;
            this.zedGraphControl.ScrollMinY = 0D;
            this.zedGraphControl.ScrollMinY2 = 0D;
            this.zedGraphControl.Size = new System.Drawing.Size(645, 543);
            this.zedGraphControl.TabIndex = 4;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(498, 590);
            this.progressBar.MarqueeAnimationSpeed = 10;
            this.progressBar.Maximum = 50;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(147, 23);
            this.progressBar.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 593);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Data:";
            // 
            // dataLabel
            // 
            this.dataLabel.AutoSize = true;
            this.dataLabel.Location = new System.Drawing.Point(60, 593);
            this.dataLabel.Name = "dataLabel";
            this.dataLabel.Size = new System.Drawing.Size(0, 17);
            this.dataLabel.TabIndex = 7;
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(667, 40);
            this.trackBar.Maximum = 10000;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(173, 56);
            this.trackBar.TabIndex = 8;
            this.trackBar.TickFrequency = 1000;
            this.trackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // trackLabel
            // 
            this.trackLabel.AutoSize = true;
            this.trackLabel.Location = new System.Drawing.Point(739, 99);
            this.trackLabel.Name = "trackLabel";
            this.trackLabel.Size = new System.Drawing.Size(44, 17);
            this.trackLabel.TabIndex = 9;
            this.trackLabel.Text = "0.000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(664, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Gamma:";
            // 
            // calPrecision
            // 
            this.calPrecision.Location = new System.Drawing.Point(667, 189);
            this.calPrecision.Name = "calPrecision";
            this.calPrecision.Size = new System.Drawing.Size(173, 32);
            this.calPrecision.TabIndex = 11;
            this.calPrecision.Text = "Calculate precision";
            this.calPrecision.UseVisualStyleBackColor = true;
            this.calPrecision.Click += new System.EventHandler(this.calPrecision_Click);
            // 
            // svmCheckBox
            // 
            this.svmCheckBox.AutoSize = true;
            this.svmCheckBox.Checked = true;
            this.svmCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.svmCheckBox.Location = new System.Drawing.Point(667, 135);
            this.svmCheckBox.Name = "svmCheckBox";
            this.svmCheckBox.Size = new System.Drawing.Size(59, 21);
            this.svmCheckBox.TabIndex = 12;
            this.svmCheckBox.Text = "SVM";
            this.svmCheckBox.UseVisualStyleBackColor = true;
            // 
            // bayesCheckBox
            // 
            this.bayesCheckBox.AutoSize = true;
            this.bayesCheckBox.Checked = true;
            this.bayesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bayesCheckBox.Location = new System.Drawing.Point(667, 162);
            this.bayesCheckBox.Name = "bayesCheckBox";
            this.bayesCheckBox.Size = new System.Drawing.Size(108, 21);
            this.bayesCheckBox.TabIndex = 13;
            this.bayesCheckBox.Text = "Naive bayes";
            this.bayesCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 619);
            this.Controls.Add(this.bayesCheckBox);
            this.Controls.Add(this.svmCheckBox);
            this.Controls.Add(this.calPrecision);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackLabel);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.dataLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.zedGraphControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadDataToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private ZedGraph.ZedGraphControl zedGraphControl;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label dataLabel;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sVMKernelTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chi2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rbfToolStripMenuItem;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Label trackLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button calPrecision;
        private System.Windows.Forms.CheckBox svmCheckBox;
        private System.Windows.Forms.CheckBox bayesCheckBox;
    }
}

