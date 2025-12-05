using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveObject : MonoBehaviour
{
   
    private void OnEnable()
    {

      transform.DOShakeScale(1, 1);
     
         
    }

}
