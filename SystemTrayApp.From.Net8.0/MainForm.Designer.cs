namespace SystemTrayApp.From.Net8._0
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            groupBox1 = new GroupBox();
            label4 = new Label();
            label3 = new Label();
            hbox1 = new TextBox();
            label2 = new Label();
            wBox = new TextBox();
            label1 = new Label();
            yBox = new TextBox();
            xBox = new TextBox();
            pictureBox1 = new PictureBox();
            button1 = new Button();
            txtLbl = new Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            button2 = new Button();
            SaveBtn = new Button();
            dataGridView1 = new DataGridView();
            tabPage3 = new TabPage();
            appSettingView11 = new AppSetting.AppSettingView1();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(hbox1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(wBox);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(yBox);
            groupBox1.Controls.Add(xBox);
            groupBox1.Location = new Point(6, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(159, 154);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Parameter";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 117);
            label4.Name = "label4";
            label4.Size = new Size(16, 15);
            label4.TabIndex = 2;
            label4.Text = "H";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 88);
            label3.Name = "label3";
            label3.Size = new Size(18, 15);
            label3.TabIndex = 2;
            label3.Text = "W";
            // 
            // hbox1
            // 
            hbox1.Location = new Point(48, 114);
            hbox1.Name = "hbox1";
            hbox1.Size = new Size(100, 23);
            hbox1.TabIndex = 4;
            hbox1.Text = "100";
            hbox1.KeyDown += xBox_KeyDown;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 59);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 2;
            label2.Text = "Y Cor";
            // 
            // wBox
            // 
            wBox.Location = new Point(48, 85);
            wBox.Name = "wBox";
            wBox.Size = new Size(100, 23);
            wBox.TabIndex = 3;
            wBox.Text = "100";
            wBox.KeyDown += xBox_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 30);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 1;
            label1.Text = "X Cor";
            // 
            // yBox
            // 
            yBox.Location = new Point(48, 56);
            yBox.Name = "yBox";
            yBox.Size = new Size(100, 23);
            yBox.TabIndex = 2;
            yBox.Text = "0";
            yBox.KeyDown += xBox_KeyDown;
            // 
            // xBox
            // 
            xBox.Location = new Point(48, 27);
            xBox.Name = "xBox";
            xBox.Size = new Size(100, 23);
            xBox.TabIndex = 1;
            xBox.Text = "0";
            xBox.KeyDown += xBox_KeyDown;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(171, 15);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(233, 145);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(90, 166);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 5;
            button1.Text = "ScreenShot";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // txtLbl
            // 
            txtLbl.AutoSize = true;
            txtLbl.Location = new Point(171, 166);
            txtLbl.Name = "txtLbl";
            txtLbl.Size = new Size(38, 15);
            txtLbl.TabIndex = 3;
            txtLbl.Text = "label5";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(419, 221);
            tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Controls.Add(txtLbl);
            tabPage1.Controls.Add(pictureBox1);
            tabPage1.Controls.Add(button1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(411, 193);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Tester";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(button2);
            tabPage2.Controls.Add(SaveBtn);
            tabPage2.Controls.Add(dataGridView1);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(411, 193);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Parameter";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(6, 167);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "Stop";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // SaveBtn
            // 
            SaveBtn.Location = new Point(330, 164);
            SaveBtn.Name = "SaveBtn";
            SaveBtn.Size = new Size(75, 23);
            SaveBtn.TabIndex = 1;
            SaveBtn.Text = "Save";
            SaveBtn.UseVisualStyleBackColor = true;
            SaveBtn.Click += SaveBtn_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(411, 163);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(appSettingView11);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(411, 193);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "User Settings";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // appSettingView11
            // 
            appSettingView11.Dock = DockStyle.Fill;
            appSettingView11.Location = new Point(3, 3);
            appSettingView11.Name = "appSettingView11";
            appSettingView11.Size = new Size(405, 187);
            appSettingView11.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(437, 240);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Watch Dog Settings";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabPage3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label4;
        private Label label3;
        private TextBox hbox1;
        private Label label2;
        private TextBox wBox;
        private Label label1;
        private TextBox yBox;
        private TextBox xBox;
        private PictureBox pictureBox1;
        private Button button1;
        private Label txtLbl;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button SaveBtn;
        private DataGridView dataGridView1;
        private Button button2;
        private TabPage tabPage3;
        private AppSetting.AppSettingView1 appSettingView11;
    }
}