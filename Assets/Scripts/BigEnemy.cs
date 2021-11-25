// Author: Fatima Nadeem - (Proper comments pending)

using System.Collections;
using UnityEngine;

public class BigEnemy : MonoBehaviour
{
    bool unlocked;
    Transform gun;
    RaycastHit2D hit;

    bool playerVisible;
    bool allowAim;
    bool allowDamage;
    bool firing;
    
    Transform bottomBound;

    public Vector2 halfWidthHeight;

    public LineRenderer lazer;
    public int speed;
    public float fireRateGap;
    public int criticalDamage;

    void Start()
    {
        unlocked = true;

        gun = transform.Find("Gun");
        lazer = gun.transform.Find("Lazer").GetComponent<LineRenderer>();
        lazer.enabled = false;
        allowAim = true;
        allowDamage = true;
        firing = false;

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

            if (GetComponent<Renderer>().isVisible)
            {
                hit = Physics2D.Raycast(gun.position, Vector2.down);

                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        playerVisible = true;
                        StartCoroutine(AimAndFire());
                    }
                    else
                    {
                        playerVisible = false;
                        if (firing)
                        {
                            StopFiring();
                        }
                    }
                }
                else
                {
                    playerVisible = false;
                    if (firing)
                    {
                        StopFiring();
                    }
                }
            } 
        }
    }

    IEnumerator AimAndFire()
    {
        lazer.SetPosition(0, gun.position);
        lazer.SetPosition(1, hit.point);

        if (allowAim)
        {
            allowAim = false;
            yield return new WaitForSeconds(fireRateGap * 2);
            if (playerVisible)
            {
                StartFiring();
            }
            else
            {
                allowAim = true;
            }
        }

        if (allowDamage)
        {
            allowDamage = false;
            if (playerVisible)
            {
                hit.collider.GetComponent<PlayerHealth>().ReduceHealth();
                yield return new WaitForSeconds(fireRateGap);
            }
            allowDamage = true;
        }
    }

    void StartFiring()
    {
        firing = true;
        AudioManager.instance.Play("Large Enemy Beam");
        lazer.enabled = true;
    }

    void StopFiring()
    {
        firing = false;
        AudioManager.instance.Stop("Large Enemy Beam");
        lazer.enabled = false;
        allowAim = true;
    }

    public void Lock()
    {
        unlocked = false;

        if (firing)
        {
            AudioManager.instance.Stop("Large Enemy Beam");
        }
    }

    public void Unlock()
    {
        unlocked = true;

        if (firing)
        {
            AudioManager.instance.Play("Large Enemy Beam");
        }
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
