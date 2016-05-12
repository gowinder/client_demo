// gowinder@hotmail.com
// Assembly-CSharp
// http_client_proxy.cs
// 2016-05-11-10:53

using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Networking;

namespace Assets.tb_client.script.go_lib.net
{

    public delegate void on_response<Targs>(object sender, http_client_proxy_event evnt) where Targs : http_client_proxy_event_args;

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


    public class http_client_proxy  : MonoBehaviour
    {
        protected uint _index;

        public string web_host { get; set; }
        public http_client_proxy()
        {
            _index = 0;
        }

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
            UnityWebRequest request = UnityWebRequest.Put(web_host, data);
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
                byte[] results = request.downloadHandler.data;
                string str_response = results.ToString();
                proxy_event.on_response(this, proxy_event);
            }
        }
    }
}