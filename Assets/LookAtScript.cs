// gowinder@hotmail.com
// Assembly-CSharp
// LookAtScript.cs
// 2016-05-10-17:41

#region

using UnityEngine;

#endregion

public class LookAtScript : MonoBehaviour
{
    public Transform target;
    // Use this for initialization
    private void Start()
    {
        target = GameObject.Find("Sphere").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        var relativePos = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos);
    }
}