// gowinder@hotmail.com
// Assembly-CSharp
// service_base.cs
// 2016-05-10-17:45

#region

using System;
using System.Threading;
using Assets.tb_client.script.go_lib.service;
using UnityEngine;

#endregion

namespace go_lib
{
    public class service_base : MonoBehaviour
    {
        protected int _id;


        protected i_event_pump _pump;
        protected bool _start_own_thread;
        protected Thread _thread;

        private readonly string fun_name = "go_tick";

        public bool is_running
        {
            get
            {
                if (_pump != null && _pump.is_open())
                    return true;

                return false;
            }
        }

        private void Awake()
        {
        }

        private void OnApplicationQuit()
        {
            stop_service();
        }

        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }


        protected virtual i_event_pump create_pump()
        {
            return new event_pump(_id);
        }

//         public service_base(int id)
//         {
//             _id = id;
//         }

        public event_base get_new_event(string event_type)
        {
//             if(_pump == null)
//                 throw new exception_base(exception_base.RETURN_NULL_REF);

            return _pump.get_new_event(event_type);
        }

        public void thread_process()
        {
            while (true)
            {
                if (is_running)
                    go_tick();
            }
        }

        protected void go_tick()
        {
            maintain();

            process_event_pump();
        }

        protected virtual void maintain()
        {
            try
            {
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        public void start_service(bool start_own_thread)
        {
            if (is_running)
                return;

            _pump = create_pump();
            _pump.open();
            _start_own_thread = start_own_thread;
            var t = new service_thread(this);
            ThreadStart threadDelegate = t.proc;
            if (start_own_thread)
            {
                _thread = new Thread(threadDelegate);
                _thread.Start();
            }
            else
            {
                InvokeRepeating(fun_name, 0.1f, 0.5f);
            }
        }

        public void stop_service()
        {
            if (!is_running)
                return;

            _pump = null;
            if (_start_own_thread)
            {
                if (_thread != null)
                    _thread.Abort();
            }
            else
            {
                CancelInvoke(fun_name);
            }
        }

        protected void process_event_pump()
        {
            try
            {
                if (!is_running)
                    return;

                const int INTERVAL = 50;

                var dt_start = DateTime.Now;
                var e = _pump.pop();
//                 for (; ; )
//                 {
//                     if (e == null)
//                         break;
//                     try
//                     {
//                         e.process();
//                     }
//                     catch (Exception ex)
//                     {
//                         Debug.Log(ex.Message);
//                     }
//                     e.recycle();
//                 }
                if (e != null)
                {
                    e.process();
                    e.recycle();
                }
                var dt_end = DateTime.Now;
                var ts = dt_end - dt_start;

                int delta_time = INTERVAL - ts.Milliseconds;
          //      Debug.Log("service delta_time " + delta_time);
                if (delta_time > 0 && _start_own_thread)
                {

                    _pump.wait(delta_time);

                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        public void send_event(event_base e)
        {
            if (!is_running)
            {
                Debug.Log("service not running");
                return;
            }

            _pump.push(e);
        }
    }


    internal class service_thread
    {
        protected service_base _s;

        public service_thread(service_base s)
        {
            _s = s;
        }


        public void proc()
        {
            _s.thread_process();
        }
    }
}