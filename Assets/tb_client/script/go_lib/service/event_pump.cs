// gowinder@hotmail.com
// client_demo.CSharp
// event_pump.cs
// 2016-05-13-11:57

#region

using System;
using System.Collections.Generic;
using System.Threading;
using Assets.tb_client.script.go_lib.service.engine_event;
using go_lib;

#endregion

namespace Assets.tb_client.script.go_lib.service
{
    internal class event_pump : i_event_pump
    {
        protected int _id;
        protected bool _is_open;
        protected object _locker;
        //protected Queue<event_base> _queue_recycle;
        protected Dictionary<string, Queue<event_base>> _map_recycle;
        protected Queue<event_base> _queue;
        protected ManualResetEvent _waiter;

        public event_pump(int id)
        {
            _id = id;
            _waiter = new ManualResetEvent(false);
            _locker = new object();
            _queue = new Queue<event_base>();
            _map_recycle = new Dictionary<string, Queue<event_base>>();
            event_builder = new base_event_builder();
        }

        public int id
        {
            get { return _id; }
        }

        public i_event_builder event_builder { get; set; }


        public void push(event_base e)
        {
            lock (_locker)
            {
                if (!_is_open)
                    throw new Exception("event pump not open");


                _queue.Enqueue(e);
                _waiter.Set();
            }
        }

        public event_base pop()
        {
            lock (_locker)
            {
                if (!_is_open)
                    throw new Exception("event pump not open");

                if (_queue.Count < 1)
                    return null;
                var e = _queue.Dequeue();
                return e;
            }
        }

        public int size()
        {
            lock (_locker)
            {
                return _queue.Count;
            }
        }

        public bool wait(int mill_second)
        {
            if (size() > 0)
                return true;

            _waiter.Reset();
            return _waiter.WaitOne(mill_second);
        }

        public void close()
        {
            throw new NotImplementedException();
        }

        public void open()
        {
            lock (_locker)
            {
                _is_open = true;
            }
        }

        public bool is_open()
        {
            return _is_open;
        }


        public void recycle(event_base e)
        {
            if (_map_recycle.ContainsKey(e.event_type))
            {
                var queue_recyle = _map_recycle[e.event_type];
                queue_recyle.Enqueue(e);
            }
            else
            {
                var queue_recyle = new Queue<event_base>();
                queue_recyle.Enqueue(e);
                _map_recycle[e.event_type] = queue_recyle;
            }
        }


        public event_base get_new_event(string event_type)
        {
            lock (_locker)
            {
                if (_map_recycle.ContainsKey(event_type))
                {
                    var queue_recyle = _map_recycle[event_type];
                    if (queue_recyle.Count < 1)
                        return queue_recyle.Dequeue();
                }
                else
                    return event_builder.build_event(event_type);
            }

            return null;
        }
    }
}