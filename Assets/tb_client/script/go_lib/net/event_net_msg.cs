using Assets.tb_client.script.go_lib.service.engine_event;
using go_lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.tb_client.script.go_lib.net
{
    class event_net_msg : event_base
    {
        public const String type = "net_msg";

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
