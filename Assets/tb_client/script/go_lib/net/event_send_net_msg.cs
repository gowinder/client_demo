using Assets.tb_client.script.go_lib.logic;
using Assets.tb_client.script.go_lib.net;
using go_lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.tb_client.script.go_lib.service.engine_event
{
    class event_send_net_msg : event_base
    {
        public const String type = "send_net_msg";

        public void set(service_base from, service_base to, netmsg msg, ArrayList parameters)
        {
            from_service = from;
            to_service = to;
            data = msg;
            data_type = event_data_type.netmsg;
            this.parameter_list = parameters;
        }

        public override void process()
        {
            if (to_service is service_network)
            {
                netmsg msg = data as netmsg;
                service_network net = to_service as service_network;
                net.send_netmsg(msg);
            }
        }
    }
}
