using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomber : MonoBehaviour
{
    //Создаем переменные снаряд (булет), позиции откуда будет лететь снаряд, и переодичность выстрела
    public GameObject bullet;
    public Transform shoot;
    public float timeShot;
    // Start is called before the first frame update
    void Start()
    {
        //Задали точку откуда будет пускаться снаряд (позиция пчелы)
        shoot.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        //Корутина которая отвечает за переодичные выстрелы
        StartCoroutine(Shooting());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(timeShot);
        //создаем метод которые будет генерировать обьект(снаряд)
        //Принимает аргументы (ГеймОбжект(снаряд-булет), расположение снаряда где он будет генерироваться, ореинтация (угол наклона) снаряда(в нашем случае = пчела))
        Instantiate(bullet, shoot.transform.position, transform.rotation);
       
        StartCoroutine(Shooting());
    }
}
