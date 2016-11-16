using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    public GameObject normal_bullet;
    public GameObject diffusion_bullet;
    public GameObject target;

    private Animator anim;
    private Rigidbody2D rigid2d;

    public float HP;

    public float ChargeTime;
    public float JumpPower;

    int MoveType;
    float NextMoveTime = 0.0f;

    bool Air = false;
    bool Attack = false;
    bool Charge = false;
    bool Magic = false;
    bool Jump = false;

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
        rigid2d = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Stage")
        {
            anim.SetBool("Air",false);
        }
        else
        {
            anim.SetBool("Air", true);
        }
    }

}
