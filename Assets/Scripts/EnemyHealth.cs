// Author: Fatima Nadeem - (Proper comments pending)

using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Health enemyHealth;

    public int awardScore = 5;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponent<Health>();
    }

    public void ReduceHealth(int damage = 1)
    {
        enemyHealth.ReduceHealth(damage);

        if (enemyHealth.lives <= 0)
        {
            ScoreManager.instance.AddScore(awardScore);
            if (GetComponent<BigEnemy>() != null)
            {
                AudioManager.instance.Stop("Large Enemy Beam");
            }
            Destroy(gameObject);
            // play enemy dead sound
            // enemy death animation
        }
    }
}
