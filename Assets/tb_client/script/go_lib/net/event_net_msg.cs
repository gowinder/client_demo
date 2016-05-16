// gowinder@hotmail.com
// client_demo.CSharp
// event_net_msg.cs
// 2016-05-13-11:56

#region

using System.Collections;
using Assets.tb_client.script.go_lib.service.engine_event;
using go_lib;

#endregion

namespace Assets.tb_client.script.go_lib.net
{
    internal class event_net_msg : event_base
    {
        public const string type = "net_msg";

        public void set(service_base from, service_base to, byte[] buff, ArrayList parameters)
        {
            set(from, to, type, buff, parameters);
        }

        public override void process()
        {
            if (data_type != event_data_type.byte_array)
                throw new exception_type_not_valid();
        }
    }
}