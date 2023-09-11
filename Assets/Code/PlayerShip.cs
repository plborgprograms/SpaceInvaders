using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public int lives = 3;

    public float bulletSpacing = 3;

    public float xVelocity = 10f;
    // Start is called before the first frame update
    void Start()
    {
        ScoreBoard.instance.UpdateLives(lives);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!PauseMenu.paused)
            {
                fire();
            }
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f);
        transform.position += movement * xVelocity * Time.deltaTime;
    }

    void fire()
    {
        Bullet myBullet = BulletPool.playerObjectPool.Get();
        myBullet.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+bulletSpacing, this.transform.position.z);
    }

    public void TakeDamage()
    {
        lives--;
        ScoreBoard.instance.UpdateLives(lives);
        if (lives == 0)
        {
            GameManager.FinishLevel(false);
        }
        else
        {
            ResetFromDeath();
        }
    }

    void ResetFromDeath()
    {

    }
}
