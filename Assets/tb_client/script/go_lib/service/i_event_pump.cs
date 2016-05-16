// gowinder@hotmail.com
// client_demo.CSharp
// i_event_pump.cs
// 2016-05-13-11:57

#region

using go_lib;

#endregion

namespace Assets.tb_client.script.go_lib.service
{
    public interface i_event_pump
    {
        void push(event_base e);
        event_base pop();
        int size();
        bool wait(int mill_second);
        void close();
        void open();
        bool is_open();
        void recycle(event_base e);
        event_base get_new_event(string type);
    }
}