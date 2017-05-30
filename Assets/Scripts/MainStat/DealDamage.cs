using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour {

	// 0 = torso, 1 = leftWeapon, 2 = RightWeapon, 3 = LeftLeg, 4 = RightLeg
	public PlayerStats playerStats;
	public bool torsoHit, leftWeaponHit, rightWeaponHit, leftLegHit, rightLegHit;

	public void ApplyDamage(int dmg){
		if(torsoHit){
			playerStats.CmdApplyDamage(0,dmg);
		}
		else if(leftWeaponHit){
			playerStats.CmdApplyDamage(1,dmg);
		}
		else if(rightWeaponHit){
			playerStats.CmdApplyDamage(2,dmg);
		}
		else if(leftLegHit){
			playerStats.CmdApplyDamage(3,dmg);
		}
		else if(rightLegHit){
			playerStats.CmdApplyDamage(4,dmg);
		}
	}
}
