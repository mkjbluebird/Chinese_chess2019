namespace Chinese_chess
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.simpleOpenGlControl = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.simpleOpenGlControl2 = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.SuspendLayout();
            // 
            // simpleOpenGlControl
            // 
            this.simpleOpenGlControl.AccumBits = ((byte)(0));
            this.simpleOpenGlControl.AutoCheckErrors = false;
            this.simpleOpenGlControl.AutoFinish = false;
            this.simpleOpenGlControl.AutoMakeCurrent = true;
            this.simpleOpenGlControl.AutoSwapBuffers = true;
            this.simpleOpenGlControl.BackColor = System.Drawing.Color.Black;
            this.simpleOpenGlControl.ColorBits = ((byte)(32));
            this.simpleOpenGlControl.DepthBits = ((byte)(16));
            this.simpleOpenGlControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleOpenGlControl.Location = new System.Drawing.Point(0, 0);
            this.simpleOpenGlControl.Name = "simpleOpenGlControl";
            this.simpleOpenGlControl.Size = new System.Drawing.Size(1383, 637);
            this.simpleOpenGlControl.StencilBits = ((byte)(0));
            this.simpleOpenGlControl.TabIndex = 0;
            // 
            // simpleOpenGlControl2
            // 
            this.simpleOpenGlControl2.AccumBits = ((byte)(0));
            this.simpleOpenGlControl2.AutoCheckErrors = false;
            this.simpleOpenGlControl2.AutoFinish = false;
            this.simpleOpenGlControl2.AutoMakeCurrent = true;
            this.simpleOpenGlControl2.AutoSwapBuffers = true;
            this.simpleOpenGlControl2.BackColor = System.Drawing.Color.Black;
            this.simpleOpenGlControl2.ColorBits = ((byte)(32));
            this.simpleOpenGlControl2.DepthBits = ((byte)(16));
            this.simpleOpenGlControl2.Location = new System.Drawing.Point(390, 233);
            this.simpleOpenGlControl2.Name = "simpleOpenGlControl2";
            this.simpleOpenGlControl2.Size = new System.Drawing.Size(50, 50);
            this.simpleOpenGlControl2.StencilBits = ((byte)(0));
            this.simpleOpenGlControl2.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1383, 637);
            this.Controls.Add(this.simpleOpenGlControl2);
            this.Controls.Add(this.simpleOpenGlControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl simpleOpenGlControl;
        private Tao.Platform.Windows.SimpleOpenGlControl simpleOpenGlControl2;
    }
}

