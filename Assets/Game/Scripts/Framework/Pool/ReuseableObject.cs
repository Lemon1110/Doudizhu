using UnityEngine;
using System.Collections;

 public abstract class ReuseableObject:MonoBehaviour  {
    /// <summary>
    /// 取出之前的重置操作
    /// </summary>
    public abstract void BeforeGetObject();


    /// <summary>
    /// 放入之前的还原操作
    /// </summary>
    public abstract void BeforeHideObject();
    //public virtual void BeforeGetObject()
    //{

    //}
    //public virtual void BeforeHideObject()
    //{

    //}
}
