using ConsoleParser.Foo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleParser
{
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 1)]
    public struct Hdr
    {
        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(0)]
        public byte _crc;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(1)]
        public byte _len;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(2)]
        public byte _id;

        [MarshalAs(UnmanagedType.U1)]
        [FieldOffset(3)]
        public byte _dt;


        [MarshalAs(UnmanagedType.U4)]
        [FieldOffset(4)]
        public uint _time_stamp;

        public String CLK
        {
            get
            {                
                return _time_stamp.ToString();
            }

        }

    }

    


    class Program
    {
        static void Main(string[] args)
        {
            test();
            binarytest();
        }

        private static void test()
        {
            byte[] cc = new byte[]
           {
                0xf4, 0x0e, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x10, 0x10, 0x03, 0x07, 0x00
           };
            Parser<RECDataStruct> parser = new Parser<RECDataStruct>();
            RECDataStruct data = parser.getT(cc);

            Console.WriteLine(7 == data._interval);

        }

        private static void binarytest()
        {
            byte[] cc = new byte[]
            {
                0xf4, 0x0e, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x10, 0x10, 0x03, 0x07, 0x00
            };

            string node = "Foo";
            IBinaryParser binaryParser = null;
            foreach (Assembly currentassembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type t = currentassembly.GetType("ConsoleParser." + node + "." + node + "BinaryParser", false, true);
                if (t != null)
                {
                    binaryParser = (IBinaryParser)Activator.CreateInstance(t);
                    break;
                }
            }
            Console.WriteLine(binaryParser != null);
            var text = binaryParser.Process(5, cc);
            Console.WriteLine(text != "");
        }
    }
}
