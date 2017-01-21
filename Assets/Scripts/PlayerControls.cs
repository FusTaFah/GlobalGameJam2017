using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : Photon.MonoBehaviour {

    //use the event system!

    //boolean which states whether or not this unit has been selected
    bool m_isSelected;
    //position the unit is to be moved to
    Vector3 m_movementPosition;
    //range of this unit's attack
    float m_attackRange;
    //distance to target that is considered close enough to stop
    float m_stoppingRange;
    //projectile firing manager
    BulletManager m_bulletManager;
    //allegiance of this unit
    public bool m_pAllegiance;
    //attacking target
    GameObject m_target;
    //attack speed of this target
    float m_attackSpeed;
    //time since the last attack
    float m_attackTimer;
    //health of this unit
    int m_health;
    //max health of this unit
    int m_maxHealth;
    //the bullet that hurts, this changes depending on the team this unit is on
    string m_damagingBullet;
    //timer for enemy scan
    float m_enemyInRangeScan;
    //max distance an idle unit can consider an enemy unit to be in targetting range
    float m_maxSearchRange;
    //unit state
    UnitState m_state;
    //texture for health bar
    Texture2D m_healthBar;
    //list of abilities
    List<UnitAbility> m_abilities;

    enum UnitState
    {
        IDLE,
        ATTACKING,
        MOVING,
        TARGETTING,
        DEAD
    }

    // initialises the fields
    void Start () {
        m_attackRange = 5.0f;
        m_stoppingRange = 0.5f;
        m_isSelected = false;
        m_movementPosition = gameObject.transform.position;
        m_bulletManager = gameObject.GetComponent<BulletManager>();
        gameObject.tag = m_pAllegiance ? "AllyUnit" : "EnemyUnit";
        m_damagingBullet = m_pAllegiance ? "EnemyBullet" : "AllyBullet";
        m_attackSpeed = 2.0f;
        m_attackTimer = 0.0f;
        m_health = 10;
        m_maxHealth = 10;
        m_enemyInRangeScan = 0.0f;
        m_maxSearchRange = 7.0f;
        m_state = UnitState.IDLE;
        m_abilities = new List<UnitAbility>();
        m_abilities.Add(new UnitAbility("Attack", 2.0f));

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
	
	// Update is called once per frame
	void Update () {
        if(m_state != UnitState.DEAD)
        {
            m_attackTimer += Time.deltaTime;
            foreach(UnitAbility ability in m_abilities)
            {
                ability.UpdateCooldown(Time.deltaTime);
            }
            if (m_state == UnitState.MOVING)
            {
                MoveToTarget();
            }
            if (m_state == UnitState.ATTACKING)
            {
                if (m_target != null)
                {
                    if (m_attackTimer > m_attackSpeed)
                    {
                        m_bulletManager.SpawnBullet(gameObject.transform.position, Quaternion.identity, m_target, m_pAllegiance ? "AllyBullet" : "EnemyBullet");
                        m_attackTimer = 0.0f;
                    }
                    
                    if ((m_target.transform.position - gameObject.transform.position).sqrMagnitude > m_attackRange * m_attackRange)
                    {
                        m_state = UnitState.TARGETTING;
                    }
                }
                else
                {
                    m_state = UnitState.IDLE;
                }
            }

            if (m_state == UnitState.TARGETTING)
            {
                if (m_target != null)
                {
                    m_movementPosition = m_target.transform.position;
                    if ((m_target.transform.position - gameObject.transform.position).sqrMagnitude > m_attackRange * m_attackRange)
                    {
                        MoveToTarget();
                    }
                    else
                    {
                        m_state = UnitState.ATTACKING;
                    }
                }
                else
                {
                    m_state = UnitState.IDLE;
                }
            }

            if (m_state == UnitState.IDLE)
            {
                if (m_enemyInRangeScan >= 2.0f)
                {
                    string enemy = m_pAllegiance ? "EnemyUnit" : "AllyUnit";
                    bool enemyFound = false;
                    foreach (GameObject g in GameObject.FindGameObjectsWithTag(enemy))
                    {
                        if ((g.transform.position - gameObject.transform.position).sqrMagnitude <= m_maxSearchRange * m_maxSearchRange)
                        {
                            Attack(g);
                            enemyFound = true;
                            break;
                        }
                    }
                    if (!enemyFound)
                    {
                        Debug.Log("Out of range");
                    }
                    m_enemyInRangeScan = 0.0f;
                }
                else
                {
                    m_enemyInRangeScan += Time.deltaTime;
                }
            }
        }
    }

    void MoveToTarget()
    {
        //get the direction from the current position to the goal
        Vector3 directionToGoal = (m_movementPosition - gameObject.transform.position).normalized;
        //if the unit is not already close enough to the goal
        if ((m_movementPosition - gameObject.transform.position).magnitude >= m_stoppingRange)
        {
            //move towards the goal position
            //gameObject.transform.forward = directionToGoal;
            gameObject.transform.forward = directionToGoal;
            //gameObject.transform.position = (gameObject.transform.position + gameObject.transform.TransformDirection(directionToGoal * Time.deltaTime * 10.0f));
            gameObject.transform.position = (gameObject.transform.position + gameObject.transform.forward * Time.deltaTime * 10.0f);
        }
        else
        {
            m_state = UnitState.IDLE;
        }
    }

    //select this unit
    public void Select()
    {
        m_isSelected = true;
        //change the colour of this unit to red
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }

    //deselect this unit
    public void Deselect()
    {
        m_isSelected = false;
        //if the unit is not selected, return the unit to its original colour
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
    }

    //set this unit's movement position
    public void Move(Vector3 newPosition)
    {
        m_state = UnitState.MOVING;
        m_movementPosition = newPosition;
    }

    public void Attack(GameObject target)
    {
        m_target = target;
        Vector3 towardsTarget = target.transform.position - gameObject.transform.position;
        if (towardsTarget.sqrMagnitude > m_attackRange * m_attackRange)
        {
            m_state = UnitState.ATTACKING;
        }
        else
        {
            m_state = UnitState.TARGETTING;
        }
    }

    //collision behaviour of this unit
    public void OnCollisionEnter(Collision coll)
    {
        
        if(coll.gameObject.tag == m_damagingBullet)
        {
            m_health -= 1;

            if (m_health <= 0)
            {
                m_state = UnitState.DEAD;
                Destroy(gameObject);
            }else
            {
                //fuck off intellisense
                float proportionRemainingHealth = ((float)m_health / (float)m_maxHealth) * m_healthBar.width;

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
                        }else
                        {
                            colors[j + i].r = 0.3f;
                            colors[j + i].g = 0.7f;
                        }
                    }
                    i += m_healthBar.width;

                    
                }
                m_healthBar.SetPixels(colors);
                m_healthBar.Apply(false);
            }
        }
    }

    public bool IsUnitDead()
    {
        return m_state == UnitState.DEAD;
    }

    public List<UnitAbility> GetAbilityList()
    {
        return m_abilities;
    }

    public bool IsSelected()
    {
        return m_isSelected;
    }

    public void OnGUI()
    {
        Vector2 spritePos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        spritePos.y = Screen.height - spritePos.y - m_healthBar.height * 5;
        spritePos.x -= m_healthBar.width / 2;
        Rect r = new Rect(spritePos, new Vector2(m_healthBar.width, m_healthBar.height));

        GUI.DrawTexture(r, m_healthBar);
    }

    public bool IsInstancedPlayer()
    {
        return photonView.isMine;
    }
}
