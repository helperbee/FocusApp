using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus
{
    public class Info
    {
        private ProcessInfo _to;
        private ProcessInfo _from;  
        public Info(ProcessInfo from, ProcessInfo to) {
            this._to = to;
            this._from = from;
        }
        public ProcessInfo To { get => _to; }
        public ProcessInfo From { get => _from; }
    }
}
