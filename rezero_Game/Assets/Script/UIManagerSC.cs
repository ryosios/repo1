using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class UIManagerSC : MonoBehaviour
{
    [SerializeField] PlayableDirector FadeTimeline = default;
    [SerializeField] PlayableAsset fade1_timeline = default;//最初のフェード
    [SerializeField] PlayableAsset fade2_timeline = default;//最後のフェード

    bool on_button = false;
    string next_stage;
    // Start is called before the first frame update


    void Start()
    {
       // GameMaster.high_score_point =  PlayerPrefs.GetInt("HIGHSCORE", 0);
       //上記はのちほど追記

        FadeTimeline.playableAsset = fade1_timeline;
        FadeTimeline.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PushStartButton()
    {
        if (on_button == false)
        {
            on_button = true;
            next_stage = "Stage_battle";
           
            StartCoroutine("SceneMove");

        }
    }

    //以下その他ボタン　メソッド　この間に

    private IEnumerator SceneMove()
    {
        FadeTimeline.playableAsset = fade2_timeline;
        FadeTimeline.Play();
        yield return new WaitForSeconds(0.5f);//タイムラインフェード0.5秒
        UnityEngine.SceneManagement.SceneManager.LoadScene(next_stage);
    }
}
