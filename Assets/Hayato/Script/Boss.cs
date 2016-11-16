using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {

    public GameObject normal_bullet;
    public GameObject diffusion_bullet;

    public float ChargeTime;
    public float JumpPower = 6f;

    public float Angle_max;

    //地面のレイヤー
    public LayerMask m_GroundLayer;

    private Animator anim;
    private Rigidbody2D rigid2d;

    int direction = -1;

    //弾の発射する方向
    private Vector2 m_FireDir;

    int MoveType;
    float NextMoveTime = 0.0f;

    bool m_Air;
    bool m_Magic;
    bool m_AirAttack;

    bool m_PlayerFlag;
    
    // Use this for initialization
    protected override void Start () {

        base.Start();

        MoveType = 1;

        m_Air = true;
        m_Magic = false;
        m_AirAttack = true;
        m_PlayerFlag = true;

        anim = GetComponent<Animator>();
        rigid2d = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {


        if(m_PlayerFlag == true)
        {
            NextMoveTime += Time.deltaTime;

            switch (MoveType)
            {
                case 0:

                    Idle();

                    break;

                case 1:

                    Jump();

                    break;

                case 2:

                    Air();

                    break;

                case 3:

                    Magic();

                    break;
            }
        }

        Direction();

        Ground();
	}

    void Idle()
    {
        if(NextMoveTime > 2f)
        {
            MoveType = 1;

            NextMoveTime = 0;
        }
    }

    void Jump()
    {
        if (m_Air == false)
        {
            rigid2d.velocity = new Vector2(rigid2d.velocity.x, JumpPower);

            anim.SetBool("Jump", true);

            m_AirAttack = true;

            m_Air = true;

            MoveType = 2;
        }
    }
    
    void Air()
    {
        if(m_AirAttack == true)
        {
            if(NextMoveTime >= 2f)
            {
                m_Magic = true;

                NextMoveTime = 0;

                MoveType = 3;
            }

            rigid2d.gravityScale = 0f;

            anim.SetBool("Air", true);
        }
        else
        {
            anim.SetBool("Magic", false);

            rigid2d.gravityScale = 1;

            if(m_Air == false)
            {
                MoveType = 0;

                NextMoveTime = 0;
            }
        }

    }

    void Charge()
    {

    }

    void Magic()
    {
        if(m_AirAttack == true && m_Magic == true)
        {
            anim.SetBool("Magic", true);

            if(NextMoveTime > 2f)
            {
                m_FireDir = new Vector2(direction, 0);

                diffusion_bullet.GetComponent<Bullet>().SpawnBullet(transform.position, Quaternion.Euler(0, 0, -Angle_max / 2 * Mathf.Sign(m_FireDir.x)) * new Vector2(Mathf.Sign(m_FireDir.x), 0));

                m_Magic = false;

                m_AirAttack = false;

                MoveType = 2;

                NextMoveTime = 0;
            }

            
        }
    }

    void Direction()
    {
        Vector3 scale = transform.localScale;
        float dx = (target.transform.position.x - transform.position.x);

        if (dx > 0.1f)
        {
            scale.x = -1f;
            direction = 1;
        }
        else
        {
            scale.x = 1f;
            direction = -1;
        }

        transform.localScale = scale;
    }

    void Ground()
    {
        Vector2 Start = transform.position;

        Vector2 End = new Vector2(transform.position.x, transform.position.y - GetComponent<SpriteRenderer>().bounds.size.y / 2 - 0.1f);

        if (Physics2D.Linecast(Start, End, m_GroundLayer))
        {
            anim.SetBool("Air", false);
            m_Air = false;
            
        }
        else
        {
            anim.SetBool("Air", true);
            anim.SetBool("Jump", false);
            m_Air = true;
        }
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            HP -= other.gameObject.GetComponent<Bullet>().m_Damage;

            if (HP <= 0)
            {
                m_PlayerFlag = false;

                rigid2d.gravityScale = 1;

                anim.SetTrigger("Die");
                
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet" && damage_flag == true && other.gameObject.GetComponent<Bullet>().m_Type == Bullet.BulletType.Penetration)
        {
            damage_flag = false;
            HP -= other.gameObject.GetComponent<Bullet>().m_Damage;

            if (HP <= 0)
            {
                rigid2d.gravityScale = 1;

                
                anim.SetTrigger("Die");
                
            }
        }
    }

}
