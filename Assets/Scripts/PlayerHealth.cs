// Author: Fatima Nadeem - (Proper comments pending)

using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    Health playerHealth;
    Slider slider;

    void Start()
    {
        playerHealth = GetComponent<Health>();

        slider = transform.parent.Find("Health Bar").GetComponent<Slider>();
        slider.maxValue = playerHealth.lives;
        slider.value = playerHealth.lives;
    }

    public void ReduceHealth(int damage = 1)
    {
        playerHealth.ReduceHealth(damage);

        if (slider.value < damage)
        {
            slider.value = 0;
        }
        else
        {
            slider.value -= damage;
        }

        if (playerHealth.lives <= 0)
        {
            Destroy(gameObject);
            transform.parent.GetComponent<PauseManager>().GameOver();
            // play game over/ player dead sound
            // player death animation
        }
    }
}
