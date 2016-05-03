using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class battle_field : MonoBehaviour
{
    static public battle_field instance;

    [HideInInspector]
    public float start_pos_x = 0;
    [HideInInspector]
    public float start_pos_z = 0;
    [HideInInspector]
    public float field_pos_y = 0;
    [HideInInspector]
    public float tile_count_x = 10;
    [HideInInspector]
    public float tile_count_z = 10;

    [HideInInspector]
    public float tile_side_len = 0.5f;

    [HideInInspector]
    public float unity_width;
    [HideInInspector]
    public float unity_height;
    [HideInInspector]
    public float mini_distance;

    public GameObject tile_prefab;

    protected Dictionary<Vector3, tile> _map_tile;


    // Use this for initialization
    void Start()
    {
        instance = this;
        get_map_tile();
        init_tile_size();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void init_tile_size()
    {
        tile_side_len = 0.55f;
        unity_width = tile_side_len * Mathf.Sqrt(3);
        unity_height = tile_side_len * 1.5f;
        mini_distance = tile_side_len * tile_side_len * 0.75f;
    }

    public void generate_grid()
    {
        clear_all_tile();

        init_tile_size();

        if (tile_prefab == null)
            tile_prefab = Resources.Load("prefab/hex_tile_prefab", typeof(GameObject)) as GameObject;


        MeshFilter mf = tile_prefab.gameObject.GetComponent<MeshFilter>();
        MeshRenderer mr = tile_prefab.gameObject.GetComponent<MeshRenderer>();
        Debug.Log(mf.mesh.bounds.size);
        Debug.Log(tile_prefab.gameObject.GetComponent<Renderer>().bounds.size);
        Debug.Log(mr.bounds.size);
        

        for (int i = 0; i < tile_count_x; i++)
        {
            for (int j = 0; j < tile_count_z; j++)
            {
                GameObject obj = (GameObject)Instantiate(tile_prefab);
                obj.transform.parent = transform;
                Vector3 pos = get_tile_center_position(i, j);
                obj.transform.localPosition = pos;
                obj.transform.localRotation = Quaternion.Euler(-90, 0, 90);
#if UNITY_EDITOR
                obj.name = "tile_" + i.ToString() + "," + j.ToString();
#endif
 //               obj.transform.localScale *= 1 * 2;// 1.1628f;

                tile tile_obj = obj.GetComponent<tile>();

                _map_tile[tile_obj.transform.localPosition] = tile_obj;
            }
        }
    }

    public Vector3 get_tile_center_position(int row, int col)
    {
        Vector3 v = new Vector3();
        v.y = 0;

        //  如果是奇数，向右移动半个格子
        if (col % 2 == 0)
            v.x = (unity_width * row);
        else
            v.x = (unity_width * (row + 0.5f));

        v.z = (unity_height * col);
        string str_log = string.Format("row {0} col {1}, {2},{3},{4}", row, col, v.z, v.y, v.z);
        Debug.Log(str_log);
        return v;
    }

    tile get_tile(Vector3 point)
    {
        //  位于矩形网格边线上的三个CELL中心点
        Vector3[] points = new Vector3[3];
        for (int i = 0; i < 3; i++)
        {
            points[i] = new Vector3();
        }
        //当前距离
        float dist;
        float mindist = mini_distance * 100; //一个非常大的值
        int index = 0;//index:被捕获的索引
        //计算出鼠标点位于哪一个矩形网格中
        int cx = (int)(point.x / unity_width);
        int cz = (int)(point.z / unity_height);

        points[0].x = (int)(unity_width * cx);
        points[1].x = (int)(unity_width * (cx + 0.5));
        points[2].x = (int)(unity_width * (cx + 1));
        //根据cy是否是偶数，决定三个点的纵坐标
        if (cz % 2 == 0)
        {
            //偶数时，三个点组成倒立三角
            points[0].y = points[2].y = (int)(unity_height * cz);
            points[1].y = (int)(unity_height * (cz + 1));
        }
        else
        {
            //奇数时，三个点组成正立三角
            points[0].y = points[2].y = (int)(unity_height * (cz + 1));
            points[1].y = (int)(unity_height * cz);
        }

        //现在找出鼠标距离哪一个点最近
        for (int i = 0; i < 3; i++)
        {
            //求出距离的平方
            dist = Vector3.Distance(point, points[i]);
            dist *= dist;

            //如果已经肯定被捕获
            if (dist < mini_distance)
            {
                index = i;
                break;
            }

            //更新最小距离值和索引
            if (dist < mindist)
            {
                mindist = dist;
                index = i;
            }
        }

        return get_tile_by_center_position(points[index]);
    }

    public tile get_tile_by_center_position(Vector3 center)
    {
        return get_map_tile()[center];
    }

    public Dictionary<Vector3, tile> get_map_tile()
    {
        if (_map_tile == null)
            _map_tile = new Dictionary<Vector3, tile>();
 
        return _map_tile;
    }

    public void clear_all_tile()
    {
        Dictionary<Vector3, tile> map_tile = get_map_tile();

        tile[] all_tile = this.GetComponentsInChildren<tile>();
        foreach (tile t in all_tile)
        {
            DestroyImmediate(t.gameObject);
        }

//         foreach(tile t in map_tile.Values)
//         {
//             DestroyImmediate(t.gameObject);
//         }

        _map_tile = new Dictionary<Vector3, tile>();
        
    }

}
