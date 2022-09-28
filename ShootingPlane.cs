using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootingPlane : MonoBehaviour
{
    //Позиция стрельбы
    [SerializeField] private Transform shootPosition;
    //Пуля
    [SerializeField] private GameObject rocket;
    //Выстрел
    private bool canShoot = true;
    //Кнопка для стрельбы
    public KeyCode shootButton;

    //Звук стрельбы
    public AudioSource ShootSound;

    //Стреляем с промежутком
    private IEnumerator Shoot()
    {
        //Еще не выстрелили
        canShoot = false;
        //Процесс движения пули
        Instantiate(rocket, shootPosition.position, transform.rotation);
        //Звук выстрела
        ShootSound.Play();
        //Стреляем раз в секунду
        yield return new WaitForSeconds(1f);
        //Выстрелили
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {   
        //Если нажата кнопка
        if (Input.GetKey(shootButton) && canShoot)
        {
            //Вызываем функцию стрельбы
            StartCoroutine(Shoot());
        }
    }
}
