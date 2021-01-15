using System.Collections;
using UnityEngine;

/*This script should go on an object where the ray is starting from
like the muzzle of a gun, not on the actual gun.*/

public class RayGun : MonoBehaviour
{
    public float shootRate;
    private float m_shootRateTimeStamp;

    public GameObject m_shotPrefab;
    [SerializeField]
    private AudioClip Clip;
    private AudioSource source;

    RaycastHit hit;
    float range = 1000.0f;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (PlayerStats.HasRifle && !PausedMenu.isPaused)
        {
            if (Input.GetMouseButton(0) && PlayerControl.aiming)
            {
                if (Time.time > m_shootRateTimeStamp)
                {
                    shootRay();
                    source.PlayOneShot(Clip);
                    m_shootRateTimeStamp = Time.time + shootRate;
                }
                
            }   
        }
    }

    void shootRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, range))
        {
            GameObject laser = GameObject.Instantiate(m_shotPrefab, transform.position, transform.rotation) as GameObject;
            laser.GetComponent<ShootBehavior>().setTarget(hit.point);
            GameObject.Destroy(laser, 2f);
        }
    }
}

