// gowinder@hotmail.com
// client_demo.CSharp
// service_logic.cs
// 2016-05-13-11:56

#region

using Assets.tb_client.script.go_lib.service;
using go_lib;

#endregion

namespace Assets.tb_client.script.go_lib.logic
{
    public class service_logic : service_base
    {
//         public service_logic()
//             : base(service_id.LOGIC_SERVICE)
//         {
// 
//         }

        private void Start()
        {
            _id = service_id.LOGIC_SERVICE;
        }

        public virtual void network_connected()
        {
        }

        public virtual void network_connect_failed()
        {
        }

        public virtual void network_disconnected()
        {
        }
    }
}