using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] PlayableDirector FadeTimeline = default;
    [SerializeField] PlayableAsset fade1_timeline = default;//最初のフェード
    [SerializeField] PlayableAsset fade2_timeline = default;//最後のフェード

    bool on_button = false;
    // Start is called before the first frame update
    void Start()
    {
        FadeTimeline.playableAsset = fade1_timeline;
        FadeTimeline.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushStartButton()
    {
        if( on_button == false)
        {
            on_button = true;
            FadeTimeline.playableAsset = fade2_timeline;
            FadeTimeline.Play();
            StartCoroutine("SceneMove");

        }
    }

    //以下その他ボタン　メソッド　この間に

    private IEnumerator SceneMove()
    {
        yield return new WaitForSeconds(0.5f);//タイムラインフェード0.5秒
        UnityEngine.SceneManagement.SceneManager.LoadScene("stage_battle");
    }
}
