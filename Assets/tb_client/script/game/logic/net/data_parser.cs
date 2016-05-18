// gowinder@hotmail.com
// client_demo.CSharp
// data_parser.cs
// 2016-05-18-14:49

#region

using Assets.tb_client.script.game.logic.data;
using Assets.tb_client.script.go_lib.logic;
using Assets.tb_client.script.go_lib.net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#endregion

namespace Assets.tb_client.script.game.logic.net
{
    public class data_parser : i_data_parser
    {
        public delegate void on_data_change(object sender, object obj);

        public static data_parser s_instance;

        public on_data_change on_account_change;
        public on_data_change on_role_change;

        public data_account account { get; set; }
        public data_role role { get; set; }

        public static data_parser instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new data_parser();

                return s_instance;
            }
        }

        public void parser_data(string str_json)
        {
            var json_root = (JObject) JsonConvert.DeserializeObject(str_json);
            if (json_root == null)
                return;

            var json_data = (JObject) json_root[net_json_name.data];
            foreach (var property in json_data.Properties())
            {
                switch (property.Name)
                {
                    case data_account.tname:
                    {
                        if (account == null)
                            account = new data_account();
                        var jobj = (JObject) json_data[property.Name];
                        account.from_json(jobj);
                        if (on_account_change != null)
                            on_account_change.Invoke(this, account);
                    }
                        break;
                    case data_role.tname:
                    {
                        if (role == null)
                            role = new data_role();
                        var jobj = (JObject) json_data[property.Name];
                        role.from_json(jobj);
                        if (on_role_change != null)
                            on_role_change(this, role);
                    }
                        break;
                }
            }
        }
    }
}