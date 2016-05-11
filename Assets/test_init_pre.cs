// gowinder@hotmail.com
// Assembly-CSharp
// test_init_pre.cs
// 2016-05-10-17:45

#region

using UnityEngine;

#endregion

public class test_init_pre : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        for (var y = 0; y < 5; y++)
        {
            for (var x = 0; x < 5; x++)
            {
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.AddComponent<Rigidbody>();
                cube.transform.position = new Vector3(x, y, 0);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}