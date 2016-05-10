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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            if (System.Environment.Is64BitOperatingSystem)
            {
                this.Text = "x64";
            }
            else
            {
                this.Text = "x86";
            }

            watchers = watcherNames.ToDictionary(name => name, name => new InputWatcher(name, pipeName));
            foreach (var name in watchers.Keys)
            {
                watchers[name].Start();
            }   
        }

        private const String name = "OpenUsuiSuji";
        private const String version = "0.00";

        private static readonly String pipeName = string.Format("{0}-{1}", name, version);
        private NamedPipeServerStream pipe = null;


        private void ClosePipe()
        {
            pipe.Close();    
        }

        private static readonly List<String> watcherNames = 
            new List<String> {
                "InputWatcher32.exe",
                "InputWatcher64.exe"
            };

        private Dictionary<String, InputWatcher> watchers = null;
    }

    class InputWatcher: IDisposable
    {
        private Thread thread = null;
        private Process process = null;
        private String pipeName = null;
        private String name = null;

        public InputWatcher(String watcherName, String pipeName)
        {
            this.name = watcherName;
            this.pipeName = pipeName;
        }

        public void Start()
        {
            thread = new Thread(Watch);
            thread.Start();
        }

        public void Dispose()
        {
            thread.Abort();
            try
            {
                process.Kill();
            }
            catch
            {

            }
        }

        private void Watch()
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
        }
    }
}
