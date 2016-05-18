using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Assets.tb_client.script.go_lib.logic.data
{
    public abstract class game_data_basic
    {
        public static bool short_json_name { get; set; }
        public enum data_edit_type
        {
            none = 0,
            update = 1,
            insert = 2,
            delete = 3,
        }
        

        protected game_data_basic()
        {
            fields = new List<object>();
            fields_name = new List<string>();
            edit_type = data_edit_type.update;

            init_fields();
        }

        protected data_edit_type edit_type { get; set; }
        protected List<object> fields { get; set; }
        protected List<string> fields_name { get; set; }
        protected BitVector32 fields_change { get; set; }
        public string table_name { get; set; }

        protected abstract void init_fields();

        public void to_json(JObject json_root)
        {
            JObject jobj = new JObject();

            if (game_data_basic.short_json_name)
            {
                for (var i = 0; i < fields.Count; i++)
                {
                    string str_name = string.Format("a{0}", i);
                    jobj[str_name] = (JObject)fields[i];
                }
            }
            else
            {
                foreach (object t in fields)
                {
                    jobj[fields_name] = (JObject)t;
                }
            }

            json_root[table_name] = jobj;
        }

        public void change_to_json(JObject json_root)
        {
            if (fields_change.Data == 0)
                return;

            JObject jobj = new JObject();

            if (game_data_basic.short_json_name)
            {
                for (var i = 0; i < fields.Count; i++)
                {
                    if (!fields_change[i])
                        continue;
                    string str_name = string.Format("a{0}", i);
                    jobj[str_name] = (JObject)fields[i];
                }
            }
            else
            {
                for (var i = 0; i < fields.Count; i++)
                {
                    if (!fields_change[i])
                        continue;
                    jobj[fields_name] = (JObject)fields[i];
                }
            }

            json_root[table_name] = jobj;
        }

        public void from_json(JObject json_root)
        {
            JObject jobj = (JObject)json_root[table_name];
            if (jobj == null)
                return;

            if (game_data_basic.short_json_name)
            {
                for (var i = 0; i < fields.Count; i++)
                {
                    string str_name = string.Format("a{0}", i);
                    fields[i] = jobj[str_name];
                }
            }
            else
            {
                for (var i = 0; i < fields.Count; i++)
                {
                    fields[i] = jobj[fields_name];
                }
            }
        }
    }
}
