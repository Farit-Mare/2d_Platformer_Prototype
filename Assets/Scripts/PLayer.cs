using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayer : MonoBehaviour
{
    //Присвоили переменную в класс РигБоди
    Rigidbody2D rb;
    public float speed;
    public float jumpHeihgt;
    public Transform groundChek;
    public bool isGrouded;
    Animator anim;
    int curHp;
    int maxHp = 3;
    bool isHit = false;
    public Main main;
    public bool key = false;
    bool canTelepot = true;
    public bool inWater = false;
    bool isClimbing = false;
    int coins = 0;
    bool canTakeDamage = true;
    public GameObject blueGem, greenGem;
    int gemCount = 0;

    // Start метод вызывается 1 раз перед запуском сцены
    void Start()
    {
        // присвоили в рб РигБоди нашего персонажа
        rb = GetComponent<Rigidbody2D>();

        // присвоили класс Аниматор нашего персонажа
        anim = GetComponent<Animator>();

        //указали что в начале игры Текущее кол-во ХП всегда максимально
        curHp = maxHp;

    }

    // Update метод-фреймрейт вызывается н(зависит от фреймрейта) кол-во раз в секунду
    void Update()
    {
        if (inWater && !isClimbing)
        {
            anim.SetInteger("State", 4);
            if (Input.GetAxis("Horizontal") != 0)
                Flip();
        }
        else
        {

            ChekGround();

            if (Input.GetAxis("Horizontal") == 0 && (isGrouded) && !isClimbing)
            {
                anim.SetInteger("State", 1);
            }
            else
            {
                Flip();
                if (isGrouded && !isClimbing)
                    anim.SetInteger("State", 2);
            }
        }

        /* Если прожата(ГетКейДаун) клавиша(КейКод) пробел(Спей), то персонаж (физ свойста рб) получает силу (Адфорс)
        в нправлении вверх(трансворм ап * джампхейт(сила прыжка)). Сила имеет 2д краткосрочный тип (ФорсМод2д. Импульс)
        УПД - добавили условеи исГраундид (проверка на нахождение персонажа на какой либо поверностИ)
        */
        if (Input.GetKeyDown(KeyCode.Space) && isGrouded)
            rb.AddForce(transform.up * jumpHeihgt, ForceMode2D.Impulse);

    }

    // FixedUpdate метод для работы с физ движком
    private void FixedUpdate()
    {
        /* ГетАксис Горизонтал отвечает за движение <- -> и с помощью Инпут отправляет информацию.
        Если нажимаем <- то значние -1, если -> то значение 1, если ничео не нажимем то 0.
        Весь этот процеес проходит в классе Вектор 2 которые отвечает за движения и присвается
        в велосити (скорость), расположенному в физеческом компоненте РигБоди (рб).
        Значение Спид позволяет задвать скорость персонажа, т.е. если спид = 5,
        то персонаж двигаеться со скоростью 5 клеток.
        При нажатии <- (-1*5) - персонаж двинулся на 5 клеток влево (аналогично вправо).
        Напровление по оси У нас не интересует, поэтому оставляем по умполчанию.
        */

        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);



    }

    void Flip()
    {
        /* трансформ.локалротэйшн отвечает за поворот персонажа. 
         т.к. нас интересует поворот только по оси Х, работаем исключительно с ней.
        Когда мы нажимаем <- значение ГетАксис равно -1, тогда мы передаем значение Еулер равное 180градусов в трансофрм
        для того что бы персонаж развернулся на 180 градусов, т.е. посмотрел в лево. (Аналагично вправо)
         */
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    void ChekGround()
    {
        /* Мы создали круг (ОверлапСекрлОл) с центром в пустом обьекте(граундЧек) и радиусом 0,2, который помещаеться в
         * массив (колайдерс)
         * В булевую переменную исГраундед присвоили наличие в созданном круге больше 1 объетка 
         * (в круге всегда 1 объект - персонаж), если условия верны, то возвращает тру и
         * сздается условия для прыжка (прожатия пробела)
         * УПД: добавили условия (если не на земле проигрывать анимацию прыжка)
         */
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundChek.position, 0.2f);
        isGrouded = colliders.Length > 1;
        if (!isGrouded && !isClimbing)
            anim.SetInteger("State", 3);
    }

    //Создаем метод для пересчёта жизней (что бы отнимать или прибавлять ХП)
    public void RecountHp(int deltaHp)
    {

        if (deltaHp < 0 && canTakeDamage)
        {
            curHp = curHp + deltaHp;
            canTakeDamage = false;
            isHit = true;
            StartCoroutine(OnHit());
        }
        if (deltaHp > 0)
            curHp = curHp + deltaHp;
        if (curHp > maxHp)
        {
            curHp = maxHp;
        }

        //когда курХп = 0, капсулКолайдер, который отвечает за стояние на "земле" деактивируеться и игрок проваливается (умирает)
        if (curHp <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            //Метод Инвок позволяет выполнить указанный метод с указанной здержкой (Луз сработает через 0.5 секунды)
            Invoke("Lose", 0.5f);
        }
    }


    // Создаем карутину. Карутина - метод с временными промежуткаим
    IEnumerator OnHit()
    {
        if (isHit)
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g - 0.08f, GetComponent<SpriteRenderer>().color.b - 0.08f);
        else
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g + 0.08f, GetComponent<SpriteRenderer>().color.b + 0.08f);


        if (GetComponent<SpriteRenderer>().color.g <= 0)
            isHit = false;
        if (GetComponent<SpriteRenderer>().color.g == 1f)
        {
            canTakeDamage = true;
            yield break;
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(OnHit());
    }

    //Создаем новый метод который обращается в методу Луз в скрипте мейн для использования в Инвок, т.к. Инвок не может юзать методы из других скриптов
    void Lose()
    {
        main.GetComponent<Main>().GameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "key")
        {
            Destroy(collision.gameObject);
            key = true;
            print("I have akey");
        }

        if (collision.gameObject.tag == "door")
        {
            if (collision.gameObject.GetComponent<door>().isOpen && canTelepot)
            {
                collision.gameObject.GetComponent<door>().Teleport(gameObject);
                canTelepot = false;
                StartCoroutine(TeleportWait());
            }
            else if (key)
                collision.gameObject.GetComponent<door>().Unlock();
        }

        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            coins++;
        }

        if (collision.gameObject.tag == "Heart")
        {
            Destroy(collision.gameObject);
            RecountHp(1);
        }

        if (collision.gameObject.tag == "poisonMushroom")
        {
            Destroy(collision.gameObject);
            RecountHp(-1);
        }

        if (collision.gameObject.tag == "BlueGem")
        {
            Destroy(collision.gameObject);
            StartCoroutine(CantTakeDamage());
        }

        if (collision.gameObject.tag == "GreenGem")
        {
            Destroy(collision.gameObject);
            StartCoroutine(SuperSpeed());
        }
    }

    IEnumerator TeleportWait()
    {
        yield return new WaitForSeconds(0.5f);
        canTelepot = true;
    }

    //Метод взаимодействия с лесницей
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ladder")
        {
            isClimbing = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
            if (Input.GetAxis("Vertical") == 0)
            {
                anim.SetInteger("State", 5);
            }
            else
            {
                anim.SetInteger("State", 6);
                transform.Translate(Vector3.up * Input.GetAxis("Vertical") * speed * 0.5f * Time.deltaTime);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isClimbing = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    //анимация батуда
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "trampoline")
            StartCoroutine(TrampolineAnim(collision.gameObject.GetComponentInParent<Animator>()));
    }

    IEnumerator TrampolineAnim(Animator trampAnim)
    {
        trampAnim.SetBool("isJump", true);
        yield return new WaitForSeconds(0.1f);
        trampAnim.SetBool("isJump", false);
    }

    IEnumerator CantTakeDamage()
    {
        gemCount++;
        blueGem.SetActive(true);
        ChekGems(blueGem);
        canTakeDamage = false;
        blueGem.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(4f);
        StartCoroutine(GemsInvis(blueGem.GetComponent<SpriteRenderer>(), 0.02f));
        yield return new WaitForSeconds(1f);
        print("Invincibility is active");
        canTakeDamage = true;
        print("Invincibility is deactive");
        gemCount--;
        blueGem.SetActive(false);
        ChekGems(greenGem);
    }

    IEnumerator SuperSpeed()
    {
        gemCount++;
        greenGem.SetActive(true);
        ChekGems(greenGem);
        speed = speed * 2;
        greenGem.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        print("Speed bonus is active");
        yield return new WaitForSeconds(4f);
        StartCoroutine(GemsInvis(greenGem.GetComponent<SpriteRenderer>(), 0.02f));
        yield return new WaitForSeconds(1f);
        speed = speed / 2;
        gemCount--;
        print("Speed bonus is deactive");
        greenGem.SetActive(false);
        ChekGems(blueGem);
    }

    void ChekGems(GameObject gem)
    {
        if (gemCount == 1)
            gem.transform.localPosition = new Vector3(0f, 0.7f, gem.transform.localPosition.z);
        else if (gemCount == 2)
        {
            blueGem.transform.localPosition = new Vector3(-0.5f, 0.5f, blueGem.transform.localPosition.z);
            greenGem.transform.localPosition = new Vector3(0.5f, 0.5f, greenGem.transform.localPosition.z);
        }

    }

    IEnumerator GemsInvis(SpriteRenderer spr, float time)
    {
        spr.color = new Color(1f, 1f, 1f, spr.color.a - time * 2);
        yield return new WaitForSeconds(time);
        if (spr.color.a > 0)
            StartCoroutine(GemsInvis(spr, time));
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bridge")
            this.transform.parent = collision.transform;

        else
            this.transform.parent = null;
    }

    public int GetCoins()
    {
        return coins;
    }

    public int GetHP()
    {
        return curHp;
    }

}
