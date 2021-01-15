using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsectUnit : Unit
{
    int counter = 0;

    /*public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        return false;
    }

    public bool Heal(int amount)
    {
        if (currentMP > 20)
        {
            currentHP += amount;
            currentMP -= 20;

            if (currentHP > HP)
                currentHP = HP;

            return true;
        }
        else
            return false;
    }*/


    public override void AttackRoutine()
    {
       if(counter == 2)
        {
            anim.SetTrigger("Stab");
            Stab();
            counter = 0;
        }
        else
        {
            anim.SetTrigger("NormalAttack");
            Attack();
            counter += 1;
        }
    }

    public void Attack()
    {
        if (PlayerStats.Blocking)
        {
            message = "You blocked and took " + (strength - 3) + " damage";
            dmg = strength - 3;
        }

        else
        {
            message = unitName + " swipes at you for " + strength + " damage";
            dmg = strength;
        }
    }

    public void Stab()
    {
        if (PlayerStats.Blocking)
        {
            message = "You blocked the Stab and took " + (strength + 5 - 2) + " damage";
            dmg = strength + 3;
        }

        else
        {
            message = unitName + " stabs you for " + (strength + 5) + " damage";
            dmg = strength + 5;
        }
    }
}
