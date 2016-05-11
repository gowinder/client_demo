using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.tb_client.script.go_lib.net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Assets.tb_client.script.game.logic
{
    public class my_http_client_proxy : http_client_proxy
    {
        protected static my_http_client_proxy s_instance;

        public static my_http_client_proxy instance()
        {
            if (s_instance != null)
                return s_instance;

            s_instance = new my_http_client_proxy();
            return s_instance;
        }

        public int do_login(string user_name, string user_pwd, int platform_id)
        {
            JObject json_root = new JObject();
            json_root[net_json_name.package_type] = (int)net_package_type.action;
            json_root[net_json_name.package_sub_type] = (int) net_package_action_sub_type.login;
            json_root[net_json_name.index] = new_index();

            json_root[net_json_name.user_name] = user_name;
            json_root[net_json_name.user_pwd] = user_pwd;
            json_root[net_json_name.platform_id] = (int)platform_id;

            string str_json = JsonConvert.SerializeObject(json_root);
            

            return 0;
        }
    }
}
