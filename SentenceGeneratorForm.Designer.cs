namespace LewisMoten.Sentencesmith
{
    partial class SentenceGeneratorForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SentenceGeneratorForm));
            this.createButton = new System.Windows.Forms.Button();
            this.BookListBox = new System.Windows.Forms.ListBox();
            this.lengthLabel = new System.Windows.Forms.Label();
            this.lengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.rareCheckBox = new System.Windows.Forms.CheckBox();
            this.BookCountLabel = new System.Windows.Forms.Label();
            this.BookCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.cancelButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.copyButton = new System.Windows.Forms.Button();
            this.taskTimer = new System.Windows.Forms.Timer(this.components);
            this.readBookButton = new System.Windows.Forms.Button();
            this.bookComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.lengthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookCountNumericUpDown)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // createButton
            // 
            resources.ApplyResources(this.createButton, "createButton");
            this.createButton.Name = "createButton";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // BookListBox
            // 
            resources.ApplyResources(this.BookListBox, "BookListBox");
            this.BookListBox.FormattingEnabled = true;
            this.BookListBox.Name = "BookListBox";
            // 
            // lengthLabel
            // 
            resources.ApplyResources(this.lengthLabel, "lengthLabel");
            this.lengthLabel.Name = "lengthLabel";
            // 
            // lengthNumericUpDown
            // 
            resources.ApplyResources(this.lengthNumericUpDown, "lengthNumericUpDown");
            this.lengthNumericUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.lengthNumericUpDown.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.lengthNumericUpDown.Name = "lengthNumericUpDown";
            this.lengthNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // rareCheckBox
            // 
            resources.ApplyResources(this.rareCheckBox, "rareCheckBox");
            this.rareCheckBox.Name = "rareCheckBox";
            this.rareCheckBox.UseVisualStyleBackColor = true;
            // 
            // BookCountLabel
            // 
            resources.ApplyResources(this.BookCountLabel, "BookCountLabel");
            this.BookCountLabel.Name = "BookCountLabel";
            // 
            // BookCountNumericUpDown
            // 
            resources.ApplyResources(this.BookCountNumericUpDown, "BookCountNumericUpDown");
            this.BookCountNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.BookCountNumericUpDown.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.BookCountNumericUpDown.Name = "BookCountNumericUpDown";
            this.BookCountNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // statusStrip
            // 
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.progressBar,
            this.cancelButton});
            this.statusStrip.Name = "statusStrip";
            // 
            // statusLabel
            // 
            resources.ApplyResources(this.statusLabel, "statusLabel");
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Spring = true;
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            // 
            // cancelButton
            // 
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.ShowDropDownArrow = false;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // copyButton
            // 
            resources.ApplyResources(this.copyButton, "copyButton");
            this.copyButton.Name = "copyButton";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // taskTimer
            // 
            this.taskTimer.Interval = 1000;
            this.taskTimer.Tick += new System.EventHandler(this.TaskTimer_Tick);
            // 
            // readBookButton
            // 
            resources.ApplyResources(this.readBookButton, "readBookButton");
            this.readBookButton.Name = "readBookButton";
            this.readBookButton.UseVisualStyleBackColor = true;
            this.readBookButton.Click += new System.EventHandler(this.readBookButton_Click);
            // 
            // bookComboBox
            // 
            resources.ApplyResources(this.bookComboBox, "bookComboBox");
            this.bookComboBox.FormattingEnabled = true;
            this.bookComboBox.Name = "bookComboBox";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // SentenceGeneratorForm
            // 
            this.AcceptButton = this.createButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bookComboBox);
            this.Controls.Add(this.readBookButton);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.BookCountNumericUpDown);
            this.Controls.Add(this.BookCountLabel);
            this.Controls.Add(this.rareCheckBox);
            this.Controls.Add(this.lengthNumericUpDown);
            this.Controls.Add(this.lengthLabel);
            this.Controls.Add(this.BookListBox);
            this.Controls.Add(this.createButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SentenceGeneratorForm";
            this.Shown += new System.EventHandler(this.BookGeneratorForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.lengthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookCountNumericUpDown)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.ListBox BookListBox;
        private System.Windows.Forms.Label lengthLabel;
        private System.Windows.Forms.NumericUpDown lengthNumericUpDown;
        private System.Windows.Forms.CheckBox rareCheckBox;
        private System.Windows.Forms.Label BookCountLabel;
        private System.Windows.Forms.NumericUpDown BookCountNumericUpDown;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.Timer taskTimer;
        private System.Windows.Forms.ToolStripDropDownButton cancelButton;
        private System.Windows.Forms.Button readBookButton;
        private System.Windows.Forms.ComboBox bookComboBox;
        private System.Windows.Forms.Label label1;
    }
}

