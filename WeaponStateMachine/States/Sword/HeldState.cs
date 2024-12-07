using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeldState : WeaponBaseClass 
{
    public Animator swordAnim;

   public override void Enter(WeaponStateManager weapon)
   {
        equipped = true;

        weapon.StartCoroutine(Wait(weapon));     
    }

   public override void Do(WeaponStateManager weapon)
   {
        if(Input.GetButtonDown("Fire1"))
        {
            weapon.SwitchState(weapon.swing);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            weapon.SwitchState(weapon.projectile);
        }
        
        if (Drop(weapon) == true) weapon.SwitchState(weapon.idle);

   }

   private IEnumerator Wait(WeaponStateManager weapon)
   {
        yield return new WaitForSeconds(0.5f);

        swinging = false;

        DisableShards(weapon);

        Debug.Log(swinging);
   } 

}
