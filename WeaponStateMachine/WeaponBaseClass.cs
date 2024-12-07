using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "SwordBaseClass", menuName = "SwordBaseClass")]
[System.Serializable]
public abstract class WeaponBaseClass 
{	
	public Rigidbody rb;
	public BoxCollider coll;
	public Transform player, gunOffset, cam, constraint;	

	public float pickUpRange = 5f;
	public float dropForwardForce, dropUpwardForce;
	    
	public float holdHeight=0f,
			 holdDistanceY=0f,
			 holdDistanceX=0f,
			 zRotation=45f;
	
	public int enterCount = 0, returnCount = 0;

	public bool equipped, swinging;
	public static bool slotFull;	
	
	//remember, "weapon" is a GameObject
	//
	public virtual void Initialize(WeaponStateManager weapon)
	{	
		Debug.Log("Hello!");
				
		rb = weapon.GetComponent<Rigidbody>();
		coll = weapon.GetComponent<BoxCollider>();

		player = GameObject.Find("Player").transform;
		gunOffset = GameObject.Find("Gun_Offset").transform;
		constraint = GameObject.Find("GunConstraint").transform;

		if (!equipped) {
			rb.isKinematic = false;
			coll.isTrigger = false;
		}
		if (equipped) {
			rb.isKinematic = true;
			coll.isTrigger = true;
			slotFull = true;
		}

	}

	public virtual void Enter(WeaponStateManager weapon)
	{
		
	}

	public virtual void Do(WeaponStateManager weapon)
	{	

	}

	public virtual void LateUpdate(WeaponStateManager weapon)
	{

	}

	public virtual void OnCollisionEnter(Collision other)
	{

	}	

	public virtual void DisableShards(WeaponStateManager weapon)
	{
		for (int i=0; i<3; i++)
		{
			weapon.transform.GetChild(i).GetComponent<BoxCollider>().enabled = swinging;
		}
	}	

	public virtual void PickUp(WeaponStateManager weapon)
	{
		Vector3 distanceToPlayer = player.position - weapon.transform.position;

		if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) {	

			equipped = true;
			slotFull = true;

			weapon.transform.SetParent(gunOffset);

			rb.isKinematic = true;
			coll.isTrigger = true;
	// UUSELESS USELESS USELESS USELESS USELESS USELESS
			//constraint.localPosition = new Vector3(holdDistanceX, holdHeight, holdDistanceY);
			weapon.transform.position = constraint.position + new Vector3(0, -1, 1);
			constraint.localRotation = Quaternion.Euler(0f, 0f, zRotation);		

			constraint.localScale = Vector3.one;

		}
			
	}

	public virtual bool Drop(WeaponStateManager weapon)
	{
		rb = weapon.GetComponent<Rigidbody>();
		coll = weapon.GetComponent<BoxCollider>();
		cam = GameObject.Find("Camera").transform;		


		if (equipped && Input.GetKeyDown(KeyCode.Q) && slotFull) {

			equipped = false;
			slotFull = false;
			
			weapon.transform.SetParent(null);	

			rb.isKinematic = false;
			coll.isTrigger = false;

			rb.AddForce(cam.forward * dropForwardForce, ForceMode.Impulse);
			rb.AddForce(cam.forward * dropUpwardForce, ForceMode.Impulse);

			float random = Random.Range(-1f, 1f);
			rb.AddTorque(new Vector3(random, random, random)*10);
			
			return true;	
		}

		return false;
	}
	
}
