using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MobiFlight.UI.Panels.Settings.Device
{
    public partial class MFSPIOutputPanel : UserControl
    {
        /// <summary>
        /// Gets raised whenever config object has changed
        /// </summary>
        public event EventHandler Changed;

        private MobiFlight.Config.SPIOutput spioutput;
        bool initialized = false;

        public MFSPIOutputPanel()
        {
            InitializeComponent();
            mfPinComboBox.Items.Clear();
        }

        public MFSPIOutputPanel(MobiFlight.Config.SPIOutput spioutput, List<MobiFlightPin> Pins)
            : this()
        {
            ComboBoxHelper.BindMobiFlightFreePins(mfPinComboBox, Pins, spioutput.SlaveSelectPin);

            if (mfPinComboBox.Items.Count > 0)
            {
                mfPinComboBox.SelectedIndex = 0;
            }
            // TODO: Complete member initialization
            this.spioutput = spioutput;
            mfPinComboBox.SelectedValue = byte.Parse(spioutput.SlaveSelectPin);
            textBox1.Text = spioutput.Name;
            setValues();

            initialized = true;
        }

        private void value_Changed(object sender, EventArgs e)
        {
            if (!initialized) return;

            setValues();

            if (Changed!=null)
                Changed(spioutput, new EventArgs());
        }

        private void setValues()
        {
            spioutput.SlaveSelectPin = mfPinComboBox.SelectedItem.ToString();
            spioutput.Name = textBox1.Text;
        }
    }
}
