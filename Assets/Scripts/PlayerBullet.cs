// Author: Fatima Nadeem - (Proper comments pending)

using UnityEngine;

public class PlayerBullet : MonoBehaviour
{  
    public float speed = 20f;

    Rigidbody2D rb;
    EnemyHealth target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = Vector2.up * speed;
        AudioManager.instance.Play("Player Fire");
    }

    void Update()
    {
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        target = collision.GetComponent<EnemyHealth>();

        if (target != null)
        {
            target.ReduceHealth();
        }

        Destroy(gameObject);
    }
}
