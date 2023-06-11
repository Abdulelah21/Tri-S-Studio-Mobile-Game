using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ammo Config", menuName = "Guns/Ammo Config", order = 3)]
public class AmmoConfigurationScriptableObject : ScriptableObject, System.ICloneable
{

    public int MaxAmmo = 120;
    public int ClipSize = 30;

    public int CurrentAmmo = 120;
    public int CurrentClipAmmo = 30;

    /// <summary>
    /// Reloads with the ammo conserving algorithm.
    /// Meaning it will only subtract the delta between the ClipSize and CurrentClipAmmo from the CurrentAmmo.
    /// </summary>
    public void Reload()
    {
        int maxReloadAmount = Mathf.Min(ClipSize, CurrentAmmo);
        int availableBulletsInCurrentClip = ClipSize - CurrentClipAmmo;
        int reloadAmount = Mathf.Min(maxReloadAmount, availableBulletsInCurrentClip);
        CurrentClipAmmo += reloadAmount;
        CurrentAmmo -= reloadAmount;
    }

    /// <summary>
    /// Reloads not conserving ammo.
    /// Meaning it will always subtract the ClipSize from CurrentAmmo (if available).
    /// </summary>
    //public void Reload()
    //{
    //    int reloadAmount = Mathf.Min(ClipSize, CurrentAmmo);
    //    CurrentClipAmmo = reloadAmount;
    //    CurrentAmmo -= reloadAmount;
    //}

    public bool CanReload()
    {
        return CurrentClipAmmo < ClipSize && CurrentAmmo > 0;
    }

    public object Clone()
    {
        AmmoConfigurationScriptableObject config = CreateInstance<AmmoConfigurationScriptableObject>();

        Utilities.CopyValues(this, config);

        return config;
    }
}