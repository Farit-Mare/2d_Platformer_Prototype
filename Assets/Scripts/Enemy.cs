using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool isHit = false;
    public GameObject drop;

    /*3 ������ ���������� �� ������������ 
     ��� ��������� � ���������� �������� ������, ������� ���������
    ��������� � ������� ���������� ������������
    1) ����������� ��� ������ ��������
    2) ��� ���������� ���������� � ��������
    3) ��� ����������� ��������*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isHit)
        {
            //���������� � ������� ������������, ����� ��������� ������(������) � �� ���� ����� ����� ��������� ������� ������������� ��
            collision.gameObject.GetComponent<PLayer>().RecountHp(-1);

            //������ ����������� - ���������� ������� - ��� ������� ����� ��� ������������ ������ � ������
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 4f, ForceMode2D.Impulse);
        }
    }

    /*private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("hey, u staying on shit");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("herewego!");
        }
    }*/ 

    public IEnumerator death()
    {
        if (drop != null)
            Instantiate(drop, transform.position, Quaternion.identity);
        isHit = true;
        GetComponent<Animator>().SetBool("death", true);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = false;
        transform.GetChild(1).GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public void startDeath()
    {
        StartCoroutine(death());
    }

}
