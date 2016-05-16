// gowinder@hotmail.com
// client_demo.CSharp
// my_service_logic.cs
// 2016-05-13-11:56

#region

using System;
using Assets.tb_client.script.go_lib.logic;
using Assets.tb_client.script.go_lib.service;
using UnityEngine;

#endregion

namespace Assets.tb_client.script.game.logic
{
    public class my_service_logic : service_logic
    {
        protected static service_logic s_instance;

        public static service_logic instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new service_logic();

                return s_instance;
            }
        }

        protected DateTime debug_t { get; set; }
        // Use this for initialization
        private void Start()
        {
            service_manager.set_logic(this);

            start_service(false);

            debug_t = DateTime.Now;
        }

        // Update is called once per frame
        private void Update()
        {
        }

        protected override void maintain()
        {
            try
            {
                var ts = DateTime.Now - debug_t;
                if (ts.Seconds > 10)
                {
                    //    Debug.Log("my logic service maintain");
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