using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class CharacterControl : MonoBehaviour
{


     float character_speed =8f;
     float character_jump_speed =650f;
     bool on_earth;//接地判定
    [SerializeField] Transform character_image_tf = default;
    [SerializeField] GameObject[] character_Spine = new GameObject[3] ;
    SkeletonAnimation[] character_Spine_skel = new SkeletonAnimation[3];

    [SerializeField] GameObject[] AttackObject = new GameObject[3];
    CircleCollider2D[] AttackCollision = new CircleCollider2D[3];

    [SerializeField] ParticleSystem ef_rem2End =  default;
    [SerializeField] ParticleSystem ef_rem2moya =  default;
    [SerializeField] ParticleSystem ef_backgroundDark = default;

    [SerializeField] GameMaster GameMasterSC = default;
    [SerializeField] AttackSlider AttackSliderScript;
    [SerializeField] CircleCollider2D Character_Image_collider = default;//キャラのコリジョン

    Vector3 character_speed_x;
    Vector3 character_speed_y;

    Rigidbody2D character_rigid2D;

    int character_jump_counter = 0;

    public int character_number { get; set; } = 0;// レム０　ラム１ レム鬼化2

    public bool is_invincible { get; set; } = false;//被ダメ時無敵中判定

    public float invincible_time { get; set; } = 0.5f;//被ダメ時無敵時間

    public bool on_attack { get; set; } = false;//アタック開始時1フレ判定

    public bool on_special { get; set; } = false;//special開始時１F判定

    public bool during_special { get; set; } = false;//specialモーション中

    // Start is called before the first frame update

    private void Awake()
    {
        for(int i = 0; i<character_Spine.Length; i++)
        {
            character_Spine_skel[i] = character_Spine[i].GetComponent<SkeletonAnimation>();
        }
        character_Spine[1].SetActive(false);
        character_Spine[2].SetActive(false);

        for (int i = 0; i < AttackObject.Length; i++)
        {
            AttackCollision[i] = AttackObject[i].GetComponent<CircleCollider2D>();
            AttackCollision[i].enabled = false;
        }
        

        AttackObject[1].SetActive(false);
        AttackObject[2].SetActive(false);
    }

    void Start()
    {
        character_rigid2D = GetComponent<Rigidbody2D>();
        
        character_speed_y = new Vector3(0, character_jump_speed, 0);

        character_Spine_skel[character_number].state.SetAnimation(0, "run", true);

    }

    // Update is called once per frame
    void Update()
    {
       
        VerticalMove();

        HorizontalMove();

        StopMove();

        CharacterMoveLimit();


        if (!during_special)
        {
            AttackMove();
        }

        if (GameMasterSC.special_count > 0 && during_special == false)//カウント０以上、必殺技期間でないとき
        {
            SpecialMove();
        }

        if (!during_special)
        {
            ChangeMove();
        }


        if (during_special)//スペシャル時の無敵と通常時の無敵
        {
            invincibleTime_Special();
        }
        else
        {
            InvincibleTime();
        }


    }

    void HorizontalMove()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //==========移動============
            character_speed_x = new Vector3(character_speed, character_rigid2D.velocity.y, 0);
            character_rigid2D.velocity = character_speed_x;
            character_image_tf.localScale = new Vector3(1,1,1);

            //==========モーション============
            if (!character_Spine_skel[character_number].AnimationName.Equals("run2")&& !character_Spine_skel[character_number].AnimationName.Equals("jump")
                && !character_Spine_skel[character_number].AnimationName.Equals("attack") && !character_Spine_skel[character_number].AnimationName.Equals("change")
                && !character_Spine_skel[character_number].AnimationName.Equals("special"))
            {
                character_Spine_skel[character_number].state.SetAnimation(0, "run2", true);
            }

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            //==========移動============
            character_speed_x = new Vector3(-character_speed, character_rigid2D.velocity.y, 0);
            character_rigid2D.velocity = character_speed_x;

            // character_image_tf.localScale = new Vector3(-1, 1, 1);
            character_image_tf.localScale = new Vector3(1, 1, 1);

            //==========モーション============
            if (!character_Spine_skel[character_number].AnimationName.Equals("run") && !character_Spine_skel[character_number].AnimationName.Equals("jump") 
                && !character_Spine_skel[character_number].AnimationName.Equals("attack")&& !character_Spine_skel[character_number].AnimationName.Equals("change")
                && !character_Spine_skel[character_number].AnimationName.Equals("special"))
            {
                character_Spine_skel[character_number].state.SetAnimation(0, "run", true);
            }

        }
    }

    void VerticalMove()
    {

       
        if (character_jump_counter < 3) {

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //==========移動============
                character_rigid2D.velocity = new Vector3(0, 0, 0);
                character_rigid2D.AddForce(character_speed_y);
                character_jump_counter += 1;

                //==========モーション============
                if (!character_Spine_skel[character_number].AnimationName.Equals("special") && !character_Spine_skel[character_number].AnimationName.Equals("attack"))
                {
                    character_Spine_skel[character_number].state.SetAnimation(0, "jump", false);
                }
                character_Spine_skel[character_number].state.AddAnimation(0, "run", true,0);

            }
        }

        if (!on_earth)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //==========移動============
                character_rigid2D.velocity = new Vector3(0, 0, 0);
                character_rigid2D.AddForce(-character_speed_y);


                //==========モーション============
                if (!character_Spine_skel[character_number].AnimationName.Equals("special") && !character_Spine_skel[character_number].AnimationName.Equals("attack"))
                {
                    character_Spine_skel[character_number].state.SetAnimation(0, "down", false);
                }
                character_Spine_skel[character_number].state.AddAnimation(0, "run", true, 0);
            }
        }



    }

    void StopMove()
    {
        if (on_earth) {

            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                //==========移動============
                character_rigid2D.velocity = new Vector3(0, 0, 0);

                //==========モーション============
                // character_Spine.state.SetAnimation(0, "idle", true);
                if (!character_Spine_skel[character_number].AnimationName.Equals("special") && !character_Spine_skel[character_number].AnimationName.Equals("attack"))
                {
                    character_Spine_skel[character_number].state.SetAnimation(0, "run", true);

                }

            }
        }

    }

    void CharacterMoveLimit()
    {
        if(this.transform.position.x<-4.5f) {

            Vector3 chara_pos = transform.position;
            chara_pos.x = -4.5f;
            transform.position = chara_pos;
        }
        if(this.transform.position.x > 3f)
        {
            Vector3 chara_pos = transform.position;
            chara_pos.x = 3f;
            transform.position = chara_pos;
        }

    }

    void AttackMove()
    {
        if (AttackSliderScript.is_attack_slider_full == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AttackCollision[character_number].enabled = true;


                //==========モーション============
                if (!character_Spine_skel[character_number].AnimationName.Equals("special"))
                {
                    character_Spine_skel[character_number].state.SetAnimation(0, "attack", false).Complete += delegate
                    {
                        AttackCollision[character_number].enabled = false;//当たり判定出てる時間はモーション依存
                    };

                    character_Spine_skel[character_number].state.AddAnimation(0, "run", true, 0);

                }


                //==========bool============
                on_attack = true;
            }
        }
        else
        {
            on_attack = false;
        }

    }

    void SpecialMove()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (character_number == 0 )
            {
                on_special = true;
                during_special = true;
                GameMasterSC.special_count -= 1;

                ef_backgroundDark.Play();


                character_Spine_skel[0].state.SetAnimation(0, "special", false).Complete += delegate {
                    //スペシャルモーション終わったら～
                    character_number = 2;
                    character_Spine[0].SetActive(false);//レムから鬼化レムにプレファブ交代
                    character_Spine[1].SetActive(false);
                    character_Spine[2].SetActive(true);
                    AttackCollision[0].enabled = false;
                    AttackCollision[1].enabled = false;
                    AttackCollision[2].enabled = false;
                    AttackObject[0].SetActive(false);//アタックコリジョンも交代
                    AttackObject[1].SetActive(false);
                    AttackObject[2].SetActive(true);

                    character_Spine_skel[2].state.SetAnimation(0, "run", true);

                    during_special = false;
                    ef_rem2moya.Play();

                    StartCoroutine("remu2_finish");
                };

            }

            else if(character_number == 1)
            {
                on_special = true;
                GameMasterSC.special_count -= 1;
                during_special = true;

                ef_backgroundDark.Play();

                character_Spine_skel[1].state.SetAnimation(0, "special", false).Complete += delegate
                {
                    during_special = false;
                };
                character_Spine_skel[1].state.AddAnimation(0, "run", true,0);
            } 
        }
        else
        {
            on_special = false;
        }

    }

    private IEnumerator remu2_finish()
    {
        yield return new WaitForSeconds(5.0f);
        character_Spine[0].SetActive(true);
        character_Spine[1].SetActive(false);
        character_Spine[2].SetActive(false);

        AttackCollision[0].enabled = false;
        AttackCollision[1].enabled = false;
        AttackCollision[2].enabled = false;

        AttackObject[0].SetActive(true);
        AttackObject[1].SetActive(false);
        AttackObject[2].SetActive(false);

        ef_rem2End.Play();
        ef_rem2moya.Stop();

        character_number = 0;
        character_Spine_skel[0].state.SetAnimation(0, "run", true);
    }

    void ChangeMove()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(character_number ==0 || character_number == 1)
            {
                if (character_number == 0)
                {
                    character_number = 1;
                    character_Spine[0].SetActive(false);
                    character_Spine[1].SetActive(true);

                    AttackCollision[0].enabled = false;
                    AttackCollision[1].enabled = false;
                    

                    AttackObject[0].SetActive(false);
                    AttackObject[1].SetActive(true);

                    character_Spine_skel[1].state.SetAnimation(0, "change", false);
                    character_Spine_skel[1].state.AddAnimation(0, "run", true, 0);
                }
                else if (character_number == 1)
                {
                    character_number = 0;
                    character_Spine[1].SetActive(false);
                    character_Spine[0].SetActive(true);

                    AttackCollision[0].enabled = false;
                    AttackCollision[1].enabled = false;

                    AttackObject[1].SetActive(false);
                    AttackObject[0].SetActive(true);

                    character_Spine_skel[0].state.SetAnimation(0, "change", false);
                    character_Spine_skel[0].state.AddAnimation(0, "run", true, 0);

                }
            }

        }
     }



    void InvincibleTime()
    {
        if (is_invincible == true)
        {
            Character_Image_collider.enabled = false;
        }
        else
        {
            Character_Image_collider.enabled = true;

        }
    }

    void invincibleTime_Special()
    {
        if (during_special)
        {
            Character_Image_collider.enabled = false;
        }
        else
        {
            Character_Image_collider.enabled = true;

        }
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("earth"))
        {
            character_jump_counter = 0; //ジャンプ回数初期化
            if (!character_Spine_skel[character_number].AnimationName.Equals("idle"))//モーション初期化
            {
                // character_Spine.state.SetAnimation(0, "idle", true);
                character_Spine_skel[character_number].state.AddAnimation(0, "run", true,0);
              
            }
        }
        else
        {
            return;
        }

    }

    void OnCollisionStay2D(Collision2D collision)
    {
      
        if (collision.gameObject.tag.Equals("earth"))
        {

            on_earth = true;
           
        }

        else
        {
           return;
        }
        

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("earth"))
        {
            on_earth = false;
        }
        else
        {
            return;
        }
    }


}
