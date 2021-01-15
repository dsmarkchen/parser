using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleParser.Foo
{
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct RECDataStruct : IParsable<RECDataStruct>
    {
        [MarshalAs(UnmanagedType.Struct)]
        [FieldOffset(0)]
        public Hdr _hdr;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(8)]
        public UInt16 _v;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(10)]
        public UInt16 _i;

        [MarshalAs(UnmanagedType.U2)]
        [FieldOffset(12)]
        public UInt16 _interval;


        public String CLK
        {
            get
            {
                return _hdr.CLK;
            }
        }
        public String DATA
        {
            get
            {
                float v = _v;
                float i = _i;
                return v.ToString() + ","
                    + i.ToString() + ","
                    + _interval.ToString() + ","
                    ;
            }
        }
        public override String ToString()
        {
            return "RECConfig";
        }

    }


    public class FooBinaryParser : IBinaryParser
    {
        public String Name { get; private set; }
        public FooBinaryParser()
        {
            Name = "Foo";
        }
        public String Process(uint id, byte[] cc)
        {
            String result = "";
            if (id == 0x05)
            {
                Parser<RECDataStruct> parser = new Parser<RECDataStruct>();
                result = parser.Process(cc);
                Name = parser.Name;
            }

            return result;
        }
    }
}
