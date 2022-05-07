using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayControlBridge : MonoBehaviour
{
    public Transform[] points;
    public float speed = 1f;
    int i = 1;
    void Start()
    {
        transform.position = new Vector3(points[0].position.x, points[0].position.y, transform.position.z);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           
            transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);

            if(transform.position == points[i].position)
            {
                if (i < points.Length - 1)
                    i++;
                else
                    i = 0;
            }
        }
    }
}
