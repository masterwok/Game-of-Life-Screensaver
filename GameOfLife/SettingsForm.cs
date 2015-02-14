using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace GameOfLife
{
    public partial class SettingsForm : Form
    {
        private Settings _settings { get; set; }

        public SettingsForm()
        {
            InitializeComponent();
            _settings = Settings.ReadSettings();
            PopulateControlValues();
        }

        private void PopulateControlValues()
        {
            CellSizeTextBox.Text = _settings.CellSize.ToString();
            SeedPopulationDensityTextbox.Text = _settings.SeedPopulationDensity.ToString();
            MaxCircleRadiusTextbox.Text = _settings.MaxCircleRadius.ToString();
            CircleDropThresholdTextbox.Text = _settings.CircleDropThreshold.ToString();
            RedTextbox.Text = _settings.RedValue.ToString();
            GreenTextbox.Text = _settings.GreenValue.ToString();
            BlueTextbox.Text = _settings.BlueValue.ToString();
            MaxFpsTextbox.Text = _settings.MaxFps.ToString();
            CycleColorsCheckbox.Checked = _settings.CycledColorsOn;
        }

        private void ChooseColorButton_Click(object sender, EventArgs e)
        {
            if (_colorDialog.ShowDialog() == DialogResult.OK)
            {
                var color = _colorDialog.Color;
                RedTextbox.Text = color.R.ToString();
                GreenTextbox.Text = color.G.ToString();
                BlueTextbox.Text = color.B.ToString();
            }
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void CancelButton_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_MouseClick(object sender, MouseEventArgs e)
        {
            Settings.WriteSettings(_settings);
            this.Close();
        }

        private void CellSizeTracker_ValueChanged(object sender, EventArgs e)
        {
            CellSizeTextBox.Text = CellSizeTracker.Value.ToString();
        }

        private void SeedPopulationDensityTracker_ValueChanged(object sender, EventArgs e)
        {
            SeedPopulationDensityTextbox.Text = SeedPopulationDensityTracker.Value.ToString();
        }

        private void MaxCircleRadiusTracker_ValueChanged(object sender, EventArgs e)
        {
            MaxCircleRadiusTextbox.Text = MaxCircleRadiusTracker.Value.ToString();
        }

        private void CircleDropThresholdTracker_ValueChanged(object sender, EventArgs e)
        {
            CircleDropThresholdTextbox.Text = CircleDropThresholdTracker.Value.ToString();
        }

        #region Change Events

        private void CellSizeTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Update tracker, floor any decimals entered and move cursor to end of the textbox
                int value = (int)Math.Floor(Double.Parse(CellSizeTextBox.Text));
                value = BoundValue(value, CellSizeTracker.Minimum, CellSizeTracker.Maximum);
                CellSizeTextBox.Text = value.ToString();
                CellSizeTextBox.Select(CellSizeTextBox.Text.Length, 0);
                CellSizeTracker.Value = value;
                _settings.CellSize = value;
            }
            catch (Exception ex) { }
        }

        private void SeedPopulationDensityTextbox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Update tracker, floor any decimals entered and move cursor to end of the textbox
                int value = (int)Math.Floor(Double.Parse(SeedPopulationDensityTextbox.Text));
                value = BoundValue(value, SeedPopulationDensityTracker.Minimum, SeedPopulationDensityTracker.Maximum);
                SeedPopulationDensityTextbox.Text = value.ToString();
                SeedPopulationDensityTextbox.Select(SeedPopulationDensityTextbox.Text.Length, 0);
                SeedPopulationDensityTracker.Value = value;
                _settings.SeedPopulationDensity = value;
            }
            catch (Exception ex) { }
        }

        private void MaxCircleRadiusTextbox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Update tracker, floor any decimals entered and move cursor to end of the textbox
                int value = (int)Math.Floor(Double.Parse(MaxCircleRadiusTextbox.Text));
                value = BoundValue(value, MaxCircleRadiusTracker.Minimum, MaxCircleRadiusTracker.Maximum);
                MaxCircleRadiusTextbox.Text = value.ToString();
                MaxCircleRadiusTextbox.Select(MaxCircleRadiusTextbox.Text.Length, 0);
                MaxCircleRadiusTracker.Value = value;
                _settings.MaxCircleRadius = value;
            }
            catch (Exception ex) { }
        }

        private void CircleDropThresholdTextbox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Update tracker, floor any decimals entered and move cursor to end of the textbox
                int value = (int)Math.Floor(Double.Parse(CircleDropThresholdTextbox.Text));
                value = BoundValue(value, CircleDropThresholdTracker.Minimum, CircleDropThresholdTracker.Maximum);
                CircleDropThresholdTextbox.Text = value.ToString();
                CircleDropThresholdTextbox.Select(CircleDropThresholdTextbox.Text.Length, 0);
                CircleDropThresholdTracker.Value = value;
                _settings.CircleDropThreshold = value;
            }
            catch (Exception ex) { }
        }

        private void RedTextbox_TextChanged(object sender, EventArgs e)
        {
            int value = BoundValue((int)Math.Floor(Double.Parse(RedTextbox.Text)), 0, 255);
            _settings.RedValue = value;
        }

        private void GreenTextbox_TextChanged(object sender, EventArgs e)
        {
            int value = BoundValue((int)Math.Floor(Double.Parse(GreenTextbox.Text)), 0, 255);
            _settings.GreenValue = value;
        }

        private void BlueTextbox_TextChanged(object sender, EventArgs e)
        {
            int value = BoundValue((int)Math.Floor(Double.Parse(BlueTextbox.Text)), 0, 255);
            _settings.BlueValue = value;
        }

        private void CycleColorsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            RedTextbox.Enabled = !CycleColorsCheckbox.Checked;
            GreenTextbox.Enabled = !CycleColorsCheckbox.Checked;
            BlueTextbox.Enabled = !CycleColorsCheckbox.Checked;
            ChooseColorButton.Enabled = !CycleColorsCheckbox.Checked;
            _settings.CycledColorsOn = CycleColorsCheckbox.Checked;
        }

        #endregion

        private int BoundValue(int value, int min, int max)
        {
            return value < min ? min : (value > max) ? max : value;
        }

        private void MaxFpsTextbox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _settings.MaxFps = BoundValue((int)Math.Floor(Double.Parse(MaxFpsTextbox.Text)), 1, 999);
                MaxFpsTextbox.Text = _settings.MaxFps.ToString();
                MaxFpsTextbox.Select(MaxFpsTextbox.Text.Length, 0);
            }
            catch (Exception ex) { }
        }




    }
}
