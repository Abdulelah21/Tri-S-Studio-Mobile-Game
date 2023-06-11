using UnityEngine;

public class GunModifierApplier : MonoBehaviour
{
    [SerializeField]
    private ImpactType impactTypeOverride;
    [SerializeField]
    private PlayerGunSelector GunSelector;
    public ImpactType ImpactTypeOverride
    {
        get { return impactTypeOverride; }
        set { impactTypeOverride = value; }
    }

  
    private void Start()
    {



        if (GunSelector.ActiveGun != null)
        {
            if (impactTypeOverride != null)
            {
                new ImpactTypeModifier()
                {
                    Amount = impactTypeOverride
                }.Apply(GunSelector.ActiveGun);
                GunSelector.ActiveGun.BulletImpactEffects = new ICollisionHandler[]
                {
            new Frost(
                1.5f,
                new AnimationCurve(new Keyframe[]{ new Keyframe(0, 1), new Keyframe(1, 0.25f)}),
                10,
                10,
                 new AnimationCurve(new Keyframe[]{  //          <-------------------- This line and below keyframes are for frost effect to slow enemy
                     new Keyframe(0, 0.25f),
                     new Keyframe(1.75f, 0.25f),
                     new Keyframe(2,1)})

                )
                


            };
                new Explode(
               1.5f,
               new AnimationCurve(new Keyframe[] { new Keyframe(0, 1), new Keyframe(1, 0.25f) }),
               10,
               10
               );




                DamageModifier damageModifier = new()
                {
                    Amount = 1.5f,
                    AttributeName = "DamageConfig/DamageCurve"
                };
                damageModifier.Apply(GunSelector.ActiveGun);

                Vector3Modifier spreadModifier = new()
                {
                    Amount = Vector3.zero,
                    AttributeName = "ShootConfig/Spread"
                };
                spreadModifier.Apply(GunSelector.ActiveGun);
            }
        }
}
}
