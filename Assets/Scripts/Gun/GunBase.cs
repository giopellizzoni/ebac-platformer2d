using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase projectileBase;
    public Transform positionToShoot;
    public float timeBetweenShoot = .3f;
    public Transform playerSideReference;
    public AudioRandomPlayAudioClips randomShot;


    private Coroutine _currentCoroutine;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
         _currentCoroutine = StartCoroutine(StartShoot());
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
        }
    }

    IEnumerator StartShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }

    }

    public void Shoot()
    {
        if (randomShot != null) { randomShot.PlayRandom(); }
        var projectile = Instantiate(projectileBase);
        projectile.transform.position = positionToShoot.position;
        projectile.side = playerSideReference.transform.localScale.x;
    }
}
