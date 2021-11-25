// Author: Fatima Nadeem - (Proper comments pending)

using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 20f;

    Rigidbody2D rb;
    PlayerHealth target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = Vector2.down * speed;
        AudioManager.instance.Play("Small Enemy Fire");
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
        target = collision.GetComponent<PlayerHealth>();

        if (target != null)
        {
            target.ReduceHealth();
        }

        Destroy(gameObject);
    }
}
