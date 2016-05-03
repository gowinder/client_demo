using UnityEngine;
using System.Collections;


public enum em_tile_geography_type
{
    TGT_ROAD = 1,
}

public class tile : MonoBehaviour
{
    public int line;
    public int row;
    
    public bool walkable = true;
    public em_tile_geography_type geograpthy_type;


    public Transform _my_trans;
    

    // Use this for initialization
    void Start()
    {
//        _my_trans = this.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
