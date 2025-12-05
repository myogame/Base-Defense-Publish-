using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrowAnimated : MonoBehaviour
{
   
    void Start()
    {
        transform.DOLocalMoveY(1.5f, 0.5f).SetLoops(-1,LoopType.Yoyo);
    }

  
}
