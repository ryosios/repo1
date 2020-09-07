using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreScript : MonoBehaviour
{
   // [SerializeField] GameMaster GameMasterSC = default;
    [SerializeField] TextMeshProUGUI highscore_textmeshpro = default;
    int high_score;
    // Start is called before the first frame update
    void Start()
    {
        //ここで先にハイスコア取得
        
        high_score = GameMaster.high_score_point;

    }

    // Update is called once per frame
    void Update()
    {
        HighScoreSet();
    }

    void HighScoreSet()
    {
        highscore_textmeshpro.text = high_score. ToString("D7");
    }
}
