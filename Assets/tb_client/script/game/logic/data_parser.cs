using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngineInternal;

namespace Assets.tb_client.script.game.logic
{
    public class data_parser
    {
        public static data_parser s_instance;
        public static data_parser instance
        {
            get
            {
                if(s_instance == null)
                    s_instance = new data_parser();
                
                return s_instance;
            }
        }

        public void parser_data(string str_json)
        {
            JObject json_root = (JObject)JsonConvert.DeserializeObject(str_json);
            if (json_root == null)
                return;
        }
    }
}
