using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderFireAnimation : MonoBehaviour
{
    private Spider _spider;

    void Start()
    {
        _spider = transform.parent.GetComponent<Spider>();
    }

    public void Fire()
    {
        Debug.Log("Acid");
        _spider.Attack();
    }
}
