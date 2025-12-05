using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Turren_Controller : MonoBehaviour
{
    DynamicJoystick leftJoyStick;
    bool player_Enter;
    GameObject torren_gun;
    GameObject gun;
    float currentBearing;
    float newBearing;
    PlayerControler player;
    [HideInInspector] public bool automatic;
    [HideInInspector] public Transform target;
    GunControler_Turret gunControler_Turret;



    private void Awake()
    {
        leftJoyStick = GameObject.Find("Dynamic Joystick").GetComponent<DynamicJoystick>();
        gunControler_Turret = GetComponent<GunControler_Turret>();
        torren_gun = transform.Find("tower").gameObject;
        gun = transform.Find("tower/gun").gameObject;
        

    }

    void Start()
    {
        player = PlayerControler.instance;
    }

    private void Update()
    {
        if (player_Enter && !automatic)
        {
            
            torren_gun.transform.Rotate(0, leftJoyStick.input.x ,0);
            newBearing = currentBearing + (leftJoyStick.input.x * 1.5f);
            SetCurrentBearing(newBearing);

           

            if (leftJoyStick.input.y <= -0.9f)
            {
                PlayerOutTorrent();
            }

        }
        else
        {
            if (automatic)
            {
                if (target == null) 
                {
                    gunControler_Turret.active_Gun = false;
                } 
                else
                {
                    if (target.gameObject.activeSelf)
                    {
                        torren_gun.transform.LookAt(target);
                        gunControler_Turret.active_Gun = true; // test => kich hoat 1 lan
                    }
                    else
                    {
                        target = null;
                    }

                }

            }
        }
    }


    void SetCurrentBearing(float rot)
    {
        currentBearing = Mathf.Clamp(rot, -200, -140);
        torren_gun.transform.rotation = Quaternion.Euler(0, rot, 0);
    }

    void PlayerOutTorrent()
    {
        player_Enter = false;
        player.gameObject.transform.SetParent(null);
        player.animator.SetBool("Hold_Torren", false);
        player.enter_Torren = false;
        player.GetComponent<Rigidbody>().isKinematic = false;
        gun.GetComponent<LineRenderer>().enabled = false;
        gun.GetComponent<Laser>().enabled = false;
        CameraFollow.instance.offset = new Vector3(0, 12, 7);
        Camera.main.transform.Rotate(new Vector3(30, 0, 0));
        gunControler_Turret.active_Gun = false;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!player_Enter && !automatic)
            {
                if(PlayerPrefs.GetInt(StringManager.TUT_STAGE) == 3)
                    TutorialManager.ins.CheckStageTutorial();

                player_Enter = true;
                player.gameObject.transform.parent = torren_gun.transform;
                player.gameObject.transform.localPosition = new Vector3(0, 0, -2f);
                player.gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);
                player.animator.SetBool("Hold_Torren", true);
                player.enter_Torren = true;
                CameraFollow.instance.offset = new Vector3(0, 4, 7);
                Camera.main.transform.Rotate(new Vector3(-30, 0, 0));
                player.GetComponent<Rigidbody>().isKinematic = true;
                gun.GetComponent<LineRenderer>().enabled = true;
                gun.GetComponent<Laser>().enabled = true;
                gunControler_Turret.active_Gun = true;


            }
           
        }
    }




}
