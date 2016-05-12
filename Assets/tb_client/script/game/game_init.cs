// gowinder@hotmail.com
// Assembly-CSharp
// game_init.cs
// 2016-05-10-17:52

using Assets.tb_client.script.game.logic;
using Assets.tb_client.script.go_lib.net;
using Assets.tb_client.script.go_lib.service;
using UnityEngine;

namespace Assets.tb_client.script.game
{
    public class game_init : MonoBehaviour
    {
        void Start()
        {
            service_manager.set_logic(my_service_logic.instance);
            service_manager.set_network(service_network.instance);


            var connect_info = new event_connect_server.connect_to_server_info()
            {
                host = "127.0.0.1",
                port = 928,

            };

            event_connect_server event_connect = new event_connect_server();
            event_connect.set(my_service_logic.instance, service_network.instance, connect_info);

            GameObject pro = GameObject.Find("httpproxy");
            if (pro != null)
            {
                http_client_proxy_event login_event = new http_client_proxy_event();
                login_event.response += on_login_response;

                my_http_client_proxy my_proxy = pro.GetComponent<my_http_client_proxy>();
                StartCoroutine(my_proxy.do_login("test1", "asdf", 0, login_event));
            }
//             var http_proxy = logic.my_http_client_proxy.instance();
// 
//             http_client_proxy_event login_event = new http_client_proxy_event();
//             login_event.response += on_login_response;
//             http_proxy.do_login("test1", "asdf", 0, login_event);
        }

        protected void on_login_response(object sender, string str_response)
        {
            Debug.Log(str_response);
        }
    }
}