using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlane : MonoBehaviour
{ 
    //˜˜˜˜˜˜˜ ˜˜˜˜˜˜
    public float max_x;
    public float max_y;
    public float min_x;
    public float min_y;

    // ˜˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜˜˜˜ ˜˜˜˜˜
    public float playerSpeed = 2.0f;

    // ˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜˜˜˜
    private float currentSpeed = 0.0f;

    // ˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜˜˜ ˜˜˜ ˜˜˜˜˜˜
    public List<KeyCode> upButton;
    public List<KeyCode> downButton;
    public List<KeyCode> leftButton;
    public List<KeyCode> rightButton;

    // ˜˜˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜˜˜˜
    private Vector3 lastMovement = new Vector3();

    // Update is called once per frame
    void Update()
    {
        // ˜˜˜˜˜˜˜ ˜˜˜˜˜ ˜ ˜˜˜˜˜
        //Rotation();

        // ˜˜˜˜˜˜˜˜˜˜˜ ˜˜˜˜˜
        Movement();
    }

    // ˜˜˜˜˜˜˜ ˜˜˜˜˜ ˜ ˜˜˜˜˜
    void Rotation()
    {
        // ˜˜˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜, ˜˜˜ ˜˜˜˜˜
        Vector3 worldPos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(worldPos);
        // ˜˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜˜ ˜˜˜˜
        float dx = this.transform.position.x - worldPos.x;
        float dy = this.transform.position.y - worldPos.y;
        // ˜˜˜˜˜˜˜˜˜ ˜˜˜˜ ˜˜˜˜˜ ˜˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜˜ ˜ ˜˜˜˜˜˜˜˜˜˜˜
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        // ˜˜˜˜˜˜˜˜˜˜˜˜˜˜ ˜˜˜˜ ˜ ˜˜˜˜˜˜
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + 180));
        // ˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜ ˜˜˜˜˜
        this.transform.rotation = rot;
    }

    // ˜˜˜˜˜˜˜˜ ˜˜˜˜˜ ˜ ˜˜˜˜˜
    void Movement()
    {
        // ˜˜˜˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜
        Vector3 movement = new Vector3();
        // ˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜ ˜˜˜˜˜˜
        movement += MoveIfPressed(upButton, Vector3.up);
        movement += MoveIfPressed(downButton, Vector3.down);
        movement += MoveIfPressed(leftButton, Vector3.left);
        movement += MoveIfPressed(rightButton, Vector3.right);
        // ˜˜˜˜ ˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜, ˜˜˜˜˜˜˜˜˜˜˜˜ ˜˜˜
        movement.Normalize();
        // ˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜ ˜˜˜˜˜˜
        if (movement.magnitude > 0)
        {
            // ˜˜˜˜˜ ˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜˜ ˜ ˜˜˜˜ ˜˜˜˜˜˜˜˜˜˜˜
            //˜˜˜˜˜˜˜˜ ˜˜ ˜˜˜˜˜˜˜
            if(this.transform.position.x > max_x)
            {
                this.transform.position = new Vector3(max_x, this.transform.position.y, this.transform.position.z);
            }
            else if (this.transform.position.y > max_y)
            {
                this.transform.position = new Vector3(this.transform.position.x, max_y, this.transform.position.z);
            }
            else if (this.transform.position.y < min_y)
            {
                this.transform.position = new Vector3(this.transform.position.x, min_y, this.transform.position.z);
            }
            else if (this.transform.position.x < min_x)
            {
                this.transform.position = new Vector3(min_x, this.transform.position.y, this.transform.position.z);
            }
            else
            {
                currentSpeed = playerSpeed;
                this.transform.Translate(movement * Time.deltaTime * playerSpeed, Space.World);
                lastMovement = movement;
            }
        }
        else
        {
            // ˜˜˜˜ ˜˜˜˜˜˜ ˜˜ ˜˜˜˜˜˜
            this.transform.Translate(lastMovement * Time.deltaTime * currentSpeed, Space.World);
            // ˜˜˜˜˜˜˜˜˜˜ ˜˜ ˜˜˜˜˜˜˜˜
            currentSpeed *= 0.9f;
        }
    }

    // ˜˜˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜˜, ˜˜˜˜ ˜˜˜˜˜˜ ˜˜˜˜˜˜
    Vector3 MoveIfPressed(List<KeyCode> keyList, Vector3 Movement)
    {
        // ˜˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜ ˜˜ ˜˜˜˜˜˜
        foreach (KeyCode element in keyList)
        {
            if (Input.GetKey(element))
            {
                // ˜˜˜˜ ˜˜˜˜˜˜, ˜˜˜˜˜˜˜˜ ˜˜˜˜˜˜˜
                return Movement;
            }
        }
        // ˜˜˜˜ ˜˜˜˜˜˜ ˜˜ ˜˜˜˜˜˜, ˜˜ ˜˜ ˜˜˜˜˜˜˜˜˜
        return Vector3.zero;
    }

}