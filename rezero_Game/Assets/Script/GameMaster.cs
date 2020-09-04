using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;




public class GameMaster : MonoBehaviour
{
    public PlayableDirector StartCutinTimeline;


    public bool is_game_playing { get; set; } = false;
    public bool is_game_end { get; set; } = false;


    public BackGroundMaker BackGroundMaker;
    public CharacterControl CharacterControl;

    float totalTime { get; set; } = 3;
    int seconds;

    public float timer_time { get; set; } = 60;//タイム
 
    

    int count_gamestart = 0;



    public int _HP { get; set; } = 5;//HP
    public bool is_invincible { get; set; } = false;//無敵中
    public float invincible_time { get; set; } = 0.5f;//無敵時間

    public int character_number { get; set; } = 0;// レム０　ラム１ レム2

    public bool is_attack { get; set; } = false;//アタック判定
    public bool is_special { get; set; } = false;//special判定１F
    public bool during_special { get; set; } = false;//specialモーション中

    public int special_count { get; set; } = 3;//specialうてる回数

    [SerializeField] CircleCollider2D Character_Image_collider = default;

    public int score_tortal_point { get; set; } = 0;//スコアトータルポイント
    public int score_point { get; set; } = 10;//弾一個10点

    public bool is_attack_slider_full { get; set; } = false; //アタックスライダーフルフラグ


    // Start is called before the first frame update
    void Start()
    {
        BackGroundMaker.SetBackGround();
        CharacterControl.enabled = false;
        StartCutinTimeline.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
      
        CountDown();

        if (is_game_playing)
        {
            BackGroundMaker.BackGroundMove();
            if(count_gamestart == 0)
            {
                //ゲーム開始時一回だけ処理
                CharacterControl.enabled = true;

                count_gamestart = 1;
            }

            Timer();
        }


        if (during_special)
        {
            invincibleTime_Special();
        }
        else
        {
            InvincibleTime();
        }

        
        


    }

    void CountDown()
    {
        totalTime -= Time.deltaTime;
        

        if (totalTime < 0)
        {
            totalTime = 0;
            is_game_playing = true;
           // Debug.Log("go");
            
        }
        seconds = (int)totalTime+1;

        
    }

    void Timer()
    {
        timer_time -= Time.deltaTime;


        if (timer_time < 0)
        {
            timer_time = 0;
            

        }
       


    }

    void InvincibleTime()
    {
        if(is_invincible == true)
        {
            Character_Image_collider.enabled = false;
        }
        else
        {
            Character_Image_collider.enabled = true;

        }
    }

    void invincibleTime_Special()
    {
        if (during_special)
        {
            Character_Image_collider.enabled = false;
        }
        else
        {
            Character_Image_collider.enabled = true;

        }
    }

    

}
