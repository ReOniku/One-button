using UnityEngine;
using System.Collections;

public class FlappyRotateBehaviour : MonoBehaviour
{
    public float speed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(Time.time * speed, 90, 0);
    }
}
