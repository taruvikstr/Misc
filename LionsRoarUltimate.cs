using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionsRoarUltimate : MonoBehaviour
    //This is for Komainus ultimate attack
    //Attached to an empty object in level
{

    public bool ultimateActive; //This bool is also checked each time a bullet is instantiated to give the dmg boost of the ult

    public void Start()
    {
        ultimateActive = false; //Making sure it's inactive at the start
    }
    public void UseLionsRoar()
    {
        if (ultimateActive == false) // This function is called from Leader Guardian script if the ult was LionsRoar
        {
            // Play lion roar sound
            AudioManager.PlaySound("Lion_Roar");

            ultimateActive = true; // setting the ult active
            StartCoroutine(UltimateTimer());
        }
    }

    IEnumerator UltimateTimer() // Takes care of how long the ultimate lasts and then sets the bool again to false
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower"); // gathering all the built towers into an array
        GameObject[] towerPlatforms = GameObject.FindGameObjectsWithTag("TowerPlatform");

        foreach (GameObject tower in towers)
        {
            if (tower.name == "Inari(Clone)")
            {
                tower.GetComponent<ElectricUnit>().SetAttackDistanceAndSpeed();
            }
            else if (tower.name == "Snake(Clone)")
            {
                tower.GetComponent<BasicUnit>().SetAttackDistanceAndSpeed();
            }
            else if (tower.name == "Komainu(Clone)")
            {
                tower.GetComponent<BombardierUnit>().SetAttackDistanceAndSpeed();
            }
            else if (tower.name == "Minotaur(Clone)")
            {
                tower.GetComponent<TrapUnit>().SetAttackDistanceAndSpeed();
            }
            else if (tower.name == "Ra(Clone)")
            {
                tower.GetComponent<TimeSlowingUnit>().SetAttackDistanceAndSpeed();
            }
            else if (tower.name == "Phoenix(Clone)")
            {
                tower.GetComponent<LongRangeUnit>().SetAttackDistanceAndSpeed();
            }
            else if (tower.name == "Walrus(Clone)")
            {
                tower.GetComponent<ConeOfElementalUnit>().SetAttackDistanceAndSpeed();
            }
        } //going through each towers scripts to update the attackSpeed variable

        foreach (GameObject platform in towerPlatforms)
        {
            Animator anim = platform.GetComponent<Animator>();
            anim.SetBool("LionsRoarON", true);
        }

            yield return new WaitForSeconds(15f);

        Debug.Log("lions roar over");

        ultimateActive = false;

        foreach (GameObject tower in towers)
        {
            if (tower.name == "Inari(Clone)")
            {
                tower.GetComponent<ElectricUnit>().SetAttackDistanceAndSpeed();
            }
            else if (tower.name == "Snake(Clone)")
            {
                tower.GetComponent<BasicUnit>().SetAttackDistanceAndSpeed();
            }
            else if (tower.name == "Komainu(Clone)")
            {
                tower.GetComponent<BombardierUnit>().SetAttackDistanceAndSpeed();
            }
            else if (tower.name == "Minotaur(Clone)")
            {
                tower.GetComponent<TrapUnit>().SetAttackDistanceAndSpeed();
            }
            else if (tower.name == "Ra(Clone)")
            {
                tower.GetComponent<TimeSlowingUnit>().SetAttackDistanceAndSpeed();
            }
            else if (tower.name == "Phoenix(Clone)")
            {
                tower.GetComponent<LongRangeUnit>().SetAttackDistanceAndSpeed();
            }
            else if (tower.name == "Walrus(Clone)")
            {
                tower.GetComponent<ConeOfElementalUnit>().SetAttackDistanceAndSpeed();
            }
        } //updating again the attackSpeed variable when ult is off

        foreach (GameObject platform in towerPlatforms)
        {
            Animator anim = platform.GetComponent<Animator>();
            anim.SetBool("LionsRoarON", false);
        }
    }
}
