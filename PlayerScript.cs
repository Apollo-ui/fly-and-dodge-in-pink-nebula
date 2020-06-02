using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject laserShot;    // Чем стрелять
    public GameObject laserGunLeft,
        laserGunRight;              // Откуда стрелять
    public float shotDelay;         // Задержка выстрелов

    public float speed;             // Скорость
    public float tilt;              // Коэф. поворота
    public float xMin, xMax, 
        zMin, zMax;                 // Границы экрана

    Rigidbody playerShip;
    float nextShotTime;
     
    void Start()
    {
        playerShip = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameController.instance.isGameStarted == false)
            return;

        // Перемещение
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        playerShip.velocity = new Vector3(moveHorizontal * speed, 0, moveVertical * speed);

        // Ограничение позиции в рамках экрана
        float xPosition = Mathf.Clamp(playerShip.position.x, xMin, xMax);
        float zPosition = Mathf.Clamp(playerShip.position.z, zMin, zMax);
        playerShip.position = new Vector3(xPosition, 0, zPosition);

        // Повороты
        playerShip.rotation = Quaternion.Euler(playerShip.velocity.z * tilt, 0, playerShip.velocity.x * tilt * -1);

        // Стрельба
        if (Time.time > nextShotTime && Input.GetButton("Fire1"))
        {
            Instantiate(laserShot, laserGunLeft.transform.position, Quaternion.identity);
            Instantiate(laserShot, laserGunRight.transform.position, Quaternion.identity);
            nextShotTime = Time.time + shotDelay;
        }
    }
}
