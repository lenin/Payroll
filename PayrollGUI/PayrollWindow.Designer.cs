namespace PayrollGUI
{
    partial class PayrollWindow
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
            this.transactionsTextBox = new System.Windows.Forms.TextBox();
            this.employeesTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addEmployeeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.runButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // transactionsTextBox
            // 
            this.transactionsTextBox.Location = new System.Drawing.Point(12, 50);
            this.transactionsTextBox.Multiline = true;
            this.transactionsTextBox.Name = "transactionsTextBox";
            this.transactionsTextBox.Size = new System.Drawing.Size(483, 129);
            this.transactionsTextBox.TabIndex = 0;
            // 
            // employeesTextBox
            // 
            this.employeesTextBox.Location = new System.Drawing.Point(12, 222);
            this.employeesTextBox.Multiline = true;
            this.employeesTextBox.Name = "employeesTextBox";
            this.employeesTextBox.Size = new System.Drawing.Size(483, 129);
            this.employeesTextBox.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(507, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // actionToolStripMenuItem
            // 
            this.actionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEmployeeMenuItem});
            this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
            this.actionToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.actionToolStripMenuItem.Text = "&Action";
            // 
            // addEmployeeMenuItem
            // 
            this.addEmployeeMenuItem.Name = "addEmployeeMenuItem";
            this.addEmployeeMenuItem.Size = new System.Drawing.Size(153, 22);
            this.addEmployeeMenuItem.Text = "&Add employee";
            this.addEmployeeMenuItem.Click += new System.EventHandler(this.addEmployeeMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pending transactions";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Employees";
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(384, 185);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(111, 23);
            this.runButton.TabIndex = 3;
            this.runButton.Text = "&Run transactions";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // PayrollWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 365);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.employeesTextBox);
            this.Controls.Add(this.transactionsTextBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PayrollWindow";
            this.Text = "PayrollWindow";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox transactionsTextBox;
        public System.Windows.Forms.TextBox employeesTextBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem addEmployeeMenuItem;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button runButton;
    }
}