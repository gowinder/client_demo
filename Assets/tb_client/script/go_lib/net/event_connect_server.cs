// gowinder@hotmail.com
// client_demo.CSharp
// event_connect_server.cs
// 2016-05-13-11:56

#region

using System;
using System.Net.Sockets;
using Assets.tb_client.script.go_lib.logic;
using Assets.tb_client.script.go_lib.service.engine_event;
using go_lib;
using LitJson;

#endregion

namespace Assets.tb_client.script.go_lib.net
{
    internal class event_connect_server : event_base
    {
        public enum connect_socket_status
        {
            connected = 1,
            failed = 2,
            disconnected = 3
        }

        public const string type = "connect_server";

        public void set(service_base from, service_base to, connect_to_server_info info)
        {
            set(from, to, type, info, null);
        }

        public override void process()
        {
            if (data_type != event_data_type.string_obj)
                throw new exception_type_not_valid();
            var str = Convert.ToString(data);

            var json = JsonMapper.ToObject(str);

            if (to_service is service_network)
            {
                var nw = to_service as service_network;
                nw.connect_to_server(json);
            }
            else if (to_service is service_logic)
            {
            }
        }

        public class connect_to_server_info
        {
            public string host { get; set; }
            public int port { get; set; }
            public connect_socket_status status { get; set; }
            public SocketError sock_error { get; set; }
            public uint session_id { get; set; }
            public event_base band_event { get; set; }

            /// <summary>
            ///     auto reconnect when disconnect or connect failed
            /// </summary>
            public bool auto_reconnect { get; set; }

            /// <summary>
            ///     silent when connect result
            /// </summary>
            public bool silent { get; set; }
        }
    }
}