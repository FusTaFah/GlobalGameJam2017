using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {
    public GameObject panel;
	// Use this for initialization
	void Start () {
        panel.SetActive(false);
	}
	void OnMouseDown()
    {
        panel.SetActive(true);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
