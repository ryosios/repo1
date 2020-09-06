using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] GameObject[] Bomb_item = new GameObject[3];
    [SerializeField] GameMaster GameMasterSC = default;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BombSetting();
    }

    void BombSetting()
    {
        switch (GameMasterSC.special_count)
        {
            case 3:
                Bomb_item[0].SetActive(true);
                Bomb_item[1].SetActive(true);
                Bomb_item[2].SetActive(true);
                
                break;
            case 2:
                Bomb_item[0].SetActive(true);
                Bomb_item[1].SetActive(true);
                Bomb_item[2].SetActive(false);
                
                break;
            case 1:
                Bomb_item[0].SetActive(true);
                Bomb_item[1].SetActive(false);
                Bomb_item[2].SetActive(false);
                
                break;
            
            case 0:
                Bomb_item[0].SetActive(false);
                Bomb_item[1].SetActive(false);
                Bomb_item[2].SetActive(false);
               
                break;

        }
    }
}
