using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOControl : MonoBehaviour
{
	public GameObject treeSpawnerPrefab;
	public GameObject pointLight;
	public float rotationMultiplier = 3f;
	public float hoverVelocity;
	public float maxHoverDistance;

	private Transform t;
	private Quaternion newRotation;
	private Vector3 originalPos;
	private float hAxis;
	private float vAxis;
	private float pointLightCounter = 0f;
	private int treeCounter = 0;

	// Use this for initialization
	void Start()
	{
		t = GetComponent<Transform>();
		originalPos = t.position;
	}

	// Update is called once per frame
	void Update()
	{
		hAxis = Input.GetAxis("Horizontal");
		vAxis = Input.GetAxis("Vertical");

		newRotation = Quaternion.Euler(vAxis * -rotationMultiplier, 0, hAxis * -rotationMultiplier);
		transform.rotation = newRotation;

		hover();

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Vector3 spawnPos = t.position - new Vector3(0, 0.5f, 0);
			GameObject newtreeSpawner = Instantiate(treeSpawnerPrefab, spawnPos, Quaternion.identity);
			newtreeSpawner.name = "Tree" + treeCounter++;
			newtreeSpawner.GetComponent<Rigidbody>().velocity = new Vector3(-hAxis, 0, 0);

			pointLight.SetActive(true);
			pointLightCounter = 1f;
		}

		if (pointLightCounter < 0f)
			pointLight.SetActive(false);
		else
			pointLightCounter -= Time.deltaTime;
	}

	void hover()
	{
		if (Mathf.Abs(t.position.y - originalPos.y) > maxHoverDistance)
			hoverVelocity *= -1f;
		t.position += new Vector3(0, hoverVelocity, 0) * Time.deltaTime;
	}
}
