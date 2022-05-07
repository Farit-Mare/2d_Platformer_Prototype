using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    //переменная для скрытия отыгранных буллетов (снарядов) 
    //Скрытие менее ресурсоемкое чем удаление
    float TimeToDisable = 10f;
    void Start()
    {
        StartCoroutine(SetDiasabled());
    }
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    IEnumerator SetDiasabled()
    {
        yield return new WaitForSeconds(TimeToDisable);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(SetDiasabled());
        gameObject.SetActive(false);
    }

}
