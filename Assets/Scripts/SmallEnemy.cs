// Author: Fatima Nadeem - (Proper comments pending)

using System.Collections;
using UnityEngine;

public class SmallEnemy : MonoBehaviour
{
    bool unlocked;
    Transform gun;
    bool allowFire;
    RaycastHit2D hit;
    float halfHeight;

    Transform bottomBound;

    public Vector2 halfWidthHeight;

    public GameObject enemyBullet;
    public int speed;
    public float fireRateGap;
    public int criticalDamage;

    void Start()
    {
        gun = transform.Find("Gun");
        unlocked = true;
        allowFire = true;
        halfWidthHeight = GetComponent<SpriteRenderer>().bounds.extents;
        bottomBound = transform.parent.Find("Bottom Bound");
    }

    void Update()
    {
        if ((transform.position.y + halfHeight) < bottomBound.position.y)
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

            hit = Physics2D.Raycast(gun.position, Vector2.down);

            if (hit.collider != null)
            {
                if ((hit.collider.CompareTag("Player")) && allowFire)
                {
                    StartCoroutine(Fire());
                }
            }
        }  
    }

    IEnumerator Fire()
    {
        allowFire = false;
        Instantiate(enemyBullet, gun.position, gun.rotation, transform.parent);
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
