using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;//血量
    public int damage;//伤害

    public float flashTime; //闪烁时间

    private SpriteRenderer sr;  //渲染精灵比如颜色、翻转、材质、遮罩等
    private Color originalColor;    //记录原始颜色

    // Start is called before the first frame update
    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();   //获取到控制颜色等等的组件
        originalColor = sr.color;   //将初始的颜色复制给这个变量
    }

    // Update is called once per frame
    public void Update()
    {
        //如果血量为零就消除
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    //写一个方法，因为要给玩家调用所以是公开的
    public void TakeDamage(int damage)
    {
        health -= damage;
        FlashColor(flashTime); //调用闪烁函数
    }

    //定义一个方法控制闪烁颜色,传一个参数进去控制闪烁时间
    public void FlashColor(float time)
    {
        sr.color = Color.red;   //颜色是红的
        Invoke("ResetColor", time);//延迟一点时间调用
    }

    //再定义一个方法
    public void ResetColor()
    {
        sr.color = originalColor;   //正常状态下的颜色
    }

}
