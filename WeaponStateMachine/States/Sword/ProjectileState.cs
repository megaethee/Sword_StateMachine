using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "ProjectileState", menuName = "SwordBaseClass/ProjectileState")]
[System.Serializable]
public class ProjectileState : WeaponBaseClass 
{	
	public GameObject shard, muzzlePoint;
	public GameObject[] shards = new GameObject[4];
	public GameObject[] pieces = new GameObject[4];
					

	public bool shoot = false, hasReturned = false;
	public float lerpSpeed = 5f;
		
	public override void Enter(WeaponStateManager weapon)
	{
		Vector3 vectorAdd = new Vector3(3f, 3f, 3f);
		muzzlePoint = GameObject.Find("MuzzlePoint");
		
		enterCount++;
		//piece = GameObject.Find("Handle");
	
		if (enterCount == 1)
		       FlipActive(enterCount, weapon);	
		else if (enterCount == 2)
			FlipActive(enterCount, weapon);
		else if (enterCount == 3)
		       FlipActive(enterCount, weapon);	
		else if (enterCount == 4) {
			shards[1] = UnityEngine.GameObject.Find("BladeAnim_Shard1(Clone)");
			shards[2] = UnityEngine.GameObject.Find("BladeAnim_Shard2(Clone)");
			shards[3] = UnityEngine.GameObject.Find("BladeAnim_Shard3(Clone)");
		
			weapon.StartCoroutine(ReturnTo(shards[1], weapon));
			weapon.StartCoroutine(ReturnTo(shards[2], weapon));
			weapon.StartCoroutine(ReturnTo(shards[3], weapon));		
		}

		if (shoot == true && enterCount != 4 && shards[enterCount] == null) {
			Debug.Log("Shooting");	
			Debug.Log(enterCount);

			GameObject newShard = GameObject.Instantiate(shard, muzzlePoint.transform.position, Quaternion.identity) as GameObject;
			newShard.transform.localScale = Vector3.one + vectorAdd;
			newShard.GetComponent<Rigidbody>().AddForce(muzzlePoint.transform.up * 2000f);

			weapon.SwitchState(weapon.swing);
		}
		else if (shoot == true && enterCount != 4 && shards[enterCount] != null)
		{
			weapon.SwitchState(weapon.swing);
		}	
	}

	private void FlipActive(int enterCount, WeaponStateManager weapon)
	{
		if (GameObject.Find("BladeAnim" + enterCount) == null) 
		{	
			weapon.SwitchState(weapon.swing);
			return;
		}
		
		pieces[enterCount] = GameObject.Find("BladeAnim" + enterCount);
		pieces[enterCount].SetActive(false);

		shard = Resources.Load<GameObject>("BladeAnim_Shard" + enterCount);
		shoot = true;
	}

	private void Switcher(GameObject shard, WeaponStateManager weapon)
	{		
		if (shard == shards[1])	
			pieces[1].SetActive(true);
		else if (shard == shards[2])
			pieces[2].SetActive(true);
		else if (shard == shards[3])
			pieces[3].SetActive(true);

		UnityEngine.GameObject.Destroy(shard);
		
		enterCount = 0;
		returnCount++;
				
		weapon.SwitchState(weapon.swing);
			
	}
	
	private IEnumerator ReturnTo(GameObject shard, WeaponStateManager weapon)
	{
		float elapsedTime = 0;
		float length = Vector3.Distance(shard.transform.position, weapon.transform.position),
			breakLength = Vector3.Distance(shard.transform.position, weapon.transform.position);
		Vector3 shardTransform = shard.transform.position;	
		
		//This loop acts like a psuedo-update, making sure the lerp is continous every "frame", at least until the elapsedTime and length meet. Without this loop the lerp will teleport.
		while (elapsedTime < length) {
			if (breakLength < 0.5f)
				break;
			if (shard == null)
				break;
			shard.transform.position = Vector3.Lerp(shardTransform, weapon.transform.position, (elapsedTime / length)*lerpSpeed);	
			elapsedTime += Time.deltaTime;
			breakLength = Vector3.Distance(shard.transform.position, weapon.transform.position);
			yield return null;
		}
		shoot = false;

		Switcher(shard, weapon);		
	}
}
