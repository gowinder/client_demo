// gowinder@hotmail.com
// client_demo.CSharp
// event_base.cs
// 2016-05-13-11:57

#region

using System;
using System.Collections;
using Assets.tb_client.script.go_lib.service;

#endregion

namespace go_lib
{
    #region

    using event_type = String;

    #endregion

    public enum event_data_type
    {
        byte_array,
        char_array,
        string_obj,
        json_data,
        netmsg
    }

    public class event_base
    {
        public service_base from_service { get; set; }
        public service_base to_service { get; set; }
        public string event_type { get; set; }
        public object data { get; set; }
        public event_data_type data_type { get; set; }
        public ArrayList parameter_list { get; set; }
        public i_event_pump owner_pump { get; set; }

        public virtual void set(service_base from, service_base to, string type, object data_obj, ArrayList parameters)
        {
            from_service = from;
            to_service = to;
            event_type = type;
            data = data_obj;
            parameter_list = parameters;
            data_type = event_data_type.byte_array;
        }

        public virtual void send()
        {
            if (to_service == null)
                throw new Exception("event_base.send() to_service is null");

            if (owner_pump == null)
                throw new Exception("event_base.send() owner_pump is null");

            owner_pump.push(this);
        }

        public void recycle()
        {
//             if (owner_pump != null)
//             {
//                 owner_pump.recycle(this);
//             }
        }

        public virtual void process()
        {
        }
    }
}