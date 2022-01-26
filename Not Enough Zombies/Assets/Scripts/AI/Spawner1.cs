using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner1 : MonoBehaviour
{
   [SerializeField] GameObject[] enemy;
    [SerializeField] float enemyCount;

    // Update is called once per frame
    void Update()
    {
        if(enemyCount <= 0)
        {
            StartCoroutine(spawn());
            enemyCount++;
            if(enemyCount == 5)
            {
                Destroy(gameObject);
            }
        }

    }

    IEnumerator spawn()
    {
            Instantiate(enemy[0], transform.position, transform.rotation);
            yield return new WaitForSeconds(2f);
    }
}
