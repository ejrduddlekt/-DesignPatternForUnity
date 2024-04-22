using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "powerUp")]
public class PowerUp : ScriptableObject, IVisitor
{
    public string powerupName;
    public GameObject powerupPrefab;
    public string powerupDescription;

    [Tooltip("Fullry heal shield")]
    public bool healShield;

    [Range(0.0f, 50f)]
    [Tooltip("Boost turbo setting up to increments of 50/mph")]
    public float turboboost;

    [Range(0.0f, 25f)]
    [Tooltip("Boost weapon range in increments of up to 25 units")]
    public int weaponRange;

    [Range(0.0f, 50f)]
    [Tooltip("Boost weapon strength in increments of up to 50%")]
    public int weaponStrength;

    public void Visit(BikeShield bikeShield)
    {
        if (healShield)
            bikeShield.health = 100f;
    }

    public void Visit(BikeWeapon bikeWeapon)
    {
        int range = bikeWeapon.range += weaponRange;

        if (range >= bikeWeapon.maxRange)
            bikeWeapon.range = bikeWeapon.maxRange;
        else 
            bikeWeapon.range = range;

        float strength = bikeWeapon.strength += Mathf.Round(bikeWeapon.strength * weaponStrength / 100);

        if(strength >= bikeWeapon.maxStrength)
            bikeWeapon.strength = bikeWeapon.maxStrength;
        else 
            bikeWeapon.strength = strength;
    }

    public void Visit(BikeEngine bikeEngine)
    {
        float boost = bikeEngine.turboBoost += turboboost;

        if(boost < 0.0f)
            bikeEngine.turboBoost = 0.0f;

        if(boost >= bikeEngine.maxTurboBoost)
            bikeEngine.turboBoost = bikeEngine.maxTurboBoost;
    }
}
