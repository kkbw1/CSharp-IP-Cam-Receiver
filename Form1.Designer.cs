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
            this.lblStat = new System.Windows.Forms.Label();
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
            this.tmrConnCheck = new System.Windows.Forms.Timer(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnSaveImg = new System.Windows.Forms.Button();
            this.gB_mode.SuspendLayout();
            this.gB_server.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(14, 269);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(268, 228);
            this.listBox1.TabIndex = 0;
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(8, 23);
            this.btn_start.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(64, 56);
            this.btn_start.TabIndex = 1;
            this.btn_start.Text = "Server Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_close
            // 
            this.btn_close.Enabled = false;
            this.btn_close.Location = new System.Drawing.Point(79, 23);
            this.btn_close.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(66, 56);
            this.btn_close.TabIndex = 2;
            this.btn_close.Text = "Server Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(14, 507);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(268, 22);
            this.textBox1.TabIndex = 3;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "My Internal IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "My External IP:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "My Port:";
            // 
            // lb_exip
            // 
            this.lb_exip.AutoSize = true;
            this.lb_exip.Location = new System.Drawing.Point(120, 132);
            this.lb_exip.Name = "lb_exip";
            this.lb_exip.Size = new System.Drawing.Size(16, 17);
            this.lb_exip.TabIndex = 7;
            this.lb_exip.Text = "0";
            // 
            // lb_inip
            // 
            this.lb_inip.AutoSize = true;
            this.lb_inip.Location = new System.Drawing.Point(120, 105);
            this.lb_inip.Name = "lb_inip";
            this.lb_inip.Size = new System.Drawing.Size(16, 17);
            this.lb_inip.TabIndex = 8;
            this.lb_inip.Text = "0";
            // 
            // gB_mode
            // 
            this.gB_mode.Controls.Add(this.rB_udp);
            this.gB_mode.Controls.Add(this.rB_tcp);
            this.gB_mode.Location = new System.Drawing.Point(152, 23);
            this.gB_mode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gB_mode.Name = "gB_mode";
            this.gB_mode.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gB_mode.Size = new System.Drawing.Size(71, 71);
            this.gB_mode.TabIndex = 10;
            this.gB_mode.TabStop = false;
            this.gB_mode.Text = "Mode";
            // 
            // rB_udp
            // 
            this.rB_udp.AutoSize = true;
            this.rB_udp.Location = new System.Drawing.Point(7, 41);
            this.rB_udp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rB_udp.Name = "rB_udp";
            this.rB_udp.Size = new System.Drawing.Size(58, 21);
            this.rB_udp.TabIndex = 1;
            this.rB_udp.Text = "UDP";
            this.rB_udp.UseVisualStyleBackColor = true;
            // 
            // rB_tcp
            // 
            this.rB_tcp.AutoSize = true;
            this.rB_tcp.Checked = true;
            this.rB_tcp.Location = new System.Drawing.Point(7, 17);
            this.rB_tcp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rB_tcp.Name = "rB_tcp";
            this.rB_tcp.Size = new System.Drawing.Size(56, 21);
            this.rB_tcp.TabIndex = 0;
            this.rB_tcp.TabStop = true;
            this.rB_tcp.Text = "TCP";
            this.rB_tcp.UseVisualStyleBackColor = true;
            // 
            // gB_server
            // 
            this.gB_server.Controls.Add(this.lblStat);
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
            this.gB_server.Location = new System.Drawing.Point(14, 41);
            this.gB_server.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gB_server.Name = "gB_server";
            this.gB_server.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gB_server.Size = new System.Drawing.Size(268, 220);
            this.gB_server.TabIndex = 17;
            this.gB_server.TabStop = false;
            this.gB_server.Text = "Server";
            // 
            // lblStat
            // 
            this.lblStat.AutoSize = true;
            this.lblStat.Location = new System.Drawing.Point(8, 187);
            this.lblStat.Name = "lblStat";
            this.lblStat.Size = new System.Drawing.Size(45, 17);
            this.lblStat.TabIndex = 20;
            this.lblStat.Text = "Conn:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(122, 101);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(137, 24);
            this.comboBox1.TabIndex = 19;
            // 
            // btn_check
            // 
            this.btn_check.Location = new System.Drawing.Point(183, 149);
            this.btn_check.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_check.Name = "btn_check";
            this.btn_check.Size = new System.Drawing.Size(32, 31);
            this.btn_check.TabIndex = 18;
            this.btn_check.Text = "c";
            this.btn_check.UseVisualStyleBackColor = true;
            this.btn_check.Click += new System.EventHandler(this.btn_check_Click);
            // 
            // tB_myport
            // 
            this.tB_myport.Location = new System.Drawing.Point(122, 152);
            this.tB_myport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tB_myport.Name = "tB_myport";
            this.tB_myport.Size = new System.Drawing.Size(52, 22);
            this.tB_myport.TabIndex = 17;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1532, 30);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(102, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.allStopToolStripMenuItem});
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(67, 24);
            this.optionToolStripMenuItem.Text = "Option";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(146, 24);
            this.clearToolStripMenuItem.Text = "Clear Chat";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // allStopToolStripMenuItem
            // 
            this.allStopToolStripMenuItem.Name = "allStopToolStripMenuItem";
            this.allStopToolStripMenuItem.Size = new System.Drawing.Size(146, 24);
            this.allStopToolStripMenuItem.Text = "All Stop";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(307, 41);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1200, 675);
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
            this.cb_cam.Location = new System.Drawing.Point(14, 537);
            this.cb_cam.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_cam.Name = "cb_cam";
            this.cb_cam.Size = new System.Drawing.Size(114, 21);
            this.cb_cam.TabIndex = 20;
            this.cb_cam.Text = "Image On/Off";
            this.cb_cam.UseVisualStyleBackColor = true;
            this.cb_cam.CheckedChanged += new System.EventHandler(this.cb_cam_CheckedChanged);
            // 
            // tmrConnCheck
            // 
            this.tmrConnCheck.Enabled = true;
            this.tmrConnCheck.Interval = 200;
            this.tmrConnCheck.Tick += new System.EventHandler(this.tmrConnCheck_Tick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.Location = new System.Drawing.Point(14, 565);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(268, 151);
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // btnSaveImg
            // 
            this.btnSaveImg.Location = new System.Drawing.Point(173, 535);
            this.btnSaveImg.Name = "btnSaveImg";
            this.btnSaveImg.Size = new System.Drawing.Size(109, 23);
            this.btnSaveImg.TabIndex = 22;
            this.btnSaveImg.Text = "Save Image";
            this.btnSaveImg.UseVisualStyleBackColor = true;
            this.btnSaveImg.Click += new System.EventHandler(this.btnSaveImg_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1532, 732);
            this.Controls.Add(this.btnSaveImg);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.cb_cam);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.gB_server);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "IP Image Receive";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gB_mode.ResumeLayout(false);
            this.gB_mode.PerformLayout();
            this.gB_server.ResumeLayout(false);
            this.gB_server.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
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
        private System.Windows.Forms.Timer tmrConnCheck;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblStat;
        private System.Windows.Forms.Button btnSaveImg;
    }
}

