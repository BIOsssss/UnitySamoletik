using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform enemy;

    // Временные промежутки между событиями, кол-во врагов
    public float timeBeforeSpawning = 1.5f;
    public float timeBetweenEnemies = 0.25f;
    public float timeBeforeWaves = 2.0f;
    public int enemiesPerWave = 5;
    private int currentNumberOfEnemies = 0;

    // Переменные для вывода на экран
    private int score = 0;
    private int waveNumber = 0;

    // Ссылки на текстовые объекты
    public UnityEngine.UI.Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

// Появление волн врагов
IEnumerator SpawnEnemies()
{
    // Начальная задержка перед первым появлением врагов
    yield return new WaitForSeconds (timeBeforeSpawning);
    // Когда таймер истекёт, начинаем производить эти действия
    while(true)
    {
        // Не создавать новых врагов, пока не уничтожены старые
        if (currentNumberOfEnemies <= 0)
        {
            float randDirection;
            float randDistance;
            // Создать 10 врагов в случайных местах за экраном
            for (int i = 0; i < enemiesPerWave; i++)
                {
                // Задаём случайные переменные для расстояния и направления
                randDistance = Random.Range(10,25);
                randDirection = 0;
                // Используем переменные для задания координат появления врага
                float posX = this. transform.position.x + (Mathf.Cos((randDirection) * Mathf.Deg2Rad) * randDistance);
                float posY = this. transform.position.y + (Mathf.Sin((randDirection) * Mathf.Deg2Rad) * randDistance);
                // Создаём врага на заданных координатах
                Instantiate (enemy, new Vector3 (posX, posY, 0), this.transform.rotation);
                currentNumberOfEnemies++;
                yield return new WaitForSeconds (timeBetweenEnemies);
            }
        }
        // Ожидание до следующей проверки
        yield return new WaitForSeconds (timeBeforeWaves);
    }
}
// Процедура уменьшения количества врагов в переменной
public void KilledEnemy()
{
    currentNumberOfEnemies--;
}
//+ очки
public void IncreaseScore(int increase)
{
score += increase;
scoreText.text = "Очки: " + score;
}
}


