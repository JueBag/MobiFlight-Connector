using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MobiFlight.UI.Panels
{
    public partial class SPIOutputPanel : UserControl
    {
        public SPIOutputPanel()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public void SetSelectedAddress(string value)
        {
            spioutputAddressesComboBox.SelectedValue = value;
        }

        public void SetAdresses(List<ListItem> pins)
        {
            spioutputAddressesComboBox.DataSource = new List<ListItem>(pins);
            spioutputAddressesComboBox.DisplayMember = "Label";
            spioutputAddressesComboBox.ValueMember = "Value";

            if (pins.Count > 0)
                spioutputAddressesComboBox.SelectedIndex = 0;

            spioutputAddressesComboBox.Enabled = pins.Count > 0;            
        }

        public void syncFromConfig(OutputConfigItem config)
        {
            if (!ComboBoxHelper.SetSelectedItem(spioutputAddressesComboBox, config.SPIOutputAddress))
            {
                // TODO: provide error message
                Log.Instance.log("_syncConfigToForm : Exception on selecting item in SPI Output Address ComboBox", LogSeverity.Debug);
            }

            if (config.SPIOutputMin != null) minValueTextBox.Text = config.SPIOutputMin;
            if (config.SPIOutputMax != null) maxValueTextBox.Text = config.SPIOutputMax;
            if (config.SPIOutputMaxRotationPercent != null) maxRotationPercentNumericUpDown.Text = config.SPIOutputMaxRotationPercent;
        }

        internal OutputConfigItem syncToConfig(OutputConfigItem config)
        {
            if (spioutputAddressesComboBox.SelectedValue != null)
            {
                config.SPIOutputAddress = spioutputAddressesComboBox.SelectedValue.ToString();
                config.SPIOutputMin = minValueTextBox.Text;
                config.SPIOutputMax = maxValueTextBox.Text;
                config.SPIOutputMaxRotationPercent = maxRotationPercentNumericUpDown.Text;
            }

            return config;
        }
    }
}
