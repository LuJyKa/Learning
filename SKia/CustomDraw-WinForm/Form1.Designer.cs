namespace skiasharp_wrap;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.skglControl1 = new SkiaSharp.Views.Desktop.SKGLControl();
        this.trackBar1 = new TrackBar();
        ((System.ComponentModel.ISupportInitialize)this.trackBar1).BeginInit();
        this.SuspendLayout();
        // 
        // skglControl1
        // 
        this.skglControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        this.skglControl1.BackColor = Color.Black;
        this.skglControl1.Location = new Point(14, 116);
        this.skglControl1.Margin = new Padding(5, 4, 5, 4);
        this.skglControl1.Name = "skglControl1";
        this.skglControl1.Size = new Size(727, 706);
        this.skglControl1.TabIndex = 0;
        this.skglControl1.VSync = true;
        // 
        // trackBar1
        // 
        this.trackBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        this.trackBar1.Location = new Point(13, 18);
        this.trackBar1.Margin = new Padding(4);
        this.trackBar1.Maximum = 100;
        this.trackBar1.Minimum = 1;
        this.trackBar1.Name = "trackBar1";
        this.trackBar1.Size = new Size(728, 69);
        this.trackBar1.TabIndex = 1;
        this.trackBar1.Value = 50;
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new SizeF(9F, 22F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(758, 835);
        this.Controls.Add(this.trackBar1);
        this.Controls.Add(this.skglControl1);
        this.Margin = new Padding(4);
        this.Name = "Form1";
        this.Text = "SkiaSharp Word Wrap";
        ((System.ComponentModel.ISupportInitialize)this.trackBar1).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private SkiaSharp.Views.Desktop.SKGLControl skglControl1;
    private TrackBar trackBar1;
}
