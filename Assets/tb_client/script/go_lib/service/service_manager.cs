// gowinder@hotmail.com
// Assembly-CSharp
// service_manager.cs
// 2016-05-10-17:45

#region

using Assets.tb_client.script.go_lib.logic;
using Assets.tb_client.script.go_lib.net;

#endregion

namespace Assets.tb_client.script.go_lib.service
{
    public class service_manager
    {
        private static service_logic _s_logic;

        private static service_network _s_network;

        public static service_logic logic()
        {
            return _s_logic;
        }

        public static service_network instance()
        {
            return _s_network;
        }

        public static void set_logic(service_logic s)
        {
            _s_logic = s;
        }

        public static void set_network(service_network s)
        {
            _s_network = s;
        }
    }
}