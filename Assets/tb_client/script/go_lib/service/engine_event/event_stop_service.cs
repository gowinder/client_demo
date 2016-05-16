// gowinder@hotmail.com
// client_demo.CSharp
// event_stop_service.cs
// 2016-05-13-11:57

#region

using go_lib;

#endregion

namespace Assets.tb_client.script.go_lib.service.engine_event
{
    internal class event_stop_service : event_base
    {
        public const string type = "stop_service";

        public void set(service_base from, service_base to)
        {
            set(from, to, type, null, null);
        }

        public override void process()
        {
            to_service.stop_service();
        }
    }
}