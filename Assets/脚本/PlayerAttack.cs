using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public int damage;  //伤害值
    public float startTime; //开始时间
    public float time;  //关闭碰撞体的间隔

    private Animator anim;  //获取父对象的动画
    private PolygonCollider2D collider2D;   //自己的碰撞体组件

    // Start is called before the first frame update
    void Start()
    {
        //将这个变量赋值，使它能够获取动画组件
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();     //一起使用可以用于获取场景中被标记为"Player"的游戏对象，并获取其上的Animator组件
        collider2D = GetComponent<PolygonCollider2D>(); //定义一下自身的碰撞体
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        //检测攻击按钮
        if (Input.GetButtonDown("Attack"))
        {
            collider2D.enabled = true;  //将碰撞框设为true
            anim.SetTrigger("Attack");  //播放攻击动画
            StartCoroutine(StartAttack());    //启动开始的协程
        }
    }

    //再加一个开始的协程
    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);
        collider2D.enabled = true;
        StartCoroutine(DisableHitBox());    //启动关闭的协程
    }

    //加一个协程，过一段时间关闭碰撞体
    IEnumerator DisableHitBox()
    {
        yield return new WaitForSeconds(time);  //暂停协程一段时间
        collider2D.enabled = false;
    }

    //触发函数，如果碰到的东西带有Enemy的tag
    private void OnTriggerEnter2D(Collider2D orther)
    {
        if (orther.gameObject.CompareTag("Enemy"))
        {
            orther.GetComponent<Enemy>().TakeDamage(damage); //如果碰到了就扣掉和伤害值一样的血量
        }
    }
}
