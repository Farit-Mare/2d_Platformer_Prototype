using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    float length, startposition;
    public GameObject _camera;
    public float parallaxEffect;
    void Start()
    {
        startposition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float _temperature = _camera.transform.position.x * (1 - parallaxEffect);
        float _distantion = _camera.transform.position.x * parallaxEffect;

        transform.position = new Vector3(startposition + _distantion, transform.position.y, transform.position.z);

        if (_temperature > startposition + length)
            startposition += length;
        else if (_temperature < startposition - length)
            startposition -= length;

    }
}
