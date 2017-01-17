namespace BPA__Game
{
    partial class TutorialHelp
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
            this.Next_btn = new System.Windows.Forms.Button();
            this.HelpTips = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Next_btn
            // 
            this.Next_btn.Location = new System.Drawing.Point(94, 216);
            this.Next_btn.Name = "Next_btn";
            this.Next_btn.Size = new System.Drawing.Size(75, 23);
            this.Next_btn.TabIndex = 0;
            this.Next_btn.Text = "Next";
            this.Next_btn.UseVisualStyleBackColor = true;
            this.Next_btn.Click += new System.EventHandler(this.Next_btn_Click);
            // 
            // HelpTips
            // 
            this.HelpTips.AutoSize = true;
            this.HelpTips.Location = new System.Drawing.Point(91, 68);
            this.HelpTips.Name = "HelpTips";
            this.HelpTips.Size = new System.Drawing.Size(35, 13);
            this.HelpTips.TabIndex = 1;
            this.HelpTips.Text = "label1";
            // 
            // TutorialHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.HelpTips);
            this.Controls.Add(this.Next_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TutorialHelp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TutorialHelp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Next_btn;
        private System.Windows.Forms.Label HelpTips;
    }
}