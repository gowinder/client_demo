// gowinder@hotmail.com
// client_demo.CSharp
// i_event_builder.cs
// 2016-05-13-11:57

#region

using go_lib;

#endregion

namespace Assets.tb_client.script.go_lib.service.engine_event
{
    internal interface i_event_builder
    {
        event_base build_event(string event_type);
    }
}