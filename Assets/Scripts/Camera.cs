using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Присваеваем значения скороксти камеры (спид) и цели за которой будет седить камера (таргет)
    
    float speed = 4f;
    public Transform target;
    void Start()
    {
        //устанвливаем начальное полоэение камеры (т.к. игра 2д, ось З остается изначальной и не отталкивается от Таргет)

        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }

    void Update()
    {
        /*Создали переменую Посишн типа Вектор3 и присвоили ей значение позиции нашей цели Таргет.
         Зафиксировали положение камеры по оси З в изначальном положении.
        К позиции камеры присвоили специальный метод, который позволит камере двигаться пладвно (начальная точка камеры,
        конечная точка камеры(позция игрока), скорость движеня камеры)
        */
        Vector3 position = target.position;
        position.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }
}
