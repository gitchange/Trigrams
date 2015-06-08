namespace Trigrams
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cbxSL1 = new System.Windows.Forms.ComboBox();
            this.cbxSL2 = new System.Windows.Forms.ComboBox();
            this.cbxSL3 = new System.Windows.Forms.ComboBox();
            this.cbxSL4 = new System.Windows.Forms.ComboBox();
            this.cbxSL5 = new System.Windows.Forms.ComboBox();
            this.cbxSL6 = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblDown = new System.Windows.Forms.Label();
            this.lblUp = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblDownChange = new System.Windows.Forms.Label();
            this.lblUpChange = new System.Windows.Forms.Label();
            this.lblOriginal = new System.Windows.Forms.Label();
            this.lblChange = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.cbxHour = new System.Windows.Forms.ComboBox();
            this.cbxMin = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMonth = new System.Windows.Forms.Label();
            this.lblDay = new System.Windows.Forms.Label();
            this.lblHour = new System.Windows.Forms.Label();
            this.lblEmptyDie = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(391, 145);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(195, 82);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(477, 117);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 21);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbxSL1
            // 
            this.cbxSL1.FormattingEnabled = true;
            this.cbxSL1.Items.AddRange(new object[] {
            "3.—○老陽",
            "2.--X老陰",
            "1.—少陽",
            "0.--少陰"});
            this.cbxSL1.Location = new System.Drawing.Point(10, 312);
            this.cbxSL1.Name = "cbxSL1";
            this.cbxSL1.Size = new System.Drawing.Size(104, 20);
            this.cbxSL1.TabIndex = 2;
            this.cbxSL1.Text = "0.--少陰";
            this.cbxSL1.SelectedIndexChanged += new System.EventHandler(this.cbxSL1_SelectedIndexChanged);
            // 
            // cbxSL2
            // 
            this.cbxSL2.FormattingEnabled = true;
            this.cbxSL2.Items.AddRange(new object[] {
            "3.—○老陽",
            "2.--X老陰",
            "1.—少陽",
            "0.--少陰"});
            this.cbxSL2.Location = new System.Drawing.Point(10, 281);
            this.cbxSL2.Name = "cbxSL2";
            this.cbxSL2.Size = new System.Drawing.Size(104, 20);
            this.cbxSL2.TabIndex = 3;
            this.cbxSL2.Text = "0.--少陰";
            // 
            // cbxSL3
            // 
            this.cbxSL3.FormattingEnabled = true;
            this.cbxSL3.Items.AddRange(new object[] {
            "3.—○老陽",
            "2.--X老陰",
            "1.—少陽",
            "0.--少陰"});
            this.cbxSL3.Location = new System.Drawing.Point(10, 252);
            this.cbxSL3.Name = "cbxSL3";
            this.cbxSL3.Size = new System.Drawing.Size(104, 20);
            this.cbxSL3.TabIndex = 4;
            this.cbxSL3.Text = "0.--少陰";
            // 
            // cbxSL4
            // 
            this.cbxSL4.FormattingEnabled = true;
            this.cbxSL4.Items.AddRange(new object[] {
            "3.—○老陽",
            "2.--X老陰",
            "1.—少陽",
            "0.--少陰"});
            this.cbxSL4.Location = new System.Drawing.Point(10, 222);
            this.cbxSL4.Name = "cbxSL4";
            this.cbxSL4.Size = new System.Drawing.Size(104, 20);
            this.cbxSL4.TabIndex = 5;
            this.cbxSL4.Text = "0.--少陰";
            // 
            // cbxSL5
            // 
            this.cbxSL5.FormattingEnabled = true;
            this.cbxSL5.Items.AddRange(new object[] {
            "3.—○老陽",
            "2.--X老陰",
            "1.—少陽",
            "0.--少陰"});
            this.cbxSL5.Location = new System.Drawing.Point(10, 194);
            this.cbxSL5.Name = "cbxSL5";
            this.cbxSL5.Size = new System.Drawing.Size(104, 20);
            this.cbxSL5.TabIndex = 6;
            this.cbxSL5.Text = "0.--少陰";
            // 
            // cbxSL6
            // 
            this.cbxSL6.FormattingEnabled = true;
            this.cbxSL6.Items.AddRange(new object[] {
            "3.—○老陽",
            "2.--X老陰",
            "1.—少陽",
            "0.--少陰"});
            this.cbxSL6.Location = new System.Drawing.Point(10, 163);
            this.cbxSL6.Name = "cbxSL6";
            this.cbxSL6.Size = new System.Drawing.Size(104, 20);
            this.cbxSL6.TabIndex = 7;
            this.cbxSL6.Text = "0.--少陰";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(10, 345);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(86, 22);
            this.textBox2.TabIndex = 8;
            // 
            // lblDown
            // 
            this.lblDown.AutoSize = true;
            this.lblDown.Font = new System.Drawing.Font("新細明體", 64F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblDown.Location = new System.Drawing.Point(135, 246);
            this.lblDown.Name = "lblDown";
            this.lblDown.Size = new System.Drawing.Size(123, 86);
            this.lblDown.TabIndex = 9;
            this.lblDown.Text = "☰";
            // 
            // lblUp
            // 
            this.lblUp.AutoSize = true;
            this.lblUp.Font = new System.Drawing.Font("新細明體", 64F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblUp.Location = new System.Drawing.Point(135, 159);
            this.lblUp.Name = "lblUp";
            this.lblUp.Size = new System.Drawing.Size(123, 86);
            this.lblUp.TabIndex = 9;
            this.lblUp.Text = "☰";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(10, 118);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(104, 39);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "裝卦";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblDownChange
            // 
            this.lblDownChange.AutoSize = true;
            this.lblDownChange.Font = new System.Drawing.Font("新細明體", 64F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblDownChange.Location = new System.Drawing.Point(288, 246);
            this.lblDownChange.Name = "lblDownChange";
            this.lblDownChange.Size = new System.Drawing.Size(123, 86);
            this.lblDownChange.TabIndex = 9;
            this.lblDownChange.Text = "☰";
            // 
            // lblUpChange
            // 
            this.lblUpChange.AutoSize = true;
            this.lblUpChange.Font = new System.Drawing.Font("新細明體", 64F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblUpChange.Location = new System.Drawing.Point(288, 159);
            this.lblUpChange.Name = "lblUpChange";
            this.lblUpChange.Size = new System.Drawing.Size(123, 86);
            this.lblUpChange.TabIndex = 9;
            this.lblUpChange.Text = "☰";
            // 
            // lblOriginal
            // 
            this.lblOriginal.AutoSize = true;
            this.lblOriginal.Font = new System.Drawing.Font("新細明體", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblOriginal.Location = new System.Drawing.Point(159, 123);
            this.lblOriginal.Name = "lblOriginal";
            this.lblOriginal.Size = new System.Drawing.Size(66, 27);
            this.lblOriginal.TabIndex = 11;
            this.lblOriginal.Text = "本卦";
            // 
            // lblChange
            // 
            this.lblChange.AutoSize = true;
            this.lblChange.Font = new System.Drawing.Font("新細明體", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblChange.Location = new System.Drawing.Point(310, 123);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(66, 27);
            this.lblChange.TabIndex = 11;
            this.lblChange.Text = "變卦";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(10, 373);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox3.Size = new System.Drawing.Size(561, 187);
            this.textBox3.TabIndex = 12;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker1.TabIndex = 13;
            // 
            // cbxHour
            // 
            this.cbxHour.FormattingEnabled = true;
            this.cbxHour.Location = new System.Drawing.Point(219, 13);
            this.cbxHour.Name = "cbxHour";
            this.cbxHour.Size = new System.Drawing.Size(53, 20);
            this.cbxHour.TabIndex = 14;
            // 
            // cbxMin
            // 
            this.cbxMin.FormattingEnabled = true;
            this.cbxMin.Location = new System.Drawing.Point(292, 13);
            this.cbxMin.Name = "cbxMin";
            this.cbxMin.Size = new System.Drawing.Size(50, 20);
            this.cbxMin.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(344, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "分";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(273, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "時";
            // 
            // lblYear
            // 
            this.lblYear.Location = new System.Drawing.Point(469, 13);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(17, 52);
            this.lblYear.TabIndex = 15;
            this.lblYear.Text = "年：";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(150, 116);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(235, 225);
            this.panel1.TabIndex = 16;
            // 
            // lblMonth
            // 
            this.lblMonth.Location = new System.Drawing.Point(446, 13);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(17, 52);
            this.lblMonth.TabIndex = 15;
            this.lblMonth.Text = "月：";
            // 
            // lblDay
            // 
            this.lblDay.Location = new System.Drawing.Point(423, 13);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(17, 52);
            this.lblDay.TabIndex = 15;
            this.lblDay.Text = "日：";
            // 
            // lblHour
            // 
            this.lblHour.Location = new System.Drawing.Point(400, 13);
            this.lblHour.Name = "lblHour";
            this.lblHour.Size = new System.Drawing.Size(17, 52);
            this.lblHour.TabIndex = 15;
            this.lblHour.Text = "時：";
            // 
            // lblEmptyDie
            // 
            this.lblEmptyDie.Location = new System.Drawing.Point(377, 13);
            this.lblEmptyDie.Name = "lblEmptyDie";
            this.lblEmptyDie.Size = new System.Drawing.Size(17, 67);
            this.lblEmptyDie.TabIndex = 15;
            this.lblEmptyDie.Text = "空亡：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 565);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblEmptyDie);
            this.Controls.Add(this.lblHour);
            this.Controls.Add(this.lblDay);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxMin);
            this.Controls.Add(this.cbxHour);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.lblChange);
            this.Controls.Add(this.lblOriginal);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblUpChange);
            this.Controls.Add(this.lblUp);
            this.Controls.Add(this.lblDownChange);
            this.Controls.Add(this.lblDown);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.cbxSL6);
            this.Controls.Add(this.cbxSL5);
            this.Controls.Add(this.cbxSL4);
            this.Controls.Add(this.cbxSL3);
            this.Controls.Add(this.cbxSL2);
            this.Controls.Add(this.cbxSL1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbxSL1;
        private System.Windows.Forms.ComboBox cbxSL2;
        private System.Windows.Forms.ComboBox cbxSL3;
        private System.Windows.Forms.ComboBox cbxSL4;
        private System.Windows.Forms.ComboBox cbxSL5;
        private System.Windows.Forms.ComboBox cbxSL6;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblDown;
        private System.Windows.Forms.Label lblUp;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblDownChange;
        private System.Windows.Forms.Label lblUpChange;
        private System.Windows.Forms.Label lblOriginal;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox cbxHour;
        private System.Windows.Forms.ComboBox cbxMin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Label lblDay;
        private System.Windows.Forms.Label lblHour;
        private System.Windows.Forms.Label lblEmptyDie;
    }
}

