﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.tb_client.script.go_lib.net
{
    class network_const
    {
        public enum EM_NETWORK_CONNECT_TYPE
        {
            NCT_SOCKET = 1,
        }

        public enum EM_NETWORK_CONNTION_STATUS
        {
            NCS_NONE = 0,
            NCS_CONNECTED = 1,
            NCS_DISCONNECTED = 2,
            NCS_CONNEC_FAILED = 3,
        }

        public const string CONNECTION_TYPE = "ct";
        public const string SERVER_ADDRESS = "sa";
        public const string SERVER_PORT = "sp";
        public const string CONNECTION_STATUS = "cs";
    }
}
