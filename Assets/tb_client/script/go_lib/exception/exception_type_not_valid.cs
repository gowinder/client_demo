using Assets.tb_client.script.go_lib.exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.tb_client.script.go_lib.service.engine_event
{
    class exception_type_not_valid : exception_base
    {
        public const String type = "type_not_valid";
        public exception_type_not_valid()
            : base(type)
        {

        }
    }
}
