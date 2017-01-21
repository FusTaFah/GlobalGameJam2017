using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    // public List<GameObject> pathPoints;
    GameObject pathGO;
    public Transform targetPathNode;
    Vector3 target;
    int pathNodeIndex = 0;
    public int health = 1;
    public int speed = 10;
    public int money = 1;
    public int attackPower = 1;
    NavMeshAgent agent;
    Transform test;
    public bool playerSeen;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pathGO = GameObject.Find("Path");
        // GetNextPathNode();
        playerSeen = false;
        target = GameObject.Find("Fortress_Gate").transform.GetChild(0).transform.position;
    }

    void GetNextPathNode()
    {
        if (pathNodeIndex < pathGO.transform.childCount)
        {
            targetPathNode = pathGO.transform.GetChild(pathNodeIndex);
        }
        else
        {
            targetPathNode = null;
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerSeen = true;
        }
    }
    void Update()
    {
        //if (targetPathNode == null)
        //{
        //    //  GetNextPathNode();

        //    if (targetPathNode == null)
        //    {
        //        // Destroy(gameObject);
        //        //ReachedGoal();
        //        return;
        //    }
        //}


        float x = EnemySpawner.x;
        float z = EnemySpawner.z;

        if (playerSeen )
        {
            agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        }
        else if(!playerSeen)
        {
            agent.SetDestination(GameObject.Find("Fortress_Gate").transform.GetChild(0).transform.position);
        }

        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <2)
        {
            agent.Stop();
        }
        else
        {
            agent.Resume();
        }



        //if (transform.position == new Vector3(targetPathNode.position.x + x, targetPathNode.position.y, targetPathNode.position.z + z))
        //{
            
        //}
        //else
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, 
        //        new Vector3(targetPathNode.position.x+x, targetPathNode.position.y, targetPathNode.position.z+z), 
        //        speed * Time.deltaTime);
        //}







        //Vector3 dir = new Vector3(targetPathNode.position.x+x, targetPathNode.position.y, targetPathNode.position.z+z) - transform.localPosition;
        ////dir = new Vector3(dir.x+x,dir.y,dir.z+z);
        //float distThisFrame = speed * Time.deltaTime;

        //if (dir.magnitude <= distThisFrame)
        //{
        //    targetPathNode = null;
        //}
        //else
        //{
        //    transform.Translate(dir.normalized * distThisFrame, Space.World);
        // transform.position = Vector3.MoveTowards(transform.position, dir.normalized * distThisFrame, 1);  
        /* Quaternion targetRotation = Quaternion.LookRotation(dir);
         this.transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation,(speed/2)* Time.deltaTime);
     */
    
    }
    public void takeDamage(int i)
    {
        health -= i;

        if (health <=0)
        {
            ReachedGoal();
        }
    }
    void ReachedGoal()
    {
        //We destroy the game object for now when it reaches to the end for now!
        Destroy(gameObject);
        //GameObject.FindObjectOfType<ScoreManager>().money += money;
    }
}
