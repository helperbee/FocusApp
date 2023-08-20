using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus
{
    public class Target
    {
        private IntPtr windowHandle;
        private string windowTitle;
        private string processName;
        private DateTime createdAt;
        public Target(IntPtr handle, string title = null, string processName = null) {
            this.windowHandle = handle;
            this.windowTitle = (title != null ? title : Helpers.GetText(handle));
            this.processName = (processName != null ? processName : Helpers.GetProcessNameFromId(Helpers.GetProcessIdFromHandle(handle)));
            this.createdAt = DateTime.Now;
            
        }
        public string ProcessName { get => this.processName; }
        public string WindowTitle { get => this.windowTitle; }
        public DateTime At { get => createdAt; }
        public IntPtr Handle
        {
            get => windowHandle;
            set => windowHandle = value;
        }
    }
}
