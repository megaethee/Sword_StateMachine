using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "IdleState", menuName = "SwordBaseClass/IdleState")]
[System.Serializable]
public class IdleState : WeaponBaseClass 
{	
// remember! To call an override in any state is to remove what it did in the base class
	public override void Enter(WeaponStateManager weapon)
	{
		returnCount = 0;
		equipped = false;
		swinging = false;

		DisableShards(weapon);
	}

	public override void Do(WeaponStateManager weapon)
	{	
		Debug.Log("Now idling");

		PickUp(weapon);
		
		if (equipped)	
			weapon.SwitchState(weapon.held);		
	}		
}
