using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCreditScript : MonoBehaviour
{
	public float displayTime;

	private float timeAfterDisplay;

	// Use this for initialization
	void Start()
	{
		timeAfterDisplay = Time.time;
	}

	// Update is called once per frame
	void Update()
	{
		timeAfterDisplay += Time.deltaTime;
		if (timeAfterDisplay > displayTime)
		{
			gameObject.SetActive(false);
		}
	}
}
