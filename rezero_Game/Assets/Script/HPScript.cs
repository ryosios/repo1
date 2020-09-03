using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPScript : MonoBehaviour
{
    [SerializeField] GameObject[] HP_item = new GameObject [5];
    [SerializeField] GameMaster GameMasterSC = default;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HPSetting();
    }

    void HPSetting()
    {
        switch (GameMasterSC._HP) {
            case 5:
                HP_item[0].SetActive(true);
                HP_item[1].SetActive(true);
                HP_item[2].SetActive(true);
                HP_item[3].SetActive(true);
                HP_item[4].SetActive(true);
                break;
            case 4:
                HP_item[0].SetActive(true);
                HP_item[1].SetActive(true);
                HP_item[2].SetActive(true);
                HP_item[3].SetActive(true);
                HP_item[4].SetActive(false);
                break;
            case 3:
                HP_item[0].SetActive(true);
                HP_item[1].SetActive(true);
                HP_item[2].SetActive(true);
                HP_item[3].SetActive(false);
                HP_item[4].SetActive(false);
                break;
            case 2:
                HP_item[0].SetActive(true);
                HP_item[1].SetActive(true);
                HP_item[2].SetActive(false);
                HP_item[3].SetActive(false);
                HP_item[4].SetActive(false);
                break;
            case 1:
                HP_item[0].SetActive(true);
                HP_item[1].SetActive(false);
                HP_item[2].SetActive(false);
                HP_item[3].SetActive(false);
                HP_item[4].SetActive(false);
                break;
            case 0:
                HP_item[0].SetActive(false);
                HP_item[1].SetActive(false);
                HP_item[2].SetActive(false);
                HP_item[3].SetActive(false);
                HP_item[4].SetActive(false);
                break;

        }
    }
}
