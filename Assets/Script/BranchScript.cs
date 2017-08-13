using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchScript : MonoBehaviour
{
	public GameObject branchPrefab;
	public GameObject endPoint;
	public bool hasBeenBranched = false;
	public float newBranchScaleMultiplier;

	private Transform t;
	private Vector3 endPos;

	public GameObject createNewBranch(int levelCounter, int branchCounter)
	{
		t = GetComponent<Transform>();
		endPos = endPoint.GetComponent<Transform>().position;

		Vector3 branchVector = endPos - t.position;
		Vector3 newPos = endPos - branchVector * Random.Range(0.1f, 0.5f);

		GameObject newBranch = Instantiate(branchPrefab, newPos, t.parent.rotation);
		newBranch.transform.Rotate(Random.Range(-90f, 90f), Random.Range(-90f, 90f), Random.Range(-90f, 90f));

		newBranch.name = "Level" + levelCounter + " Branch" + branchCounter;

		newBranch.transform.localScale *= newBranchScaleMultiplier;

		return newBranch;
	}

	public void setHasBeenBranchedToTrue()
	{
		hasBeenBranched = true;
	}
}
