using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandMessenger;

namespace MobiFlight
{
    public class MobiFlightSPIOutput : IConnectedDevice
    {
        public const string TYPE = "SPIOutput";
        public const int OutputLower = 0;
        public const int OutputUpper = 255;

        private String _name = "SPIOutput";
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }


        private DeviceType _type = DeviceType.SPIOutput;
        public DeviceType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public CmdMessenger CmdMessenger { get; set; }
        public int SPIOutputNumber { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int MaxRotationPercent { get; set; }
        
        public MobiFlightSPIOutput()
        {
            Min = 0;
            Max = 255;
            MaxRotationPercent = 100;
        }

        private int map(int value)
        {
            int outputUpper = (int)Math.Round((float) OutputUpper * MaxRotationPercent / 100);
            float relVal = (value - Min) / (float)(Max - Min);
            return (int)Math.Round((relVal * (outputUpper - OutputLower)) + Min, 0);
        }

        public void MoveToPosition(int value)
        {
            int mappedValue = map(value);
            
            var command = new SendCommand((int)MobiFlightModule.Command.SetSPIOutput);
            command.AddArgument(SPIOutputNumber);
            command.AddArgument(mappedValue);
            Log.Instance.log("Command: SetSPIOutput <" + (int)MobiFlightModule.Command.SetSPIOutput + "," +
                              SPIOutputNumber + "," +
                              mappedValue + ";>", LogSeverity.Debug);
            // Send command
            CmdMessenger.SendCommand(command);
        }
    }
}
