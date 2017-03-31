namespace IrisClassification
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
            this.eye1_imageBox = new Emgu.CV.UI.ImageBox();
            this.eye2_imageBox = new Emgu.CV.UI.ImageBox();
            this.template1_imageBox = new Emgu.CV.UI.ImageBox();
            this.mask1_imageBox = new Emgu.CV.UI.ImageBox();
            this.xor1_imageBox = new Emgu.CV.UI.ImageBox();
            this.xor2_imageBox = new Emgu.CV.UI.ImageBox();
            this.mask2_imageBox = new Emgu.CV.UI.ImageBox();
            this.template2_imageBox = new Emgu.CV.UI.ImageBox();
            this.eye1Button = new System.Windows.Forms.Button();
            this.eye2Button = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.compareEyesButton = new System.Windows.Forms.Button();
            this.hammDistanceLabel = new System.Windows.Forms.Label();
            this.angleLabel = new System.Windows.Forms.Label();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.calcHistogramButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.eye1_imageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eye2_imageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.template1_imageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mask1_imageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xor1_imageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xor2_imageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mask2_imageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.template2_imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // eye1_imageBox
            // 
            this.eye1_imageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.eye1_imageBox.Location = new System.Drawing.Point(12, 12);
            this.eye1_imageBox.Name = "eye1_imageBox";
            this.eye1_imageBox.Size = new System.Drawing.Size(320, 280);
            this.eye1_imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.eye1_imageBox.TabIndex = 2;
            this.eye1_imageBox.TabStop = false;
            // 
            // eye2_imageBox
            // 
            this.eye2_imageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.eye2_imageBox.Location = new System.Drawing.Point(354, 12);
            this.eye2_imageBox.Name = "eye2_imageBox";
            this.eye2_imageBox.Size = new System.Drawing.Size(320, 280);
            this.eye2_imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.eye2_imageBox.TabIndex = 3;
            this.eye2_imageBox.TabStop = false;
            // 
            // template1_imageBox
            // 
            this.template1_imageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.template1_imageBox.Location = new System.Drawing.Point(12, 298);
            this.template1_imageBox.Name = "template1_imageBox";
            this.template1_imageBox.Size = new System.Drawing.Size(320, 38);
            this.template1_imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.template1_imageBox.TabIndex = 2;
            this.template1_imageBox.TabStop = false;
            // 
            // mask1_imageBox
            // 
            this.mask1_imageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mask1_imageBox.Location = new System.Drawing.Point(12, 342);
            this.mask1_imageBox.Name = "mask1_imageBox";
            this.mask1_imageBox.Size = new System.Drawing.Size(320, 38);
            this.mask1_imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mask1_imageBox.TabIndex = 4;
            this.mask1_imageBox.TabStop = false;
            // 
            // xor1_imageBox
            // 
            this.xor1_imageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.xor1_imageBox.Location = new System.Drawing.Point(12, 386);
            this.xor1_imageBox.Name = "xor1_imageBox";
            this.xor1_imageBox.Size = new System.Drawing.Size(320, 38);
            this.xor1_imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.xor1_imageBox.TabIndex = 5;
            this.xor1_imageBox.TabStop = false;
            // 
            // xor2_imageBox
            // 
            this.xor2_imageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.xor2_imageBox.Location = new System.Drawing.Point(354, 386);
            this.xor2_imageBox.Name = "xor2_imageBox";
            this.xor2_imageBox.Size = new System.Drawing.Size(320, 38);
            this.xor2_imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.xor2_imageBox.TabIndex = 8;
            this.xor2_imageBox.TabStop = false;
            // 
            // mask2_imageBox
            // 
            this.mask2_imageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mask2_imageBox.Location = new System.Drawing.Point(354, 342);
            this.mask2_imageBox.Name = "mask2_imageBox";
            this.mask2_imageBox.Size = new System.Drawing.Size(320, 38);
            this.mask2_imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mask2_imageBox.TabIndex = 7;
            this.mask2_imageBox.TabStop = false;
            // 
            // template2_imageBox
            // 
            this.template2_imageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.template2_imageBox.Location = new System.Drawing.Point(354, 298);
            this.template2_imageBox.Name = "template2_imageBox";
            this.template2_imageBox.Size = new System.Drawing.Size(320, 38);
            this.template2_imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.template2_imageBox.TabIndex = 6;
            this.template2_imageBox.TabStop = false;
            // 
            // eye1Button
            // 
            this.eye1Button.Location = new System.Drawing.Point(12, 430);
            this.eye1Button.Name = "eye1Button";
            this.eye1Button.Size = new System.Drawing.Size(156, 39);
            this.eye1Button.TabIndex = 9;
            this.eye1Button.Text = "Load first eye";
            this.eye1Button.UseVisualStyleBackColor = true;
            this.eye1Button.Click += new System.EventHandler(this.eye1Button_Click);
            // 
            // eye2Button
            // 
            this.eye2Button.Location = new System.Drawing.Point(354, 430);
            this.eye2Button.Name = "eye2Button";
            this.eye2Button.Size = new System.Drawing.Size(156, 39);
            this.eye2Button.TabIndex = 10;
            this.eye2Button.Text = "Load second eye";
            this.eye2Button.UseVisualStyleBackColor = true;
            this.eye2Button.Click += new System.EventHandler(this.eye2Button_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // compareEyesButton
            // 
            this.compareEyesButton.Enabled = false;
            this.compareEyesButton.Location = new System.Drawing.Point(12, 475);
            this.compareEyesButton.Name = "compareEyesButton";
            this.compareEyesButton.Size = new System.Drawing.Size(156, 42);
            this.compareEyesButton.TabIndex = 11;
            this.compareEyesButton.Text = "Compare loaded eyes";
            this.compareEyesButton.UseVisualStyleBackColor = true;
            this.compareEyesButton.Click += new System.EventHandler(this.compareEyesButton_Click);
            // 
            // hammDistanceLabel
            // 
            this.hammDistanceLabel.AutoSize = true;
            this.hammDistanceLabel.Location = new System.Drawing.Point(184, 441);
            this.hammDistanceLabel.Name = "hammDistanceLabel";
            this.hammDistanceLabel.Size = new System.Drawing.Size(13, 17);
            this.hammDistanceLabel.TabIndex = 12;
            this.hammDistanceLabel.Text = "-";
            // 
            // angleLabel
            // 
            this.angleLabel.AutoSize = true;
            this.angleLabel.Location = new System.Drawing.Point(184, 466);
            this.angleLabel.Name = "angleLabel";
            this.angleLabel.Size = new System.Drawing.Size(13, 17);
            this.angleLabel.TabIndex = 13;
            this.angleLabel.Text = "-";
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(691, 13);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(662, 503);
            this.zedGraphControl1.TabIndex = 14;
            // 
            // calcHistogramButton
            // 
            this.calcHistogramButton.Location = new System.Drawing.Point(354, 478);
            this.calcHistogramButton.Name = "calcHistogramButton";
            this.calcHistogramButton.Size = new System.Drawing.Size(156, 39);
            this.calcHistogramButton.TabIndex = 15;
            this.calcHistogramButton.Text = "Calculate histogram";
            this.calcHistogramButton.UseVisualStyleBackColor = true;
            this.calcHistogramButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 529);
            this.Controls.Add(this.calcHistogramButton);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.angleLabel);
            this.Controls.Add(this.hammDistanceLabel);
            this.Controls.Add(this.compareEyesButton);
            this.Controls.Add(this.eye2Button);
            this.Controls.Add(this.eye1Button);
            this.Controls.Add(this.xor2_imageBox);
            this.Controls.Add(this.mask2_imageBox);
            this.Controls.Add(this.template2_imageBox);
            this.Controls.Add(this.xor1_imageBox);
            this.Controls.Add(this.mask1_imageBox);
            this.Controls.Add(this.template1_imageBox);
            this.Controls.Add(this.eye2_imageBox);
            this.Controls.Add(this.eye1_imageBox);
            this.Name = "Form1";
            this.Text = "]";
            ((System.ComponentModel.ISupportInitialize)(this.eye1_imageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eye2_imageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.template1_imageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mask1_imageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xor1_imageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xor2_imageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mask2_imageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.template2_imageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox eye1_imageBox;
        private Emgu.CV.UI.ImageBox eye2_imageBox;
        private Emgu.CV.UI.ImageBox template1_imageBox;
        private Emgu.CV.UI.ImageBox mask1_imageBox;
        private Emgu.CV.UI.ImageBox xor1_imageBox;
        private Emgu.CV.UI.ImageBox xor2_imageBox;
        private Emgu.CV.UI.ImageBox mask2_imageBox;
        private Emgu.CV.UI.ImageBox template2_imageBox;
        private System.Windows.Forms.Button eye1Button;
        private System.Windows.Forms.Button eye2Button;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button compareEyesButton;
        private System.Windows.Forms.Label hammDistanceLabel;
        private System.Windows.Forms.Label angleLabel;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Button calcHistogramButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;

    }
}

