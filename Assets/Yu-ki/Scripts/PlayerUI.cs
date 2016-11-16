using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public GameObject m_Player;

    public Text m_BulletTypeText;

    public RectTransform m_HPRedBar;
    public RectTransform m_HPGreenBer;
    public float m_RedBarSpeed = 0.3f;

    private Player m_PlayerComp;
    
    //HPの最大値
    private float m_MaxHP;

	// Use this for initialization
	void Start () {

        m_PlayerComp = m_Player.GetComponent<Player>();
        m_MaxHP = m_PlayerComp.m_HP;

        m_PlayerComp.m_HP = 50;
	}
	
	// Update is called once per frame
	void Update () {

        m_BulletTypeText.text = m_PlayerComp.m_BulletType.ToString();

        m_HPGreenBer.localScale = new Vector3(m_PlayerComp.m_HP / m_MaxHP, m_HPGreenBer.localScale.y, m_HPGreenBer.localScale.z);

        if(m_HPRedBar.localScale.x > m_HPGreenBer.localScale.x)
        {
            m_HPRedBar.localScale -= new Vector3(m_RedBarSpeed * Time.deltaTime,0,0);
        }

        if (m_HPRedBar.localScale.x < m_HPGreenBer.localScale.x)
        {
            m_HPRedBar.localScale = m_HPGreenBer.localScale;
        }

    }
}
