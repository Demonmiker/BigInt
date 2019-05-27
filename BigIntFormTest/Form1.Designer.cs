namespace BigIntFormTest
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tb_num2 = new System.Windows.Forms.RichTextBox();
            this.tb_num1 = new System.Windows.Forms.RichTextBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_sub = new System.Windows.Forms.Button();
            this.btn_mul = new System.Windows.Forms.Button();
            this.btn_div = new System.Windows.Forms.Button();
            this.tb_res = new System.Windows.Forms.RichTextBox();
            this.btn_test = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tb_Tests = new System.Windows.Forms.TextBox();
            this.tb_Min = new System.Windows.Forms.TextBox();
            this.tb_Max = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_gen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_num2
            // 
            this.tb_num2.Location = new System.Drawing.Point(12, 140);
            this.tb_num2.Name = "tb_num2";
            this.tb_num2.Size = new System.Drawing.Size(955, 100);
            this.tb_num2.TabIndex = 0;
            this.tb_num2.Text = "";
            // 
            // tb_num1
            // 
            this.tb_num1.Location = new System.Drawing.Point(12, 12);
            this.tb_num1.Name = "tb_num1";
            this.tb_num1.Size = new System.Drawing.Size(955, 100);
            this.tb_num1.TabIndex = 1;
            this.tb_num1.Text = "";
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(12, 256);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(33, 23);
            this.btn_add.TabIndex = 2;
            this.btn_add.Text = "+";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_sub
            // 
            this.btn_sub.Location = new System.Drawing.Point(51, 256);
            this.btn_sub.Name = "btn_sub";
            this.btn_sub.Size = new System.Drawing.Size(33, 23);
            this.btn_sub.TabIndex = 3;
            this.btn_sub.Text = "-";
            this.btn_sub.UseVisualStyleBackColor = true;
            this.btn_sub.Click += new System.EventHandler(this.btn_sub_Click);
            // 
            // btn_mul
            // 
            this.btn_mul.Location = new System.Drawing.Point(90, 256);
            this.btn_mul.Name = "btn_mul";
            this.btn_mul.Size = new System.Drawing.Size(33, 23);
            this.btn_mul.TabIndex = 4;
            this.btn_mul.Text = "*";
            this.btn_mul.UseVisualStyleBackColor = true;
            this.btn_mul.Click += new System.EventHandler(this.btn_mul_Click);
            // 
            // btn_div
            // 
            this.btn_div.Location = new System.Drawing.Point(129, 256);
            this.btn_div.Name = "btn_div";
            this.btn_div.Size = new System.Drawing.Size(33, 23);
            this.btn_div.TabIndex = 5;
            this.btn_div.Text = "/";
            this.btn_div.UseVisualStyleBackColor = true;
            this.btn_div.Click += new System.EventHandler(this.btn_div_Click);
            // 
            // tb_res
            // 
            this.tb_res.Location = new System.Drawing.Point(12, 295);
            this.tb_res.Name = "tb_res";
            this.tb_res.Size = new System.Drawing.Size(955, 100);
            this.tb_res.TabIndex = 6;
            this.tb_res.Text = "";
            // 
            // btn_test
            // 
            this.btn_test.Location = new System.Drawing.Point(330, 401);
            this.btn_test.Name = "btn_test";
            this.btn_test.Size = new System.Drawing.Size(58, 23);
            this.btn_test.TabIndex = 7;
            this.btn_test.Text = "FullTest";
            this.btn_test.UseVisualStyleBackColor = true;
            this.btn_test.Click += new System.EventHandler(this.btn_test_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(371, 269);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 9;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(394, 401);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(226, 23);
            this.progressBar1.TabIndex = 10;
            // 
            // tb_Tests
            // 
            this.tb_Tests.Location = new System.Drawing.Point(12, 401);
            this.tb_Tests.Name = "tb_Tests";
            this.tb_Tests.Size = new System.Drawing.Size(100, 20);
            this.tb_Tests.TabIndex = 11;
            this.tb_Tests.Text = "100000";
            // 
            // tb_Min
            // 
            this.tb_Min.Location = new System.Drawing.Point(118, 401);
            this.tb_Min.Name = "tb_Min";
            this.tb_Min.Size = new System.Drawing.Size(100, 20);
            this.tb_Min.TabIndex = 12;
            this.tb_Min.Text = "1";
            // 
            // tb_Max
            // 
            this.tb_Max.Location = new System.Drawing.Point(224, 401);
            this.tb_Max.Name = "tb_Max";
            this.tb_Max.Size = new System.Drawing.Size(100, 20);
            this.tb_Max.TabIndex = 13;
            this.tb_Max.Text = "800";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 428);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "label1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(129, 428);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(239, 428);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "label4";
            // 
            // btn_gen
            // 
            this.btn_gen.Location = new System.Drawing.Point(175, 427);
            this.btn_gen.Name = "btn_gen";
            this.btn_gen.Size = new System.Drawing.Size(58, 23);
            this.btn_gen.TabIndex = 17;
            this.btn_gen.Text = "Gen";
            this.btn_gen.UseVisualStyleBackColor = true;
            this.btn_gen.Click += new System.EventHandler(this.btn_gen_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(997, 450);
            this.Controls.Add(this.btn_gen);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_Max);
            this.Controls.Add(this.tb_Min);
            this.Controls.Add(this.tb_Tests);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_test);
            this.Controls.Add(this.tb_res);
            this.Controls.Add(this.btn_div);
            this.Controls.Add(this.btn_mul);
            this.Controls.Add(this.btn_sub);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.tb_num1);
            this.Controls.Add(this.tb_num2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox tb_num2;
        private System.Windows.Forms.RichTextBox tb_num1;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_sub;
        private System.Windows.Forms.Button btn_mul;
        private System.Windows.Forms.Button btn_div;
        private System.Windows.Forms.RichTextBox tb_res;
        private System.Windows.Forms.Button btn_test;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox tb_Tests;
        private System.Windows.Forms.TextBox tb_Min;
        private System.Windows.Forms.TextBox tb_Max;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_gen;
    }
}

