using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public GameObject asteroidExplosion;
    public GameObject playerExplosion;
    public float rotationSpeed;
    public float minSpeed, maxSpeed;
    public int pointsPerAsteroid;

    void Start()
    {
        // Вращение
        Rigidbody asteroid = GetComponent<Rigidbody>();
        asteroid.angularVelocity = Random.insideUnitSphere * rotationSpeed;

        // Скорость
        float zSpeed = Random.Range(minSpeed, maxSpeed);
        asteroid.velocity = new Vector3(0, 0, zSpeed * -1);
    }

      private void OnTriggerEnter(Collider other)
    {
        // Границы экрана
        if (other.tag == "GameBoundary")
        {
            return;
        }
        
        // Уничтожение и подсчёт очков
        if (other.tag == "PlayerShot" || other.tag == "Player")
        {
            Destroy(gameObject);

            if (other.tag == "Player")
            {
                other.gameObject.SetActive(false);
                Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
                GameController.instance.isGameStarted = false;
            }
            else
            {
                Destroy(other.gameObject);
                GameController.instance.incrementScore(pointsPerAsteroid);
                Instantiate(asteroidExplosion, transform.position, Quaternion.identity);
            }
        }
    }
}
