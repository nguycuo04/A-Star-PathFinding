using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    [SerializeField] float rotateDuration = 1.0f; 
    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 360, 0), rotateDuration, RotateMode.FastBeyond360)
           .SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo); 
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
