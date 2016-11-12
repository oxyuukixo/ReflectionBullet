using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimage : MonoBehaviour {

    public float m_DispTime;

    private SpriteRenderer m_SpriteRenderer;

	// Use this for initialization
	void Start () {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        m_SpriteRenderer.color -= new Color(0, 0, 0, (1 / m_DispTime) * Time.deltaTime);

        if(m_SpriteRenderer.color.a <= 0)
        {
            Destroy(gameObject);
        }
	}

    public void SpawnAfterimage(Sprite DispSprite,Vector2 Position,bool FilpX)
    {
        GameObject SpawnSprite = Instantiate(gameObject);

        SpawnSprite.transform.position = Position;

        SpawnSprite.GetComponent<SpriteRenderer>().sprite = DispSprite;
        SpawnSprite.GetComponent<SpriteRenderer>().flipX = FilpX;
    }
}
