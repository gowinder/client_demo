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
            my_service_logic logic_ser = new my_service_logic();
            service_network network_ser = new service_network();

            service_manager.set_logic(logic_ser);
            service_manager.set_network(network_ser);

            event_connect_server event_connect = new event_connect_server();
            event_connect.set(logic_ser, network_ser);
        }
    }
}