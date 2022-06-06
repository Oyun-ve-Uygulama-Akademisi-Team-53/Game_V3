using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderControl : MonoBehaviour    
{

    public Transform target;
    public float speed;
    Rigidbody rig;
    public int can = 100;
    
    PlayerController pc;
    public GameObject player;
    public bool yilan = false;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        pc = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!pc.oyunDurduMu) {

            if (!pc.zamanDurduMu) {

                if (!yilan)
                {

                    Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
                    rig.MovePosition(pos);
                    Vector3 lookVector = target.position - transform.position;
                    lookVector.y = transform.position.y;
                    Quaternion rot = Quaternion.LookRotation(lookVector);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
                }
                else
                {
                    Vector3 pos = Vector3.MoveTowards(transform.position, target.position, -speed * Time.fixedDeltaTime);
                    rig.MovePosition(pos);
                    Vector3 lookVector = -(target.position - transform.position);
                    lookVector.y = transform.position.y;
                    Quaternion rot = Quaternion.LookRotation(lookVector);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
                }
            }
        }



        if (can<= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
