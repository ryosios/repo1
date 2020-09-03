using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class CharacterControl : MonoBehaviour
{


    [SerializeField] float character_speed =10f;
    [SerializeField] float character_jump_speed =10000f;
    [SerializeField] bool on_earth;//接地判定
    [SerializeField] Transform character_image_tf;
    [SerializeField] GameObject[] character_Spine = new GameObject[3] ;
    SkeletonAnimation[] character_Spine_skel = new SkeletonAnimation[3];
    [SerializeField] CircleCollider2D[] AttackCollision = new CircleCollider2D[3];

    [SerializeField] ParticleSystem ef_rem2End;
    [SerializeField] ParticleSystem ef_rem2moya;
    [SerializeField] ParticleSystem ef_backgroundDark;
    



    Vector3 character_speed_x;
    Vector3 character_speed_y;

    Rigidbody2D character_rigid2D;

    int character_jump_counter = 0;

    public bool is_special = false;//special判定１F
    public bool during_special = false;//specialモーション中
    public bool is_attack = false;//アタック判定

    public AttackSlider AttackSliderScript;


    int special_count = 3;//specialうてる回数
    int character_number = 0;// レム０　ラム１ レム2


    // Start is called before the first frame update

    private void Awake()
    {
        for(int i = 0; i<character_Spine.Length; i++)
        {
            character_Spine_skel[i] = character_Spine[i].GetComponent<SkeletonAnimation>();
        }
        character_Spine[1].SetActive(false);
        character_Spine[2].SetActive(false);

        AttackCollision[1].enabled = false;
        AttackCollision[2].enabled = false;
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

        
        AttackMove();

        if (special_count > 0 && during_special == false)//カウント０以上、必殺技期間でないとき
        {
            SpecialMove();
        }

        ChangeMove();

        

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
                if (!character_Spine_skel[character_number].AnimationName.Equals("special"))
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
                character_Spine_skel[character_number].state.SetAnimation(0, "down", false);
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
                character_Spine_skel[character_number].state.SetAnimation(0, "run", true);

               

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
                //==========モーション============
                character_Spine_skel[character_number].state.SetAnimation(0, "attack", false);
                character_Spine_skel[character_number].state.AddAnimation(0, "run", true, 0);

                //==========bool============
                is_attack = true;
            }
        }
        else
        {
            is_attack = false;
        }

    }

    void SpecialMove()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (character_number == 0 )
            {
                is_special = true;
                during_special = true;
                special_count -= 1;

                ef_backgroundDark.Play();


                character_Spine_skel[0].state.SetAnimation(0, "special", false).Complete += delegate {
                    character_number = 2;
                    character_Spine[0].SetActive(false);
                    character_Spine[1].SetActive(false);
                    character_Spine[2].SetActive(true);
                    AttackCollision[0].enabled = false;
                    AttackCollision[1].enabled = false;
                    AttackCollision[2].enabled = true;

                    character_Spine_skel[2].state.SetAnimation(0, "run", true);

                    during_special = false;
                    ef_rem2moya.Play();

                    StartCoroutine("remu2_finish");
                };

            }

            else if(character_number == 1)
            {
                is_special = true;
                special_count -= 1;
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
            is_special = false;
        }

    }

    private IEnumerator remu2_finish()
    {
        yield return new WaitForSeconds(5.0f);
        character_Spine[0].SetActive(true);
        character_Spine[1].SetActive(false);
        character_Spine[2].SetActive(false);

        AttackCollision[0].enabled = true;
        AttackCollision[1].enabled = false;
        AttackCollision[2].enabled = false;

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
                    AttackCollision[1].enabled = true;

                    character_Spine_skel[1].state.SetAnimation(0, "change", false);
                    character_Spine_skel[1].state.AddAnimation(0, "run", true, 0);
                }
                else if (character_number == 1)
                {
                    character_number = 0;
                    character_Spine[1].SetActive(false);
                    character_Spine[0].SetActive(true);

                    AttackCollision[1].enabled = false;
                    AttackCollision[0].enabled = true;

                    character_Spine_skel[0].state.SetAnimation(0, "change", false);
                    character_Spine_skel[0].state.AddAnimation(0, "run", true, 0);

                }
            }

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
