namespace SeatTest.UI
{
    partial class PreFlightCheckControl
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
            if (disposing)
            {
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCushionActual = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCushionLCL = new System.Windows.Forms.TextBox();
            this.lblCushionUCL = new System.Windows.Forms.TextBox();
            this.lblBackrestUCL = new System.Windows.Forms.TextBox();
            this.lblBackrestLCL = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBackrestActual = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSeatUCL = new System.Windows.Forms.TextBox();
            this.lblSeatLCL = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblSeatActual = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pan_State = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F);
            this.lblTitle.ForeColor = System.Drawing.Color.Gold;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(306, 41);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "请检查对应座椅位置";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 22F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 41);
            this.label1.TabIndex = 1;
            this.label1.Text = "座椅前后";
            // 
            // lblCushionActual
            // 
            this.lblCushionActual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(21)))), ((int)(((byte)(41)))));
            this.lblCushionActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCushionActual.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCushionActual.ForeColor = System.Drawing.Color.White;
            this.lblCushionActual.Location = new System.Drawing.Point(429, 57);
            this.lblCushionActual.Name = "lblCushionActual";
            this.lblCushionActual.ReadOnly = true;
            this.lblCushionActual.Size = new System.Drawing.Size(140, 46);
            this.lblCushionActual.TabIndex = 2;
            this.lblCushionActual.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(156, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "mm";
            // 
            // lblCushionLCL
            // 
            this.lblCushionLCL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(21)))), ((int)(((byte)(41)))));
            this.lblCushionLCL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCushionLCL.Font = new System.Drawing.Font("Segoe UI", 22F);
            this.lblCushionLCL.ForeColor = System.Drawing.Color.White;
            this.lblCushionLCL.Location = new System.Drawing.Point(412, 119);
            this.lblCushionLCL.Name = "lblCushionLCL";
            this.lblCushionLCL.ReadOnly = true;
            this.lblCushionLCL.Size = new System.Drawing.Size(85, 47);
            this.lblCushionLCL.TabIndex = 4;
            this.lblCushionLCL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCushionUCL
            // 
            this.lblCushionUCL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(21)))), ((int)(((byte)(41)))));
            this.lblCushionUCL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCushionUCL.Font = new System.Drawing.Font("Segoe UI", 22F);
            this.lblCushionUCL.ForeColor = System.Drawing.Color.White;
            this.lblCushionUCL.Location = new System.Drawing.Point(503, 119);
            this.lblCushionUCL.Name = "lblCushionUCL";
            this.lblCushionUCL.ReadOnly = true;
            this.lblCushionUCL.Size = new System.Drawing.Size(85, 47);
            this.lblCushionUCL.TabIndex = 5;
            this.lblCushionUCL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblBackrestUCL
            // 
            this.lblBackrestUCL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(21)))), ((int)(((byte)(41)))));
            this.lblBackrestUCL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBackrestUCL.Font = new System.Drawing.Font("Segoe UI", 22F);
            this.lblBackrestUCL.ForeColor = System.Drawing.Color.White;
            this.lblBackrestUCL.Location = new System.Drawing.Point(295, 119);
            this.lblBackrestUCL.Name = "lblBackrestUCL";
            this.lblBackrestUCL.ReadOnly = true;
            this.lblBackrestUCL.Size = new System.Drawing.Size(85, 47);
            this.lblBackrestUCL.TabIndex = 10;
            this.lblBackrestUCL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblBackrestLCL
            // 
            this.lblBackrestLCL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(21)))), ((int)(((byte)(41)))));
            this.lblBackrestLCL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBackrestLCL.Font = new System.Drawing.Font("Segoe UI", 22F);
            this.lblBackrestLCL.ForeColor = System.Drawing.Color.White;
            this.lblBackrestLCL.Location = new System.Drawing.Point(206, 119);
            this.lblBackrestLCL.Name = "lblBackrestLCL";
            this.lblBackrestLCL.ReadOnly = true;
            this.lblBackrestLCL.Size = new System.Drawing.Size(85, 47);
            this.lblBackrestLCL.TabIndex = 9;
            this.lblBackrestLCL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(362, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 32);
            this.label3.TabIndex = 8;
            this.label3.Text = "mm";
            // 
            // lblBackrestActual
            // 
            this.lblBackrestActual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(21)))), ((int)(((byte)(41)))));
            this.lblBackrestActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBackrestActual.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBackrestActual.ForeColor = System.Drawing.Color.White;
            this.lblBackrestActual.Location = new System.Drawing.Point(218, 57);
            this.lblBackrestActual.Name = "lblBackrestActual";
            this.lblBackrestActual.ReadOnly = true;
            this.lblBackrestActual.Size = new System.Drawing.Size(140, 46);
            this.lblBackrestActual.TabIndex = 7;
            this.lblBackrestActual.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 22F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(218, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 41);
            this.label4.TabIndex = 6;
            this.label4.Text = "靠背前后";
            // 
            // lblSeatUCL
            // 
            this.lblSeatUCL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(21)))), ((int)(((byte)(41)))));
            this.lblSeatUCL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSeatUCL.Font = new System.Drawing.Font("Segoe UI", 22F);
            this.lblSeatUCL.ForeColor = System.Drawing.Color.White;
            this.lblSeatUCL.Location = new System.Drawing.Point(91, 119);
            this.lblSeatUCL.Name = "lblSeatUCL";
            this.lblSeatUCL.ReadOnly = true;
            this.lblSeatUCL.Size = new System.Drawing.Size(85, 47);
            this.lblSeatUCL.TabIndex = 15;
            this.lblSeatUCL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSeatLCL
            // 
            this.lblSeatLCL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(21)))), ((int)(((byte)(41)))));
            this.lblSeatLCL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSeatLCL.Font = new System.Drawing.Font("Segoe UI", 22F);
            this.lblSeatLCL.ForeColor = System.Drawing.Color.White;
            this.lblSeatLCL.Location = new System.Drawing.Point(2, 119);
            this.lblSeatLCL.Name = "lblSeatLCL";
            this.lblSeatLCL.ReadOnly = true;
            this.lblSeatLCL.Size = new System.Drawing.Size(85, 47);
            this.lblSeatLCL.TabIndex = 14;
            this.lblSeatLCL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(571, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 32);
            this.label5.TabIndex = 13;
            this.label5.Text = "mm";
            // 
            // lblSeatActual
            // 
            this.lblSeatActual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(21)))), ((int)(((byte)(41)))));
            this.lblSeatActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSeatActual.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeatActual.ForeColor = System.Drawing.Color.White;
            this.lblSeatActual.Location = new System.Drawing.Point(13, 57);
            this.lblSeatActual.Name = "lblSeatActual";
            this.lblSeatActual.ReadOnly = true;
            this.lblSeatActual.Size = new System.Drawing.Size(140, 46);
            this.lblSeatActual.TabIndex = 12;
            this.lblSeatActual.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 22F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(426, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 41);
            this.label6.TabIndex = 11;
            this.label6.Text = "座垫位置";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblTitle);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(629, 528);
            this.splitContainer1.SplitterDistance = 44;
            this.splitContainer1.TabIndex = 16;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lblCushionLCL);
            this.splitContainer2.Panel1.Controls.Add(this.label4);
            this.splitContainer2.Panel1.Controls.Add(this.lblBackrestActual);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.lblSeatUCL);
            this.splitContainer2.Panel1.Controls.Add(this.lblBackrestLCL);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.lblBackrestUCL);
            this.splitContainer2.Panel1.Controls.Add(this.lblSeatLCL);
            this.splitContainer2.Panel1.Controls.Add(this.lblCushionUCL);
            this.splitContainer2.Panel1.Controls.Add(this.lblCushionActual);
            this.splitContainer2.Panel1.Controls.Add(this.label6);
            this.splitContainer2.Panel1.Controls.Add(this.label5);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.lblSeatActual);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pan_State);
            this.splitContainer2.Size = new System.Drawing.Size(629, 480);
            this.splitContainer2.SplitterDistance = 175;
            this.splitContainer2.TabIndex = 16;
            // 
            // pan_State
            // 
            this.pan_State.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_State.Location = new System.Drawing.Point(0, 0);
            this.pan_State.Name = "pan_State";
            this.pan_State.Size = new System.Drawing.Size(629, 301);
            this.pan_State.TabIndex = 0;
            // 
            // PreFlightCheckControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(21)))), ((int)(((byte)(41)))));
            this.Controls.Add(this.splitContainer1);
            this.MaximumSize = new System.Drawing.Size(629, 528);
            this.MinimumSize = new System.Drawing.Size(629, 466);
            this.Name = "PreFlightCheckControl";
            this.Size = new System.Drawing.Size(629, 528);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox lblCushionActual;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox lblCushionLCL;
        private System.Windows.Forms.TextBox lblCushionUCL;
        private System.Windows.Forms.TextBox lblBackrestUCL;
        private System.Windows.Forms.TextBox lblBackrestLCL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox lblBackrestActual;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox lblSeatUCL;
        private System.Windows.Forms.TextBox lblSeatLCL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox lblSeatActual;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel pan_State;
    }
}
