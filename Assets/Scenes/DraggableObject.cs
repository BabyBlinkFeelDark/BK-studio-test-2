using UnityEngine;

/// <summary>
/// Класс для управления перетаскиванием объекта в сцене.
/// Объект можно перетаскивать мышью, сохраняя его высоту.
/// </summary>
public class DraggableObject : MonoBehaviour
{
    /// <summary>
    /// Ссылка на основную камеру в сцене.
    /// </summary>
    private Camera mainCamera;

    /// <summary>
    /// Флаг, указывающий, перетаскивается ли объект в данный момент.
    /// </summary>
    private bool isDragging = false;

    /// <summary>
    /// Смещение между объектом и курсором мыши при начале перетаскивания.
    /// </summary>
    private Vector3 offset;

    /// <summary>
    /// Инициализация. Получает основную камеру при старте.
    /// </summary>
    void Start()
    {
        mainCamera = Camera.main; // Получаем основную камеру
    }

    /// <summary>
    /// Вызывается при нажатии кнопки мыши на объекте.
    /// Начинает процесс перетаскивания и вычисляет смещение.
    /// </summary>
    void OnMouseDown()
    {
        // Начинаем перетаскивание
        isDragging = true;

        // Расчет смещения между объектом и курсором
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z; // Получаем расстояние до объекта
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        offset = transform.position - worldPosition;
    }

    /// <summary>
    /// Вызывается при отпускании кнопки мыши.
    /// Заканчивает процесс перетаскивания.
    /// </summary>
    void OnMouseUp()
    {
        // Заканчиваем перетаскивание
        isDragging = false;
    }

    /// <summary>
    /// Обновление состояния каждый кадр.
    /// Если объект перетаскивается, обновляет его позицию в соответствии с курсором мыши.
    /// </summary>
    void Update()
    {
        if (isDragging)
        {
            // Перемещение объекта по сцене
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z; // Получаем расстояние до объекта
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition) + offset;
            transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z); // Сохраняем высоту объекта
        }
    }
}
