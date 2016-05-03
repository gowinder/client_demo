using Assets.tb_client.script.go_lib.net;
using go_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.tb_client.script.go_lib.service.engine_event
{
    class base_event_builder : i_event_builder
    {
        public event_base build_event(String event_type)
        {
            switch (event_type)
            {
                case event_stop_service.type:
                    {
                        return new event_stop_service();
                    }
                case event_connect_server.type:
                    {
                        return new event_connect_server();
                    }
                case event_connect_status.type:
                    {
                        return new event_connect_status();
                    }
                case event_send_net_msg.type:
                    {
                        return new event_send_net_msg();
                    }
                default:
                    return null;
            }


        }
    }
}
