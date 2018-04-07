namespace NoiseRemovalTool
{
    partial class AnalisisSetupForm
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
            this.btnBegin = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.stackTypeInput = new System.Windows.Forms.ComboBox();
            this.thresholdInput = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.levelLabel = new System.Windows.Forms.Label();
            this.remainderLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.imagesInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.imageAmountLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBegin
            // 
            this.btnBegin.BackColor = System.Drawing.Color.DimGray;
            this.btnBegin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBegin.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnBegin.Location = new System.Drawing.Point(484, 164);
            this.btnBegin.Name = "btnBegin";
            this.btnBegin.Size = new System.Drawing.Size(200, 60);
            this.btnBegin.TabIndex = 2;
            this.btnBegin.Text = "Begin";
            this.btnBegin.UseVisualStyleBackColor = false;
            this.btnBegin.Click += new System.EventHandler(this.btnBegin_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DimGray;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(713, 164);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 60);
            this.button1.TabIndex = 3;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // stackTypeInput
            // 
            this.stackTypeInput.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.stackTypeInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stackTypeInput.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stackTypeInput.FormattingEnabled = true;
            this.stackTypeInput.Items.AddRange(new object[] {
            "Median",
            "Mean",
            "Advanced",
            "Random"});
            this.stackTypeInput.Location = new System.Drawing.Point(934, 57);
            this.stackTypeInput.Name = "stackTypeInput";
            this.stackTypeInput.Size = new System.Drawing.Size(163, 33);
            this.stackTypeInput.TabIndex = 17;
            // 
            // thresholdInput
            // 
            this.thresholdInput.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.thresholdInput.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thresholdInput.Location = new System.Drawing.Point(1107, 55);
            this.thresholdInput.Name = "thresholdInput";
            this.thresholdInput.Size = new System.Drawing.Size(163, 32);
            this.thresholdInput.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label9.Location = new System.Drawing.Point(1115, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(146, 29);
            this.label9.TabIndex = 14;
            this.label9.Text = "Threshold";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label8.Location = new System.Drawing.Point(935, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(160, 29);
            this.label8.TabIndex = 13;
            this.label8.Text = "Stack Type";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Location = new System.Drawing.Point(667, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(256, 29);
            this.label7.TabIndex = 12;
            this.label7.Text = "Images per Group";
            // 
            // levelLabel
            // 
            this.levelLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.levelLabel.AutoSize = true;
            this.levelLabel.Font = new System.Drawing.Font("Verdana", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelLabel.ForeColor = System.Drawing.Color.White;
            this.levelLabel.Location = new System.Drawing.Point(41, 58);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(31, 26);
            this.levelLabel.TabIndex = 11;
            this.levelLabel.Text = "#";
            // 
            // remainderLabel
            // 
            this.remainderLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.remainderLabel.AutoSize = true;
            this.remainderLabel.Font = new System.Drawing.Font("Verdana", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remainderLabel.ForeColor = System.Drawing.Color.White;
            this.remainderLabel.Location = new System.Drawing.Point(495, 58);
            this.remainderLabel.Name = "remainderLabel";
            this.remainderLabel.Size = new System.Drawing.Size(156, 26);
            this.remainderLabel.TabIndex = 10;
            this.remainderLabel.Text = "# Image(s)";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(493, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 29);
            this.label4.TabIndex = 9;
            this.label4.Text = "Remainder";
            // 
            // imagesInput
            // 
            this.imagesInput.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.imagesInput.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imagesInput.Location = new System.Drawing.Point(666, 55);
            this.imagesInput.Name = "imagesInput";
            this.imagesInput.Size = new System.Drawing.Size(259, 32);
            this.imagesInput.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(15, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 29);
            this.label3.TabIndex = 8;
            this.label3.Text = "Level";
            // 
            // imageAmountLabel
            // 
            this.imageAmountLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.imageAmountLabel.AutoSize = true;
            this.imageAmountLabel.Font = new System.Drawing.Font("Verdana", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imageAmountLabel.ForeColor = System.Drawing.Color.White;
            this.imageAmountLabel.Location = new System.Drawing.Point(144, 58);
            this.imageAmountLabel.Name = "imageAmountLabel";
            this.imageAmountLabel.Size = new System.Drawing.Size(311, 26);
            this.imageAmountLabel.TabIndex = 4;
            this.imageAmountLabel.Text = "# Group(s) of # Images";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(186, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(227, 29);
            this.label2.TabIndex = 7;
            this.label2.Text = "Image Amounts";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.29897F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.70103F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 174F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 269F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 169F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 174F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.imageAmountLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.imagesInput, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.remainderLabel, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.levelLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label9, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.thresholdInput, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.stackTypeInput, 4, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(41, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1277, 102);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // AnalisisSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1344, 249);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnBegin);
            this.Name = "AnalisisSetupForm";
            this.Text = "Analysis Setup";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBegin;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox stackTypeInput;
        private System.Windows.Forms.TextBox thresholdInput;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.Label remainderLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox imagesInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label imageAmountLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}