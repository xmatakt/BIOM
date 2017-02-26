namespace FaceDetectionAndExtraction.Forms
{
    partial class FaceDetectionForm
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
            this.emguImageBox = new Emgu.CV.UI.ImageBox();
            this.loadImageButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.emguImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // emguImageBox
            // 
            this.emguImageBox.Location = new System.Drawing.Point(12, 12);
            this.emguImageBox.Name = "emguImageBox";
            this.emguImageBox.Size = new System.Drawing.Size(431, 298);
            this.emguImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.emguImageBox.TabIndex = 2;
            this.emguImageBox.TabStop = false;
            // 
            // loadImageButton
            // 
            this.loadImageButton.Location = new System.Drawing.Point(12, 316);
            this.loadImageButton.Name = "loadImageButton";
            this.loadImageButton.Size = new System.Drawing.Size(189, 35);
            this.loadImageButton.TabIndex = 3;
            this.loadImageButton.Text = "Load image";
            this.loadImageButton.UseVisualStyleBackColor = true;
            this.loadImageButton.Click += new System.EventHandler(this.loadImageButton_Click);
            // 
            // FaceDetectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 394);
            this.Controls.Add(this.loadImageButton);
            this.Controls.Add(this.emguImageBox);
            this.Name = "FaceDetectionForm";
            this.Text = "FaceDetectionForm";
            this.Load += new System.EventHandler(this.FaceDetectionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.emguImageBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox emguImageBox;
        private System.Windows.Forms.Button loadImageButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}