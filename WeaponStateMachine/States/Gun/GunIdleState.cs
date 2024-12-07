using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunIdleState : WeaponBaseClass 
{
   public override void Enter(WeaponStateManager weapon) {
	equipped = false;
   }

   public override void Do(WeaponStateManager weapon) {

	PickUp(weapon);

	if (equipped)
		weapon.SwitchState(weapon.shoot);
   } 
}
