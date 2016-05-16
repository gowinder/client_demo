// gowinder@hotmail.com
// client_demo.CSharp
// net_package_type.cs
// 2016-05-13-11:56

namespace Assets.tb_client.script.game.logic
{
    public enum net_package_type
    {
        echo = 0,
        notification = 1,
        action = 2,
        query = 4,
        gm_action = 5,
        gm_query = 6
    }

    public enum net_package_action_sub_type
    {
        login = 1,
        create_role,
        battle
    }
}