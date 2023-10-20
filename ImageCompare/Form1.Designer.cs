namespace ImageCompare
{
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            label1 = new Label();
            textBox1 = new TextBox();
            lblAddress = new Label();
            button2 = new Button();
            label3 = new Label();
            progressBar1 = new ProgressBar();
            lblPercent = new Label();
            btnCopy = new Button();
            btnCancel = new Button();
            button3 = new Button();
            linkLabel1 = new LinkLabel();
            button4 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.YellowGreen;
            button1.Location = new Point(565, 37);
            button1.Name = "button1";
            button1.Size = new Size(112, 31);
            button1.TabIndex = 0;
            button1.Text = "Scan";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 19);
            label1.Name = "label1";
            label1.Size = new Size(168, 15);
            label1.TabIndex = 1;
            label1.Text = "Set Directory to start scanning!";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(21, 74);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(774, 498);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.DoubleClick += textBox1_DoubleClick;
            // 
            // lblAddress
            // 
            lblAddress.BackColor = SystemColors.InactiveBorder;
            lblAddress.Location = new Point(21, 37);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(457, 31);
            lblAddress.TabIndex = 3;
            lblAddress.Text = "...";
            lblAddress.TextAlign = ContentAlignment.MiddleCenter;
            lblAddress.Click += BTNBrowse_Click;
            // 
            // button2
            // 
            button2.Location = new Point(484, 37);
            button2.Name = "button2";
            button2.Size = new Size(75, 31);
            button2.TabIndex = 4;
            button2.Text = "Browse";
            button2.UseVisualStyleBackColor = true;
            button2.Click += BTNBrowse_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(375, 19);
            label3.Name = "label3";
            label3.Size = new Size(0, 15);
            label3.TabIndex = 6;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(21, 578);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(618, 27);
            progressBar1.TabIndex = 7;
            // 
            // lblPercent
            // 
            lblPercent.AutoSize = true;
            lblPercent.Location = new Point(795, 575);
            lblPercent.Name = "lblPercent";
            lblPercent.Size = new Size(0, 15);
            lblPercent.TabIndex = 8;
            // 
            // btnCopy
            // 
            btnCopy.Location = new Point(696, 578);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(45, 27);
            btnCopy.TabIndex = 9;
            btnCopy.Text = "Copy";
            btnCopy.UseVisualStyleBackColor = true;
            btnCopy.Click += btnCopy_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.IndianRed;
            btnCancel.Enabled = false;
            btnCancel.Location = new Point(683, 37);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(112, 31);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Stop";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // button3
            // 
            button3.Location = new Point(645, 578);
            button3.Name = "button3";
            button3.Size = new Size(45, 27);
            button3.TabIndex = 11;
            button3.Text = "Sort";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(331, 618);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(98, 15);
            linkLabel1.TabIndex = 12;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "©-2023 Bigjavani";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // button4
            // 
            button4.Location = new Point(750, 578);
            button4.Name = "button4";
            button4.Size = new Size(45, 27);
            button4.TabIndex = 13;
            button4.Text = "NU";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(811, 642);
            Controls.Add(button4);
            Controls.Add(linkLabel1);
            Controls.Add(button3);
            Controls.Add(btnCancel);
            Controls.Add(btnCopy);
            Controls.Add(lblPercent);
            Controls.Add(progressBar1);
            Controls.Add(label3);
            Controls.Add(button2);
            Controls.Add(lblAddress);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Corrupt Image Finder 1.2";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private TextBox textBox1;
        private Label lblAddress;
        private Button button2;
        private Label label3;
        private ProgressBar progressBar1;
        private Label lblPercent;
        private Button btnCopy;
        private Button btnCancel;
        private Button button3;
        private LinkLabel linkLabel1;
        private Button button4;
    }
}