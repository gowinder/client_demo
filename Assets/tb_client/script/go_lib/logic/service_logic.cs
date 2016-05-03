using Assets.tb_client.script.go_lib.service;
using go_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.tb_client.script.go_lib.logic
{
    class service_logic : service_base
    {
//         public service_logic()
//             : base(service_id.LOGIC_SERVICE)
//         {
// 
//         }

        void Start()
        {
            _id = service_id.LOGIC_SERVICE;
        }

        virtual public void network_connected()
        {
            
        }

        virtual public void network_connect_failed()
        {
            
        }

        virtual public void network_disconnected()
        {
            
        }

    }
}
