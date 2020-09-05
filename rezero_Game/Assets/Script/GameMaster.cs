using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;



public class GameMaster : MonoBehaviour
{
    [SerializeField] BackGroundMaker BackGroundMaker = default;
    [SerializeField] CharacterControl CharacterControl = default;
    [SerializeField] PlayableDirector FadeTimeline = default;
    [SerializeField] PlayableAsset fade_1_timeline = default;
    [SerializeField] PlayableAsset fade_2_timeline = default;

   
    [SerializeField] GameObject[] Boss_Timeline = new GameObject [3];
    List<PlayableDirector> Boss_Timeline_PD = new List<PlayableDirector>();
    



    //==========ゲームシーン関係==========
    public bool is_game_playing { get; set; } = false; //ゲーム中判定
    public bool is_game_end { get; set; } = false; //ゲーム終了中判定
    public bool is_onetime_titlemove { get; set; } = false; //タイトル遷移メソッド１F判定


    float totalTime { get; set; } = 3;//ゲーム開始時カウントダウン時間
    int seconds;
    bool is_onecall_gamestart = false;//ゲーム開始時1度だけ呼ぶ用

    public float timer_time { get; set; } = 60;//ゲーム終了までの時間
    float titlescene_move_count = 3f;//タイトルシーン遷移までの時間

    float boss_recast_tortal_time = 5f;//ボスの出現間隔
    float boss_recast_time = 0;
    public bool is_boss_playing { get; set; } = false;//ボス戦闘中判定
    int boss_timeline_number = 0;//どのボスのタイムラインを選択するか




    //=====UI系=====
    public int _HP { get; set; } = 5;//HP
    public int special_count { get; set; } = 3;//Bomb回数
    public int score_tortal_point { get; set; } = 0;//スコアトータルポイント
    public int score_point { get; set; } = 10;//弾一個10点

    


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Boss_Timeline.Length; i++)
        {
            var PDobj = Boss_Timeline[i].GetComponent<PlayableDirector>();
            Boss_Timeline_PD.Add(PDobj);
        }
        BackGroundMaker.SetBackGround();
        CharacterControl.enabled = false;
        FadeTimeline.playableAsset = fade_1_timeline;
        FadeTimeline.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
      
        CountDown();

        if (is_game_playing)
        {
            BackGroundMaker.BackGroundMove();
            if(is_onecall_gamestart == false)
            {
                is_onecall_gamestart = true;
                //ゲーム開始時一回だけ処理
                CharacterControl.enabled = true;

                
            }

            Timer();//カウントダウン
            
            BossRecastTimer();

        }


        

       

        if (is_game_end==true&&is_game_playing==true&&is_onetime_titlemove == false)
        {
            is_onetime_titlemove = true;


            StartCoroutine("MoveTitleScene");
        }

       
        HP_Watching();//HP監視　0になったらis_game_end false ゲーム終了

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

  

   
        
   void HP_Watching()
    {
        if (_HP <= 0)
        {
            is_game_end = true;
        }

    }

    private IEnumerator MoveTitleScene()
    {
        yield return new WaitForSeconds(titlescene_move_count);

        //フェード開始
        FadeTimeline.playableAsset = fade_2_timeline;
        FadeTimeline.Play();

        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("stage_title");
    }

    void BossRecastTimer()
    {//ボス戦闘中でない場合にタイマー進める
        if (!is_boss_playing)
        {
            

            boss_recast_time += Time.deltaTime;
            if (boss_recast_time >= boss_recast_tortal_time)
            {
                Debug.Log("koko3");
                is_boss_playing = true;
                boss_recast_time = 0;

                BossSet();

            }
        }
    }

    void BossSet()
    {
        boss_timeline_number = UnityEngine.Random.RandomRange(0, Boss_Timeline.Length);//ボスの配列で0～3ランダム選択
        Instantiate(Boss_Timeline[boss_timeline_number]);//ボスのTimelineゲームオブジェクトを選択したナンバーでインスタンス化
    }
}
