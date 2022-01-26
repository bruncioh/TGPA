using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "WeaponData", order = 1)]
[System.Serializable]
public class WeaponData : ScriptableObject
{
    public enum WeaponFireModes { AUTO, SEMIAUTO, BURST, LASER }

    public string mPrefabName;
    public GameObject mPrefab;
    public int mNoPrefabsToMake;

    //weapon stats
    public float mRange = 100.0f;
    public float mDamage = 10.0f;
    public int mAmmo;
    public int mMagSize;
    public float mReloadTime;
    public double mFireTime = 0;
    public double mFireDelay;
}
