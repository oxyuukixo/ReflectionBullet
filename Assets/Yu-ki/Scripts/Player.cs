using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    Vector2 m_FireDir = new Vector2(1, 0);

    public GameObject m_Bullet;

    public float m_Speed = 5f;

    public float m_JumpPower = 5f;

    public LayerMask GroundLayer;

    private RaycastHit2D m_ray;

    private Rigidbody2D m_Rigidbody;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;

    // Use this for initialization
    void Start()
    {

        //コンポーネントの取得
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //スティックの入力を取得
        float XAxis = Input.GetAxis("Horizontal");
        float YAxis = Input.GetAxis("Vertical");

        //X軸の入力があったら
        if (XAxis != 0f)
        {
            m_Rigidbody.velocity = new Vector2(XAxis * m_Speed, m_Rigidbody.velocity.y);


            m_SpriteRenderer.flipX = XAxis > 0;


            if (Mathf.Abs(XAxis) >= 1)
            {
                m_FireDir.x = Mathf.Abs(m_FireDir.x) * Mathf.Sign(XAxis);
            }
        }
        else
        {
            m_Rigidbody.velocity = new Vector2(0, m_Rigidbody.velocity.y);
        }

        m_Animator.SetFloat("Speed", Mathf.Abs(m_Rigidbody.velocity.x));

        //Y軸の入力があったら
        if (YAxis != 0f)
        {
            m_FireDir = Quaternion.Euler(0, 0, YAxis * Mathf.Sign(m_FireDir.x)) * m_FireDir;
        }

        //1ボタンが押されたら
        if (Input.GetKeyDown("joystick button 0"))
        {
            GameObject BulletObj = Instantiate(m_Bullet);

            BulletObj.transform.position = transform.position;

            BulletObj.GetComponent<Rigidbody2D>().velocity = m_FireDir * 20;
        }

        //3ボタンが押されたら
        if (Input.GetKeyDown("joystick button 2"))
        {
            m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, m_JumpPower);

            m_Animator.SetTrigger("JumpTrigger");
        }

        //弾の打つ方向へRayを飛ばす
        m_ray = Physics2D.Raycast(transform.position, -Vector2.up);

        Debug.DrawRay(transform.position, m_FireDir * 5000, new Color(255, 0, 0));

        Vector2 Start = transform.position;

        Vector2 End = new Vector2(transform.position.x, transform.position.y - GetComponent<SpriteRenderer>().bounds.size.y / 2);

        if (Physics2D.Linecast(Start, End, GroundLayer))
        {
            m_Animator.SetBool("IsFall", false);
        }
        else
        {
            m_Animator.SetBool("IsFall", true);
        }
    }
}
