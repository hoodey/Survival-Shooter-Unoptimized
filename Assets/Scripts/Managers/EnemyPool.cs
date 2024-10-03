using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] int enemyAmount;

    Queue<Enemy> remainingEnemies = new Queue<Enemy> ();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            var b = Instantiate(enemyPrefab, this.transform);
            b.SetPool(this);
            b.gameObject.SetActive(false);
        }
    }

    public void SpawnEnemyAtLocation(Vector3 location)
    {
        if (remainingEnemies.Count > 0)
        {
            var current = remainingEnemies.Dequeue();

            current.transform.position = location;

            current.gameObject.SetActive(true);

        }
    }

    public void AddToQueue(Enemy enemy)
    {
        remainingEnemies.Enqueue(enemy);
    }

}
