// gowinder@hotmail.com
// Assembly-CSharp
// http_client_proxy.cs
// 2016-05-11-10:53

namespace Assets.tb_client.script.go_lib.net
{
    public class http_client_proxy
    {
        protected uint _index;

        public http_client_proxy()
        {
            _index = 0;
        }

        public uint new_index()
        {
            return ++_index;
        }
    }
}