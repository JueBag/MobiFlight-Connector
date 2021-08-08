using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MobiFlight.Config
{
    public class SPIOutput : BaseDevice
    {
        const ushort _paramCount = 2;
        
        [XmlAttribute]
        public String SlaveSelectPin = "53";

        public SPIOutput() { Name = "SPIOutput"; _type = DeviceType.SPIOutput; }

        override public String ToInternal()
        {
            return base.ToInternal() + Separator
                 + SlaveSelectPin + Separator
                 + Name + End;
        }

        override public bool FromInternal(String value)
        {
            if (value.Length == value.IndexOf(End) + 1) value = value.Substring(0, value.Length - 1);
            String[] paramList = value.Split(Separator);
            if (paramList.Count() != _paramCount + 1)
            {
                throw new ArgumentException("Param count does not match. " + paramList.Count() + " given, " + _paramCount + " expected");
            }

            SlaveSelectPin = paramList[1];
            Name = paramList[2];
            
            return true;
        }
        public override bool Equals(object obj)
        {
            SPIOutput other = obj as SPIOutput;
            if (other == null)
            {
                return false;
            }

            return this.Name == other.Name
                && this.SlaveSelectPin == other.SlaveSelectPin
                && this.Type == other.Type;
        }

        public override string ToString()
        {
            return Type + ":" + Name + " SlaveSelectPin:" + SlaveSelectPin;
        }
    }
}
