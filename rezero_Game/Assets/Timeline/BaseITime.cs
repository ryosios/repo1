using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class BaseITime : MonoBehaviour,ITimeControl
{

    [SerializeField] GameObject this_root_obj = default;
    GameMaster GameMasterSC;

    // 何度も呼ばれる

    void Start()
    {
        GameMasterSC = GameObject.Find("GameMasterObject").GetComponent<GameMaster>();
    }

    public void SetTime(double time)
    {
       
    }

    // クリップ開始時に呼ばれる
    public void OnControlTimeStart()
    {
        Debug.Log("start_timeline");
    }

    // クリップから抜ける時に呼ばれる
    public void OnControlTimeStop()
    {
        GameMasterSC.is_boss_playing = false;
        GameObject.Destroy(this_root_obj);
        Debug.Log("end_timeline");
    }
}
