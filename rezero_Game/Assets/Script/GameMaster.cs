using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;




public class GameMaster : MonoBehaviour
{
    public PlayableDirector StartCutinTimeline;


    public bool is_game_playing = false;
    public bool is_game_end = false;


    public BackGroundMaker BackGroundMaker;
    public CharacterControl CharacterControl;

    public float totalTime = 3;
    int seconds;

    public float timer_time = 60;
 
    

    int count_gamestart = 0;



    public int _HP = 5;
    public bool is_invincible = false;
    public float invincible_time = 0.5f;

    [SerializeField] CircleCollider2D Character_Image_collider = default;


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



        InvincibleTime();
        


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


}
