using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float mov_spd = 1f;
    public float rot_spd = 1f;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    bool Picking = false;

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
            float cam_rot = Input.GetAxisRaw("Camera Rotation");
            float player_mov = Input.GetAxisRaw("Player Movement");

            // camera rotation and player movement calculation
            cam_rot *= rot_spd * Time.deltaTime;
            transform.Rotate(0, cam_rot, 0);
            player_mov *= mov_spd * Time.deltaTime;
            transform.Translate(0, 0, player_mov);

            // Animate the player.
            Animating(cam_rot, player_mov);

            if (Input.GetKey("f"))
            {
                Picking = true;

            }
            APick(Picking);
            Picking = false;
        }
        else
        {
            Animating(0, 0);
        }
    }

    void APick(bool Picking)
    {
        anim.SetBool("Pick", Picking);
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
