using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision_Red : MonoBehaviour
{
    [SerializeField] GameMaster GameMsterSC = default;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void OnParticleCollision(GameObject obj)
    {

        switch (obj.name)
        {

            case "AttackCollision_remu":
                
                //スコア加算しない
                break;

            case "AttackCollision_ramu":
                //スコア加算する
                break;

            case "AttackCollision_remu2":
                //スコア加算する
                break;

            case "Character_Image":
                //ダメージ
                GameMsterSC._HP -= 1;

                //無敵時間bool
                GameMsterSC.is_invincible = true;
                StartCoroutine("InvincibleTimer");
                break;

            default:

                break;
        }
    }
    private IEnumerator InvincibleTimer()
    {
        yield return new WaitForSeconds(GameMsterSC.invincible_time);
        //ここ注意
        //必殺技使用中はfalseにしない。必殺技のほうでfalseにする
        if(!GameMsterSC.during_special)
        {
            GameMsterSC.is_invincible = false;

        }

    }
}
