using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 5f;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (GameVariables.playerMoveable)
        {
            float h = -Input.GetAxisRaw("Horizontal");
            float v = -Input.GetAxisRaw("Vertical");

                 // Move the player around the scene.
            Move (h, v);

            // Turn the player to face the mouse cursor.
            Turning ();

            // Animate the player.
            Animating (h, v);
        }
        else
        {
            Animating (0, 0);
        }
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);

        movement = movement.normalized * speed * Time.deltaTime* 0.2f;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if(Physics.Raycast(camRay,out floorHit,camRayLength,floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsWalking", walking);
        anim.speed = 3;
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Key")
        {
            GameVariables.keys.Add(obj.gameObject.name.Substring(3));
            Destroy(obj.gameObject);

            UnityEngine.UI.RawImage k = GameObject.Find(obj.gameObject.name + "HUD").GetComponent<UnityEngine.UI.RawImage>();
            if (k != null)
            {
                k.enabled = true;
            }

        }
        else if (obj.gameObject.tag == "Door")
        {
            if (obj.gameObject.name == "Door1" && !GameVariables.p1complete)
            {
                enableCanvas("p1Canvas");
            	GameVariables.playerMoveable = false;
            }
        }
        else if (obj.gameObject.name == "laptop01")
        {
            enableCanvas("p1Canvas2");
            GameVariables.playerMoveable = false;
        }

    }

    void enableCanvas(string cName)
    {
        UnityEngine.CanvasGroup cg = GameObject.Find(cName).GetComponent<CanvasGroup>();
        if (cg != null)
        {
            cg.alpha = 1;
            cg.blocksRaycasts = true;
        }
    }

}
