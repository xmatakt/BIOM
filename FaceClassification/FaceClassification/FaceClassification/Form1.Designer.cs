namespace FaceClassification
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pCAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pcaComputeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.selectedFolderLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.classificationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sVMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crossvalidationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sVMCrossvalidation = new System.Windows.Forms.ToolStripMenuItem();
            this.zedGraphControl_partial = new ZedGraph.ZedGraphControl();
            this.zedGraphControl_total = new ZedGraph.ZedGraphControl();
            this.euclideanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.euclideanCrossValidation = new System.Windows.Forms.ToolStripMenuItem();
            this.allCrossvalidation = new System.Windows.Forms.ToolStripMenuItem();
            this.mahalanobisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mahalanobisCrossvalidation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.pCAToolStripMenuItem,
            this.classificationToolStripMenuItem,
            this.crossvalidationToolStripMenuItem,
            this.saToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1202, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseFolderToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // chooseFolderToolStripMenuItem
            // 
            this.chooseFolderToolStripMenuItem.Name = "chooseFolderToolStripMenuItem";
            this.chooseFolderToolStripMenuItem.Size = new System.Drawing.Size(177, 26);
            this.chooseFolderToolStripMenuItem.Text = "Choose folder";
            this.chooseFolderToolStripMenuItem.Click += new System.EventHandler(this.chooseFolderToolStripMenuItem_Click);
            // 
            // pCAToolStripMenuItem
            // 
            this.pCAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pcaComputeToolStripMenuItem});
            this.pCAToolStripMenuItem.Name = "pCAToolStripMenuItem";
            this.pCAToolStripMenuItem.Size = new System.Drawing.Size(78, 24);
            this.pCAToolStripMenuItem.Text = "Datasets";
            // 
            // pcaComputeToolStripMenuItem
            // 
            this.pcaComputeToolStripMenuItem.Name = "pcaComputeToolStripMenuItem";
            this.pcaComputeToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.pcaComputeToolStripMenuItem.Text = "Create";
            this.pcaComputeToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pcaComputeToolStripMenuItem.Click += new System.EventHandler(this.pcaComputeToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectedFolderLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 607);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1202, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // selectedFolderLabel
            // 
            this.selectedFolderLabel.Name = "selectedFolderLabel";
            this.selectedFolderLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // classificationToolStripMenuItem
            // 
            this.classificationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sVMToolStripMenuItem,
            this.euclideanToolStripMenuItem,
            this.mahalanobisToolStripMenuItem});
            this.classificationToolStripMenuItem.Name = "classificationToolStripMenuItem";
            this.classificationToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.classificationToolStripMenuItem.Text = "Classification";
            // 
            // sVMToolStripMenuItem
            // 
            this.sVMToolStripMenuItem.Name = "sVMToolStripMenuItem";
            this.sVMToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.sVMToolStripMenuItem.Text = "SVM";
            this.sVMToolStripMenuItem.Click += new System.EventHandler(this.sVMToolStripMenuItem_Click);
            // 
            // crossvalidationToolStripMenuItem
            // 
            this.crossvalidationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sVMCrossvalidation,
            this.euclideanCrossValidation,
            this.mahalanobisCrossvalidation,
            this.allCrossvalidation});
            this.crossvalidationToolStripMenuItem.Name = "crossvalidationToolStripMenuItem";
            this.crossvalidationToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.crossvalidationToolStripMenuItem.Text = "Crossvalidation";
            // 
            // sVMCrossvalidation
            // 
            this.sVMCrossvalidation.Name = "sVMCrossvalidation";
            this.sVMCrossvalidation.Size = new System.Drawing.Size(181, 26);
            this.sVMCrossvalidation.Text = "SVM";
            this.sVMCrossvalidation.Click += new System.EventHandler(this.sVMCrossvalidation_Click);
            // 
            // zedGraphControl_partial
            // 
            this.zedGraphControl_partial.Location = new System.Drawing.Point(0, 32);
            this.zedGraphControl_partial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.zedGraphControl_partial.Name = "zedGraphControl_partial";
            this.zedGraphControl_partial.ScrollGrace = 0D;
            this.zedGraphControl_partial.ScrollMaxX = 0D;
            this.zedGraphControl_partial.ScrollMaxY = 0D;
            this.zedGraphControl_partial.ScrollMaxY2 = 0D;
            this.zedGraphControl_partial.ScrollMinX = 0D;
            this.zedGraphControl_partial.ScrollMinY = 0D;
            this.zedGraphControl_partial.ScrollMinY2 = 0D;
            this.zedGraphControl_partial.Size = new System.Drawing.Size(620, 561);
            this.zedGraphControl_partial.TabIndex = 2;
            // 
            // zedGraphControl_total
            // 
            this.zedGraphControl_total.Location = new System.Drawing.Point(628, 32);
            this.zedGraphControl_total.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.zedGraphControl_total.Name = "zedGraphControl_total";
            this.zedGraphControl_total.ScrollGrace = 0D;
            this.zedGraphControl_total.ScrollMaxX = 0D;
            this.zedGraphControl_total.ScrollMaxY = 0D;
            this.zedGraphControl_total.ScrollMaxY2 = 0D;
            this.zedGraphControl_total.ScrollMinX = 0D;
            this.zedGraphControl_total.ScrollMinY = 0D;
            this.zedGraphControl_total.ScrollMinY2 = 0D;
            this.zedGraphControl_total.Size = new System.Drawing.Size(561, 561);
            this.zedGraphControl_total.TabIndex = 3;
            // 
            // euclideanToolStripMenuItem
            // 
            this.euclideanToolStripMenuItem.Name = "euclideanToolStripMenuItem";
            this.euclideanToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.euclideanToolStripMenuItem.Text = "Euclidean";
            this.euclideanToolStripMenuItem.Click += new System.EventHandler(this.euclideanToolStripMenuItem_Click);
            // 
            // euclideanCrossValidation
            // 
            this.euclideanCrossValidation.Name = "euclideanCrossValidation";
            this.euclideanCrossValidation.Size = new System.Drawing.Size(181, 26);
            this.euclideanCrossValidation.Text = "Euclidean";
            this.euclideanCrossValidation.Click += new System.EventHandler(this.euclideanCrossValidation_Click);
            // 
            // allCrossvalidation
            // 
            this.allCrossvalidation.Name = "allCrossvalidation";
            this.allCrossvalidation.Size = new System.Drawing.Size(181, 26);
            this.allCrossvalidation.Text = "All";
            this.allCrossvalidation.Click += new System.EventHandler(this.allCrossvalidation_Click);
            // 
            // mahalanobisToolStripMenuItem
            // 
            this.mahalanobisToolStripMenuItem.Name = "mahalanobisToolStripMenuItem";
            this.mahalanobisToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.mahalanobisToolStripMenuItem.Text = "Mahalanobis";
            this.mahalanobisToolStripMenuItem.Click += new System.EventHandler(this.mahalanobisToolStripMenuItem_Click);
            // 
            // saToolStripMenuItem
            // 
            this.saToolStripMenuItem.Name = "saToolStripMenuItem";
            this.saToolStripMenuItem.Size = new System.Drawing.Size(35, 24);
            this.saToolStripMenuItem.Text = "sa";
            this.saToolStripMenuItem.Click += new System.EventHandler(this.saToolStripMenuItem_Click);
            // 
            // mahalanobisCrossvalidation
            // 
            this.mahalanobisCrossvalidation.Name = "mahalanobisCrossvalidation";
            this.mahalanobisCrossvalidation.Size = new System.Drawing.Size(181, 26);
            this.mahalanobisCrossvalidation.Text = "Mahalanobis";
            this.mahalanobisCrossvalidation.Click += new System.EventHandler(this.mahalanobisCrossvalidation_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1202, 629);
            this.Controls.Add(this.zedGraphControl_total);
            this.Controls.Add(this.zedGraphControl_partial);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseFolderToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel selectedFolderLabel;
        private System.Windows.Forms.ToolStripMenuItem pCAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pcaComputeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem classificationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sVMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem crossvalidationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sVMCrossvalidation;
        private ZedGraph.ZedGraphControl zedGraphControl_partial;
        private ZedGraph.ZedGraphControl zedGraphControl_total;
        private System.Windows.Forms.ToolStripMenuItem euclideanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem euclideanCrossValidation;
        private System.Windows.Forms.ToolStripMenuItem allCrossvalidation;
        private System.Windows.Forms.ToolStripMenuItem mahalanobisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mahalanobisCrossvalidation;
    }
}

