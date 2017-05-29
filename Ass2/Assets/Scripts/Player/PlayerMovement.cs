using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float mov_spd = 1f;
    public float rot_spd = 10f;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    bool Picking = false;
    public AudioClip Step;
    public AudioClip Grab;
    AudioSource audio;
    public float time = 0.2f;
    public bool PlayWalkingAudio = false;
    public bool PlayGrabAudio = false;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            PlayWalkingAudio = true;
            PlayGrabAudio = true;
            time = 0.2f + Time.deltaTime;
        }


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
                if (PlayGrabAudio)
                {
                    audio.PlayOneShot(Grab, 0.7F);
                }
                PlayGrabAudio = false;
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

        if (walking)
        {

            if (PlayWalkingAudio)
            {
                audio.PlayOneShot(Step, 0.7F);
            }
            PlayWalkingAudio = false;
        }
    }

    void OnTriggerEnter(Collider obj)
    {

        if (obj.gameObject.tag == "Door")
        {
            if (obj.gameObject.name == "Door1" && !GameVariables.p1complete)
            {
                enableCanvas("p1Canvas");
            	GameVariables.playerMoveable = false;
            }
            else if (obj.gameObject.name == "Door2" && !GameVariables.p2complete && !GameVariables.p2DoorOpen) {
                enableCanvas("p2Canvas");
                GameVariables.playerMoveable = false;
            }
            else if (obj.gameObject.name == "Door3" && !GameVariables.p3complete) {
                enableCanvas("p3Canvas");
                GameVariables.playerMoveable = false;
            }
        }
        else if (obj.gameObject.name == "laptop01")
        {
            enableCanvas("p1Canvas2");
            GameVariables.playerMoveable = false;
        }
        else if (obj.gameObject.name == "GreenCarpet")
        {
            if (GameVariables.p2option == 4)
                GameVariables.p2DoorOpen = true;
        }
        else if (obj.gameObject.name == "p2Complete")
        {
		          GameVariables.currentPuzzle++;
            GameVariables.p2complete = true;
            GameObject.Find("HelpPanelText").GetComponent<UnityEngine.UI.Text>().text = "To complete this puzzle you"
            + " will need to use 2 conditions with an AND or an OR.\n\nAND will only equate to TRUE when both of the "
            + "conditions used with it are TRUE.\nOR will only equate to FALSE when both of the conditions used with "
            + "it are FALSE.";
            Destroy(obj);
        }

    }

    void OnTriggerStay(Collider obj)
    {
        if(anim.GetBool("Pick"))
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
        }
    }

    void OnTriggerExit(Collider obj)
    {
        if (obj.gameObject.name == "GreenCarpet")
        {
            if (GameVariables.p2option == 4)
                GameVariables.p2DoorOpen = false;
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
