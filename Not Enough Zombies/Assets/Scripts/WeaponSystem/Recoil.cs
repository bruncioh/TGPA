using UnityEngine;

public class Recoil : MonoBehaviour
{
    //rotation
    private Vector3 mCurrentRotation;
    private Vector3 mEndRotation;

    //recoil coordinates
    [SerializeField] private float mRecoilX;
    [SerializeField] private float mRecoilY;
    [SerializeField] private float mRecoilZ;

    //Settings
    [SerializeField] private float mRecoilSpeed;
    [SerializeField] private float mSnappiness;
    void Start()
    {
        
    }

    void Update()
    {
        mEndRotation = Vector3.Lerp(mEndRotation, Vector3.zero, mRecoilSpeed * Time.deltaTime);
        mCurrentRotation = Vector3.Slerp(mCurrentRotation, mEndRotation, mSnappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(mCurrentRotation);
    }

    public void ApplyRecoil()
    {
        mEndRotation += new Vector3(mRecoilX, Random.Range(-mRecoilY, mRecoilY), Random.Range(-mRecoilZ, mRecoilZ));
    }
}
