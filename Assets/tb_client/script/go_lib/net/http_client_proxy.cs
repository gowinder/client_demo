// gowinder@hotmail.com
// client_demo.CSharp
// http_client_proxy.cs
// 2016-05-13-11:56

#region

using System;
using System.Collections;
using Assets.tb_client.script.go_lib.logic;
using UnityEngine;
using UnityEngine.Experimental.Networking;

#endregion

namespace Assets.tb_client.script.go_lib.net
{
    public delegate void on_response<Targs>(object sender, http_client_proxy_event evnt)
        where Targs : http_client_proxy_event_args;

    public class http_client_proxy_event_args : EventArgs
    {
        public string response { get; set; }
    }

    public class http_client_proxy_event
    {
        public on_response<http_client_proxy_event_args> on_response;
        public bool is_error { get; set; }
        public string error { get; set; }
        public string response { get; set; }
    }


    public class http_client_proxy : MonoBehaviour
    {
        protected uint _index;
        public i_data_parser parser { get; set; }

        public http_client_proxy()
        {
            _index = 0;
        }

        public string web_host { get; set; }

        public uint new_index()
        {
            return ++_index;
        }

        protected void send_request(string data, http_client_proxy_event proxy_event)
        {
            StartCoroutine(send(data, proxy_event));
        }

        protected IEnumerator send(string data, http_client_proxy_event proxy_event)
        {
            var request = UnityWebRequest.Put(web_host, data);
            yield return request.Send();

            proxy_event.is_error = request.isError;
            if (request.isError)
            {
                proxy_event.error = request.error;
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                proxy_event.response = request.downloadHandler.text;
                // Or retrieve results as binary data
                var results = request.downloadHandler.data;
                var str_response = results.ToString();

                if(parser != null)
                    parser.parser_data(str_response);

                proxy_event.on_response(this, proxy_event);
            }
        }
    }
}