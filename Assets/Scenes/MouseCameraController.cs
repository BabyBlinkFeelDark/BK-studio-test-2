using UnityEngine;

/// <summary>
/// Класс для управления камерой с помощью мыши.
/// Позволяет вращать, перемещать и приближать/удалять камеру.
/// </summary>
public class MouseCameraController : MonoBehaviour
{
    /// <summary>
    /// Чувствительность мыши для вращения камеры.
    /// </summary>
    public float mouseSensitivity = 1000f;

    /// <summary>
    /// Скорость перемещения камеры.
    /// </summary>
    public float moveSpeed = 10f;

    /// <summary>
    /// Скорость приближения и удаления камеры.
    /// </summary>
    public float scrollSpeed = 5f;

    /// <summary>
    /// Угол поворота камеры по оси X.
    /// </summary>
    private float xRotation = 0f;

    /// <summary>
    /// Обновление состояния камеры каждый кадр.
    /// Обрабатывает вращение, перемещение и приближение/удаление камеры.
    /// </summary>
    void Update()
    {
        // Приближение и удаление с помощью колесика мыши
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            transform.position += transform.forward * scroll * scrollSpeed; // Изменение позиции камеры
        }

        // Поворот камеры при зажатой правой кнопкой мыши
        if (Input.GetMouseButton(1)) // Правая кнопка мыши
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // Движение мыши по оси X
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // Движение мыши по оси Y

            xRotation -= mouseY; // Изменение угла по оси X
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Ограничение угла по оси X

            // Применение вращения к камере
            transform.localRotation = Quaternion.Euler(xRotation, transform.localEulerAngles.y + mouseX, 0);
        }

        // Перемещение в пространстве при зажатой средней кнопкой мыши
        if (Input.GetMouseButton(2)) // Средняя кнопка мыши
        {
            float moveX = Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime; // Движение по оси X
            float moveZ = Input.GetAxis("Mouse Y") * moveSpeed * Time.deltaTime; // Движение по оси Y

            Vector3 move = transform.right * moveX + transform.up * moveZ; // Вычисление вектора перемещения
            transform.position += move; // Изменение позиции камеры
        }

        // Перемещение с помощью клавиш WASD
        float horizontal = Input.GetAxis("Horizontal"); // A/D или стрелки влево/вправо
        float vertical = Input.GetAxis("Vertical");     // W/S или стрелки вверх/вниз

        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical; // Вычисление направления перемещения
        transform.position += moveDirection * moveSpeed * Time.deltaTime; // Изменение позиции камеры
    }
}
