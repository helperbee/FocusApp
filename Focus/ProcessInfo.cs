using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus
{
    public class ProcessInfo
    {
        private string _processName;
        private string _windowTitle;
        private IntPtr _handle;
        private DateTime _start;
        private DateTime _end;
        public ProcessInfo(IntPtr handle) {
            this._processName = Helpers.GetProcessNameFromId(Helpers.GetProcessIdFromHandle(handle));//Fix this later.
            this._windowTitle = Helpers.GetText(handle);
            this._handle = handle;
            this._start = DateTime.Now;
        }
        public string ProcessName { get => _processName; }
        public string WindowTitle { get => _windowTitle; }
        public IntPtr Handle { get => _handle; }
        public DateTime Start { get => _start; }
        public DateTime End { get => _end; set => _end = value; }
        public TimeSpan duration { get => _end - _start; }
    }
}
