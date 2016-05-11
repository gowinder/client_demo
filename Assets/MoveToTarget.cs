// gowinder@hotmail.com
// Assembly-CSharp
// MoveToTarget.cs
// 2016-05-10-17:41

#region

using UnityEngine;

#endregion

public class MoveToTarget : MonoBehaviour
{
    private Transform end;

    private Transform start;
    // Use this for initialization
    private void Start()
    {
        start = GameObject.Find("Cube").GetComponent<Transform>();
        end = GameObject.Find("GameObject").GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = Vector3.Lerp(start.position, end.position, Time.deltaTime);
    }
}