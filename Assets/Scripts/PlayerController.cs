using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Player player;
    private float k; // ����������� ���������� �������� (������� �� ��������� �� ������ ���������)

    private float angle;
    private Vector2 direction;
    public void Enable()
    {
        player = Manager.Get.Player;
        k = 1;
        Manager.Get.GameController.fixedUpdateEvent.AddListener(Move);
        direction = Vector2.down;
    }


    // ��� ������������ � ������������ ����������� �������� ���� ����� ��������
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


    //�������� � ���� ����������� ��������
    private void Move()
    {
        // ���������� ������������ ��������
        if (Input.GetMouseButton(0))
        {
            float horizontal = -Input.GetAxis("Horizontal");
            angle = Mathf.Clamp(angle + horizontal, -45, 45);
            direction = RotateVector(Vector2.down, angle);
        }

        // ������������ ����������� ��������
            Debug.DrawRay(player.transform.position, new Vector3(direction.x, 0, direction.y) * 10, Color.green);

        //��������
            player.Rb.velocity = new Vector3(direction.x * speed * k, player.Rb.velocity.y, direction.y * speed * k);

    }

    // ��������� ������ ����������� �������� �� ���� rotateAngle
    private Vector2 RotateVector(Vector2 vector, float rotateAngle)
    {
        Vector2 rotated_vector;
        rotateAngle = rotateAngle * Mathf.Deg2Rad;
        rotated_vector.x = vector.x * Mathf.Cos(rotateAngle) - vector.y * Mathf.Sin(rotateAngle);
        rotated_vector.y = vector.x * Mathf.Sin(rotateAngle) + vector.y * Mathf.Cos(rotateAngle);
        return rotated_vector;
    }

    //�������� �������� �������� 
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

    // ��������� ���������� �������� �� ����� timeChange. �� ������� 2 �������
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
