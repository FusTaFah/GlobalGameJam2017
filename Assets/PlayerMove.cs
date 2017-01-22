using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Photon.MonoBehaviour {

    RaycastHit hitInfo;
    bool hit = false;
    bool down;
    private float rayLength = 250f;
    public GameObject target;
    float speed = 25f;
    GameObject m_ability;
	
    // Use this for initialization
	void Start () {
        m_ability = PhotonNetwork.Instantiate("Ability", gameObject.transform.position, gameObject.transform.rotation, 0);
        m_ability.GetComponent<UnitAbility>().SetUnitAbility("Attack", 2.0f, 0.1f, 3, 5.0f, 1);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(1))
        {
            hitInfo = new RaycastHit();
            hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

            down = true;
            GameObject targeObj = Instantiate(target, hitInfo.point, Quaternion.identity) as GameObject;


        }
        //if the clicked position is a location move to there and activate animations
        if (hit)
        {
            if (hitInfo.collider == null) return;
            if (hitInfo.collider.name == "Terrain")
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, hitInfo.point, Time.deltaTime * speed);
                transform.LookAt(hitInfo.point);
                // anim.SetBool("isRunning", true);
                //print(anim.GetBool("isRunning"));

            }
            else if(hitInfo.collider.tag == "Enemy")
            {
                
            }
            else if (hitInfo.collider == null) return;
        }
        //if player is on the target position then reset everything
        if (down)
        {
            if (transform.localPosition == hitInfo.point)
            {
                hitInfo = new RaycastHit();
                hit = false;
                down = false;
              //  anim.SetBool("isRunning", false);
                //   print(anim.GetBool("isRunning"));
            }
        }
        //if (Physics.Raycast(ray, out hit, rayLength))
        //{
        //    if (hit.collider.name == "Terrain")
        //    {
        //        if (Input.GetMouseButton(1))
        //        {
        //            transform.position = Vector3.MoveTowards(transform.position, hit.point, 50 * Time.deltaTime);
        //        }
        //        if (Input.GetMouseButtonDown(1))
        //        {
        //            GameObject targeObj = Instantiate(target, hit.point, Quaternion.identity) as GameObject;
        //        }

        //    }
        //}	
    }
}
