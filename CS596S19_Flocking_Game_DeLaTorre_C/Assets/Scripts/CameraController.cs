using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    //Mouse Speed
    float mainSpeed = 100.0f;

    //Mouse Sensitivity
    float camSens = 0.25f; 

    //Cursor Initial Position
    private Vector3 lastMouse = new Vector3(255, 255, 255);

    private bool menuActive = true;

    void Update()
    {
        if(menuActive == false) 
        {
            //Mouse camera angle
            lastMouse = Input.mousePosition - lastMouse;
            lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
            lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
            transform.eulerAngles = lastMouse;
            lastMouse = Input.mousePosition;

            //Camera movement
            Vector3 camMovement = GetInput();
            transform.Translate(camMovement);
        }

    }

    private Vector3 GetInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 camMovement_Speed = new Vector3();
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            camMovement_Speed += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            camMovement_Speed += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            camMovement_Speed += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            camMovement_Speed += new Vector3(1, 0, 0);
        }
        return camMovement_Speed;
    }

    public void MenuActive(bool val) 
    {
        menuActive = val;
    }
}