using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Сколько раз нужно попасть во врага, чтобы уничтожить его
    public int health = 2;

    // Анимация при уничтожении объекта
    public Transform explosion;

    // Переменная для звука во время попадания лазера
    public AudioClip hitSound;

    void OnCollisionEnter2D(Collision2D theCollision)
    {
        //Проверяем коллизию с объектом типа «лазер»
        if(theCollision.gameObject.name.Contains("rocket"))
        {
            RocketScript rocket = theCollision.gameObject.GetComponent("RocketScript") as RocketScript;
            health -= rocket.damage;
            Destroy (theCollision.gameObject);
            // Воспроизвести звук попадания выстрела
            GetComponent<AudioSource>().PlayOneShot(hitSound);
        }
        if (health <= 0)
        {
        
        // Срабатывает при уничтожении объекта
        if(explosion)
        {
            GameObject exploder = ((Transform)Instantiate(explosion, this.transform.position, this.transform.rotation)).gameObject;
            Destroy(exploder, 2.0f);
        }
            Destroy (this.gameObject);
            GameController controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent("GameController") as GameController;
            controller.KilledEnemy();
            controller.IncreaseScore(10);
        }
    }
}
