using Assets.tb_client.script.go_lib.service;
using go_lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
using Assets.tb_client.script.go_lib.logic;
using Assets.tb_client.script.go_lib.service.engine_event;

namespace Assets.tb_client.script.go_lib.net
{
    class event_connect_server : event_base
    {
        public const String type = "connect_server";

        public void set(service_base from, service_base to, byte[] buff, ArrayList parameters)
        {
            set(from, to, type, buff, parameters);
        }

        public override void process()
        {
            if (data_type != event_data_type.string_obj)
                throw new exception_type_not_valid();
            string str = Convert.ToString(data);

            JsonData json = JsonMapper.ToObject(str);
            
            if (to_service is service_network)
            {
                service_network nw = to_service as service_network;
                nw.connect_to_server(json);
            }
            else if (to_service is service_logic)
            {

            }
        }
    }
}
