using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_F: MonoBehaviour
{
    public float delta = 0.1f;
    public float max;
    private float a = 0;
    private float zerox;
    private float zeroy;
    BoxCollider2D box_collider;
    void Start()
    {
        zerox = transform.localPosition.x;
        zeroy = transform.localPosition.y;
        box_collider = GetComponent<BoxCollider2D>();
        a = delta;
    }

    // Update is called once per frame
    void Update()
    {
        //프레임마다 a만큼 이동하면 144Hz 모니터에서는 2.4배 빨라짐 -> 60fps 기준 속도로 보정
        float newXPosition = transform.localPosition.x + a * Time.deltaTime * 60f;
        transform.localPosition = new Vector2(newXPosition, transform.localPosition.y);


        if (delta < 0f)
        {
            if (transform.localPosition.x < max)
            {
                box_collider.enabled = false;
                transform.localPosition = new Vector2(zerox, zeroy);
                a = 0;
                Invoke("Move", 0.5f);
            }
        }
        else
        {
            if (transform.localPosition.x > max)
            {
                box_collider.enabled = false;
                transform.localPosition = new Vector2(zerox, zeroy);
                a = 0;
                Invoke("Move", 0.5f);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D Circle)//공과 장애물 충돌
    {
        if (Circle.gameObject.tag == "Player")
        {
            box_collider.enabled = false;
            transform.localPosition = new Vector2(zerox, zeroy);
            a = 0;
            Invoke("Move", 0.5f);
        }
    }
    void Move()
    {
        box_collider.enabled = true;
        a = delta;
    }
}