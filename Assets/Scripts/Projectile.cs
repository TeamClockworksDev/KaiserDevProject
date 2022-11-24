using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1.0f;

    private int directionModifier;

    public void InitializeOnSpawn(bool goingRight)
    {
        directionModifier = goingRight ? +1 : -1;
    }

    void Update()
    {
        float xDleta = speed * directionModifier * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + xDleta, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter(Collider col)
    {
        if (!col.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }
    }

}
