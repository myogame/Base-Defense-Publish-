using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandMove : MonoBehaviour
{
    public int value;
    public bool isMoveX;

    private void Start()
    {
        if(isMoveX)
            transform.DOLocalMoveX(value, 1).SetLoops(-1, LoopType.Yoyo);
        else
            transform.DOLocalMoveY(value,1).SetLoops(-1, LoopType.Restart);
    }
}
