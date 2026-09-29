using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class GameManager : MonoBehaviour
{
    //점수와 스테이지 관리
    public int totalpoint;
    public int totalpoint2;
    public int stagepoint;
    public int stagepoint2;
    public int stageindex;
    public int health;
    public int change;
    int tmp_point;
    public Player_move player_move;
    public GameObject[] stages;
    public GameObject[] stages2;
    public GameObject[] UI_game;
    public GameObject Game;
    public GameObject Store;
    public GameObject Main_Menu;
    public Image[] UI_health;
    public Image[] UI_health2;
    public Text UI_point;
    public Text UI_point2;
    public Text UI_stage;
    public Text UI_stage2;
    public GameObject UI_restart_button;
    public GameObject UI_restart_button2;
    public GameObject UI_main_button;
    public GameObject UI_main_button2;
    public GameObject[] Buy_Button;
    public GameObject jeongmoon;
    public GameObject jungmoon;
    public GameObject UI_retired;
    

    //상점
    public GameObject[] store_page;
    public Text UI_money;
    public Text UI_money2;
    public int[] item_price;
    AudioSource audiosource;
    public AudioClip[] audiobuy;
    public GameObject money1;
    public GameObject money2;

    //재시작 시 체력/하트 UI 복구용
    int max_health;
    Color heart_color = Color.white;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
        max_health = health;
        if (UI_health.Length > 0)
            heart_color = UI_health[0].color;
    }

    //현재 캐릭터(change)에 맞는 점수/돈에 더하기
    public void Add_Point(int point)
    {
        if (change == 0)
            stagepoint += point;
        else
            stagepoint2 += point;
    }

    int Current_Money()
    {
        return change == 0 ? totalpoint : totalpoint2;
    }

    void Spend_Money(int price)
    {
        if (change == 0)
            totalpoint -= price;
        else
            totalpoint2 -= price;
    }

    private void Update()
    {   
        UI_point.text = (totalpoint + stagepoint).ToString();
        UI_point2.text = (totalpoint2 + stagepoint2).ToString();
        UI_money.text = totalpoint.ToString();
        UI_money2.text = totalpoint2.ToString();
    }

    public void Next_Stage()
    {
        if (change == 0)
        {   
            money1.SetActive(true);
            money2.SetActive(false);

            if(stageindex == 0)
            {
                jeongmoon.SetActive(true);
                jungmoon.SetActive(false);
            }

            //스테이지 전환
            if (stageindex < stages.Length - 1) //스테이지 렝스가 총 2
            {
                stages[stageindex].SetActive(false);
                stageindex++;
                if (stageindex == 1)
                {
                    jeongmoon.SetActive(false);
                    jungmoon.SetActive(true);
                }
                stages[stageindex].SetActive(true);
                Player_Respawn();

                //UI_stage.text = "STAGE" + (stageindex + 1);
            }

            else //게임 클리어
            { 
                //시간 정지
                Time.timeScale = 0;
           
                veiw_button();
            }

            totalpoint += stagepoint;
            stagepoint = 0;
        }

        else if(change == 1)
        {
            if (stageindex == 0)
            {
                jeongmoon.SetActive(true);
                jungmoon.SetActive(false);
            }
            //스테이지 전환
            if (stageindex < stages2.Length - 1) //스테이지 렝스가 총 3
            {
               
                stages2[stageindex].SetActive(false);
                stageindex++;
                if (stageindex == 1)
                {
                    jeongmoon.SetActive(false);
                    jungmoon.SetActive(true);
                }
                stages2[stageindex].SetActive(true);
                Player_Respawn();

                //UI_stage2.text = "STAGE" + (stageindex + 1);
            }
            else //게임 클리어
            {
              
                if (stageindex == 1)
                {
                    jeongmoon.SetActive(false);
                    jungmoon.SetActive(true);
                }

                //시간 정지
                Time.timeScale = 0;
                veiw_button();
            }
            totalpoint2 += stagepoint2;
            stagepoint2 = 0;
        }


    }

    public void Health_Down()
    {
        if (change == 0)
        {
            if (health > 1)
            {
                health--;
                UI_health[health].color = new Color(1, 0, 0, 0.4f);
            }

            //health가 0일 때
            else
            {
                //모든 생명이 다했을 때
                UI_health[0].color = new Color(1, 0, 0, 0.4f);
                //플레이어 죽음 효과
                player_move.On_Die();
                //죽음 UI

                //재시도 UI
                veiw_button();
            }
        }

        else if(change == 1)
        {
            if (health > 1)
            {
                health--;
                UI_health2[health].color = new Color(1, 0, 0, 0.4f);
            }

            //health가 0일 때
            else
            {
                //모든 생명이 다했을 때
                UI_health2[0].color = new Color(1, 0, 0, 0.4f);
                //플레이어 죽음 효과
                player_move.On_Die();
                //죽음 UI

                //재시도 UI
                veiw_button();
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {   
        //낙하시 생명력 감소 
        if(collision.gameObject.tag == "Player")
        {   
            //리스폰
            if (health > 1)
            {   
                Player_Respawn();
            }

            //생명력 감소
            Health_Down();
        }
    }

    void Player_Respawn()
    {
        player_move.transform.position = new Vector3(0, 5, -1);
        player_move.Velocity_Zero();
    }
    void veiw_button()
    {
        if (change == 0)
        {   
            UI_retired.SetActive(true);
        }

        else if (change == 1)
        {
            UI_retired.SetActive(true);
        }

    }
    public void Restart()
    {
        Time.timeScale = 1;
        Game.SetActive(false);
        Store.SetActive(false);
        Main_Menu.SetActive(false);
        Game.SetActive(true);
        player_move.On_live();
        UI_retired.SetActive(false);
        //체력과 하트 UI를 되돌리지 않으면 재시작 직후 한 번만 맞아도 다시 죽음
        Reset_Health();
        Player_Respawn();
    }

    void Reset_Health()
    {
        health = max_health;
        foreach (Image heart in UI_health)
            heart.color = heart_color;
        foreach (Image heart in UI_health2)
            heart.color = heart_color;
    }

 
    public void Start_Game()
    {
        Game.SetActive(true);
        Store.SetActive(false);
        Main_Menu.SetActive(false);
        Player_Respawn();
        
    }

    public void Stop_Game()
    {
        Game.SetActive(false);
        Store.SetActive(false);
        Main_Menu.SetActive(true);
    }

    public void Start_Store()
    {
        Store.SetActive(true);
        Game.SetActive(false);
        Main_Menu.SetActive(false);
    }

    public void Stop_Store()
    {
        Store.SetActive(false);
        Game.SetActive(false);
        Main_Menu.SetActive(true);
    }

    public void buy(int index)
    {

        int price = item_price[index];

        //캐릭터는 한 번만 구매 가능 (두 번 사면 change가 2가 되어 스테이지/체력 처리가 멈춤)
        bool already_owned = index == 0 && change != 0;

        //캐릭터를 바꾼 뒤에는 totalpoint2가 실제 보유 금액
        if (price > Current_Money() || already_owned)
        {
            Play_Sound(1);
        }

        else
        {
            //구입 완료 창 UI

            if (index == 0)
            {
                Buy_Button[0].SetActive(true);
                Play_Sound(0);
                //구현
                change_UI();
                max_health += 1;
                change = 1;
                totalpoint = totalpoint - item_price[0];
                totalpoint2 = totalpoint;
                Reset_Health();
                money1.SetActive(false);
                money2.SetActive(true);
            }
            else if (index == 1)
            {
                Buy_Button[1].SetActive(true);
                Play_Sound(0);
                player_move.maxspeed += 2;
                Spend_Money(item_price[1]);
            }
            else if (index == 2)
            {
                Buy_Button[2].SetActive(true);
                Play_Sound(0); 
                player_move.jumppower += 2;
                Spend_Money(item_price[2]);
            }
            else if (index == 3)//과잠
            {
                Buy_Button[3].SetActive(true);
                Play_Sound(0);
                Spend_Money(item_price[3]);
            }
            else if(index == 4)
            {
                Buy_Button[4].SetActive(true);
                Play_Sound(0);
                Spend_Money(item_price[4]);
            }
     
        }
          
           
  
    }
    void Play_Sound(int flag) 
    {
        if (flag == 0)
        {
            audiosource.clip = audiobuy[0];
            audiosource.Play();
        }
        else if(flag == 1)
        {
            audiosource.clip = audiobuy[1];
            audiosource.Play();
        }
    }

    public void Next_Page_Btn()
    {
        store_page[0].SetActive(false);
        store_page[1].SetActive(true);
    }

    public void Return_Page_Btn()
    {
        store_page[1].SetActive(false);
        store_page[0].SetActive(true);
    }

    void change_UI()
    {
        UI_game[0].SetActive(false);
        UI_game[1].SetActive(true);
    }

    public void Close_Buy_UI(int index)
    {
        if (index == 0)
            Buy_Button[0].SetActive(false);
        if (index == 1)
            Buy_Button[1].SetActive(false);
        if (index == 2)
            Buy_Button[2].SetActive(false);
        if (index == 3)
            Buy_Button[3].SetActive(false);
        if (index == 4)
            Buy_Button[4].SetActive(false);
    }

}
