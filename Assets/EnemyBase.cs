using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float enemyHealth = 10f;
    public float EnemyHealth
    {
        get { return enemyHealth; }

        set
        {
            Debug.Log(enemyHealth);
            enemyHealth = value;
            if (enemyHealth < 0)
            {
                enemyHealth = 0;
                //여기에 사망 이벤트
            }
        }
    }

    float enemySpeed = 1f;
    public float EnemySpeed
    {
        get{return enemySpeed;}
    }

    float enemydamage = 5f;
    public float Enemydamage 
    {
        get { return enemydamage; }
    }


}
