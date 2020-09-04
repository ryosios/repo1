using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{

    [SerializeField] GameMaster GameMasterSC = default;
    [SerializeField] TextMeshProUGUI score_textmeshpro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreSet();
    }

    void ScoreSet()
    {
        score_textmeshpro.text = GameMasterSC.score_tortal_point.ToString("D7");
    }
}
