// Author: Fatima Nadeem - (Proper comments pending)

using System.Collections;
using UnityEngine;

public class MediumEnemy : MonoBehaviour
{
    bool unlocked;
    Transform gun1;
    Transform gun2;
    bool allowFire;
    RaycastHit2D hit1;
    RaycastHit2D hit2;

    Transform bottomBound;

    public Vector2 halfWidthHeight;

    public GameObject enemyBullet;
    public int speed;
    public float fireRateGap;
    public int criticalDamage;

    void Start()
    {
        gun1 = transform.Find("Gun1");
        gun2 = transform.Find("Gun2");
        unlocked = true;
        allowFire = true;
        halfWidthHeight = GetComponent<SpriteRenderer>().bounds.extents;
        bottomBound = transform.parent.Find("Bottom Bound");
    }

    void Update()
    {
        if ((transform.position.y + halfWidthHeight.y) < bottomBound.position.y)
        {
            Destroy(gameObject);
        }

        if (unlocked)
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);

            if (!GetComponent<Renderer>().isVisible)
            {
                return;
            }

            hit1 = Physics2D.Raycast(gun1.position, Vector2.down);
            hit2 = Physics2D.Raycast(gun2.position, Vector2.down);

            if ((hit1.collider != null))
            {
                if ((hit1.collider.CompareTag("Player")) && allowFire)
                {
                    StartCoroutine(Fire());
                }
            }
            else if ((hit2.collider != null))
            {
                if ((hit2.collider.CompareTag("Player")) && allowFire)
                {
                    StartCoroutine(Fire());
                }
            }
        }     
    }

    IEnumerator Fire()
    {
        allowFire = false;
        Instantiate(enemyBullet, gun1.position, gun1.rotation, transform.parent);
        Instantiate(enemyBullet, gun2.position, gun2.rotation, transform.parent);
        yield return new WaitForSeconds(fireRateGap);
        allowFire = true;
    }

    public void Lock()
    {
        unlocked = false;
    }

    public void Unlock()
    {
        unlocked = true;
        allowFire = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().
                ReduceHealth(criticalDamage);
            AudioManager.instance.Play("Enemy Hits Player");
        }

        Destroy(gameObject);
    }
}
