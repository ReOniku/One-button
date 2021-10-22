using UnityEngine;
using System.Collections;

public class FlappyCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public static FlappyCamera instance;
    public float f;
    public float deltaTimeRef;
    //private Vector3 _speed;

    private void Start()
    {
        instance = this;
        offset = transform.position - target.position;
        //_speed = Vector3.zero;
    }

    private void Update()
    {
        var ideaPos = target.position + offset;
        var factorDeltaTime = Time.deltaTime / deltaTimeRef;
        var c = f * factorDeltaTime;
        transform.position = Vector3.Lerp(transform.position, ideaPos, c);
    }

    public void SyncPos()
    {
        transform.position = target.position + offset;
        //_speed = Vector3.zero;
    }
}
