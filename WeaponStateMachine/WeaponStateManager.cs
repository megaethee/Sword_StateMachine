using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStateManager : MonoBehaviour
{	
	public WeaponBaseClass swordCurrentState;
	public WeaponBaseClass gunCurrentState;

	public bool sword, gun;
		
	public SwingState swing = new SwingState();
	public IdleState idle = new IdleState();
	public ProjectileState projectile = new ProjectileState();
	public HeldState held = new HeldState();
	
	public GunIdleState gunIdle = new GunIdleState();
	public ShootState shoot = new ShootState();

    // Start is called before the first frame update
    void Start()
    {
		if (sword == true) {	
			swordCurrentState = idle; 	
			swordCurrentState.Initialize(this);
			swordCurrentState.Enter(this); 

			gun = false;
		}
		else if (gun == true) {
			gunCurrentState = gunIdle;
			gunCurrentState.Initialize(this);
			gunCurrentState.Enter(this);

			sword = false;
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (sword == true) swordCurrentState.Do(this);
		else if (gun == true) gunCurrentState.Do(this);
    }

    public void SwitchState(WeaponBaseClass state)
    {
		if (sword == true) {
			swordCurrentState = state;
			state.Enter(this);
		}
		else if (gun == true) {
			gunCurrentState = state;
			state.Enter(this);	
		}
    }
}
