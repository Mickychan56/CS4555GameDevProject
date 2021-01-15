using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEngine;

public class CrabBossUnit : Unit
{
    int counter = 0;
    bool Taunted = false;
    

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
        if (counter == 2 && Taunted == false)
        {
            anim.SetTrigger("Taunt");
            Taunt();
            Taunted = true;
            counter = 0;
        }
        else if (counter == 2 && Taunted == true)
        {
            anim.SetTrigger("Heavy Bash");
            Bash();
            this.strength -= 4;
            counter = 0;
        }
        else
        {
            anim.SetTrigger("Swipe");
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

    public void Taunt()
    {
        message = "The monster roars Strengthening itself!";
        this.strength += 4;
    }

    public void Bash()
    {
        if (PlayerStats.Blocking)
        {
            message = "You blocked the Bash and took " + (strength + 8 - 2) + " damage";
            dmg = strength + 6;
        }

        else
        {
            message = unitName + " bashes you for " + (strength + 8) + " damage";
            dmg = strength + 8;
        }
    }

   
}

