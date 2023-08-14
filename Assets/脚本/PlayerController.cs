using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;  //跑步速度
    public int jumpSpeed; //跳跃速度
    public float doulbJumpSpeed;    //二段跳速度
    public bool isGround;   //判断是地面
    public bool canDoubleJump;  //判断二段跳

    private Rigidbody2D myRigidbody;    //声明2d刚体
    private Animator myAnim;    //声明动画组件
    private BoxCollider2D myFeet;   //获取我的脚的盒状碰撞体


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();  //获取一下刚体数据
        myAnim = GetComponent<Animator>();  //获取一下动画数据
        myFeet = GetComponent<BoxCollider2D>(); //获取盒状碰撞体

    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Filp();
        Jump();
        CheckGround();
        SwitchAnimation();
    }

    //检查地面
    void CheckGround()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));    //如果碰到了这个layer的话isGround就会返回true
        //测试
        Debug.Log(isGround);
    }

    //翻转
    void Filp()
    {
        bool PlayerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (PlayerHasXAxisSpeed)
        {
            //如过速度大于0.1的话才会翻转，大于0不翻转不大于0翻转
            if (myRigidbody.velocity.x > 0)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (myRigidbody.velocity.x < 0)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    //跑步函数
    void Run()
    {
        float moveDir = Input.GetAxisRaw("Horizontal");     //检测运动方向，因为是水平方向所以用的是Horizontal
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);    //设定一个速度，x=方向*速度，y保持不变
        myRigidbody.velocity = playerVel;   //把速度赋值给刚体的位置

        bool PlayerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;   //一个布尔类型的变量，代表玩家的x轴速度大于一个很小很小的值（不是0）
        myAnim.SetBool("Walk", PlayerHasXAxisSpeed);    //结合上一句的意思是如果满足x轴速度大于一个很小数，这个变量就是true
    }


    //跳跃函数
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))    //这里的jump指的是unity接受按键的部分，一般是接受到空格键识别为jump。在编辑—>program settings->输入管理器->跳跃
        {
            //判断如果在地面上才可以跳跃
            if (isGround)
            {
                myAnim.SetBool("Jump", true);   //跳跃时布尔值jump是true，方便动画切换
                Vector2 jumpVe1 = new Vector2(0.0f, jumpSpeed); //只需要y=0即可
                myRigidbody.velocity = Vector2.up * jumpVe1;    //给一个方向
                canDoubleJump = true;   //跳起后可以二段跳
            }
            else
            {
                //先判断是否能二段跳
                if (canDoubleJump)
                {
                    myAnim.SetBool("Doublejump", true);   //跳跃时布尔值jump是true，方便动画切换
                    Vector2 doubleJumpVel = new Vector2(0.0f, doulbJumpSpeed);  //给二维向量类型的变量赋值，x是0，y是二段跳的速度
                    myRigidbody.velocity = Vector2.up * doulbJumpSpeed; //给刚体的速度属性赋值，Vector2.up意思是朝向上的单位向量*速度
                    canDoubleJump = false;  //此处是跳过了，所以是false
                }
            }
        }
    }

    //动画切换
    void SwitchAnimation()
    {
        myAnim.SetBool("Idle", false);  //初始idle是false，这句的意思其实是只有在非待机状态下才需要动画切换
        if (myAnim.GetBool("Jump"))
        {
            //判断角色是否跳跃到了最高点
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("Fall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("Fall", false);
            myAnim.SetBool("Idle", true);
        }

        /*二段跳*/
        if (myAnim.GetBool("DoubleJump"))
        {
            //判断角色是否跳跃到了最高点
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("DoubleJump", false);
                myAnim.SetBool("DoubleFall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("DoubleFall", false);
            myAnim.SetBool("Idle", true);
        }
    }

}
    

