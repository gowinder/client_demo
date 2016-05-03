using Assets.tb_client.script.go_lib.logic;
using Assets.tb_client.script.go_lib.service;
using Assets.tb_client.script.go_lib.service.engine_event;
using go_lib;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.tb_client.script.go_lib.net
{
    class event_connect_status : event_base
    {
        public const String type = "connect_status";
        public void set(service_base from, service_base to, JsonData json)
        {
//           string str_json = json.ToString();
//             byte[] buff = new byte[str_json.Length];
//             System.Buffer.BlockCopy(str_json.ToCharArray(), 0, buff, 0, buff.Length);
//             set(from, to, type, buff, null);

            this.from_service = from;
            this.to_service = to;
            this.data = json;
            this.data_type = event_data_type.json_data;
        }

        public override void process()
        {
            if (!(to_service is service_logic))
                return;

            service_logic service = to_service as service_logic;
            
            if(this.data_type != event_data_type.json_data)
                throw new exception_type_not_valid();

            JsonData json = this.data as JsonData;
            network_const.EM_NETWORK_CONNTION_STATUS status = (network_const.EM_NETWORK_CONNTION_STATUS)(int)json[network_const.CONNECTION_STATUS];
            switch (status)
            {
                case network_const.EM_NETWORK_CONNTION_STATUS.NCS_CONNECTED:
                    {
                        Debug.Log("network connected");
                        service.network_connected();
                    }
                    break;
                case network_const.EM_NETWORK_CONNTION_STATUS.NCS_CONNEC_FAILED:
                    {
                        Debug.Log("network connect faield");
                        service.network_connect_failed();
                    }
                    break;
                case network_const.EM_NETWORK_CONNTION_STATUS.NCS_DISCONNECTED:
                    {
                        Debug.Log("network disconnected");
                        service.network_disconnected();
                    }
                    break;
            }
        }
    }
}
