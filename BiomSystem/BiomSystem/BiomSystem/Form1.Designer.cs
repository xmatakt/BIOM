namespace BiomSystem
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
            this.face1ImageBox = new Emgu.CV.UI.ImageBox();
            this.xor1ImageBox = new Emgu.CV.UI.ImageBox();
            this.xor2ImageBox = new Emgu.CV.UI.ImageBox();
            this.face2ImageBox = new Emgu.CV.UI.ImageBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.button3 = new System.Windows.Forms.Button();
            this.hammDistanceLabel = new System.Windows.Forms.Label();
            this.euclidDistanceLabel = new System.Windows.Forms.Label();
            this.similarityLabel = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.face1ImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xor1ImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xor2ImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.face2ImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // face1ImageBox
            // 
            this.face1ImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.face1ImageBox.Location = new System.Drawing.Point(12, 12);
            this.face1ImageBox.Name = "face1ImageBox";
            this.face1ImageBox.Size = new System.Drawing.Size(239, 240);
            this.face1ImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.face1ImageBox.TabIndex = 2;
            this.face1ImageBox.TabStop = false;
            // 
            // xor1ImageBox
            // 
            this.xor1ImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.xor1ImageBox.Location = new System.Drawing.Point(12, 258);
            this.xor1ImageBox.Name = "xor1ImageBox";
            this.xor1ImageBox.Size = new System.Drawing.Size(239, 39);
            this.xor1ImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.xor1ImageBox.TabIndex = 2;
            this.xor1ImageBox.TabStop = false;
            // 
            // xor2ImageBox
            // 
            this.xor2ImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.xor2ImageBox.Location = new System.Drawing.Point(266, 258);
            this.xor2ImageBox.Name = "xor2ImageBox";
            this.xor2ImageBox.Size = new System.Drawing.Size(239, 39);
            this.xor2ImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.xor2ImageBox.TabIndex = 3;
            this.xor2ImageBox.TabStop = false;
            // 
            // face2ImageBox
            // 
            this.face2ImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.face2ImageBox.Location = new System.Drawing.Point(266, 12);
            this.face2ImageBox.Name = "face2ImageBox";
            this.face2ImageBox.Size = new System.Drawing.Size(239, 240);
            this.face2ImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.face2ImageBox.TabIndex = 4;
            this.face2ImageBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 303);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(239, 44);
            this.button1.TabIndex = 5;
            this.button1.Text = "Choose first person";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(266, 303);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(239, 44);
            this.button2.TabIndex = 6;
            this.button2.Text = "Choose second person";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 353);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(239, 41);
            this.button3.TabIndex = 7;
            this.button3.Text = "Compare persons";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // hammDistanceLabel
            // 
            this.hammDistanceLabel.AutoSize = true;
            this.hammDistanceLabel.Location = new System.Drawing.Point(511, 12);
            this.hammDistanceLabel.Name = "hammDistanceLabel";
            this.hammDistanceLabel.Size = new System.Drawing.Size(128, 17);
            this.hammDistanceLabel.TabIndex = 8;
            this.hammDistanceLabel.Text = "Hamming distance:";
            // 
            // euclidDistanceLabel
            // 
            this.euclidDistanceLabel.AutoSize = true;
            this.euclidDistanceLabel.Location = new System.Drawing.Point(511, 44);
            this.euclidDistanceLabel.Name = "euclidDistanceLabel";
            this.euclidDistanceLabel.Size = new System.Drawing.Size(131, 17);
            this.euclidDistanceLabel.TabIndex = 9;
            this.euclidDistanceLabel.Text = "Euclidean distance:";
            // 
            // similarityLabel
            // 
            this.similarityLabel.AutoSize = true;
            this.similarityLabel.Location = new System.Drawing.Point(511, 75);
            this.similarityLabel.Name = "similarityLabel";
            this.similarityLabel.Size = new System.Drawing.Size(68, 17);
            this.similarityLabel.TabIndex = 10;
            this.similarityLabel.Text = "Similarity:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(266, 353);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(239, 41);
            this.button4.TabIndex = 11;
            this.button4.Text = "Generate ROC Curve";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 405);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.similarityLabel);
            this.Controls.Add(this.euclidDistanceLabel);
            this.Controls.Add(this.hammDistanceLabel);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.xor2ImageBox);
            this.Controls.Add(this.face2ImageBox);
            this.Controls.Add(this.xor1ImageBox);
            this.Controls.Add(this.face1ImageBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.face1ImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xor1ImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xor2ImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.face2ImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox face1ImageBox;
        private Emgu.CV.UI.ImageBox xor1ImageBox;
        private Emgu.CV.UI.ImageBox xor2ImageBox;
        private Emgu.CV.UI.ImageBox face2ImageBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label hammDistanceLabel;
        private System.Windows.Forms.Label euclidDistanceLabel;
        private System.Windows.Forms.Label similarityLabel;
        private System.Windows.Forms.Button button4;
    }
}

