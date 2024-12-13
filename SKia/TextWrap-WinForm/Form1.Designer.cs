namespace TextWrap
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
            this.SKGLControl_Canvas = new SkiaSharp.Views.Desktop.SKGLControl();
            this.TrackBar_TextWrap = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar_TextWrap)).BeginInit();
            this.SuspendLayout();
            // 
            // SKGLControl_Canvas
            // 
            this.SKGLControl_Canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SKGLControl_Canvas.BackColor = System.Drawing.Color.Black;
            this.SKGLControl_Canvas.Location = new System.Drawing.Point(14, 88);
            this.SKGLControl_Canvas.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.SKGLControl_Canvas.Name = "SKGLControl_Canvas";
            this.SKGLControl_Canvas.Size = new System.Drawing.Size(772, 349);
            this.SKGLControl_Canvas.TabIndex = 0;
            this.SKGLControl_Canvas.VSync = false;
            this.SKGLControl_Canvas.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs>(this.SKGLControl_Canvas_PaintSurface);
            // 
            // TrackBar_TextWrap
            // 
            this.TrackBar_TextWrap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TrackBar_TextWrap.Location = new System.Drawing.Point(12, 12);
            this.TrackBar_TextWrap.Maximum = 100;
            this.TrackBar_TextWrap.Name = "TrackBar_TextWrap";
            this.TrackBar_TextWrap.Size = new System.Drawing.Size(774, 69);
            this.TrackBar_TextWrap.TabIndex = 1;
            this.TrackBar_TextWrap.Value = 50;
            this.TrackBar_TextWrap.ValueChanged += new System.EventHandler(this.TrackBar_TextWrap_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TrackBar_TextWrap);
            this.Controls.Add(this.SKGLControl_Canvas);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar_TextWrap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SkiaSharp.Views.Desktop.SKGLControl SKGLControl_Canvas;
        private System.Windows.Forms.TrackBar TrackBar_TextWrap;
    }
}

