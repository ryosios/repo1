using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class BaseITime : MonoBehaviour,ITimeControl
{

    [SerializeField] GameObject this_root_obj = default;
    [SerializeField] Canvas CanvasCutin = default;
    GameMaster GameMasterSC;
    GameObject cutin;
    

    // 何度も呼ばれる

    void Start()
    {
        GameMasterSC = GameObject.Find("GameMasterObject").GetComponent<GameMaster>();
        //   cutin = GameObject.Find("cutin");
        CanvasCutin.worldCamera = Camera.main;
        
    }

    public void SetTime(double time)
    {
     //   cutin.transform.position = new Vector2(Camera.main.transform.position.y,0);
    }

    // クリップ開始時に呼ばれる
    public void OnControlTimeStart()
    {
        Debug.Log("start_timeline");
    }

    // クリップから抜ける時に呼ばれる
    public void OnControlTimeStop()
    {
        if(!ReferenceEquals(GameMasterSC, null))
        { 
            GameMasterSC.is_boss_playing = false;
            GameObject.Destroy(this_root_obj);
        }


        Debug.Log("end_timeline");
    }
}
