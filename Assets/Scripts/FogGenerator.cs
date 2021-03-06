﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogGenerator : MonoBehaviour
{
	public GameObject prefab;
	public float offset;
	public int layers;
	public float thickness;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < layers; i++)
		{
			var pos = transform.position - new Vector3(0, offset + thickness * i, 0);
			var gameObject = GameObject.Instantiate(prefab, pos, Quaternion.Euler(90, 0, 0), transform);

			gameObject.GetComponent<SpriteRenderer>().color = new Color(0.08f,0.1f,0.13f, i * 0.015f);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
