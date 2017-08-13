using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeControl : MonoBehaviour
{
	private Transform t;
	private Quaternion quatRotation;
	private float x;
	private float y;

	// Use this for initialization
	void Start()
	{
		t = GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update()
	{
		t.Rotate(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			// reset rotation
			quatRotation = Quaternion.Euler(0, 0, 0);
			t.rotation = quatRotation;
		}
	}
}
