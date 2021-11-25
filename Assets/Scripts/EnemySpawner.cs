// Author: Fatima Nadeem - (Proper comments pending)

using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform leftBound;
    public Transform rightBound;
    public EnemyProbability[] enemyProbabilities;
    public float enemySpnRateGap;

    int probSum;
    bool unlocked;
    bool allowSpawn;
    GameObject enemy;

    float halfWidth;
    float leftBoundx;
    float rightBoundx;
    float x;
    float y;

    void Start()
    {
        probSum = 0;
        foreach (EnemyProbability enemyProb in enemyProbabilities)
        {
            probSum += enemyProb.prob;
        }

        unlocked = true;
        allowSpawn = true;

        leftBoundx = leftBound.position.x;
        rightBoundx = rightBound.position.x;
        y = transform.position.y;
    }

    void Update()
    {
        if (unlocked && allowSpawn)
        {
            StartCoroutine(Spawn());

        } 
    }

    IEnumerator Spawn()
    {
        allowSpawn = false;

        int enemyRand = Random.Range(0, probSum);

        foreach (EnemyProbability enemyProb in enemyProbabilities)
        {
            if (enemyRand < enemyProb.prob)
            {
                enemy = Instantiate(enemyProb.enemy, transform.position, 
                    Quaternion.identity, transform.parent);

                // streamline enemies for heaven's sake this look's ridiculous
                
                SmallEnemy sE = enemy.GetComponent<SmallEnemy>();
                if (sE != null)
                {
                    halfWidth = sE.halfWidthHeight.x;
                }

                MediumEnemy mE = enemy.GetComponent<MediumEnemy>();
                if (mE != null)
                {
                    halfWidth = mE.halfWidthHeight.x;
                }

                BigEnemy lE = enemy.GetComponent<BigEnemy>();
                if (lE != null)
                {
                    halfWidth = lE.halfWidthHeight.x;
                }

                x = Random.Range(
                    (leftBoundx + halfWidth),
                    (rightBoundx - halfWidth)
                    );

                enemy.transform.position = new Vector2(x, y);
            }
            else
            {
                enemyRand -= enemyProb.prob;
            }
        }

        yield return new WaitForSeconds(enemySpnRateGap);

        allowSpawn = true;
    }

    public void Lock()
    {
        unlocked = false;
    }

    public void Unlock()
    {
        unlocked = true;
        allowSpawn = true;
    }

}
