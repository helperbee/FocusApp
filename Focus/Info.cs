using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus
{
    public class Info
    {
        private string _to;
        private string _from;  
        public Info(IntPtr from, IntPtr to) {
            this._to = Helpers.GetText(to);
            this._from = Helpers.GetText(from);
        }
        public string To
        {
            get => _to;
        }
        public string From
        {
            get => _from;
        }
    }
}
