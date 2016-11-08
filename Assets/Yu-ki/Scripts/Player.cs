using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    //弾の発射する方法
    Vector2 m_FireDir = new Vector2(1, 0);

    //移動スピード
    public float m_Speed = 5f;

    //ジャンプ力
    public float m_JumpPower = 5f;

    //地面のレイヤー
    public LayerMask m_GroundLayer;

    //チャージが完了するまでの時間
    public float m_ChargeTime;

    //使用する弾
    public GameObject m_NormalBullet;       //通常弾
    public GameObject m_SpeedBullet;          //スピード弾
    public GameObject m_PenetrationBullet;    //貫通弾
    public GameObject m_DiffusionBullet;      //拡散弾
    public GameObject m_ExplosionBullet;      //爆裂弾
    public GameObject m_DivisionBullet;        //分裂弾

    //弾の種類
    [HideInInspector]
    public Bullet.BulletType m_BulletType;

    //地面のあたり判定をするためのレイ
    private RaycastHit2D m_ray;

    //弾をリスト化するための配列
    private GameObject[] m_BulletList;

    //現在のチャージ時間
    private float m_ChargeCurrentTime;

    //コンポーネント用の変数
    private Rigidbody2D m_Rigidbody;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;

    // Use this for initialization
    void Start()
    {
        m_BulletList = new GameObject[6];
        m_BulletList[0] = m_NormalBullet;
        m_BulletList[1] = m_SpeedBullet;
        m_BulletList[2] = m_PenetrationBullet;
        m_BulletList[3] = m_DiffusionBullet;
        m_BulletList[4] = m_ExplosionBullet;
        m_BulletList[5] = m_DivisionBullet;

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

            m_FireDir.x = Mathf.Abs(m_FireDir.x) * Mathf.Sign(XAxis);
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

        if(Input.GetKey("joystick button 0"))
        {
            m_ChargeCurrentTime += Time.deltaTime;
        }
        
        //1ボタンが離されたら
        if (Input.GetKeyUp("joystick button 0"))
        {
            //チャージが完了していたら
            if(m_ChargeCurrentTime > m_ChargeTime)
            {
                GameObject BulletObj = Instantiate(m_BulletList[(int)m_BulletType]);

                BulletObj.transform.position = transform.position;

                BulletObj.GetComponent<Rigidbody2D>().velocity = m_FireDir * 20;
            }
            else
            {
                GameObject BulletObj = Instantiate(m_BulletList[0]);

                BulletObj.transform.position = transform.position;

                BulletObj.GetComponent<Rigidbody2D>().velocity = m_FireDir * 20;
            }

            m_ChargeCurrentTime = 0;

        }

        //3ボタンが押されたら
        if (Input.GetKeyDown("joystick button 2"))
        {
            m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, m_JumpPower);

            m_Animator.SetTrigger("JumpTrigger");
        }

        if (Input.GetKeyDown("joystick button 4"))
        {
            if((m_BulletType -= 1) < Bullet.BulletType.Speed)
            {
                m_BulletType = Bullet.BulletType.Division;
            }
        }

        if (Input.GetKeyDown("joystick button 5"))
        {
            if ((m_BulletType += 1) > Bullet.BulletType.Division)
            {
                m_BulletType = Bullet.BulletType.Speed;
            }
        }

        //弾の打つ方向へRayを飛ばす
        m_ray = Physics2D.Raycast(transform.position, -Vector2.up);

        Debug.DrawRay(transform.position, m_FireDir * 5000, new Color(255, 0, 0));

        Vector2 Start = transform.position;

        Vector2 End = new Vector2(transform.position.x, transform.position.y - GetComponent<SpriteRenderer>().bounds.size.y / 2);

        if (Physics2D.Linecast(Start, End, m_GroundLayer))
        {
            m_Animator.SetBool("IsFall", false);
        }
        else
        {
            m_Animator.SetBool("IsFall", true);
        }
    }
}
