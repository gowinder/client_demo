// gowinder@hotmail.com
// client_demo.CSharp
// exception_type_not_valid.cs
// 2016-05-13-11:56

#region

using Assets.tb_client.script.go_lib.exception;

#endregion

namespace Assets.tb_client.script.go_lib.service.engine_event
{
    internal class exception_type_not_valid : exception_base
    {
        public const string type = "type_not_valid";

        public exception_type_not_valid()
            : base(type)
        {
        }
    }
}