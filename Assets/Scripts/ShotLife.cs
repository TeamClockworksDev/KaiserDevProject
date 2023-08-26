using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotLife : MonoBehaviour
{ 
    public float life = 1;

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;
        if (life <= 0){
;
          Destroy(this.gameObject); 
        }
        
    }
       private void OnDestroy()
    {
        ProjectileShooter.instance.shotcount-=1;
    }

}
