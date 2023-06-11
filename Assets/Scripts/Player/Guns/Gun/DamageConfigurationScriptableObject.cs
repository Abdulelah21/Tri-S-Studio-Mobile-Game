using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[CreateAssetMenu(fileName ="Damage Config",menuName ="Guns/Damage Config",order =1)]
public class DamageConfigurationScriptableObject : ScriptableObject, System.ICloneable
{
/*    public MinMaxCurve DamageCurve;*/
    public float DamageValue;



/*    private void Reset()
    {
        DamageCurve.mode = ParticleSystemCurveMode.Curve;
    }*/

    public int GetDamage(float Distance = 0)
    {
        return Mathf.CeilToInt(DamageValue);
    }

    public object Clone()
    {
        DamageConfigurationScriptableObject config = CreateInstance<DamageConfigurationScriptableObject>();

        config.DamageValue = DamageValue;
        return config;
    }
}