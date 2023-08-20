using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Focus
{
    public class Session
    {
        private AutoResetEvent _countdown;
        private string sessionName;
        private double sessionMinutes;
        private DateTime _end;//end timestamp
        private List<Target> Target_target; //use a dictionary here - this is the list of accepted windows during a session
        private List<Target> Attempted; //This is the list of windows that were tabbed into during a session but rejected due to them not being supported.
        public Session(List<Target> target, double sessionMinutes, string sessionName) {
            this.sessionMinutes = sessionMinutes;
            this._end = DateTime.Now.AddMinutes(sessionMinutes);
            this.Attempted = new List<Target>();
            this.Target_target = target;
            this.sessionName = sessionName;
            this._countdown = new AutoResetEvent(false);
            System.Threading.Timer timer = new System.Threading.Timer(_ =>
            {
                Debug.WriteLine($"SESSION FINISHED - {sessionName}");
                this.Ender();
                this._countdown.Set();
            }, null, TimeSpan.FromMinutes(sessionMinutes), Timeout.InfiniteTimeSpan);
        }
        private void Ender()
        {
            Program.sessionStorage.Add(Program.session);
            Program.session = null;
        }
        public void EndEarly() { this.Ender(); this._countdown.Set(); }
        public string Name { get => this.sessionName; }
        public string Minutes { get => this.sessionMinutes.ToString(); }//Change later
        public List<Target> AttemptedList { get => this.Attempted; set => this.Attempted = value; }
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
