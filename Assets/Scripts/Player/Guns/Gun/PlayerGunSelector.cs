using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerGunSelector : MonoBehaviour
{

    [SerializeField]
    private GunType Gun;
    [SerializeField]
    private Transform GunParent;
    [SerializeField]
    private List<GunScriptableObject> Guns;

    // Here I will write Player Inverse Kinematics (Animation)

    [Space]
    [Header("Runtime Filled")]
    public GunScriptableObject ActiveGun;

    private void Awake()
    {
        GunScriptableObject gun = Guns.Find(gun => gun.Type == Gun);

        if (gun == null)
        {
            Debug.LogError($"No GunScriptableObject found for GunType: {gun}");
        }

        ActiveGun = gun.Clone() as GunScriptableObject;
        ActiveGun.Spawn(GunParent, this);

        //some magic for IK below 

    }

    public void ApplyModifiers(IModifier[] Modifiers)
    {
        ActiveGun.Despawn();
        Destroy(ActiveGun);

        Awake();

        foreach (IModifier modifier in Modifiers)
        {
            modifier.Apply(ActiveGun);
        }
    }

}
