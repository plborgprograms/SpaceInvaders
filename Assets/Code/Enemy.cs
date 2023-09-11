using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Enemy : MonoBehaviour
{
    public static int syncWaitTime = 2;
    public int points = 100;
    public float initialWait = 0;

    public float fireInterval = 0;

    public float bulletSpacing = -2;

    // Start is called before the first frame update
    void Start()
    {
        syncWaitTime++;
        initialWait = syncWaitTime % EnemyUnit.enemiesPerRowInThisGame;
            
        InvokeRepeating("Shoot" , initialWait, fireInterval);
    }

    void OnDisable()
    {
        CancelInvoke("Shoot" );
    }

    void Shoot()
    {
        if (!Blocked())
        {
            Bullet myBullet = BulletPool.enemyObjectPool.Get();
            myBullet.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + bulletSpacing, this.transform.position.z);
        }
    }

    bool Blocked()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                return true;
            }
        }
        return false;
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if (other.CompareTag("moveDownCollider"))
        {
            transform.parent.GetComponent<EnemyUnit>().ChangeDirection(other.gameObject);
        }

        if (other.CompareTag("finishZoneCollider"))
        {
            GameManager.FinishLevel(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.transform.tag);
        if (collision.transform.CompareTag("moveDownCollider"))
        {
            transform.parent.GetComponent<EnemyUnit>().ChangeDirection(collision.gameObject);           
        }

        if (collision.transform.CompareTag("finishZoneCollider"))
        {

        }
    }
}
