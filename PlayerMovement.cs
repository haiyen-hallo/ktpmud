using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Vector2 VectorToRight = new Vector2(1, 0); // khoi tao vector phai 
    private Vector2 VectorToLeft = new Vector2(-1, 0); // khoi tao vector trai
    //private string CurrentAnimation = "";

    //public bool isNonLoopAnimation = false;
    public Transform Right;
    public Transform Left;
    public GameObject Bullet;
    public int JumpCount = 0;
    public int MaxJumpCount = 2;
    public bool OnGround = false;
    public float MoveSpeed = 1;
    public float JumpStrength = 1;
    public Rigidbody2D PlayerRigidbody2D; //dung dac tinh vat ly 
    public SpriteRenderer PlayerSpriteRenderer; // su dung xay dung sprite cua nhan vat 
    public Animator PlayerAnimation; // dung chuc nang animator
    private void Update()
    {
        KeyController();
    }
    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(7, 8);
    }
    void KeyController()
    {
        if (Input.GetKeyDown("w") == true)
        {
            PlayerAttack();
        }
        if (Input.GetKey("d"))
        {
            PlayerMove(VectorToRight); // nhap D thi kich hoat vector phai truc X
            RotatePlayer(false);
        }
        else if (Input.GetKey("a"))
        {
            PlayerMove(VectorToLeft);// nhap D thi kich hoat vector trai truc X
            RotatePlayer(true);
        }
        else
        {
            AnimationStop();
        }
        if (Input.GetKeyDown("space"))
        {
            PlayerJump(); // xay dunng ham nhay
        }
    }
    void PlayerMove(Vector2 MoveVector)
    {
        Vector2 NewMoveVector = new Vector2(MoveVector.x * MoveSpeed, PlayerRigidbody2D.velocity.y);// ket hop di chuyen trai phai cung voi toc do di chuyen 
        PlayerRigidbody2D.velocity = NewMoveVector;
        if (OnGround == true)
        {
            PlayerAnimation.Play("run");// khi roi mat dat thi ket hop animation run
        }
    }
    void RotatePlayer(bool Bool_Value)
    {
        PlayerSpriteRenderer.flipX = Bool_Value;
    }
    void AnimationStop()
    {
        if (OnGround == true)
        {
            PlayerAnimation.Play("idle");
        }
    }
    public void PlayerJump()
    {
        if (OnGround == true && JumpCount <= 0) // cho phep nhay khi nv cham dat 
        {
            if (JumpCount < MaxJumpCount)
            {
                JumpCount++;
                StartCoroutine(AnimationJump());
                StartCoroutine(CheckJumpCount());
                PlayerRigidbody2D.AddForce(new Vector2(0, 1) * JumpStrength, ForceMode2D.Impulse);
            }
        }
        if (OnGround == false && JumpCount > 0)
        {
            if (JumpCount < MaxJumpCount)
            {
                JumpCount++;
                StartCoroutine(AnimationJump());

                PlayerRigidbody2D.velocity = new Vector2(PlayerRigidbody2D.velocity.x, 0);
                PlayerRigidbody2D.AddForce(new Vector2(0, 1) * JumpStrength, ForceMode2D.Impulse);
            }
        }
    }
    public void ResetJumpCount()
    {
        JumpCount = 0;
    }
    IEnumerator CheckJumpCount()
    {
        yield return new WaitForSeconds(0.1f);
        if (OnGround == true)
        {
            JumpCount = 0;
        }
    }
    IEnumerator AnimationJump()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerAnimation.Play("jump");
        if (OnGround == false && JumpCount > 1)
        {
            PlayerAnimation.Play("doublejump");
        }
        else if (OnGround == false && JumpCount < MaxJumpCount)
        {
            yield return new WaitForSeconds(0.55f);
            PlayerAnimation.Play("fall");
        }
    }
    //void PlayingAnimation(string AnimationName) //play loop animation
    //{
    //    if (CurrentAnimation != AnimationName && isNonLoopAnimation == false)
    //    {
    //        CurrentAnimation = AnimationName;
    //        PlayerAnimation.Play(CurrentAnimation);
    //    }
    //}
    //void PlayingNonLoopAnimation(string AnimationName) //play loop animation
    //{
    //    if (CurrentAnimation != AnimationName)
    //    {
    //        CurrentAnimation = AnimationName;
    //        PlayerAnimation.Play(CurrentAnimation);
    //    }
    //}
    //IEnumerator PrepareNonLoopAnimation(string AnimationName)
    //{
    //    isNonLoopAnimation = true;
    //    PlayingNonLoopAnimation("attack");
    //    yield return new WaitForEndOfFrame();
    //    var CurrentAnimationInfo = PlayerAnimation.GetCurrentAnimatorStateInfo(0);
    //    if (CurrentAnimationInfo.IsName(AnimationName) == true)
    //    {
    //        var AnimationDuration = CurrentAnimationInfo.length;
    //        yield return new WaitForSeconds(AnimationDuration);
    //        isNonLoopAnimation = false;
    //    }
    //    else
    //    {
    //        yield return null;
    //        isNonLoopAnimation = false;
    //    }
    //}
    void PlayerAttack()
    {
        if(OnGround == true)
        {
            OnGround = false;
            PlayerAnimation.Play("attack");
        }
        //CreateBullet();
        CreateBullet();
    }
    void CreateBullet()
    {
        Vector3 BulletPosition = new Vector3();
        Vector2 Bullets = new Vector2();
        float BulletSpeed = 15;
        if (PlayerSpriteRenderer.flipX == true)
        {
            BulletPosition = Left.position;
            Bullets = new Vector2(-1, 0);
        }
        else
        {
            BulletPosition = Right.position;
            Bullets = new Vector2(1, 0);
        }
        var NewBullet = Instantiate(Bullet,BulletPosition,Quaternion.identity,null);
        var NewBulletRigidbody = NewBullet.GetComponent<Rigidbody2D>();
        NewBulletRigidbody.velocity = Bullets * BulletSpeed;
    }
    public GameObject BouncingBullet;
    //void CreateBouncingBullet()
    //{
    //    Vector3 BulletPosition = new Vector3();
    //    Vector2 Bullets = new Vector2();
    //    float BulletSpeed = 7;
    //    if (PlayerSpriteRenderer.flipX == true)
    //    {
    //        BulletPosition = Left.position;
    //        Bullets = new Vector2(-1, 0);
    //    }
    //    else
    //    {
    //        BulletPosition = Right.position;
    //        Bullets = new Vector2(1, 0);
    //    }
    //    var NewBullet = Instantiate(BouncingBullet, BulletPosition, Quaternion.identity, null);
    //    var NewBulletRigidbody = NewBullet.GetComponent<Rigidbody2D>();
    //    NewBulletRigidbody.velocity = Bullets * BulletSpeed;
    //}
}