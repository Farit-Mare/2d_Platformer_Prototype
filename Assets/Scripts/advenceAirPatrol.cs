using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class advenceAirPatrol : MonoBehaviour
{
    public Transform[] points;
    public float speed;
    public float waitTime;
    bool canGo = true;
    int y = 1;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(points[0].position.x, points[0].position.y, transform.position.z);   
    }

    // Update is called once per frame
    void Update()
    {
        if (canGo)
            /*Метод МувЕовард содержит 3 аргумента. Начальное положение, конечное положение и скорость передвижения
             * Присваевам этот метод нашему персонажу (врагу)
             * */
            transform.position = Vector3.MoveTowards(transform.position, points[y].position, speed * Time.deltaTime);

        if (transform.position == points[y].position)
        {
            //условие для избежания переполнения
            if (y < points.Length - 1)
                y++;
            else
                y = 0;
            canGo = false;
            StartCoroutine(Waiting());
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(waitTime);
        canGo = true;
    }
}

