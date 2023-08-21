using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//让这个脚本继承Enemy
public class EnemyBat :  Enemy
{
    

    // Start is called before the first frame update
    void Start()
    {
        base.Start();   //调用父级函数
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();  //调用父级的函数
    }

    
}
