using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Platform: MonoBehaviour
{
    [SerializeField] float fall_time = 0.5f, destory_time = 2f, respawn_time = 3f;
    Rigidbody2D rb;
    Vector3 start_pos;
    Quaternion start_rot;
    bool is_falling;
    Collider2D[] colliders;
    Renderer[] renderers;

    private void Start()
    {
        rb  = GetComponent<Rigidbody2D>();
        start_pos = transform.position;
        start_rot = transform.rotation;
        colliders = GetComponentsInChildren<Collider2D>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //이미 떨어지는 중이면 Invoke가 중복 예약되지 않도록 무시
        if (is_falling)
            return;

        if(collision.gameObject.tag == "Player" && collision.transform.position.y > transform.position.y)
        {
            is_falling = true;
            Invoke("Fall_Platform", fall_time);
            Invoke("De_Active", destory_time);
        }
    }

    void Fall_Platform()
    {
        rb.isKinematic = false;
    }

    //예전에는 SetActive(false)로 영구히 사라져서, 죽고 리스폰하면 지나갈 발판이 없어졌음
    //-> 숨겼다가 respawn_time 후 원래 자리에 다시 생성
    void De_Active()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        Set_Visible(false);
        Invoke("Respawn", respawn_time);
    }

    void Respawn()
    {
        transform.SetPositionAndRotation(start_pos, start_rot);
        Set_Visible(true);
        is_falling = false;
    }

    void Set_Visible(bool visible)
    {
        foreach (Collider2D col in colliders)
            col.enabled = visible;
        foreach (Renderer ren in renderers)
            ren.enabled = visible;
    }

}