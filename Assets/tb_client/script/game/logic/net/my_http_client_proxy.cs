// gowinder@hotmail.com
// client_demo.CSharp
// my_http_client_proxy.cs
// 2016-05-13-11:56

#region

using Assets.tb_client.script.go_lib.net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

#endregion

namespace Assets.tb_client.script.game.logic.net
{
    public class my_http_client_proxy : http_client_proxy
    {
        protected static my_http_client_proxy s_instance;

        public my_http_client_proxy()
        {
            parser = data_parser.instance;
            web_host = "http://127.0.0.1:9981/test_request/";
        }

        public static my_http_client_proxy instance()
        {
            if (s_instance != null)
                return s_instance;

            s_instance = new my_http_client_proxy();
            return s_instance;
        }

        public static my_http_client_proxy get_instance()
        {
            const string proxy_name = "my_http_client_proxy";
            var pro = GameObject.Find(proxy_name);
            if (pro == null)
            {
                pro = new GameObject(proxy_name);
                pro.AddComponent<my_http_client_proxy>();
            }
            var my_proxy = pro.GetComponent<my_http_client_proxy>();
            return my_proxy;
        }


        public void do_login(string user_name, string user_pwd, int platform_id, http_client_proxy_event proxy_event)
        {
            var json_root = new JObject();
            json_root[net_json_name.package_type] = (int) net_package_type.action;
            json_root[net_json_name.package_sub_type] = (int) net_package_action_sub_type.login;
            json_root[net_json_name.index] = new_index();

            json_root[net_json_name.user_name] = user_name;
            json_root[net_json_name.user_pwd] = user_pwd;
            json_root[net_json_name.platform_id] = platform_id;

            var str_json = JsonConvert.SerializeObject(json_root);

            send_request(str_json, proxy_event);

//             UnityWebRequest request = UnityWebRequest.Put(web_host, str_json);
//             yield return request.Send();
// 
//             proxy_event.is_error = request.isError;
//             if (request.isError)
//             {
//                 Debug.Log(request.error);
//             }
//             else
//             {
//                 string str_response = request.downloadHandler.text;
//                 proxy_event.on_response(this, proxy_event);
//             }
        }

       
        public void query_role(http_client_proxy_event query_event)
        {
            var json_root = new JObject();
            json_root[net_json_name.package_type] = (int)net_package_type.query;
            json_root[net_json_name.package_sub_type] = (int)net_package_query_sub_type.role;
            json_root[net_json_name.index] = new_index();

            var str_json = JsonConvert.SerializeObject(json_root);

            send_request(str_json, query_event);
        }
    }
}