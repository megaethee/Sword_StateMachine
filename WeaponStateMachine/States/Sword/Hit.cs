using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
	public AudioSource weaponClang;
	
	void Start()
	{
		weaponClang = GameObject.Find("SwordClang").GetComponent<AudioSource>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			Debug.Log("Hit");
			Destroy(other.gameObject);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "Enemy")
		{
			Debug.Log("Hit");
			Destroy(collision.gameObject);
		}
	}
}
