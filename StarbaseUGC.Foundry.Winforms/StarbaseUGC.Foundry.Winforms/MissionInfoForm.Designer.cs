namespace StarbaseUGC.Foundry.Winforms
{
    partial class MissionInfoForm
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
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.AuthorTextBox = new System.Windows.Forms.TextBox();
            this.FactionTextBox = new System.Windows.Forms.TextBox();
            this.MinLevelTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DescriptionTextBox = new System.Windows.Forms.TextBox();
            this.objectiveListBox = new System.Windows.Forms.ListBox();
            this.dialogueListBox = new System.Windows.Forms.ListBox();
            this.dialogueTextBox = new System.Windows.Forms.TextBox();
            this.buttonsListBox = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(91, 25);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(100, 20);
            this.NameTextBox.TabIndex = 0;
            // 
            // AuthorTextBox
            // 
            this.AuthorTextBox.Location = new System.Drawing.Point(91, 51);
            this.AuthorTextBox.Name = "AuthorTextBox";
            this.AuthorTextBox.Size = new System.Drawing.Size(100, 20);
            this.AuthorTextBox.TabIndex = 1;
            // 
            // FactionTextBox
            // 
            this.FactionTextBox.Location = new System.Drawing.Point(502, 25);
            this.FactionTextBox.Name = "FactionTextBox";
            this.FactionTextBox.Size = new System.Drawing.Size(100, 20);
            this.FactionTextBox.TabIndex = 2;
            // 
            // MinLevelTextBox
            // 
            this.MinLevelTextBox.Location = new System.Drawing.Point(502, 51);
            this.MinLevelTextBox.Name = "MinLevelTextBox";
            this.MinLevelTextBox.Size = new System.Drawing.Size(100, 20);
            this.MinLevelTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mission Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Author";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(446, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Faction";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(446, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "MinLevel";
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.Location = new System.Drawing.Point(12, 77);
            this.DescriptionTextBox.Multiline = true;
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.Size = new System.Drawing.Size(590, 114);
            this.DescriptionTextBox.TabIndex = 8;
            // 
            // objectiveListBox
            // 
            this.objectiveListBox.FormattingEnabled = true;
            this.objectiveListBox.Location = new System.Drawing.Point(12, 210);
            this.objectiveListBox.Name = "objectiveListBox";
            this.objectiveListBox.Size = new System.Drawing.Size(292, 186);
            this.objectiveListBox.TabIndex = 9;
            // 
            // dialogueListBox
            // 
            this.dialogueListBox.FormattingEnabled = true;
            this.dialogueListBox.Location = new System.Drawing.Point(310, 210);
            this.dialogueListBox.Name = "dialogueListBox";
            this.dialogueListBox.Size = new System.Drawing.Size(292, 186);
            this.dialogueListBox.TabIndex = 10;
            this.dialogueListBox.SelectedIndexChanged += new System.EventHandler(this.dialogueListBox_SelectedIndexChanged);
            // 
            // dialogueTextBox
            // 
            this.dialogueTextBox.Location = new System.Drawing.Point(12, 415);
            this.dialogueTextBox.Multiline = true;
            this.dialogueTextBox.Name = "dialogueTextBox";
            this.dialogueTextBox.Size = new System.Drawing.Size(380, 122);
            this.dialogueTextBox.TabIndex = 11;
            // 
            // buttonsListBox
            // 
            this.buttonsListBox.FormattingEnabled = true;
            this.buttonsListBox.Location = new System.Drawing.Point(398, 415);
            this.buttonsListBox.Name = "buttonsListBox";
            this.buttonsListBox.Size = new System.Drawing.Size(204, 121);
            this.buttonsListBox.TabIndex = 12;
            this.buttonsListBox.SelectedIndexChanged += new System.EventHandler(this.buttonsListBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 194);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Objectives";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(307, 194);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Dialogues";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 399);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Dialogue";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(395, 399);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "DialogueButtons";
            // 
            // MissionInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 548);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonsListBox);
            this.Controls.Add(this.dialogueTextBox);
            this.Controls.Add(this.dialogueListBox);
            this.Controls.Add(this.objectiveListBox);
            this.Controls.Add(this.DescriptionTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MinLevelTextBox);
            this.Controls.Add(this.FactionTextBox);
            this.Controls.Add(this.AuthorTextBox);
            this.Controls.Add(this.NameTextBox);
            this.Name = "MissionInfoForm";
            this.Text = "MissionInfo";
            this.Load += new System.EventHandler(this.MissionInfoForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.TextBox AuthorTextBox;
        private System.Windows.Forms.TextBox FactionTextBox;
        private System.Windows.Forms.TextBox MinLevelTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox DescriptionTextBox;
        private System.Windows.Forms.ListBox objectiveListBox;
        private System.Windows.Forms.ListBox dialogueListBox;
        private System.Windows.Forms.TextBox dialogueTextBox;
        private System.Windows.Forms.ListBox buttonsListBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}