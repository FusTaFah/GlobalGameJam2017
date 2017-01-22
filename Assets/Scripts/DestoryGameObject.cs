using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryGameObject : MonoBehaviour {

	void DestroyObject()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
