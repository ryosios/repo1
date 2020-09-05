using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision_Blue : MonoBehaviour
{

    GameMaster GameMsterSC;
    CharacterControl CharacterControlSC;
    // Start is called before the first frame update
    void Start()
    {
        CharacterControlSC = GameObject.Find("Character_locator").GetComponent<CharacterControl>();
        GameMsterSC = GameObject.Find("GameMasterObject").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void OnParticleCollision(GameObject obj)
    {
        
        switch (obj.name)
        {
          
            case "AttackCollision_remu":
                //スコア加算
               
                GameMsterSC.score_tortal_point += GameMsterSC.score_point;
                break;
           
            case "AttackCollision_ramu":
              //スコア加算しない
                break;
           
            case "AttackCollision_remu2":
                //スコア加算する
                GameMsterSC.score_tortal_point += GameMsterSC.score_point;
                break;

            case "Character_Image":
                //ダメージ
                GameMsterSC._HP -= 1;
               
                //無敵時間bool
                CharacterControlSC.is_invincible = true;
                StartCoroutine("InvincibleTimer");
                break;

            default:
              
                break;
        }
    }
    private IEnumerator InvincibleTimer()
    {
        yield return new WaitForSeconds(CharacterControlSC.invincible_time);

        if (!CharacterControlSC.during_special)
        {
            CharacterControlSC. is_invincible = false;
        }
    }
}
