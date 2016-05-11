// gowinder@hotmail.com
// Assembly-CSharp
// event_send_net_msg.cs
// 2016-05-10-17:45

#region

using System.Collections;
using Assets.tb_client.script.go_lib.net;
using go_lib;

#endregion

namespace Assets.tb_client.script.go_lib.service.engine_event
{
    internal class event_send_net_msg : event_base
    {
        public const string type = "send_net_msg";

        public void set(service_base from, service_base to, netmsg msg, ArrayList parameters)
        {
            from_service = from;
            to_service = to;
            data = msg;
            data_type = event_data_type.netmsg;
            parameter_list = parameters;
        }

        public override void process()
        {
            if (to_service is service_network)
            {
                var msg = data as netmsg;
                var net = to_service as service_network;
                net.send_netmsg(msg);
            }
        }
    }
}