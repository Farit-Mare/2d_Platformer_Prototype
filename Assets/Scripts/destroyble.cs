using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyble : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 4f, ForceMode2D.Impulse);
            gameObject.GetComponentInParent<Enemy>().startDeath();
            gameObject.GetComponentInParent<Rigidbody2D>().AddForce(transform.up * 6f, ForceMode2D.Impulse);
        }
    }
}
