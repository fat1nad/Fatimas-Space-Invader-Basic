// Author: Fatima Nadeem - (Proper comments pending)

using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject player;

    GameObject playerInstance;
    Player playerComp;
    Transform playerStartPoint;
    EnemySpawner enemySpawner;
    bool unfrozen;
    bool paused;
    GameObject pauseScreen;
    GameObject pauseScreenRest;
    GameObject pauseSettingsScreenBackBtn;
    GameObject gameOverScreen;

    void Start()
    {
        playerStartPoint = transform.Find("Player Start Point");

        PlayerSetUp();

        enemySpawner = transform.Find("Up Bound").GetComponent<EnemySpawner>();
        // name change of up bound

        pauseScreen = transform.Find("Pause Screen").gameObject;
        pauseSettingsScreenBackBtn = pauseScreen.transform
            .Find("Pause Settings Screen Back Button").gameObject;
        pauseScreenRest = pauseScreen.transform
            .Find("Pause Screen Rest").gameObject; //name change
        gameOverScreen = transform.Find("Game Over Screen").gameObject;

        unfrozen = true;
        paused = false;
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && unfrozen)
        {
            Pause();
        }
    }

    public void Pause()
    {
        paused = true;
        
        Freeze();

        pauseSettingsScreenBackBtn.SetActive(false);
        pauseScreenRest.SetActive(true); //name change
        pauseScreen.SetActive(true);
    }

    public void Unpause()
    {
        paused = false;
        
        Unfreeze();
        pauseScreen.SetActive(false);
    }

    public void GoToPauseSettings()
    {
        pauseScreenRest.SetActive(false); // name change
        ScreenManager.instance.settings.SetActive(true);
        pauseSettingsScreenBackBtn.SetActive(true);
    }

    public void ClosePauseSettings()
    {
        ScreenManager.instance.settings.SetActive(false);
        pauseSettingsScreenBackBtn.SetActive(false);
        pauseScreenRest.SetActive(true); // name change
    }

    public void GoToMainMenu() // can be removed
    {
        Reset();
        ScreenManager.instance.GoToMainMenu();
    }

    public void GameOver()
    {
        ScoreManager.instance.UpdateHighScore();
        Freeze();
        AudioManager.instance.Play("Game Over");
        gameOverScreen.SetActive(true);
    }

    void Freeze()
    {
        unfrozen = false;

        AudioManager.instance.Stop("BG Music");

        if (player != null)
        {
            playerComp.Lock();
        }

        enemySpawner.Lock();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            // streamline enemy components
            
            SmallEnemy sE = enemy.GetComponent<SmallEnemy>();
            if (sE != null)
            {
                sE.Lock();
            }

            MediumEnemy mE = enemy.GetComponent<MediumEnemy>();
            if (mE != null)
            {
                mE.Lock();
            }

            BigEnemy lE = enemy.GetComponent<BigEnemy>();
            if (lE != null)
            {
                lE.Lock();
            }
        }

        Time.timeScale = 0; // Freezing bullets
    }

    void Unfreeze()
    {
        AudioManager.instance.Play("BG Music");

        if (player != null)
        {
            playerComp.Unlock();
        }

        enemySpawner.Unlock();

        // streamline enemy components

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            SmallEnemy sE = enemy.GetComponent<SmallEnemy>();
            if (sE != null)
            {
                sE.Unlock();
            }

            MediumEnemy mE = enemy.GetComponent<MediumEnemy>();
            if (mE != null)
            {
                mE.Unlock();
            }

            BigEnemy lE = enemy.GetComponent<BigEnemy>();
            if (lE != null)
            {
                lE.Unlock();
            }
        }

        Time.timeScale = 1;

        unfrozen = true;
    }


    void PlayerSetUp()
    {
        playerInstance = Instantiate(player, playerStartPoint.position, 
            playerStartPoint.rotation, transform);
        playerInstance.name = "Player";
        playerComp = playerInstance.GetComponent<Player>();
    }
    
    public void Reset()
    {
        ScoreManager.instance.ResetScore();
        
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        if (paused)
        {
            Unpause();
            AudioManager.instance.Stop("Large Enemy Beam");
            GameObject tempPlayer = playerInstance;
            PlayerSetUp();
            Destroy(tempPlayer);
        }
        else
        {
            gameOverScreen.SetActive(false);
            Unfreeze();
            AudioManager.instance.Stop("Large Enemy Beam");
            PlayerSetUp();
        }
    }


}
