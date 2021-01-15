using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Animator anim;
    public string unitName;
    public int HP;
    public int MP;
    public int strength;
    public int defense;
    public int speed;
    public int currentHP;
    public int currentMP;

    public string message;
    public int dmg;
    public bool usingSkill = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public bool TakeDamage(int dmg)
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
    }

    public virtual void AttackRoutine()
    {
        if (PlayerStats.Blocking)
        {
            message = "You blocked and took " + (strength - 3) + " damage";
            dmg = strength - 3;
        }

        else
        {
            message = unitName + " attacked for " + strength + " damage";
            dmg = strength;
        }
    }

    public void GoIdle()
    {
        anim.SetTrigger("Idle");
    }

    public void Dead()
    {
        anim.SetTrigger("Dead");
    }
}
