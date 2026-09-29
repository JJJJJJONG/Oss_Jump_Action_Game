using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Professor : MonoBehaviour
{
    public float Speed;
    public float Upspeed;
    public float max;
    public float maxrange;
    private float a = 0;
    private float zerox;
    private float zeroy;
    private float firstspeed;

    TilemapCollider2D tile_collider;

    // Start is called before the first frame update
    void Start()
    {
        zerox = transform.localPosition.x;
        zeroy = transform.localPosition.y;
        firstspeed = Speed;
        tile_collider = GetComponent<TilemapCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //플레이어와 발판은 부모가 달라 localPosition끼리 비교하면 안 됨 -> 콜라이더 경계로 "위에서 밟았는지" 판단
        if (collision.gameObject.tag == "Player" && collision.collider.bounds.min.y >= tile_collider.bounds.max.y - 0.1f)
        {
            a = Upspeed;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float r = Speed + transform.localPosition.x;
        float r2 = a + transform.localPosition.y;
        if (transform.localPosition.x == zerox && transform.localPosition.y == zeroy)
        {
            tile_collider.enabled = true;
        }
        if (a > 0)
        {
            Speed = 0;
        }
        transform.localPosition = new Vector2(r, r2);
        if (transform.localPosition.y > max)
        {
            a = 0;
        }
        if (Speed == 0 && a == 0)
        {
            Break_Later();
        }
        if (Speed > 0)
        {
            if (transform.localPosition.x > maxrange)
            {
                Break_Later();
            }
        }
        else if (Speed < 0)
        {
            if (transform.localPosition.x < maxrange)
            {
                Break_Later();
            }
        }
    }

    //FixedUpdate마다 Invoke가 새로 쌓여서 초기화가 수십 번 연달아 일어나던 문제 방지
    void Break_Later()
    {
        if (!IsInvoking("breakplaform"))
            Invoke("breakplaform", 5f);
    }

    void breakplaform()
    {
        tile_collider.enabled = false;
        Speed = firstspeed;
        transform.localPosition = new Vector2(zerox, zeroy);
        a = 0;
    }
}