using System.Reflection;
using static UnityEngine.ParticleSystem;

public class DamageModifier : AbstractValueModifier<float>
{
    public override void Apply(GunScriptableObject Gun)
    {
        try
        {
            float damageCurve = GetAttribute<float>(
                Gun,
                out object targetObject,
                out FieldInfo field
            );

       /*     switch (damageCurve.mode)
            {
                case UnityEngine.ParticleSystemCurveMode.TwoConstants:
                    damageCurve.constantMin *= Amount;
                    damageCurve.constantMax *= Amount;
                    break;
                case UnityEngine.ParticleSystemCurveMode.TwoCurves:
                    damageCurve.curveMultiplier *= Amount;
                    break;
                case UnityEngine.ParticleSystemCurveMode.Curve:
                    damageCurve.curveMultiplier *= Amount;
                    break;
                case UnityEngine.ParticleSystemCurveMode.Constant:
                    damageCurve.constant *= Amount;
                    break;
            }*/

            field.SetValue(targetObject, damageCurve);
        }
        catch (InvalidPathSpecifiedException) { } // don't kill the flow, just log those errors
                                                  // so we can fix them!
    }
}
