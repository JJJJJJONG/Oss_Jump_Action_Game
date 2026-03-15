using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_move : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriterenderer;
    Animator animator;
    CapsuleCollider2D capsulecollider;
    public int nextmove;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
        capsulecollider = GetComponent<CapsuleCollider2D>();
        Invoke("Think", 5);
    }


    void FixedUpdate()
    {
        //움직임
        rigid.velocity = new Vector2(nextmove, rigid.velocity.y);

        //낭떠러지 체크
        Vector2 front_vector = new Vector2(rigid.position.x + (nextmove*0.4f), rigid.position.y);

        Debug.DrawRay(front_vector, Vector3.down, new Color(0, 1, 0));

        RaycastHit2D rayhit = Physics2D.Raycast(front_vector, Vector3.down, 1, LayerMask.GetMask("Platform"));

        if (rayhit.collider == null)
        {
            Turn();
        }


    }
    void Think() //반복적으로 돌아다니게 하는 함수
    {   
        //다음활동 설정
        nextmove = Random.Range(-1, 2); //-1 0 1 중에 랜덤으로 속도 결정
        //애니메이션 추가
        animator.SetInteger("work_speed", nextmove);
        //방향 전환
        if (nextmove != 0)
        {
          spriterenderer.flipX = nextmove == 1;
        }
        //재귀
        float next_think = Random.Range(2f, 5f);
        Invoke("Think", next_think);
    }

    void Turn()
    {
        nextmove *= -1;
        spriterenderer.flipX = nextmove == 1;
        CancelInvoke();
        Invoke("Think", 2); 
    }

    public void Enemy_Damaged()
    {   

        //충돌 후 밀림 방지
        gameObject.layer = 8;
        //콜라이더 비활성화
        capsulecollider.enabled = false;
        //색깔 바꾸기
        spriterenderer.color = new Color(1, 1, 1, 0.4f);
        //뒤집기
        spriterenderer.flipY = true;
        //점프 모션
        rigid.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
        //후에 떨어지고 사라 짐
        Invoke("De_Active", 5);
    }

    void De_Active()
    {
        gameObject.SetActive(false);
    }


}