using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
  
    public static ProjectileShooter instance {get; private set;}
    public KeyCode ShootKey = KeyCode.X;

    [Space]

    public GameObject projectile;
    public GameObject spawnPoint;
    public int shotcount=0;

    private CharacterControl projectileOwner = null;

    void Awake()
    {   
        if(instance == null) instance = this;
        projectileOwner = gameObject.GetComponent<CharacterControl>();
    }


    void Update()
    {
        if (Input.GetKeyDown(ShootKey)&&shotcount<3)
        {
            GameObject spawnedProjectile = GameObject.Instantiate(projectile, spawnPoint.transform.position, Quaternion.identity);
            spawnedProjectile.GetComponent<Projectile>().InitializeOnSpawn(projectileOwner.leftright);
            shotcount+=1;
        }
      
    }

}
