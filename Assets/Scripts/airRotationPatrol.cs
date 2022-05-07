using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class airRotationPatrol : MonoBehaviour
{
    public Transform pointOne;
    public Transform pointTwo;
    public float speed;
    public float waitTime;
    bool canGo = true;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(pointOne.position.x, pointOne.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        if (canGo)
            /*Метод МувЕовард содержит 3 аргумента. Начальное положение, конечное положение и скорость передвижения
             * Присваевам этот метод нашему персонажу (врагу)
             * */
            transform.position = Vector3.MoveTowards(transform.position, pointOne.position, speed * Time.deltaTime);

        if (transform.position == pointOne.position)
        {
            Transform t = pointOne;
            pointOne = pointTwo;
            pointTwo = t;
            canGo = false;
            StartCoroutine(Waiting());
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(waitTime);
        //условия поворота
        if(transform.rotation.y == 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        } else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        canGo = true;
    }
}