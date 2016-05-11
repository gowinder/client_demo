// gowinder@hotmail.com
// Assembly-CSharp
// tile.cs
// 2016-05-10-17:45

#region

using UnityEngine;

#endregion

public enum em_tile_geography_type
{
    TGT_ROAD = 1
}

public class tile : MonoBehaviour
{
    public Transform _my_trans;
    public em_tile_geography_type geograpthy_type;
    public int line;
    public int row;

    public bool walkable = true;


    // Use this for initialization
    private void Start()
    {
//        _my_trans = this.transform;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}