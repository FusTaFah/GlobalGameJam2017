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
    int maxHealth;
    public int speed = 10;
    public int money = 1;
    public int attackPower = 1;
    NavMeshAgent agent;
    Transform test;
    public bool playerSeen;
    //texture for health bar
    Texture2D m_healthBar;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pathGO = GameObject.Find("Path");
        // GetNextPathNode();
        playerSeen = false;
        target = GameObject.Find("Fortress Gate").transform.GetChild(0).transform.position;

        maxHealth = health;

        int spriteX = 20;
        int spriteY = 4;
        m_healthBar = new Texture2D(spriteX, spriteY);
        Color[] colors = m_healthBar.GetPixels();
        Debug.Log(m_healthBar.GetPixels().Length);
        for (int i = 0; i < spriteX * spriteY; i++)
        {
            colors[i].a = 0.88f;
            colors[i].r = 0.3f;
            colors[i].g = 0.7f;
            colors[i].b = 0.3f;
        }
        m_healthBar.SetPixels(colors);
        m_healthBar.Apply(false);
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
            agent.SetDestination(GameObject.Find("Fortress Gate").transform.GetChild(0).transform.position);
        }

        //if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <2)
        //{
        //    agent.Stop();
        //}
        //else
        //{
        //    agent.Resume();
        //}



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
    public void takeDamage(int damage)
    {
        health -= damage;

        //fuck off intellisense
        float proportionRemainingHealth = ((float)health / (float)maxHealth) * m_healthBar.width;

        Color[] colors = m_healthBar.GetPixels();
        int textureArea = m_healthBar.width * m_healthBar.height;
        for (int i = 0; i < textureArea - 1;)
        {
            for (int j = 0; j < m_healthBar.width; j++)
            {
                colors[j + i].a = 0.88f;
                colors[j + i].b = 0.3f;
                if (j >= proportionRemainingHealth)
                {
                    colors[j + i].g = 0.3f;
                    colors[j + i].r = 0.7f;
                }
                else
                {
                    colors[j + i].r = 0.3f;
                    colors[j + i].g = 0.7f;
                }
            }
            i += m_healthBar.width;


        }
        m_healthBar.SetPixels(colors);
        m_healthBar.Apply(false);

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

    public void OnGUI()
    {
        Vector2 spritePos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        spritePos.y = Screen.height - spritePos.y - m_healthBar.height * 5;
        spritePos.x -= m_healthBar.width / 2;
        Rect r = new Rect(spritePos, new Vector2(m_healthBar.width, m_healthBar.height));

        GUI.DrawTexture(r, m_healthBar);
    }
}
