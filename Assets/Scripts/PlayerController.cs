using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Player player;
    private float k; // коэффициент увеличения скорости (зависит от собранных на дороге звездочек)

    private float angle;
    private Vector2 direction;
    public void Enable()
    {
        player = Manager.Get.Player;
        k = 1;
        Manager.Get.GameController.fixedUpdateEvent.AddListener(Move);
        direction = Vector2.down;
    }


    // При столкновении с препятствием направление движения мяча будет сброшено
    public void ResetPosition() => StartCoroutine(ResetPositionCoroutine());
    public IEnumerator ResetPositionCoroutine()
    {
        Manager.Get.GameController.fixedUpdateEvent.RemoveListener(Move);

        player.transform.position = new Vector3(0, 2, player.transform.position.z - 1);
        direction = Vector2.down;
        angle = 0;
        player.Rb.Sleep();
        yield return new WaitForSeconds(2);
        Manager.Get.GameController.fixedUpdateEvent.AddListener(Move);
    }


    //Движение и ввод направления движения
    private void Move()
    {
        // Управление направлением движения
        if (Input.GetMouseButton(0))
        {
            float horizontal = -Input.GetAxis("Horizontal");
            angle = Mathf.Clamp(angle + horizontal, -45, 45);
            direction = RotateVector(Vector2.down, angle);
        }

        // Визуализация направления движения
            Debug.DrawRay(player.transform.position, new Vector3(direction.x, 0, direction.y) * 10, Color.green);

        //Движение
            player.Rb.velocity = new Vector3(direction.x * speed * k, player.Rb.velocity.y, direction.y * speed * k);

    }

    // Повернуть вектор направления движения на угол rotateAngle
    private Vector2 RotateVector(Vector2 vector, float rotateAngle)
    {
        Vector2 rotated_vector;
        rotateAngle = rotateAngle * Mathf.Deg2Rad;
        rotated_vector.x = vector.x * Mathf.Cos(rotateAngle) - vector.y * Mathf.Sin(rotateAngle);
        rotated_vector.y = vector.x * Mathf.Sin(rotateAngle) + vector.y * Mathf.Cos(rotateAngle);
        return rotated_vector;
    }

    //Изменить скорость движения 
    public void UpdateSpeed(int score)
    {
        switch (score)
        {
            case 10:
                StartCoroutine(Change_k(1.5f));
                break;
            case 25:
                StartCoroutine(Change_k(2));
                break;
            case 50:
                StartCoroutine(Change_k(3));
                break;
            case 100:
                StartCoroutine(Change_k(4));
                break;

        }
    }

    // Плавноное увеличение скорости за время timeChange. По условию 2 секунды
    private IEnumerator Change_k(float new_k)
    {
        float time = 0;
        float timeChange =  2;

        float delta = (new_k - k) / timeChange;

        while (time < timeChange)
        {
            time += Time.deltaTime;
            
            k += delta * Time.deltaTime;

            yield return null;
        }


    }
}
