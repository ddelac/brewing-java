using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level3BossLaser : MonoBehaviour
{
    float invulnerabilityTime = .5f;
    float canDamage = 0;
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        
        if (other.tag == "Level3_Player")
        {
            Level3_Player player = other.GetComponent<Level3_Player>();
            if (player != null)
            {
                
                if(Time.time > canDamage)
                {
                    canDamage = Time.time + invulnerabilityTime;
                    player.Damage(15);
                }

            }
        }

    }
}
