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
        public Target(IntPtr handle) {
            this.windowHandle = handle;
        }
        public string WindowTitle
        {
            get => Helpers.GetText(windowHandle);            
        }
        public IntPtr Handle
        {
            get => windowHandle;
            set => windowHandle = value;
        }
    }
}
