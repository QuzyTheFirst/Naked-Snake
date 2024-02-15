using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeExploder : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionParticles;
    
    public IEnumerator Explode(Queue<Rigidbody> rigidbodies)
    {
        SoundManager.Instance.Play("UhOh");
        yield return new WaitForSeconds(.2f);
        foreach (Rigidbody rig in rigidbodies)
        {
            rig.isKinematic = false;
            rig.useGravity = true;

            float randomXValue = Random.Range(-1f, 1f);
            float randomYValue = Random.Range(0, 1f);
            float randomZValue = Random.Range(-1f, 1f);

            Vector3 randomDirection = (new Vector3(randomXValue * 5f, randomYValue * 10f, randomZValue * 5f)).normalized;
            
            rig.velocity = randomDirection * 10f;
            rig.angularVelocity = new Vector3(randomXValue, randomYValue, randomZValue) * 720f;

            _explosionParticles.transform.position = rig.position;
            _explosionParticles.Play();
            
            SoundManager.Instance.Play("Explosion");
            
            yield return new WaitForSecondsRealtime(.2f);
        }
    }
}
