using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus
{
    public class Session
    {
        private DateTime _end;
        private Target Target_target;
        public Session(Target target, double sessionMinutes) {
            this._end = DateTime.Now.AddMinutes(sessionMinutes);
            this.Target_target = target;
        }

        public Target TargetInfo { get => Target_target; }
        public bool IsSessionFinished()
        {
            return DateTime.Now > this._end;
        }
    }
}
