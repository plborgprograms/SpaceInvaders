using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    List<float> times = new List<float>() { 5, 7f, 10f };

    public float speed = 10.0f;
    private bool isMovingRight = true;

    public bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Disappear());
        Invoke("Disappear", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (isMovingRight)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }
    }

    public GameObject lastCollider = null;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("moveDownCollider"))
        {
            if (lastCollider == null || other.gameObject.GetInstanceID() != lastCollider.GetInstanceID())
            {
                lastCollider = other.gameObject;
                Invoke("ReverseDirection", 0f);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag);
        if (collision.transform.CompareTag("moveDownCollider"))
        {
            if (lastCollider == null || collision.gameObject.GetInstanceID() != lastCollider.GetInstanceID())
            {
                lastCollider = collision.gameObject;
                Invoke("ReverseDirection", 0f);
            }
        }
    }

    void ReverseDirection()
    {
        isMovingRight = !isMovingRight;
        StartCoroutine(Disappear());
    }

    float getWaitTime()
    {
        return times[Random.Range(0, times.Count)];
    }

    IEnumerator Disappear()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<Collider>().enabled = false;
        isMoving = false;

        float waitTime = getWaitTime();
        yield return new WaitForSeconds(waitTime);

        this.GetComponent<MeshRenderer>().enabled = true;
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<Collider>().enabled = true;
        isMoving = true;
    }
}
