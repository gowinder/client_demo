using go_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.tb_client.script.go_lib.service.engine_event
{
    interface i_event_builder
    {
        event_base build_event(String event_type);
    }
}
