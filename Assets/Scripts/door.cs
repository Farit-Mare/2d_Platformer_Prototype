using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public bool isOpen = false;
    public Transform _door;
    public Sprite mid, top;

    public void Unlock()
    {
        isOpen = true;
        GetComponent<SpriteRenderer>().sprite = mid;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = top;
    }

    public void Teleport(GameObject player)
    {
        player.transform.position = new Vector3(_door.transform.position.x, _door.transform.position.y,player.transform.position.z);
    }
}
