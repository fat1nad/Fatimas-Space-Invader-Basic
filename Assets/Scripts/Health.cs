// Author: Fatima Nadeem - (Proper comments pending)

using UnityEngine;

public class Health : MonoBehaviour
{
    public int lives;

    float visibleHealth;
    float decrement;
    SpriteRenderer rend;
    
    void Start()
    {
        decrement = 1f / lives;
        visibleHealth = 1f;
        rend = GetComponent<SpriteRenderer>();
    }

    public void ReduceHealth(int damage = 1)
    {
        if (lives < damage)
        {
            lives = 0;
            visibleHealth = 0;
        }
        else
        {
            lives -= damage;
            visibleHealth -= decrement * damage;
        }
        
        rend.color = new Color(1f, visibleHealth, visibleHealth);
    }
}
