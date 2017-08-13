using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawnerScript : MonoBehaviour
{
	public GameObject branchPrefab;
	public int maxLevelOfBranches = 4;
	public float growthInterval = 3f;
	public GameObject bushPrefab;
	public bool spawnBushOnFirstBranch;
	public List<Material> bushColors;
	public bool useSingleRandomColor;
	public bool useOnlyTheColorBelow;
	public int colorIndexInArray;

	private Transform t;
	private Rigidbody rb;
	private ArrayList tree;
	private BranchScript branchScript;
	private int levelCounter = 0;
	private int branchCounter = 0;
	private float time;
	private bool readyToSpawn = false;

	// Use this for initialization
	void Start()
	{
		tree = new ArrayList();
		t = GetComponent<Transform>();
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		if (readyToSpawn == true)
		{
			if (Time.time > time)
			{
				if (levelCounter == 0)
					createNewRoot();
				else if (levelCounter < maxLevelOfBranches)
					addBranchesToTree();
				else
					addBushesAndFinishTree();

				time += growthInterval;
			}
		}
	}

	public void addBranchesToTree()
	{
		for (int i = tree.Count - 1; i >= 0; i--)
		{
			GameObject oldBranch = (GameObject)tree[i];
			branchScript = oldBranch.GetComponent<BranchScript>();
			if (!branchScript.hasBeenBranched)
			{
				for (int j = 0; j < Random.Range(3, 6); j++)
				{
					GameObject newBranch = branchScript.createNewBranch(levelCounter, branchCounter);
					newBranch.transform.SetParent(t);
					tree.Add(newBranch);
					branchCounter++;
				}
				branchScript.setHasBeenBranchedToTrue();
			}
			else
			{
				break;
			}
		}
		levelCounter++;
		branchCounter = 0;
	}

	private void addBushesAndFinishTree()
	{
		int randomBushColor = (int)Random.Range(0, bushColors.Count);
		for (int i = tree.Count - 1; i >= 0; i--)
		{
			GameObject endBranch = (GameObject)tree[i];
			branchScript = endBranch.GetComponent<BranchScript>();
			if (!branchScript.hasBeenBranched)
				createNewBush(endBranch, branchScript, randomBushColor);
			else
				break;
		}
		if (spawnBushOnFirstBranch)
		{
			GameObject firstBranch = (GameObject)tree[0];
			branchScript = firstBranch.GetComponent<BranchScript>();
			createNewBush(firstBranch, branchScript, randomBushColor);
		}
	}

	private void createNewBush(GameObject branch, BranchScript givenBranchScript, int randomBushColor)
	{
		Vector3 spawnPos = branchScript.endPoint.transform.position;
		GameObject newBush = Instantiate(bushPrefab, spawnPos, Quaternion.identity);
		newBush.transform.localScale = newBush.transform.localScale * Random.Range(0.1f, 1f);
		newBush.transform.parent = branch.transform;
		if (useOnlyTheColorBelow) // if useOnlyTheColorBelow == true
		{
			randomBushColor = colorIndexInArray;
		}
		else if (!useSingleRandomColor) // if useSingleRandomColor == false
		{
			/*
			 * Here bushColors.Count gives a number greater than the size of the array
			 * but since Random.Range never reaches bushColors.Count (but it does get close in terms of decimals)
			 * and since type casting into int truncates the decimals, it looks at all elements in the array.
			*/
			randomBushColor = (int)Random.Range(0, bushColors.Count);
		}
		newBush.GetComponent<Renderer>().material = bushColors[randomBushColor];
		branchScript.setHasBeenBranchedToTrue();
	}

	private void createNewRoot()
	{
		GameObject newBranch = Instantiate(branchPrefab, t.position, Quaternion.identity);
		newBranch.name = "Root";
		newBranch.transform.SetParent(t);
		tree.Add(newBranch);
		levelCounter++;
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			readyToSpawn = true;
			rb.isKinematic = true;
			time = Time.time;
			t.SetParent(col.gameObject.transform);
		}
	}
}
