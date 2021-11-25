// Author: Fatima Nadeem - (Proper comments pending)

using UnityEngine;

public class Player  : MonoBehaviour
{
    Transform leftBound;
    Transform rightBound;
    float x;
    float y;
    Vector2 pos;
    Transform gun;
    bool unlocked;
    
    public GameObject bullet;

    void Start()
    {
        leftBound = transform.parent.Find("Left Bound");
        rightBound = transform.parent.Find("Right Bound");
        y = transform.position.y;
        gun = transform.Find("Gun");
        unlocked = true;
    }

    void Update()
    {
        if (unlocked)
        {
            x = Mathf.Clamp(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            leftBound.position.x,
            rightBound.position.x);
            pos = new Vector2(x, y);
            transform.position = pos;

            if (Input.GetButtonDown("Fire1"))
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        Instantiate(bullet, gun.position, gun.rotation, transform.parent);
    }

    public void Lock()
    {
        unlocked = false;
    }

    public void Unlock()
    {
        unlocked = true;
    }
}
