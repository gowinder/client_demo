// gowinder@hotmail.com
// client_demo.CSharp
// account.cs
// 2016-05-13-11:56

using Assets.tb_client.script.go_lib.logic.data;

namespace Assets.tb_client.script.game.logic.data
{
    public class data_account : game_data_basic
    {
        public const string tname = "account";
        public data_account()
        {
            table_name = tname;
        }
        public data_role role { get; set; }
        protected override void init_fields()
        {
            fields_name.Add("id");
            fields.Add(0);

            fields_name.Add("name");
            fields.Add("");

            fields_name.Add("role_id");
            fields.Add(0);
        }

        public uint id {
            get { return (uint)fields[0]; }
            set { fields[0] = value; }
        }

        public string name
        {
            get { return (string) fields[1]; }
            set { fields[1] = value; }
        }

        public uint role_id
        {
            get { return (uint) fields[2]; }
            set { fields[2] = value; }
        }
    }
}