namespace PayrollGUI
{
    partial class AddEmployeeWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.empIdTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.hourlyRadioButton = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.hourlyRateTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.salaryTextBox = new System.Windows.Forms.TextBox();
            this.salaryRadioButton = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.commissionSalaryTextBox = new System.Windows.Forms.TextBox();
            this.commissionRadioButton = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.commissionTextBox = new System.Windows.Forms.TextBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Employee ID:";
            // 
            // empIdTextBox
            // 
            this.empIdTextBox.Location = new System.Drawing.Point(88, 12);
            this.empIdTextBox.Name = "empIdTextBox";
            this.empIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.empIdTextBox.TabIndex = 1;
            this.empIdTextBox.TextChanged += new System.EventHandler(this.empIdTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Name:";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(88, 38);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(100, 20);
            this.nameTextBox.TabIndex = 1;
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Address:";
            // 
            // addressTextBox
            // 
            this.addressTextBox.Location = new System.Drawing.Point(88, 64);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(100, 20);
            this.addressTextBox.TabIndex = 1;
            this.addressTextBox.TextChanged += new System.EventHandler(this.addressTextBox_TextChanged);
            // 
            // hourlyRadioButton
            // 
            this.hourlyRadioButton.AutoSize = true;
            this.hourlyRadioButton.Location = new System.Drawing.Point(206, 11);
            this.hourlyRadioButton.Name = "hourlyRadioButton";
            this.hourlyRadioButton.Size = new System.Drawing.Size(55, 17);
            this.hourlyRadioButton.TabIndex = 2;
            this.hourlyRadioButton.TabStop = true;
            this.hourlyRadioButton.Text = "Hourly";
            this.hourlyRadioButton.UseVisualStyleBackColor = true;
            this.hourlyRadioButton.CheckedChanged += new System.EventHandler(this.hourlyRadioButton_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(320, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Rate:";
            // 
            // hourlyRateTextBox
            // 
            this.hourlyRateTextBox.Enabled = false;
            this.hourlyRateTextBox.Location = new System.Drawing.Point(359, 10);
            this.hourlyRateTextBox.Name = "hourlyRateTextBox";
            this.hourlyRateTextBox.Size = new System.Drawing.Size(100, 20);
            this.hourlyRateTextBox.TabIndex = 1;
            this.hourlyRateTextBox.TextChanged += new System.EventHandler(this.hourlyRateTextBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(314, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Salary:";
            // 
            // salaryTextBox
            // 
            this.salaryTextBox.Enabled = false;
            this.salaryTextBox.Location = new System.Drawing.Point(359, 36);
            this.salaryTextBox.Name = "salaryTextBox";
            this.salaryTextBox.Size = new System.Drawing.Size(100, 20);
            this.salaryTextBox.TabIndex = 1;
            this.salaryTextBox.TextChanged += new System.EventHandler(this.salaryTextBox_TextChanged);
            // 
            // salaryRadioButton
            // 
            this.salaryRadioButton.AutoSize = true;
            this.salaryRadioButton.Location = new System.Drawing.Point(206, 37);
            this.salaryRadioButton.Name = "salaryRadioButton";
            this.salaryRadioButton.Size = new System.Drawing.Size(63, 17);
            this.salaryRadioButton.TabIndex = 2;
            this.salaryRadioButton.TabStop = true;
            this.salaryRadioButton.Text = "Salaried";
            this.salaryRadioButton.UseVisualStyleBackColor = true;
            this.salaryRadioButton.CheckedChanged += new System.EventHandler(this.salaryRadioButton_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(314, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Salary:";
            // 
            // commissionSalaryTextBox
            // 
            this.commissionSalaryTextBox.Enabled = false;
            this.commissionSalaryTextBox.Location = new System.Drawing.Point(359, 62);
            this.commissionSalaryTextBox.Name = "commissionSalaryTextBox";
            this.commissionSalaryTextBox.Size = new System.Drawing.Size(100, 20);
            this.commissionSalaryTextBox.TabIndex = 1;
            this.commissionSalaryTextBox.TextChanged += new System.EventHandler(this.commissionSalaryTextBox_TextChanged);
            // 
            // commissionRadioButton
            // 
            this.commissionRadioButton.AutoSize = true;
            this.commissionRadioButton.Location = new System.Drawing.Point(206, 63);
            this.commissionRadioButton.Name = "commissionRadioButton";
            this.commissionRadioButton.Size = new System.Drawing.Size(92, 17);
            this.commissionRadioButton.TabIndex = 2;
            this.commissionRadioButton.TabStop = true;
            this.commissionRadioButton.Text = "Commissioned";
            this.commissionRadioButton.UseVisualStyleBackColor = true;
            this.commissionRadioButton.CheckedChanged += new System.EventHandler(this.commissionRadioButton_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(288, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Commission:";
            // 
            // commissionTextBox
            // 
            this.commissionTextBox.Enabled = false;
            this.commissionTextBox.Location = new System.Drawing.Point(359, 88);
            this.commissionTextBox.Name = "commissionTextBox";
            this.commissionTextBox.Size = new System.Drawing.Size(100, 20);
            this.commissionTextBox.TabIndex = 1;
            this.commissionTextBox.TextChanged += new System.EventHandler(this.commissionTextBox_TextChanged);
            // 
            // submitButton
            // 
            this.submitButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.submitButton.Enabled = false;
            this.submitButton.Location = new System.Drawing.Point(384, 123);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 3;
            this.submitButton.Text = "&Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(303, 123);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AddEmployeeWindow
            // 
            this.AcceptButton = this.submitButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(472, 156);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.commissionRadioButton);
            this.Controls.Add(this.salaryRadioButton);
            this.Controls.Add(this.hourlyRadioButton);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.commissionTextBox);
            this.Controls.Add(this.commissionSalaryTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.salaryTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.hourlyRateTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.empIdTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AddEmployeeWindow";
            this.Text = "AddEmployeeWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox empIdTextBox;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox addressTextBox;
        public System.Windows.Forms.RadioButton hourlyRadioButton;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox hourlyRateTextBox;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox salaryTextBox;
        public System.Windows.Forms.RadioButton salaryRadioButton;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox commissionSalaryTextBox;
        public System.Windows.Forms.RadioButton commissionRadioButton;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox commissionTextBox;
        public System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button cancelButton;
    }
}