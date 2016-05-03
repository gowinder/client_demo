﻿using go_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        event_base get_new_event(String type);
    }
}
