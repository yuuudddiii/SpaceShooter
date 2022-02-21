﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private float _speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += new Vector3(0, -1f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        movement1();
    }

    public void movement1()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y >= 8) Destroy(this.gameObject);
    }
}
