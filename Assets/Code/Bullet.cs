using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public IObjectPool<Bullet> objectPool;

    public float speed = 10f;
    public float lifetime = 5f;
    //public GameObject impactEffect;

    private Rigidbody rb;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * speed;
        StartCoroutine(DeactivateRoutine(lifetime));
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Enemy"))
        {
            //GameObject effect = Instantiate(impactEffect, transform.position, Quaternion.identity);
            //Destroy(effect, 1f);
            //Debug.Log("Total Score:" + ScoreBoard.score);
            ScoreBoard.instance.UpdateScoreBoard(other.GetComponent<Enemy>().points);
            other.gameObject.SetActive(false);
            Deactivate();

            other.transform.parent.GetComponent<EnemyUnit>().UpdateFinishedStatus();
        }

        if (other.CompareTag("UFO"))
        {
            //GameObject effect = Instantiate(impactEffect, transform.position, Quaternion.identity);
            //Destroy(effect, 1f);
            //Debug.Log("Total Score:" + ScoreBoard.score);
            ScoreBoard.instance.UpdateScoreBoard(other.GetComponent<Enemy>().points);
            other.gameObject.SetActive(false);
            Deactivate();
        }

        if (other.CompareTag("Player"))
        {
            //GameObject effect = Instantiate(impactEffect, transform.position, Quaternion.identity);
            //Destroy(effect, 1f);
            other.gameObject.GetComponent<PlayerShip>().TakeDamage();

            Deactivate();
        }
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Deactivate();
    }

    void Deactivate()
    {
        rb.velocity = new Vector3(0f, 0f, 0f);

        // release the projectile back to the pool
        objectPool.Release(this);
    }
}
