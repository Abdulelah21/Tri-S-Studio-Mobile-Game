using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffectReplacementModifier : AbstractValueModifier<ICollisionHandler[]>
{
    public ImpactEffectReplacementModifier() : base() { }
    public ImpactEffectReplacementModifier(ICollisionHandler[] Values) : base(Values, "ShootConfig/BulletImpactEffects") { }

    public override void Apply(GunScriptableObject Gun)
    {
        Gun.BulletImpactEffects = Amount;


    }
}
