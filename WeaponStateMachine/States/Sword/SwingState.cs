using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(fileName = "SwingState", menuName = "SwordBaseClass/SwingState")]
[System.Serializable]
public class SwingState : WeaponBaseClass 
{
	public AudioSource weaponSound;
	public Animator swordAnim;
	
	public float weaponRange;
	
	public override void Enter(WeaponStateManager weapon)
	{	
		weapon.StartCoroutine(Wait(weapon));	

		Debug.Log("Now swinging!");
		swordAnim = weapon.GetComponent<Animator>();
		swordAnim.SetTrigger("isEquipped");

		weaponSound = GameObject.Find("SwordSlash").GetComponent<AudioSource>();

		swordAnim.SetTrigger("fire");
		weaponSound.Play();
		
		weapon.SwitchState(weapon.held);
	}		

	private IEnumerator Wait(WeaponStateManager weapon)
	{
		swinging = true;

		DisableShards(weapon);

		Debug.Log(swinging);
		yield return new WaitForSeconds(0.5f);
	}
}
