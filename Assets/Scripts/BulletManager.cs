using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour {

    /*
     * The inspiration of this bullet manager came from
     * "Ammo & Enemies: Reduce, Reuse, Recycle" (Matthew Hodge 2016)
     * http://www.gamasutra.com/blogs/MatthewHodge/20160224/266535/Ammo__Enemies_Reduce_Reuse_Recycle.php?utm_source=dlvr.it&utm_medium=twitter
     * */

    //a list of bullet perfabs
    List<GameObject> bulletPrefabs;

	// Use this for initialization
	void Start () {
        //initialises the list
        bulletPrefabs = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject bullet in bulletPrefabs)
        {
            //if the bullet object in the list is currently not being used
            if (!bullet.GetComponent<BulletSpan>().BeingUsed)
            {
                //declare the bullet disabled, but don't remove it
                DisableBullet(bullet);
            }
        }
	}

    public void SpawnBullet(Vector3 position, Quaternion rotation, GameObject target, string bulletType)
    {
        //not found until proven found by the following loop
        bool found = false;
        //look through all the bullets in the bulletprefab list
        foreach(GameObject tr in bulletPrefabs){
            //if the bullet is not currently being used and the bullet type is that which we were requested to fire
            if (!tr.GetComponent<BulletSpan>().BeingUsed && tr.GetComponent<BulletSpan>().BulletType == bulletType)
            {
                //reset the game object
                tr.SetActive(true);
                //set its position
                tr.transform.position = position;
                //set its rotation
                tr.transform.rotation = rotation;
                //apply an impulse in the parameterized direction
                //tr.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.Impulse);
                tr.GetComponent<BulletSpan>().Target = target;
                //unused bullet of correct type found, do not proceed to the next behaviour of making another prefab
                found = true;
                //the object is found, no need to stay in the loop
                break;
            }
        }
        //else make a new one
        if(!found)
        {
            //instantiate a gameobject and assign it to a local variable
            GameObject g = Instantiate(Resources.Load("Prefabs/" + bulletType, typeof(GameObject)), position, rotation) as GameObject;
            //apply an impulse to the gameobejct's rigidbody
            //g.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.Impulse);
            g.GetComponent<BulletSpan>().Target = target;
            //initialise its BulletSpan attributes
            g.GetComponent<BulletSpan>().BeingUsed = true;
            g.GetComponent<BulletSpan>().Timer = 0.0f;
            g.GetComponent<BulletSpan>().BulletType = bulletType;
            //add the gameobject to the list for reference
            bulletPrefabs.Add(g);
        }
    }

    void DisableBullet(GameObject bullet)
    {
        //set its beingUsed value to false
        bullet.GetComponent<BulletSpan>().BeingUsed = false;
        //reset its timer
        bullet.GetComponent<BulletSpan>().Timer = 0.0f;
        //put it at the origin of the world
        bullet.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        //disable it
        bullet.SetActive(false);
    }
}
