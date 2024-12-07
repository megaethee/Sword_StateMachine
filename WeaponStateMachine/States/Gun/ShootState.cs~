using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShootState : WeaponBaseClass    
{			  
	public Camera fireCam;
	public LayerMask layerMask;
	public Image crossHair;
	public ParticleSystem flash;
	public AudioSource weaponSound;
	
	public Animator gunAnim;

	public float weaponRange;

	public override void Enter(WeaponStateManager weapon)
	{
		gunAnim = weapon.GetComponent<Animator>();
		crossHair = GameObject.Find("CrossHair").GetComponent<Image>();
		fireCam = GameObject.Find("Camera").GetComponent<Camera>();
		weaponSound = GameObject.Find("Gunshot").GetComponent<AudioSource>();
		flash = GameObject.Find("GunFlash").GetComponent<ParticleSystem>();

		equipped = true;	
	}

	public override void Do(WeaponStateManager weapon)
	{
		RaycastHit hit;
		Vector3 rayOrigin = fireCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));
		//Debug.DrawRay(rayOrigin, transform.forward * weaponRange, Color.green, 1f);

		if (Input.GetButtonDown("Fire1")) {
			flash.Play();
			weaponSound.Play();
			gunAnim.SetTrigger("fire");	
		}

		if (Physics.Raycast(rayOrigin, fireCam.transform.forward, out hit, weaponRange, layerMask, QueryTriggerInteraction.Ignore) && hit.transform.CompareTag("Enemy")) {
			UnityEngine.GameObject.Destroy(hit.transform.gameObject);
		}

		if (Drop(weapon) == true) weapon.SwitchState(weapon.gunIdle);
	}
}
