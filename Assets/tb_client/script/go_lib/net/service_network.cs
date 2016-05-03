using Assets.tb_client.script.go_lib.logic;
using Assets.tb_client.script.go_lib.service;
using Assets.tb_client.script.go_lib.service.engine_event;
using Assets.tb_client.script.go_lib.tools;
using go_lib;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace Assets.tb_client.script.go_lib.net
{

    struct NET_MSG_HEAD 
    {
        public int size;		//!<
	    public int type;		//!<
	    public int	end_flag;	//!< 	EM_NET_MSG_END_FLAG
	    public int	msg_number;	//!< 
    }


    struct VARIABLE_BUFFER
    {
	    public int	size;
	    public char[]	buffer;
    }

    class service_network : service_base
    {
        void Start()
        {
            init();
            debug_t = DateTime.Now;
            _id = service_id.NETWORK_SERVICE;
            service_manager.set_network(this);
            start_service(true);
        }


        void Update()
        {

        }

        public void send_netmsg(netmsg msg)
        {

        }

        public network_const.EM_NETWORK_CONNECT_TYPE connection_type { get; set; }
        public string server_address { get; set; }
        public int server_port { get; set; }

        protected const int RECEIVE_BUFF_SIZE = 512 * 1024;
        protected const int SEND_BUFF_SIZE = 512 * 1024;
        protected byte[] _recive_buff;
        protected byte[] _send_buff;
        protected int _recive_offset;
        protected int _send_offset;

        protected Socket _socket;


        private void init()
        {
            _recive_buff = new byte[RECEIVE_BUFF_SIZE];
            _recive_offset = 0;

            _send_buff = new byte[SEND_BUFF_SIZE];
            _send_offset = 0;
        }


        public void connect_to_server(JsonData json)
        {
            parse_json(json);

            do_connect();
        }

        protected virtual void parse_json(JsonData json)
        {
            connection_type = (network_const.EM_NETWORK_CONNECT_TYPE)(int)json[network_const.CONNECTION_TYPE];
            server_address = json[network_const.SERVER_ADDRESS].ToString();
            server_port = (int)json[network_const.SERVER_PORT];
        }

        protected virtual void do_connect()
        {
            switch (connection_type)
            {
                case network_const.EM_NETWORK_CONNECT_TYPE.NCT_SOCKET:
                    {
                        do_socket_connect();
                    }
                    break;
            }
        }

        protected virtual void do_socket_connect()
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(server_address), server_port);
            _socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                _socket.Blocking = false;
                _socket.Connect(ipe);
            }
            catch (Exception e)
            {
                Debug.Log(" Connect failed IP: " + server_address + " Port : " + server_port.ToString());
                Debug.Log(e.Message);
                send_connect_status_to_logic(network_const.EM_NETWORK_CONNTION_STATUS.NCS_CONNEC_FAILED);

                return;
            }


            send_connect_status_to_logic(network_const.EM_NETWORK_CONNTION_STATUS.NCS_CONNECTED);


        }

        private void send_connect_status_to_logic(network_const.EM_NETWORK_CONNTION_STATUS status)
        {
            JsonData json = new JsonData();
            json[network_const.CONNECTION_STATUS] = (int)network_const.EM_NETWORK_CONNTION_STATUS.NCS_CONNECTED;

            event_connect_status e = (event_connect_status)service_manager.logic().get_new_event(event_connect_status.type);
            e.set(this, service_manager.logic(), json);
            service_manager.network().send_event(e);
        }

        protected DateTime debug_t { get; set; }
        protected override void maintain()
        {
            try
            {
                TimeSpan ts = DateTime.Now - debug_t;
                if (ts.Seconds > 1)
                {
                    Debug.Log("network service maintain");
                    debug_t = DateTime.Now;
                }
                if(_socket == null)
                    return;

                if (!_socket.Connected)
                    return;

                try
                {
                    maintain_receive();
                }
                catch(Exception ex)
                {
                    activate_close_socket();
                    Debug.Log("service_network maintain send: " + ex.Message);
                    
                }
               
                try
                {
                    maintain_send();
                }
                catch (Exception ex)
                {
                    activate_close_socket();
                    Debug.Log("service_network maintain send: " + ex.Message);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        protected void on_receive_completed()
        {

        }

        protected void on_send_completed()
        {

        }

        private void maintain_send()
        {
            if (_send_offset > 0)
            {

                int send_begin_offset = 0;
                int send_times = 0;
                // [9/13/2013 gowinder@hotmail.com]
                while (send_begin_offset < _send_offset && send_times < 10)
                {
                    int left_count = _send_offset - send_begin_offset;
                    int nSended = _socket.Send(_send_buff, send_begin_offset, left_count, SocketFlags.None);

                    if (nSended < 0)
                    {
                        //  todo close socket

                    }
                    if (nSended > 0)
                    {
                        send_begin_offset += nSended;
                        send_times++;
                    }
                }

                if (send_begin_offset < _send_offset)
                {
                    int left_count = _send_offset - send_begin_offset;
                    Array.Copy(_send_buff, send_begin_offset, _send_buff, 0, left_count);

                    _send_offset = left_count;
                }
            }

        }

        private void maintain_receive()
        {
            if (_socket.Available > 0)
            {
                int buff_remain = RECEIVE_BUFF_SIZE - _recive_offset;
                int receive_count = _socket.Receive(_recive_buff, _recive_offset, buff_remain, SocketFlags.None);
                if (receive_count == 0)
                {
                    //  todo close socket 
                }
                else
                {
                    if (receive_count > buff_remain)
                    {
                        //  todo close socket
                    }
                    _recive_offset += receive_count;
                }
            }

            int head_size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(NET_MSG_HEAD));
            if (_recive_offset >= head_size)
            {
                NET_MSG_HEAD msg_head = (NET_MSG_HEAD)base_tools.BytesToStruts(_recive_buff, typeof(NET_MSG_HEAD));
//                 if ((int)msg_head == 0)
//                 {
//                     //  todo close socket
//                 }
                int size = msg_head.size;
                while (_recive_offset >= size)
                {
                    byte[] msg_buff = new byte[size];
                    Array.Copy(_recive_buff, msg_buff, size);
                    event_net_msg ev = (event_net_msg)service_manager.logic().get_new_event(event_net_msg.type);
                    ArrayList parameters = new ArrayList();
                    event_paramter ep = new event_paramter();
                    ep.name = "socket ip";
                    ep.data = "null";
                    parameters.Add(ep);
                    ev.set(this, service_manager.logic(), msg_buff, parameters);
                    ev.send();

                    int left_buffer = _recive_offset - size;
                    if (left_buffer > 0)
                        Array.Copy(_recive_buff, size, _recive_buff, 0, left_buffer);
                    _recive_offset -= size;
                    msg_head = (NET_MSG_HEAD)base_tools.BytesToStruts(_recive_buff, typeof(NET_MSG_HEAD));
                    size = msg_head.size;
                }
            }
        }

        private void activate_close_socket()
        {
            _socket.Close();

             JsonData json = new JsonData();
            json[network_const.CONNECTION_STATUS] = (int)network_const.EM_NETWORK_CONNTION_STATUS.NCS_DISCONNECTED;

            event_connect_status ev = (event_connect_status)service_manager.logic().get_new_event(event_connect_status.type);
            ev.set(this, service_manager.logic(), json);
            ev.send();
        }

    }
}
