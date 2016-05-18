using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.tb_client.script.go_lib.logic.data;

namespace Assets.tb_client.script.game.logic.data
{
    public class data_role : game_data_basic
    {
        public data_role()
        {
            table_name = tname;
        }

        public const string tname = "game_role";
        protected override void init_fields()
        {
            fields_name.Add("id");
            fields.Add(0);

            fields_name.Add("account_id");
            fields.Add(0);

            fields_name.Add("name");
            fields.Add("");
        }

        public uint id
        {
            get { return (uint)fields[0]; }
            set { fields[0] = value; }
        }

        public uint account_id
        {
            get { return (uint)fields[1]; }
            set { fields[1] = value; }
        }

        public string name
        {
            get { return (string)fields[2]; }
            set { fields[2] = value; }
        }
    }
}
