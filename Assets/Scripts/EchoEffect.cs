using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{

    // Script credit to Blackthornprod - https://www.youtube.com/watch?v=_TcEfIXpmRI

    private float timeBtwSpawns;
    [SerializeField] public float startTimeBtwSpawns;

    public GameObject echo;
    private Ball ball;

    private void Update()
    {
        DecideToSpawnEcho();
    }

    private void DecideToSpawnEcho()
    {
        if (GetComponent<Ball>().hasStarted)
        {

            if (timeBtwSpawns <= 0)
            {
                GameObject instance = (GameObject)Instantiate(echo, transform.position, Quaternion.identity);
                Destroy(instance, 2f);
                timeBtwSpawns = startTimeBtwSpawns;
            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
        }
    }
}
