using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    //弾を撃つことのできる角度
    public float m_FireMaxAngle;

    //移動スピード
    public float m_Speed = 5f;
    public float m_DashSpeed = 10f;

    //ダッシュする時間
    public float m_DashTime = 2f;

    //ジャンプ力
    public float m_JumpPower = 5f;
    public float m_SecondJumpPower = 5f;

    //体力
    public float m_HP;

    //地面のレイヤー
    public LayerMask m_GroundLayer;

    //ジャージ時に表示するオブジェクト
    public GameObject m_ChargeObject;

    //チャージが完了するまでの時間
    public float m_ChargeTime;

    //右を向いてスタートするかのフラグ
    public bool m_RightStart;

    public GameObject m_AfterimageObject;

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

    //弾の発射する方向
    private Vector2 m_FireDir = new Vector2(1, 0);

    //アナログスティックの入力値
    float m_AxisX;
    float m_AxisY;

    //弾をリスト化するための配列
    private GameObject[] m_BulletList;

    //現在のチャージ時間
    private float m_ChargeCurrentTime;

    //チャージしているときに出現するオブジェクトの実態
    private GameObject m_ChargeingObject;

    //ジャンプしているかどうかのフラグ
    private bool m_IsJump = false;
    private bool m_IsSecondJump = false;

    //攻撃しているかどうかのフラグ
    private bool m_IsFire = false;

    //ダッシュ状態かどうかのフラグ
    private bool m_IsDash = false;

    //現在のダッシュ時間
    private float m_DashCurrentTime;

    //生きているかどうかのフラグ
    private bool m_IsSurvival = true;

    //コンポーネント用の変数
    private Rigidbody2D m_Rigidbody;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;

    //=============================================================================
    //
    // Purpose : 初期化関数．
    //
    // Return : エラーが起きたら-1を返す。．
    //
    //=============================================================================
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

        //右向きのフラグが立っていたら右を向かせる
        m_SpriteRenderer.flipX = m_RightStart;
        m_FireDir.x = m_RightStart ? 1 : -1;
    }

    //=============================================================================
    //
    // Purpose : 更新関数．
    //
    //=============================================================================
    void Update()
    {
        //プレイヤーが生きてたら
        if(m_IsSurvival)
        {
            //攻撃しているかのチェック
            FireCheck();

            //移動
            Move();

            //攻撃
            Fire();

            //ジャンプ
            Jump();

            //ダッシュ
            Dash();

            //弾の変更
            ChangeBullet();

            //地面の判定
            GroundCheck();
        }
    }

    //=============================================================================
    //
    // Purpose : 攻撃のチェック関数
    //
    // Return : 攻撃していたらtrue,していなかったらfalseを返す。
    //
    //=============================================================================
    private bool FireCheck()
    {
        AnimatorStateInfo stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);

        if (!stateInfo.IsName("PlayerFire") && !stateInfo.IsName("PlayerUpFire") && !stateInfo.IsName("PlayerDownFire")
        && !stateInfo.IsName("PlayerJumpUpFire") && !stateInfo.IsName("PlayerJumpFire") && !stateInfo.IsName("PlayerJumpDownFire"))
        {
            m_IsFire = false;
        }

        return m_IsFire;
    }

    //=============================================================================
    //
    // Purpose : 移動関数
    //
    //=============================================================================
    private void Move()
    {
        //スティックの入力を取得
        m_AxisX = Input.GetAxis("Horizontal");
        m_AxisY = Input.GetAxis("Vertical");

        //X軸の入力があったら
        if (m_AxisX != 0f && !m_IsFire)
        {
            m_Rigidbody.velocity = new Vector2(m_AxisX * m_Speed, m_Rigidbody.velocity.y);

            m_SpriteRenderer.flipX = m_AxisX > 0;

            m_FireDir.x = Mathf.Abs(m_FireDir.x) * Mathf.Sign(m_AxisX);
        }
        else
        {
            m_Rigidbody.velocity = new Vector2(0, m_Rigidbody.velocity.y);
        }

        m_Animator.SetFloat("Speed", Mathf.Abs(m_Rigidbody.velocity.x));
    }

    //=============================================================================
    //
    // Purpose : 攻撃関数
    //
    //=============================================================================
    private void Fire()
    {
        //1ボタンが押されていたら
        if (Input.GetKey("joystick button 0"))
        {
            //チャージ時間を加算する
            m_ChargeCurrentTime += Time.deltaTime;

            if(m_ChargeingObject)
            {
                m_ChargeingObject.transform.position = transform.position + new Vector3(0, -0.5f);

                if (m_ChargeCurrentTime >= m_ChargeTime)
                {
                    m_ChargeingObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0, 0);
                }
            }
            else if(m_ChargeCurrentTime >= 0.5)
            {
                m_ChargeingObject = Instantiate(m_ChargeObject);
                m_ChargeingObject.transform.position = transform.position + new Vector3(0, -0.5f);
            }
            
        }

        //1ボタンが離されたら
        if (Input.GetKeyUp("joystick button 0"))
        {
            //生成する弾のオブジェクト
            GameObject BulletObj;

            //チャージが完了していたら
            if (m_ChargeCurrentTime >= m_ChargeTime)
            {
                //選択されている弾を生成
                BulletObj = m_BulletList[(int)m_BulletType];
            }
            else
            {
                //通常弾を生成
                BulletObj = m_BulletList[0];
            }

            if(m_AxisY > 0.5)
            {
                BulletObj.GetComponent<Bullet>().SpawnBullet(transform.position, Quaternion.Euler(0, 0, m_FireMaxAngle / 2 * Mathf.Sign(m_FireDir.x)) * new Vector2(Mathf.Sign(m_FireDir.x), 0));
                m_Animator.SetInteger("FireDir", 1);
            }
            else if(m_AxisY < -0.5)
            {
                BulletObj.GetComponent<Bullet>().SpawnBullet(transform.position, Quaternion.Euler(0, 0, -m_FireMaxAngle / 2 * Mathf.Sign(m_FireDir.x)) * new Vector2(Mathf.Sign(m_FireDir.x), 0));
                m_Animator.SetInteger("FireDir", -1);
            }
            else
            {
                BulletObj.GetComponent<Bullet>().SpawnBullet(transform.position, new Vector2(Mathf.Sign(m_FireDir.x), 0));
                m_Animator.SetInteger("FireDir", 0);
            }

            m_Animator.SetTrigger("FireTrigger");

            m_IsFire = true;

            m_ChargeCurrentTime = 0;

            Destroy(m_ChargeingObject);

            m_ChargeingObject = null;
        }
    }

    //=============================================================================
    //
    // Purpose 地面の判定関数
    //
    //=============================================================================
    private void GroundCheck()
    {
        Vector2 Start = transform.position;

        Vector2 End = new Vector2(transform.position.x, transform.position.y - GetComponent<SpriteRenderer>().bounds.size.y / 2 - 0.1f);

        if (Physics2D.Linecast(Start, End, m_GroundLayer))
        {
            m_Animator.SetBool("IsFall", false);
            m_IsJump = false;
            m_IsSecondJump = false;
        }
        else
        {
            m_Animator.SetBool("IsFall", true);
            m_Animator.SetBool("IsJump", false);
            m_IsJump = true;
        }
    }

    //=============================================================================
    //
    // Purpose 弾の種類変更関数
    //
    //=============================================================================
    void ChangeBullet()
    {
        //3ボタンが押されたら
        if (Input.GetKeyDown("joystick button 2") && !m_IsFire)
        {
            if (!m_IsJump)
            {
                m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, m_JumpPower);

                m_Animator.SetBool("IsJump", true);

                m_IsJump = true;
            }
            else if (!m_IsSecondJump)
            {
                m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, m_SecondJumpPower);

                m_Animator.SetTrigger("SecondJumpTrigger");

                m_IsSecondJump = true;
            }
        }
    }

    //=============================================================================
    //
    // Purpose ジャンプ関数
    //
    //=============================================================================
    void Jump()
    {
        if (Input.GetKeyDown("joystick button 6"))
        {
            if ((m_BulletType -= 1) < Bullet.BulletType.Speed)
            {
                m_BulletType = Bullet.BulletType.Division;
            }
        }

        if (Input.GetKeyDown("joystick button 7"))
        {
            if ((m_BulletType += 1) > Bullet.BulletType.Division)
            {
                m_BulletType = Bullet.BulletType.Speed;
            }
        }
    }

    //=============================================================================
    //
    // Purpose ダッシュ　関数
    //
    //=============================================================================
    void Dash()
    {
        if (Input.GetKeyDown("joystick button 5") && !m_IsJump && !m_IsSecondJump && m_Rigidbody.velocity.x != 0)
        {
            m_IsDash = true;
            m_Animator.SetBool("IsDash", true);
            m_DashCurrentTime = 0;
        }

        if(Input.GetKeyUp("joystick button 5"))
        {
            m_IsDash = false;
            m_Animator.SetBool("IsDash", false);
        }

        if (m_IsDash)
        {
            if((m_DashCurrentTime += Time.deltaTime) < m_DashTime)
            {
                m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.normalized.x * m_DashSpeed, m_Rigidbody.velocity.y);

                m_AfterimageObject.GetComponent<Afterimage>().SpawnAfterimage(m_SpriteRenderer.sprite, transform.position,m_SpriteRenderer.flipX);
            }
            else
            {
                m_IsDash = false;
                m_Animator.SetBool("IsDash", false);
            }
        }
    }

    //=============================================================================
    //
    // Purpose オブジェクトとヒットした瞬間
    //
    //=============================================================================
    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.gameObject.tag == "Enemy")
        {
            m_HP -= hit.gameObject.GetComponent<Enemy>().m_Damage;

            m_Animator.SetTrigger("DamageTrigger");
        }

        if (hit.gameObject.tag == "EnemyBullet")
        {
            m_HP -= hit.gameObject.GetComponent<Bullet>().m_Damage;

            m_Animator.SetTrigger("DamageTrigger");
        }

        if(m_HP <= 0)
        {
            m_IsSurvival = false;
            m_Animator.SetTrigger("DieTrigger");
        }
    }
}
