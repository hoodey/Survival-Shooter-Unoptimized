using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{    
    EnemyPool pool;


    private void OnDisable()
    {
        if (pool != null)
        {
            pool.AddToQueue(this);
        }
    }

    public void SetPool(EnemyPool bp)
    {
        pool = bp;
    }

}
