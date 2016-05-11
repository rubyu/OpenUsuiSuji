using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenUsuiSuji
{
    delegate void ClientCallback(long t1, long t2);

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            T1Value.Paint += new PaintEventHandler(UpdateT1);
            T2Value.Paint += new PaintEventHandler(UpdateT2);

            if (System.Environment.Is64BitOperatingSystem)
            {
                this.Text = "x64";
            }
            else
            {
                this.Text = "x86";
            }

            watchers = watcherNames.ToDictionary(
                key => key, 
                key => new InputWatcher(key, GetWatcherPipeName(name, version, key)));

            foreach (var name in watchers.Keys)
            {
                watchers[name].Start();
            }
            
            clients = watcherNames.ToDictionary(
                key => key,
                key => new Client(GetWatcherPipeName(name, version, key), UpdateDisplay));

            foreach (var name in clients.Keys)
            {
                clients[name].Start();
            }
        }

        private const String name = "OpenUsuiSuji";
        private const String version = "0.00";

        private String GetWatcherPipeName(String name, String version, String postfix)
        {
            return string.Format("{0}-{1}-{2}", name, version, postfix);
        }

        private static readonly List<String> watcherNames = 
            new List<String> {
                "InputWatcher32.exe",
                "InputWatcher64.exe"
            };

        private Dictionary<String, InputWatcher> watchers = null;
        private Dictionary<String, Client> clients = null;


        private long t1 = 0;
        private long t2 = 0;

        public void UpdateDisplay(long t1, long t2)
        {
            if (T1Value.InvokeRequired)
            {
                ClientCallback f = new ClientCallback(UpdateDisplay);
                this.Invoke(f, new object[] { t1, t2 });
            } 
            else
            {
                this.t1 = t1;
                this.t2 = t2;
                T1Value.Refresh();
                T2Value.Refresh();
            }
        }

        public void UpdateT1(object sender, PaintEventArgs args)
        {
            T1Value.Text = t1.ToString();
        }

        public void UpdateT2(object sender, PaintEventArgs args)
        {
            T2Value.Text = t2.ToString();
        }
    }

    class Client: IDisposable
    {
        private Thread thread = null;
        private NamedPipeClientStream stream = null;
        private String pipeName = null;
        private ClientCallback callback = null;

        private static readonly byte[] OK = { 0x4F, 0x4B };

        public Client(String pipeName, ClientCallback callback)
        {
            this.pipeName = pipeName;
            this.callback = callback;
        }

        public void Start()
        {
            thread = new Thread(Watch);
            thread.Start();
        }
        public void Dispose()
        {
            if (thread != null)
            {
                thread.Abort();
            }
            
            try
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            catch
            {

            }
        }

        private void Watch()
        {
            stream = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut);
            stream.Connect();
            while (true)
            {
                byte[] buf = new byte[8];
                long t1 = DateTime.Now.Ticks;
                stream.Read(buf, 0, 8);
                long t2 = DateTime.Now.Ticks;
                long t0 = BitConverter.ToInt64(buf, 0);

                Debug.Print("received: {0}", BitConverter.ToString(buf));
                Debug.Print("t0: {0}", t0);
                Debug.Print("t1: {0}", t1);
                Debug.Print("t2: {0}", t2);

                //callback(t1, t2);
                callback(t1 - t0, t2 - t0);

                stream.WriteByte(79);
                stream.WriteByte(75);
                stream.Flush();
                stream.WaitForPipeDrain();

                //Thread.Sleep(1000);
            }
        }
    }

    class InputWatcher: IDisposable
    {
        private Thread thread = null;
        private Process process = null;
        private String pipeName = null;
        private String name = null;
        
        public InputWatcher(String name, String pipeName)
        {
            this.name = name;
            this.pipeName = pipeName;
        }

        public void Start()
        {
            thread = new Thread(Watch);
            thread.Start();
        }

        public void Dispose()
        {
            if (thread != null)
            {
                thread.Abort();
            }
            try
            {
                if (process != null)
                {
                    process.Kill();
                }
            }
            catch
            {

            }
        }

        private void Watch()
        {
            while (true)
            {
                ProcessStartInfo info = new ProcessStartInfo()
                {
                    FileName = name,
                    Arguments = pipeName,
                    CreateNoWindow = true,
                    UseShellExecute = false
                };
                process = Process.Start(info);
                process.WaitForExit();

                Thread.Sleep(1000);
            }
        }
    }
}
