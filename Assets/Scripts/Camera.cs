using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // ����������� �������� ��������� ������ (����) � ���� �� ������� ����� ������ ������ (������)
    
    float speed = 4f;
    public Transform target;
    void Start()
    {
        //������������ ��������� ��������� ������ (�.�. ���� 2�, ��� � �������� ����������� � �� ������������� �� ������)

        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }

    void Update()
    {
        /*������� ��������� ������ ���� ������3 � ��������� �� �������� ������� ����� ���� ������.
         ������������� ��������� ������ �� ��� � � ����������� ���������.
        � ������� ������ ��������� ����������� �����, ������� �������� ������ ��������� ������� (��������� ����� ������,
        �������� ����� ������(������ ������), �������� ������� ������)
        */
        Vector3 position = target.position;
        position.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }
}
