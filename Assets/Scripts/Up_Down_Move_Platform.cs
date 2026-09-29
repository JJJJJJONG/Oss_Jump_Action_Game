using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up_Down_Move_Platform : MonoBehaviour
{
    public Transform startpos;
    public Transform endpos;
    public Transform despos;
    public float speed;

    //발판 위에 올라탄 플레이어 (발판이 움직인 만큼 같이 이동시켜서 미끄러지거나 튕기지 않게 함)
    readonly HashSet<Rigidbody2D> riders = new HashSet<Rigidbody2D>();

    void Start()
    {
        transform.position = startpos.position;
        despos = endpos;
    }

    void FixedUpdate()
    {
        Vector3 before = transform.position;
        Vector2 next = Vector2.MoveTowards(transform.position, despos.position, Time.deltaTime * speed);
        //Vector2를 그대로 대입하면 z가 0으로 바뀌므로 z 유지
        transform.position = new Vector3(next.x, next.y, before.z);

        Vector2 delta = transform.position - before;
        foreach (Rigidbody2D rider in riders)
        {
            if (rider != null)
                rider.position += delta;
        }

        if (Vector2.Distance(transform.position, despos.position) <= 0.05f)
        {
            if (despos == endpos)
            {
                despos = startpos;
            }

            else
            {
                despos = endpos;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.rigidbody != null
            && collision.transform.position.y > transform.position.y)
        {
            riders.Add(collision.rigidbody);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.rigidbody != null)
            riders.Remove(collision.rigidbody);
    }

}