// gowinder@hotmail.com
// client_demo.CSharp
// game_init.cs
// 2016-05-13-11:56

#region

using Assets.tb_client.script.game.logic;
using Assets.tb_client.script.game.logic.data;
using Assets.tb_client.script.game.logic.net;
using Assets.tb_client.script.go_lib.net;
using Assets.tb_client.script.go_lib.service;
using UnityEngine;

#endregion

namespace Assets.tb_client.script.game
{
    public class game_init : MonoBehaviour
    {
        private void Start()
        {
            service_manager.set_logic(my_service_logic.instance);
            service_manager.set_network(service_network.instance);


            var connect_info = new event_connect_server.connect_to_server_info
            {
                host = "127.0.0.1",
                port = 928
            };

            var event_connect = new event_connect_server();
            event_connect.set(my_service_logic.instance, service_network.instance, connect_info);

            var my_proxy = my_http_client_proxy.get_instance();

            data_parser.instance.on_role_change += (sender, o) =>
            {
                var role = o as data_role;
                Debug.Log("role changed : name=" + role.name);
            };

            var login_event = new http_client_proxy_event();
            login_event.on_response += on_login_response;

            my_proxy.do_login("test1", "asdf", 0, login_event);
        }

        protected void on_login_response(object sender, http_client_proxy_event evnt)
        {
            Debug.Log(evnt.response);

            var query_event = new http_client_proxy_event();
            query_event.on_response += (s, e) =>
            {
                Debug.Log("query role response = " + e.response);
            };

            my_http_client_proxy.get_instance().query_role(query_event);
        }
    }
}