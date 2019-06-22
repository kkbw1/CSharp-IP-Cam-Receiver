namespace TCP_UDP_IMAGE
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_exip = new System.Windows.Forms.Label();
            this.lb_inip = new System.Windows.Forms.Label();
            this.gB_mode = new System.Windows.Forms.GroupBox();
            this.rB_udp = new System.Windows.Forms.RadioButton();
            this.rB_tcp = new System.Windows.Forms.RadioButton();
            this.gB_server = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btn_check = new System.Windows.Forms.Button();
            this.tB_myport = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allStopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cb_cam = new System.Windows.Forms.CheckBox();
            this.gB_mode.SuspendLayout();
            this.gB_server.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 181);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(204, 172);
            this.listBox1.TabIndex = 0;
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(7, 17);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(56, 42);
            this.btn_start.TabIndex = 1;
            this.btn_start.Text = "Server Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_close
            // 
            this.btn_close.Enabled = false;
            this.btn_close.Location = new System.Drawing.Point(69, 17);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(58, 42);
            this.btn_close.TabIndex = 2;
            this.btn_close.Text = "Server Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(12, 359);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(204, 21);
            this.textBox1.TabIndex = 3;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "My Internal IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "My External IP:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "My Port:";
            // 
            // lb_exip
            // 
            this.lb_exip.AutoSize = true;
            this.lb_exip.Location = new System.Drawing.Point(105, 99);
            this.lb_exip.Name = "lb_exip";
            this.lb_exip.Size = new System.Drawing.Size(11, 12);
            this.lb_exip.TabIndex = 7;
            this.lb_exip.Text = "0";
            // 
            // lb_inip
            // 
            this.lb_inip.AutoSize = true;
            this.lb_inip.Location = new System.Drawing.Point(105, 79);
            this.lb_inip.Name = "lb_inip";
            this.lb_inip.Size = new System.Drawing.Size(11, 12);
            this.lb_inip.TabIndex = 8;
            this.lb_inip.Text = "0";
            // 
            // gB_mode
            // 
            this.gB_mode.Controls.Add(this.rB_udp);
            this.gB_mode.Controls.Add(this.rB_tcp);
            this.gB_mode.Location = new System.Drawing.Point(133, 17);
            this.gB_mode.Name = "gB_mode";
            this.gB_mode.Size = new System.Drawing.Size(62, 53);
            this.gB_mode.TabIndex = 10;
            this.gB_mode.TabStop = false;
            this.gB_mode.Text = "Mode";
            // 
            // rB_udp
            // 
            this.rB_udp.AutoSize = true;
            this.rB_udp.Location = new System.Drawing.Point(6, 31);
            this.rB_udp.Name = "rB_udp";
            this.rB_udp.Size = new System.Drawing.Size(47, 16);
            this.rB_udp.TabIndex = 1;
            this.rB_udp.Text = "UDP";
            this.rB_udp.UseVisualStyleBackColor = true;
            // 
            // rB_tcp
            // 
            this.rB_tcp.AutoSize = true;
            this.rB_tcp.Checked = true;
            this.rB_tcp.Location = new System.Drawing.Point(6, 13);
            this.rB_tcp.Name = "rB_tcp";
            this.rB_tcp.Size = new System.Drawing.Size(48, 16);
            this.rB_tcp.TabIndex = 0;
            this.rB_tcp.TabStop = true;
            this.rB_tcp.Text = "TCP";
            this.rB_tcp.UseVisualStyleBackColor = true;
            // 
            // gB_server
            // 
            this.gB_server.Controls.Add(this.comboBox1);
            this.gB_server.Controls.Add(this.btn_check);
            this.gB_server.Controls.Add(this.tB_myport);
            this.gB_server.Controls.Add(this.gB_mode);
            this.gB_server.Controls.Add(this.lb_inip);
            this.gB_server.Controls.Add(this.lb_exip);
            this.gB_server.Controls.Add(this.label3);
            this.gB_server.Controls.Add(this.label2);
            this.gB_server.Controls.Add(this.label1);
            this.gB_server.Controls.Add(this.btn_close);
            this.gB_server.Controls.Add(this.btn_start);
            this.gB_server.Location = new System.Drawing.Point(12, 31);
            this.gB_server.Name = "gB_server";
            this.gB_server.Size = new System.Drawing.Size(204, 144);
            this.gB_server.TabIndex = 17;
            this.gB_server.TabStop = false;
            this.gB_server.Text = "Server";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(107, 76);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(97, 20);
            this.comboBox1.TabIndex = 19;
            // 
            // btn_check
            // 
            this.btn_check.Location = new System.Drawing.Point(160, 112);
            this.btn_check.Name = "btn_check";
            this.btn_check.Size = new System.Drawing.Size(15, 23);
            this.btn_check.TabIndex = 18;
            this.btn_check.Text = "c";
            this.btn_check.UseVisualStyleBackColor = true;
            this.btn_check.Click += new System.EventHandler(this.btn_check_Click);
            // 
            // tB_myport
            // 
            this.tB_myport.Location = new System.Drawing.Point(107, 114);
            this.tB_myport.Name = "tB_myport";
            this.tB_myport.Size = new System.Drawing.Size(46, 21);
            this.tB_myport.TabIndex = 17;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(662, 24);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.allStopToolStripMenuItem});
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.optionToolStripMenuItem.Text = "Option";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // allStopToolStripMenuItem
            // 
            this.allStopToolStripMenuItem.Name = "allStopToolStripMenuItem";
            this.allStopToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.allStopToolStripMenuItem.Text = "All Stop";
            this.allStopToolStripMenuItem.Click += new System.EventHandler(this.allStopToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(222, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(430, 322);
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 0;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // cb_cam
            // 
            this.cb_cam.AutoSize = true;
            this.cb_cam.Location = new System.Drawing.Point(383, 364);
            this.cb_cam.Name = "cb_cam";
            this.cb_cam.Size = new System.Drawing.Size(93, 16);
            this.cb_cam.TabIndex = 20;
            this.cb_cam.Text = "CAM On/Off";
            this.cb_cam.UseVisualStyleBackColor = true;
            this.cb_cam.CheckedChanged += new System.EventHandler(this.cb_cam_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 386);
            this.Controls.Add(this.cb_cam);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.gB_server);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.gB_mode.ResumeLayout(false);
            this.gB_mode.PerformLayout();
            this.gB_server.ResumeLayout(false);
            this.gB_server.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lb_exip;
        private System.Windows.Forms.Label lb_inip;
        private System.Windows.Forms.GroupBox gB_mode;
        private System.Windows.Forms.RadioButton rB_udp;
        private System.Windows.Forms.RadioButton rB_tcp;
        private System.Windows.Forms.GroupBox gB_server;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allStopToolStripMenuItem;
        private System.Windows.Forms.TextBox tB_myport;
        private System.Windows.Forms.Button btn_check;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox cb_cam;
    }
}

