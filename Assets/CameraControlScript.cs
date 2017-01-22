using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlScript : MonoBehaviour {

    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    private bool doMovement = true;
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 70f;

    private float mouseX;
    private float mouseY;

    private bool VerticalRotationEnabled = true;
    private float VerticalRotationMin = 0f;
    private float VerticalRotationMax = 65f;

    public float easeFactor = 10f;

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }
        if (!doMovement)
        {
            return;
        }
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(-Vector3.forward * panSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(-Vector3.right * panSpeed * Time.deltaTime);
        }
        if (Input.GetMouseButton(2))
        {
            if (Input.mousePosition.x != mouseX)
            {
                var cameraRotationY = (Input.mousePosition.x - mouseX)*Time.deltaTime;
                transform.Rotate(0,cameraRotationY,0);
            }
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 100 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;

        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;

    }
}
