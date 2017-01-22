using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Photon.MonoBehaviour {

    float spawnCooldown = 0.2f;
    float spawnCooldownRemaining = 5;
    public GameObject spawnPointFirst;
    public float MinX = 0;
    public float MaxX = 10;
    public float MinZ = 0;
    public float MaxZ = 10;
    public static float x;
    public static float z;

    [System.Serializable]
    public class WaveComponent
    {
        public GameObject enemyPrefab;
        public int num;
        [System.NonSerialized]
        public int spawned = 0;
    }

    public WaveComponent[] waveComps;


	// Use this for initialization
	void Start () {
       // spawnPointFirst = GameObject.Find("Sphere").transform.GetChild(0);

    }

    // Update is called once per frame
    void Update () {
        
        if (PhotonNetwork.inRoom)
        {
            x = Random.Range(MinX, MaxX);
            z = Random.Range(MinZ, MaxZ);
            spawnCooldownRemaining -= Time.deltaTime;

            if (spawnCooldownRemaining < 0)
            {
                spawnCooldownRemaining = spawnCooldown;
                bool didSpawn = false;

                foreach (WaveComponent wc in waveComps)
                {
                    if (wc.spawned < wc.num)
                    {
                        wc.spawned++;
                        PhotonNetwork.Instantiate(wc.enemyPrefab.name, 
                            new Vector3(spawnPointFirst.transform.position.x + x, 5,spawnPointFirst.transform.position.z+z), 
                            transform.rotation, 0);
                        didSpawn = true;
                        break;
                    }
                }
                if (didSpawn == false)
                {

                 //   transform.parent.GetChild(1).gameObject.SetActive(true);
                   // Destroy(gameObject);
                    //spawn next wave
                }
            }
           
        }
	}
}
