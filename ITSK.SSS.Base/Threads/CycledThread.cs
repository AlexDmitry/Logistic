using System;
using System.Threading;

namespace ITSK.SSS.Base.Threads
{
    public delegate void CycledThreadDelegate();

    /// <summary>
    /// Данный класс является обёрткой над потоком, он позволяет выполнять циклическую операцию до тех пор пока не будет остановлен
    /// </summary>
    public class CycledThread
    {
        private readonly CycledThreadDelegate _del;
        private readonly ManualResetEvent _threadSync;
        private Thread _thread;
        private Boolean _shouldStop;
        private Boolean _shouldPause;
        private readonly TimeSpan _waitTimeout;

        public CycledThread(CycledThreadDelegate del, TimeSpan waitTimeout)
        {
            _del = del;
            _waitTimeout = waitTimeout;
            _thread = new Thread(DoWork);
            _thread.IsBackground = true;
            _threadSync = new ManualResetEvent(false);
            _state = ThreadState.Unstarted;
        }

        /// <summary>
        /// Состояние циклического потока
        /// </summary>
        public ThreadState State
        {
            get { return _state; }
        }

        private ThreadState _state;

        private void DoWork()
        {
            _state = ThreadState.Running;
            while (!_shouldStop)
            {
                _del.Invoke();

                if (_shouldPause)
                {
                    _state = ThreadState.Suspended;
                    _threadSync.WaitOne();
                    _threadSync.Reset();
                    _state = ThreadState.Running;
                }
                else _threadSync.WaitOne(_waitTimeout);
            }
            _state = ThreadState.Stopped;
        }

        public void Start()
        {
            lock (this)
            {
                switch (_state)
                {
                    case ThreadState.Unstarted:
                        _thread.Start();
                        _threadSync.Reset();
                        break;
                    case ThreadState.Suspended:
                        _shouldPause = false;
                        _threadSync.Set();
                        break;
                    case ThreadState.Stopped:
                        _state = ThreadState.Unstarted;
                        _thread = new Thread(DoWork);
                        Start();
                        break;
                    case ThreadState.Running:
                        return;
                    default:
                        throw new ArgumentException();
                }
            }
        }

        public void Stop(Int32 waitForJoin = Int32.MaxValue)
        {
            lock (this)
            {
                if (_state == ThreadState.Running || _state == ThreadState.Suspended)
                {
                    _shouldStop = true;
                    _threadSync.Set();
                    _thread.Join(waitForJoin);
                    if (_thread.ThreadState != ThreadState.Stopped)
                        _thread.Abort();
                }
            }
        }

        public void Pause(Int32 pauseFor = Int32.MaxValue)
        {
            lock (this)
            {
                if (State == ThreadState.Running)
                {
                    _shouldPause = true;
                    /*Ждём пока запаузится*/
                    while (State != ThreadState.Suspended)
                    {
                        Thread.Sleep(5);
                    }
                }
            }
        }
    }
}
