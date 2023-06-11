using UnityEngine;

public class SetImpactType : MonoBehaviour
{
    public ImpactType[] impactTypes;

    public void SetImpactTypeByName(string impactName)
    {
        ImpactType selectedImpactType = null;

        foreach (ImpactType impactType in impactTypes)
        {
            if (impactType.name == impactName)
            {
                selectedImpactType = impactType;
                break;
            }
        }

        if (selectedImpactType != null)
        {
            GunModifierApplier gunImpacts = FindObjectOfType<GunModifierApplier>();
            gunImpacts.ImpactTypeOverride = selectedImpactType;
        }
    }
}
