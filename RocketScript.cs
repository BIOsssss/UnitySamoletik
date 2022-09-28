using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    private float speed = 7f;

    public int damage = 1;

    private void Start() 
    {
        StartCoroutine(Death());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        Destroy(gameObject);
    }
}
