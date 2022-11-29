using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefabSmall,bulletPrefabBig;
    public float timer;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            timer = Time.time;
        }

        if (Input.GetButtonUp("Fire1")&&(Time.time - timer)<.5)
        {
            ShootSmall();
        }
        else if (Input.GetButtonUp("Fire1") && (Time.time - timer) > .5)
        {
            ShootBig();
        }
    }

    void ShootSmall()
    {
        Instantiate(bulletPrefabSmall, firePoint.position, firePoint.rotation);
    }

    void ShootBig()
    {
        Instantiate(bulletPrefabBig, firePoint.position, firePoint.rotation);
    }
}
