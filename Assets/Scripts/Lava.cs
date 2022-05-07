using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    float timer = 0f;
    float timerHit = 0f;


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 4f)
        {
            timer = 0;
            transform.localScale = new Vector3(-1f, 1f, 1f);
            transform.position = new Vector3(176f, transform.position.y, transform.position.z);

        }
        else if (timer >= 2f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            transform.position = new Vector3(0.9f, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PLayer>().isGrouded = false;
            collision.GetComponent<PLayer>().inWater = true;
            timerHit += Time.deltaTime;
            if (timerHit >= 0.1f)
            {
                collision.gameObject.GetComponent<PLayer>().RecountHp(-3);
                timerHit = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PLayer>().isGrouded = true;
            collision.GetComponent<PLayer>().inWater = false;
            timerHit = 0;
        }
    }
}
