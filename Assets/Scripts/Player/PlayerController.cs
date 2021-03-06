using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDeathable
{
    private Rigidbody rb_Player;
    private Animator animator_Player;

    [SerializeField] private GameObject bullet, spawnBullet;
    [SerializeField] private int bulletInWeapon, maxBulletInWeapon;
    private int differenceBullet, currentQuantityBullet;

    [SerializeField] private AudioClip audioShot,audioReloadGun, audioPain, audioStep;

    [SerializeField] private float speedPlayer;
    [SerializeField] private bool isReloadWeapon, isRun;

    private Vector3 movement;


    void Start()
    {
        rb_Player = GetComponent<Rigidbody>();
        animator_Player = GetComponent<Animator>();

        transform.rotation = Quaternion.identity;

        currentQuantityBullet = bulletInWeapon;
    }

    void Update()
    {
        bool isDeath = GameController.GetInstance().IsDeathPlayer();
        bool isWin = GameController.GetInstance().IsWinPlayer();

        Shot();
        BoostSpeed();

        if (Input.GetKeyDown(KeyCode.Escape) && !isDeath && !isWin)
        {
            GameController.GetInstance().PauseMode();
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // получаем информацию по вводу игрока
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // придаем движение персонажа
        movement = transform.right * horizontal + transform.forward * vertical;
        rb_Player.MovePosition(transform.position + (movement * speedPlayer / 16));

        // проверка на движение для запуска соответствующих анимаций
        if (horizontal != 0.0f || vertical != 0.0f)
        {
            if (!isRun)
            {
                animator_Player.SetInteger("isWalk", 1);
            }
        }
        else
        {
            animator_Player.SetInteger("isWalk", 0);
        }
    }

    void BoostSpeed() // добавим нашему персонажу возможность увеличить скорость движения
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedPlayer = 3;
            animator_Player.SetInteger("isWalk", 2);
            isRun = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedPlayer = 2;
            animator_Player.SetInteger("isWalk", 0);
            isRun = false;
        }
    }

    void Shot() // стрельба нашего персонажа
    {
        bool isPause = GameController.GetInstance().IsPauseMode();

        if (Input.GetKeyDown(KeyCode.Mouse0) && isReloadWeapon == false && isRun == false && currentQuantityBullet > 0 && !isPause)
        {
            // для луча найдем центр камеры и зададим переменную для хранения информации по попаданию лучем в какой объект
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            // пустим луч и создадим пулю (выстрел)
            if (Physics.Raycast(ray, out hit, 50))
            {
                if (currentQuantityBullet != 0)
                {
                    Instantiate(bullet, spawnBullet.transform.position, spawnBullet.transform.rotation);
                    GameController.GetInstance().PlayAudio(audioShot);
                    currentQuantityBullet -= 1;
                }

                // зададим направление спаунера пули в направлении прицела
                // чтоб пуля летела по лучу
                Vector3 direction = hit.point - spawnBullet.transform.position;
                spawnBullet.transform.rotation = Quaternion.LookRotation(direction);
            }
        }

        ReloadGunStart();
        ReloadGunExit();
    }

    void ReloadGunStart() // если начата перезарядка оружия
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isReloadWeapon = true;
            animator_Player.SetBool("isReload", true);
            GameController.GetInstance().PlayAudio(audioReloadGun);

            // определяем разницу потронов текущих от полной обоимы
            differenceBullet = bulletInWeapon - currentQuantityBullet;
        }
    }

    // так как мне оказались доступными только для чтения анимации
    // поставить event в конце воспроизведения для возврата от анимации нет возможности
    // попробовал реализовать черех Coroutine
    IEnumerator CorReloadGunExit()
    {
        yield return new WaitForSeconds(2.5f);
        isReloadWeapon = false;
        animator_Player.SetBool("isReload", false);

        // возможно, что патронов в магазине больше нет
        if (maxBulletInWeapon == 0)
        {
            Debug.Log("Магазин пуст");
        }

        // вариант, если в магазине у игрока патронов меньше,
        // чем необходимо для заполнения до полной обоимы
        if (differenceBullet > maxBulletInWeapon)
        {
            differenceBullet = maxBulletInWeapon;
            currentQuantityBullet += differenceBullet;
            maxBulletInWeapon -= differenceBullet;
            yield break;
        }

        // проверим разницу и количество доступных патронов
        if (differenceBullet != 0 && maxBulletInWeapon != 0)
        {
            currentQuantityBullet += differenceBullet;
            maxBulletInWeapon -= differenceBullet;
        }

        differenceBullet = 0;
    }

    void ReloadGunExit() // если перезарядка оружия окончена
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            StartCoroutine(CorReloadGunExit());
        }
    }

    //для вызова в MainUI
    public int GetCurrentBullet()
    {
        return currentQuantityBullet;
    }
    //для вызова в MainUI
    public int GetMaxBullet()
    {
        return maxBulletInWeapon;
    }

    //для подбора патронов
    public int GetMaxBullet(int bullet)
    {
        maxBulletInWeapon += bullet;
        return maxBulletInWeapon;
    }

    public void Death()
    {
        bool isDeath = GameController.GetInstance().IsDeathPlayer();

        if (!isDeath)
        {
            GameController.GetInstance().DeathPlayer();
        }
    }

    // на случай получения урона от огненого шара
    public void PlayAudioPain()
    {
        GameController.GetInstance().PlayAudio(audioPain);
    }

    public void PlayAudioStep()
    {
        GameController.GetInstance().PlayAudio(audioStep);
    }

    void IDeathable.Kill()
    {
        Death();
    }
}
