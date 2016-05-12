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

    public delegate void on_response<Targs>(object sender, string str_response) where Targs : http_client_proxy_event_args;

    public class http_client_proxy_event_args : EventArgs
    {
        public string response { get; set; }
    }

    public class http_client_proxy_event
    {
        public on_response<http_client_proxy_event_args> response;
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

        protected IEnumerator send_request(string data, http_client_proxy_event proxy_event)
        {
            UnityWebRequest request = UnityWebRequest.Put(web_host, data);
            yield return request.Send();

            if (request.isError)
            {
                Debug.Log(request.error);
            }
            else
            {
                // Show results as text
                Debug.Log(request.downloadHandler.text);

                // Or retrieve results as binary data
                byte[] results = request.downloadHandler.data;
                string str_response = results.ToString();
                proxy_event.response(this, str_response);
            }

        }
    }
}