// gowinder@hotmail.com
// client_demo.CSharp
// my_http_client_proxy.cs
// 2016-05-12-16:08

#region

using System.Collections;
using System.Text;
using Assets.tb_client.script.go_lib.net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Experimental.Networking;

#endregion

namespace Assets.tb_client.script.game.logic
{
    public class my_http_client_proxy : http_client_proxy
    {
        protected static my_http_client_proxy s_instance;

        public my_http_client_proxy()
        {
            web_host = "http://127.0.0.1:9981/test_request/";
        }

        public static my_http_client_proxy instance()
        {
            if (s_instance != null)
                return s_instance;

            s_instance = new my_http_client_proxy();
            return s_instance;
        }

        public IEnumerator do_login(string user_name, string user_pwd, int platform_id, http_client_proxy_event proxy_event)
        {
            var json_root = new JObject();
            json_root[net_json_name.package_type] = (int) net_package_type.action;
            json_root[net_json_name.package_sub_type] = (int) net_package_action_sub_type.login;
            json_root[net_json_name.index] = new_index();

            json_root[net_json_name.user_name] = user_name;
            json_root[net_json_name.user_pwd] = user_pwd;
            json_root[net_json_name.platform_id] = platform_id;

            var str_json = JsonConvert.SerializeObject(json_root);

            UnityWebRequest request = UnityWebRequest.Put(web_host, str_json);
            yield return request.Send();
            if (request.isError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string str_response = request.downloadHandler.text;
                proxy_event.response(this, str_response);
            }

            //             var client = UnityCommunicationManager.CreateInstance().GetHttpClient();
            //             var content_buffer = Encoding.UTF8.GetBytes(str_json);
            //             client.Error += Client_Error;
            //             client.BeginPost(web_host, content_buffer, response =>
            //             {
            //                 UnityEngine.Debug.Log("response is" + response.StringContent);
            //             });
        }
        
    }
}