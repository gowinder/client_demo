using Assets.tb_client.script.go_lib.logic;
using Assets.tb_client.script.go_lib.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.tb_client.script.game.logic
{
    class my_service_logic  : service_logic
    {
        // Use this for initialization
        void Start()
        {            
            service_manager.set_logic(this);

            start_service(false);

            debug_t = DateTime.Now;
        }

        // Update is called once per frame
        void Update()
        {

        }

        protected DateTime debug_t { get; set; }

        protected override void maintain()
        {
            try
            {
                TimeSpan ts = DateTime.Now - debug_t;
                if (ts.Seconds > 10)
                {
                    Debug.Log("my logic service maintain");
                    debug_t = DateTime.Now;
                    
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }
}
