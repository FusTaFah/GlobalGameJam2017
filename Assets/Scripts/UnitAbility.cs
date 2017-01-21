using UnityEngine;
using System.Collections;

public class UnitAbility {

    string m_abilityName;
    float m_cooldown;
    float m_cooldownTimer;

    public UnitAbility(string abilityName, float cooldown)
    {
        m_abilityName = abilityName;
        m_cooldown = cooldown;
        m_cooldownTimer = 0.0f;
    }

    public void UpdateCooldown(float deltaTime)
    {
        
        m_cooldownTimer -= deltaTime;
    }

    public string GetAbilityName()
    {
        return m_abilityName;
    }

    public float GetCurrentCooldown()
    {
        return m_cooldownTimer;
    }

    public float GetMaxCooldown()
    {
        return m_cooldown;
    }
}
