using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public GameObject m_Player;

    private Player m_PlayerComp;

    //使用するテキスト
    private Text m_BulletType;

	// Use this for initialization
	void Start () {

        m_PlayerComp = m_Player.GetComponent<Player>();

        m_BulletType = transform.FindChild("BulletType").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        m_BulletType.text = m_PlayerComp.m_BulletType.ToString();
	}
}
