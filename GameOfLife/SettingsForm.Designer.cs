namespace GameOfLife
{
    partial class SettingsForm
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
            this.CellSizeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CellSizeTracker = new System.Windows.Forms.TrackBar();
            this.SeedPopulationDensityTracker = new System.Windows.Forms.TrackBar();
            this.MaxCircleRadiusTracker = new System.Windows.Forms.TrackBar();
            this.MaxFpsTextbox = new System.Windows.Forms.TextBox();
            this.CircleDropThresholdTracker = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BlueTextbox = new System.Windows.Forms.TextBox();
            this.GreenTextbox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.RedTextbox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.CycleColorsCheckbox = new System.Windows.Forms.CheckBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.CellSizeTextBox = new System.Windows.Forms.TextBox();
            this.SeedPopulationDensityTextbox = new System.Windows.Forms.TextBox();
            this.MaxCircleRadiusTextbox = new System.Windows.Forms.TextBox();
            this.CircleDropThresholdTextbox = new System.Windows.Forms.TextBox();
            this._colorDialog = new System.Windows.Forms.ColorDialog();
            this.ChooseColorButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CellSizeTracker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SeedPopulationDensityTracker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxCircleRadiusTracker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CircleDropThresholdTracker)).BeginInit();
            this.SuspendLayout();
            // 
            // CellSizeLabel
            // 
            this.CellSizeLabel.AutoSize = true;
            this.CellSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CellSizeLabel.Location = new System.Drawing.Point(16, 18);
            this.CellSizeLabel.Name = "CellSizeLabel";
            this.CellSizeLabel.Size = new System.Drawing.Size(79, 20);
            this.CellSizeLabel.TabIndex = 0;
            this.CellSizeLabel.Text = "Cell size:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(527, 265);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Max FPS:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Max circle radius:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Seed population density:";
            // 
            // CellSizeTracker
            // 
            this.CellSizeTracker.Location = new System.Drawing.Point(224, 18);
            this.CellSizeTracker.Maximum = 100;
            this.CellSizeTracker.Name = "CellSizeTracker";
            this.CellSizeTracker.Size = new System.Drawing.Size(385, 56);
            this.CellSizeTracker.TabIndex = 5;
            this.CellSizeTracker.ValueChanged += new System.EventHandler(this.CellSizeTracker_ValueChanged);
            // 
            // SeedPopulationDensityTracker
            // 
            this.SeedPopulationDensityTracker.Location = new System.Drawing.Point(224, 80);
            this.SeedPopulationDensityTracker.Maximum = 100;
            this.SeedPopulationDensityTracker.Name = "SeedPopulationDensityTracker";
            this.SeedPopulationDensityTracker.Size = new System.Drawing.Size(385, 56);
            this.SeedPopulationDensityTracker.TabIndex = 6;
            this.SeedPopulationDensityTracker.ValueChanged += new System.EventHandler(this.SeedPopulationDensityTracker_ValueChanged);
            // 
            // MaxCircleRadiusTracker
            // 
            this.MaxCircleRadiusTracker.Location = new System.Drawing.Point(224, 142);
            this.MaxCircleRadiusTracker.Maximum = 100;
            this.MaxCircleRadiusTracker.Name = "MaxCircleRadiusTracker";
            this.MaxCircleRadiusTracker.Size = new System.Drawing.Size(385, 56);
            this.MaxCircleRadiusTracker.TabIndex = 7;
            this.MaxCircleRadiusTracker.ValueChanged += new System.EventHandler(this.MaxCircleRadiusTracker_ValueChanged);
            // 
            // MaxFpsTextbox
            // 
            this.MaxFpsTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxFpsTextbox.Location = new System.Drawing.Point(615, 262);
            this.MaxFpsTextbox.Name = "MaxFpsTextbox";
            this.MaxFpsTextbox.Size = new System.Drawing.Size(78, 27);
            this.MaxFpsTextbox.TabIndex = 8;
            this.MaxFpsTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MaxFpsTextbox.TextChanged += new System.EventHandler(this.MaxFpsTextbox_TextChanged);
            // 
            // CircleDropThresholdTracker
            // 
            this.CircleDropThresholdTracker.Location = new System.Drawing.Point(224, 204);
            this.CircleDropThresholdTracker.Maximum = 100;
            this.CircleDropThresholdTracker.Name = "CircleDropThresholdTracker";
            this.CircleDropThresholdTracker.Size = new System.Drawing.Size(385, 56);
            this.CircleDropThresholdTracker.TabIndex = 10;
            this.CircleDropThresholdTracker.ValueChanged += new System.EventHandler(this.CircleDropThresholdTracker_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(179, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Circle drop threshhold:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(193, 266);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "B:";
            // 
            // BlueTextbox
            // 
            this.BlueTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BlueTextbox.Location = new System.Drawing.Point(225, 263);
            this.BlueTextbox.Name = "BlueTextbox";
            this.BlueTextbox.Size = new System.Drawing.Size(53, 27);
            this.BlueTextbox.TabIndex = 16;
            this.BlueTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.BlueTextbox.TextChanged += new System.EventHandler(this.BlueTextbox_TextChanged);
            // 
            // GreenTextbox
            // 
            this.GreenTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GreenTextbox.Location = new System.Drawing.Point(134, 263);
            this.GreenTextbox.Name = "GreenTextbox";
            this.GreenTextbox.Size = new System.Drawing.Size(53, 27);
            this.GreenTextbox.TabIndex = 18;
            this.GreenTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GreenTextbox.TextChanged += new System.EventHandler(this.GreenTextbox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(101, 266);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 20);
            this.label7.TabIndex = 17;
            this.label7.Text = "G:";
            // 
            // RedTextbox
            // 
            this.RedTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RedTextbox.Location = new System.Drawing.Point(42, 263);
            this.RedTextbox.Name = "RedTextbox";
            this.RedTextbox.Size = new System.Drawing.Size(53, 27);
            this.RedTextbox.TabIndex = 20;
            this.RedTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RedTextbox.TextChanged += new System.EventHandler(this.RedTextbox_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(10, 266);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 20);
            this.label8.TabIndex = 19;
            this.label8.Text = "R:";
            // 
            // CycleColorsCheckbox
            // 
            this.CycleColorsCheckbox.AutoSize = true;
            this.CycleColorsCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CycleColorsCheckbox.Location = new System.Drawing.Point(12, 314);
            this.CycleColorsCheckbox.Name = "CycleColorsCheckbox";
            this.CycleColorsCheckbox.Size = new System.Drawing.Size(127, 24);
            this.CycleColorsCheckbox.TabIndex = 21;
            this.CycleColorsCheckbox.Text = "Cycle Colors";
            this.CycleColorsCheckbox.UseVisualStyleBackColor = true;
            this.CycleColorsCheckbox.CheckedChanged += new System.EventHandler(this.CycleColorsCheckbox_CheckedChanged);
            // 
            // SaveButton
            // 
            this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.Location = new System.Drawing.Point(617, 314);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(91, 33);
            this.SaveButton.TabIndex = 22;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SaveButton_MouseClick);
            // 
            // CancelButton
            // 
            this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.Location = new System.Drawing.Point(506, 314);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(91, 33);
            this.CancelButton.TabIndex = 23;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CancelButton_MouseClick);
            // 
            // CellSizeTextBox
            // 
            this.CellSizeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CellSizeTextBox.Location = new System.Drawing.Point(615, 15);
            this.CellSizeTextBox.Name = "CellSizeTextBox";
            this.CellSizeTextBox.Size = new System.Drawing.Size(78, 27);
            this.CellSizeTextBox.TabIndex = 24;
            this.CellSizeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CellSizeTextBox.TextChanged += new System.EventHandler(this.CellSizeTextBox_TextChanged);
            // 
            // SeedPopulationDensityTextbox
            // 
            this.SeedPopulationDensityTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SeedPopulationDensityTextbox.Location = new System.Drawing.Point(615, 77);
            this.SeedPopulationDensityTextbox.Name = "SeedPopulationDensityTextbox";
            this.SeedPopulationDensityTextbox.Size = new System.Drawing.Size(78, 27);
            this.SeedPopulationDensityTextbox.TabIndex = 25;
            this.SeedPopulationDensityTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SeedPopulationDensityTextbox.TextChanged += new System.EventHandler(this.SeedPopulationDensityTextbox_TextChanged);
            // 
            // MaxCircleRadiusTextbox
            // 
            this.MaxCircleRadiusTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxCircleRadiusTextbox.Location = new System.Drawing.Point(615, 139);
            this.MaxCircleRadiusTextbox.Name = "MaxCircleRadiusTextbox";
            this.MaxCircleRadiusTextbox.Size = new System.Drawing.Size(78, 27);
            this.MaxCircleRadiusTextbox.TabIndex = 26;
            this.MaxCircleRadiusTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MaxCircleRadiusTextbox.TextChanged += new System.EventHandler(this.MaxCircleRadiusTextbox_TextChanged);
            // 
            // CircleDropThresholdTextbox
            // 
            this.CircleDropThresholdTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CircleDropThresholdTextbox.Location = new System.Drawing.Point(615, 201);
            this.CircleDropThresholdTextbox.Name = "CircleDropThresholdTextbox";
            this.CircleDropThresholdTextbox.Size = new System.Drawing.Size(78, 27);
            this.CircleDropThresholdTextbox.TabIndex = 27;
            this.CircleDropThresholdTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CircleDropThresholdTextbox.TextChanged += new System.EventHandler(this.CircleDropThresholdTextbox_TextChanged);
            // 
            // ChooseColorButton
            // 
            this.ChooseColorButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChooseColorButton.Location = new System.Drawing.Point(293, 260);
            this.ChooseColorButton.Name = "ChooseColorButton";
            this.ChooseColorButton.Size = new System.Drawing.Size(158, 33);
            this.ChooseColorButton.TabIndex = 28;
            this.ChooseColorButton.Text = "Choose Color";
            this.ChooseColorButton.UseVisualStyleBackColor = true;
            this.ChooseColorButton.Click += new System.EventHandler(this.ChooseColorButton_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 359);
            this.Controls.Add(this.ChooseColorButton);
            this.Controls.Add(this.CircleDropThresholdTextbox);
            this.Controls.Add(this.MaxCircleRadiusTextbox);
            this.Controls.Add(this.SeedPopulationDensityTextbox);
            this.Controls.Add(this.CellSizeTextBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.CycleColorsCheckbox);
            this.Controls.Add(this.RedTextbox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.GreenTextbox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.BlueTextbox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CircleDropThresholdTracker);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.MaxFpsTextbox);
            this.Controls.Add(this.MaxCircleRadiusTracker);
            this.Controls.Add(this.SeedPopulationDensityTracker);
            this.Controls.Add(this.CellSizeTracker);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CellSizeLabel);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.CellSizeTracker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SeedPopulationDensityTracker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxCircleRadiusTracker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CircleDropThresholdTracker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CellSizeLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar CellSizeTracker;
        private System.Windows.Forms.TrackBar SeedPopulationDensityTracker;
        private System.Windows.Forms.TrackBar MaxCircleRadiusTracker;
        private System.Windows.Forms.TextBox MaxFpsTextbox;
        private System.Windows.Forms.TrackBar CircleDropThresholdTracker;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox BlueTextbox;
        private System.Windows.Forms.TextBox GreenTextbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox RedTextbox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox CycleColorsCheckbox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.TextBox CellSizeTextBox;
        private System.Windows.Forms.TextBox SeedPopulationDensityTextbox;
        private System.Windows.Forms.TextBox MaxCircleRadiusTextbox;
        private System.Windows.Forms.TextBox CircleDropThresholdTextbox;
        private System.Windows.Forms.ColorDialog _colorDialog;
        private System.Windows.Forms.Button ChooseColorButton;
    }
}