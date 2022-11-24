using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public KeyCode ShootKey = KeyCode.X;

    [Space]

    public GameObject projectile;
    public GameObject spawnPoint;

    private CharacterControl projectileOwner = null;

    void Start()
    {
        projectileOwner = gameObject.GetComponent<CharacterControl>();
    }


    void Update()
    {
        if (Input.GetKeyDown(ShootKey))
        {
            GameObject spawnedProjectile = GameObject.Instantiate(projectile, spawnPoint.transform.position, Quaternion.identity);
            spawnedProjectile.GetComponent<Projectile>().InitializeOnSpawn(projectileOwner.leftright);
        }
    }
}
