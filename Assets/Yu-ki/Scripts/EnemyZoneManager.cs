using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyZoneManager : MonoBehaviour {

    //リスポーンする敵
    public GameObject[] m_SpawnEnemy;

    public GameObject[] m_SpawnPoint;

    //エネミーゾーンのコリジョン
    public GameObject[] m_CollisionObject;

    //リスポーンする数
    public int m_SpawnNum;

    //1度に出して置ける敵の数
    public int m_EnemyNum;

    //リスポーンする間隔
    public float m_SpawnTime;

    //エネミーゾーンに入ったかどうか
    public bool m_IsEnter = false;



    //リスポーンがすべて完了したかどうか
    public bool m_IsSpawnFinish;

    //前回出現してからの時間
    private float m_SpawnCurrentTime = 0;

    //リスポーンしたエネミーの親となるオブジェクト
    private GameObject m_EnemyPar;

    //今何体リスポーンさせているか
    private int m_SpawnCurrentNum = 0;

	// Use this for initialization
	void Start () {
        m_EnemyPar = new GameObject();

        m_EnemyPar.transform.parent = transform;

        for(int i = 0;i < m_CollisionObject.Length;i++)
        {
            m_CollisionObject[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsEnter && !m_IsSpawnFinish && (m_SpawnCurrentTime += Time.deltaTime) >= m_SpawnTime && m_EnemyNum > m_EnemyPar.transform.childCount)
        {
            Spawn();

            m_SpawnCurrentTime = 0;

            if(m_SpawnCurrentNum >= m_SpawnNum)
            {
                m_IsSpawnFinish = true;
            }
        }

        if(m_IsSpawnFinish && m_EnemyPar.transform.childCount <= 0)
        {
            GameObject.Find("Main Camera").GetComponent<CameraMove>().slide_x = true;

            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.tag == "Player" && !m_IsEnter)
        {
            GameObject.Find("Main Camera").GetComponent<CameraMove>().slide_x = false;

            m_IsEnter = true;

            for(int i = 0;i < m_SpawnPoint.Length;i++)
            {
                Spawn(m_SpawnPoint[i].transform);
            }

            for (int i = 0; i < m_CollisionObject.Length; i++)
            {
                m_CollisionObject[i].SetActive(true);
            }
        }
    }

    //敵のスポーン関数
    void Spawn()
    {
        GameObject SpawnEnemy = Instantiate(m_SpawnEnemy[Random.Range(0, m_SpawnEnemy.Length)]);

        SpawnEnemy.transform.position = m_SpawnPoint[Random.Range(0, m_SpawnPoint.Length)].transform.position;

        SpawnEnemy.transform.parent = m_EnemyPar.transform;

        m_SpawnCurrentNum++;
    }

    //敵のスポーン関数(座標指定)
    void Spawn(Transform pos)
    {
        GameObject SpawnEnemy = Instantiate(m_SpawnEnemy[Random.Range(0, m_SpawnEnemy.Length)]);

        SpawnEnemy.transform.position = pos.position;

        SpawnEnemy.transform.parent = m_EnemyPar.transform;

        m_SpawnCurrentNum++;
    }
}
