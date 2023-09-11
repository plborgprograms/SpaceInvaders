using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        enemiesPerRowInThisGame = enemiesPerRow;
        SpawnEnemies();
    }

    public float xVelocity = .1f;
    public float xAcceleration = .1f;
    public float yVelocity = 5.0f;
    public float yAcceleration = 0f;
    public int numberOfRegularEnemies = 3;
    public int numberOfStrongEnemies = 2;

    public static int enemiesPerRowInThisGame = 11;

    public int enemiesPerRow = 11;
    public int enemyXSpacing = 5;
    public int enemyYSpacing = 5;

    public GameObject regularEnemy;
    public GameObject strongEnemy;

    public int direction = 1;

    public float firingWaitTime = 3f;

    private int activeChildCount = 0;

    void SpawnEnemies()
    {
        int totalEnemyRows = numberOfRegularEnemies + numberOfStrongEnemies;
        Vector3 groupCenter = this.transform.position;
        //Vector3 startPoint = new Vector3(groupCenter.x- (enemyXSpacing * enemiesPerRow/2), groupCenter.y + (enemyYSpacing * totalEnemyRows/2),
        //    this.transform.position.z);
        Vector3 startPoint = new Vector3(groupCenter.x - ((enemiesPerRow - 1) * enemyXSpacing) / 2f,
            groupCenter.y + ((totalEnemyRows - 1) * enemyYSpacing) / 2f, groupCenter.z);

        for (int i = 0; i < numberOfStrongEnemies; i++)
        {
            for (int j = 0; j < enemiesPerRow; j++)
            {
                Vector3 position = new Vector3(startPoint.x + j * enemyXSpacing, startPoint.y - i * enemyYSpacing, startPoint.z);
                GameObject newEnemy = GameObject.Instantiate(strongEnemy, position, Quaternion.identity);
                //newEnemy.GetComponent<Enemy>().initialWait = firingWaitTime + j;
                newEnemy.transform.parent = this.transform;
            }
        }

        for (int i = 0; i < numberOfRegularEnemies; i++)
        {
            for (int j = 0; j < enemiesPerRow; j++)
            {
                Vector3 position = new Vector3(startPoint.x + j * enemyXSpacing, 
                    startPoint.y - (numberOfStrongEnemies + i) * enemyYSpacing, startPoint.z);
                GameObject newEnemy = GameObject.Instantiate(regularEnemy, position, Quaternion.identity);
                newEnemy.transform.parent = this.transform;
            }
        }

        activeChildCount += (numberOfStrongEnemies + numberOfRegularEnemies) * enemiesPerRow;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * transform.right * xVelocity * Time.deltaTime;
    }
    public GameObject lastCollider = null;

    public void ChangeDirection(GameObject other)
    {
        if (lastCollider == null || other.GetInstanceID() != lastCollider.GetInstanceID())
        {
            //xVelocity += xAcceleration;
            yVelocity -= yAcceleration;
            direction = direction * (-1);
            transform.position -= transform.up * yVelocity * Time.deltaTime;
            lastCollider = other.gameObject;

        }
    }

    public void UpdateFinishedStatus()
    {
        xVelocity += xAcceleration;

        activeChildCount--;
        if (CheckIfFinished())
        {
            GameManager.FinishLevel(true);
        }
    }

    public bool CheckIfFinished()
    {
        return activeChildCount == 0;
    }
}
