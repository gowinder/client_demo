// gowinder@hotmail.com
// Assembly-CSharp
// MotionScript.cs
// 2016-05-10-17:41

#region

using UnityEngine;

#endregion

public class MotionScript : MonoBehaviour
{
    public float speed = 3f;
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(-Input.GetAxis("Horizontal")*speed*Time.deltaTime, 0, 0);
    }
}