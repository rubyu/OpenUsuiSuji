using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InputWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                return;
            }
            
            String name = args[0];

            NamedPipeServerStream stream = new NamedPipeServerStream(name, PipeDirection.InOut, 1);
            stream.WaitForConnection();
            while (true)
            {
                long t0 = DateTime.Now.Ticks;
                byte[] bytes = BitConverter.GetBytes(t0);
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
                stream.WaitForPipeDrain();

                Debug.Assert(stream.ReadByte() == 79);
                Debug.Assert(stream.ReadByte() == 75);

                //Thread.Sleep(1000);
            }
        }
    }
}
