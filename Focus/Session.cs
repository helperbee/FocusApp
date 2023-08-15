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
        private List<Target> Target_target;
        public Session(List<Target> target, double sessionMinutes) {
            this._end = DateTime.Now.AddMinutes(sessionMinutes);
            this.Target_target = target;
        }

        public List<Target> TargetList { get => Target_target; }
        public Target FindTarget(IntPtr handle)
        {
            foreach(Target target in Target_target) { 
                if (target.Handle == handle)
                    return target;
            }
            return null;
        }
        public bool IsSessionFinished()
        {
            return DateTime.Now > this._end;
        }
    }
}
