using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject[] block;
    public Sprite buttonDown;
    internal bool interactable;

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
        if (collision.gameObject.tag == "MarkBox")
        {
            GetComponent<SpriteRenderer>().sprite = buttonDown;
            GetComponent<CircleCollider2D>().enabled = false;
            foreach  (GameObject blockObjects in block)
            {
                Destroy(blockObjects);
            }
        }
    }
}
