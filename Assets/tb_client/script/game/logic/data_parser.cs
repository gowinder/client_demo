// gowinder@hotmail.com
// client_demo.CSharp
// data_parser.cs
// 2016-05-13-12:21

#region

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#endregion

namespace Assets.tb_client.script.game.logic
{
    public class data_parser
    {
        public static data_parser s_instance;

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

            JObject json_data = (JObject) json_root[net_json_name.data];
            foreach (var property in json_data.Properties())
            {
                switch (property.Name)
                {
                    case "data_role":
                    {
                    }
                        break;
                }
            }
        }
    }
}