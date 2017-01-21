using UnityEngine;
using System.Collections;

public class BulletSpan : MonoBehaviour {

    //variable to keep track of how long this bullet has been fired for.
    float timer;
    //boolean to say whether or not this bullet is currently in flight
    bool beingUsed;

    float lifeSpan;

    int damage;

    //public set and get methods for timer
    public float Timer { get { return timer; } set { timer = value; } }
    //public set and get methods for beingUsed
    public bool BeingUsed { get { return beingUsed; } set { beingUsed = value; } }

    public float LifeSpan { get { return lifeSpan; } set { lifeSpan = value; } }

    public int Damage { get { return damage; } set { damage = value; } }

    // Use this for initialization
    void Start()
    {
        lifeSpan = 10.0f;
        //initialised beingUsed to true
        beingUsed = true;
        //initialise timer to 0
        timer = 0.0f;
    }

    void Update()
    {
        //if the bullet is in flight
        if (beingUsed)
        {
            //increase the time the bullet has been flying for
            timer += Time.deltaTime;
            //if the time the bullet has been flying for is greater than 3 seconds
            if (timer >= lifeSpan)
            {
                //flag this bullet for removal by the manager
                beingUsed = false;
                //reset the timer
                timer = 0.0f;
                Destroy(gameObject);
            }
        }
        else
        {
            //flag this bullet for removal by the manager
            beingUsed = false;
            //reset the timer
            timer = 0.0f;
        }
        
    }

    //when the bullet collides with any collision object
    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.gameObject.tag == "Enemy")
        {
            coll.collider.gameObject.GetComponent<Enemy>().takeDamage(damage);
            //flag this bullet for removal by the manager
            beingUsed = false;
            //reset the timer
            timer = 0.0f;
        }

    }
}
