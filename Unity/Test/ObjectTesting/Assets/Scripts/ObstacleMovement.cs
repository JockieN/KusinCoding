using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{

    public float speed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(PingPong(Time.time * speed, -5, 5), transform.position.y, transform.position.z);

        if (Input.GetKey(KeyCode.LeftShift))
        {   if (speed != 0)
                speed = 0;
            else
                speed = 3;
        }
    }

    float PingPong(float t, float minLength, float maxLength)
    {
        return Mathf.PingPong(t, maxLength - minLength) + minLength;
    }
}
