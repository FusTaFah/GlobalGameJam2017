using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {

    bool foundPlayer;
    GameObject player;
    Camera m_cam;

	// Use this for initialization
	void Start () {
        foundPlayer = false;
        m_cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {

        if (!foundPlayer)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject currentPlayer in players)
            {
                if (currentPlayer.GetComponent<PlayerControls>().IsInstancedPlayer())
                {
                    player = currentPlayer;
                    foundPlayer = true;
                    break;
                }
            }
        }
        else
        {
            //set the camera position to the new position
            //gameObject.transform.position = player.transform.position + new Vector3(0.0f, 10.0f, -10.0f);
            Vector3 mousePos = Input.mousePosition;
            //store the mouse position in a temporary variable
            Vector3 cameraToWorld = mousePos;

            //translate this position such that it is on the near clipping plane
            cameraToWorld.z = m_cam.nearClipPlane;
            //obtain the world point of the near clipping plane
            Vector3 worldPoint = m_cam.ScreenToWorldPoint(cameraToWorld);
            //use the world point of the mouse and the camera's position to get a direction
            //towards the world
            Vector3 cameraToWorldDirection = (worldPoint - gameObject.transform.position).normalized;
            //use this position to initialise a ray

            Ray x = new Ray(gameObject.transform.position, cameraToWorldDirection);
            //prepare a raycast towards where the mouse is pointing
            RaycastHit rch;
            Physics.Raycast(x, out rch);

            if (Input.GetButton("Fire2"))
            {
                //if (rch.collider.tag == "Plane")
                //{
                //    player.GetComponent<PlayerControls>().Move(gameObject.transform.position + cameraToWorldDirection * rch.distance + new Vector3(0.0f, 1.0f, 0.0f));

                //}

                if(rch.collider.tag == "Enemy")
                {
                    player.GetComponent<PlayerControls>().Attack(rch.collider.gameObject);
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                player.GetComponent<PlayerControls>().UseAbility(1);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {

            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                
            }

            if (Input.GetKeyDown(KeyCode.R))
            {

            }
        }
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
