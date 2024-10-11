using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Класс для управления контекстным меню в игре.
/// Позволяет открывать меню по правому клику на объектах с тегом "Draggable",
/// а также изменять цвет, прозрачность и скрывать объекты.
/// </summary>
public class ContextMenu : MonoBehaviour
{
    /// <summary>
    /// Ссылка на панель меню, которая будет отображаться при правом клике.
    /// </summary>
    public GameObject menu; // Ссылка на панель меню

    /// <summary>
    /// Ссылка на выбранный объект, который будет изменяться.
    /// </summary>
    public DraggableObject selectedObject; // Выбранный объект

    /// <summary>
    /// Инициализация. Скрывает меню по умолчанию.
    /// </summary>
    void Start()
    {
        menu.SetActive(false); // Скрываем меню по умолчанию
    }

    /// <summary>
    /// Обновление состояния каждый кадр.
    /// Проверяет нажатия мыши для открытия или закрытия контекстного меню.
    /// </summary>
    void Update()
    {
        // Проверка правого клика для открытия меню
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Draggable")) // Убедитесь, что у ваших объектов есть этот тег
                {
                    selectedObject = hit.transform.GetComponent<DraggableObject>();
                    ShowMenu(hit.point);
                }
            }
        }
        
        // Проверка левого клика для скрытия меню, если не над меню
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUI())
            {
                HideMenu();
            }
        }
    }

    /// <summary>
    /// Отображает контекстное меню в заданной позиции.
    /// </summary>
    /// <param name="position">Позиция, в которой будет отображаться меню.</param>
    void ShowMenu(Vector3 position)
    {
        // Устанавливаем позицию меню
        menu.transform.position = position + new Vector3(100, 150, 0); // Сдвигаем меню немного вверх
        menu.SetActive(true);
    }

     /// <summary>
    /// Скрывает контекстное меню и очищает выбранный объект.
    /// </summary>
    void HideMenu()
    {
        menu.SetActive(false);
        selectedObject = null;
    }

    /// <summary>
    /// Изменяет цвет выбранного объекта на случайный цвет.
    /// Если объект не выбран, ничего не происходит.
    /// </summary>
    public void ChangeColor()
    {
        if (selectedObject != null)
        {
            Color newColor = new Color(Random.value, Random.value, Random.value); // Генерация случайного цвета
            selectedObject.GetComponent<Renderer>().material.color = newColor;
            HideMenu();
        }
    }

     /// <summary>
    /// Изменяет прозрачность выбранного объекта между полной непрозрачностью и 50%.
    /// Устанавливает необходимые параметры для прозрачного отображения материала.
    /// Если объект не выбран, выводит сообщение об ошибке.
    /// </summary>
public void ChangeTransparency()
{
    if (selectedObject != null)
    {
        Debug.Log("ChangeTransparency");
        
        Renderer renderer = selectedObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            Color color = renderer.material.color;
            color.a = color.a == 1 ? 0.5f : 1; // Переключение между полной непрозрачностью и 50%
            renderer.material.color = color; // Изменяем цвет с новым альфа-значением

            // Убедитесь, что материал настроен на прозрачность
            renderer.material.SetFloat("_Mode", 2); // Устанавливаем режим на Transparent
            renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            renderer.material.SetInt("_ZWrite", 0);
            renderer.material.DisableKeyword("_ALPHATEST_ON");
            renderer.material.EnableKeyword("_ALPHABLEND_ON");
            renderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            renderer.material.renderQueue = 3000; // Устанавливаем очередь рендеринга для прозрачных объектов

            HideMenu();
        }
        else
        {
            Debug.LogError("Renderer component not found on selected object.");
        }
    }
    else
    {
        Debug.LogError("Selected object is null.");
    }
}

    /// <summary>
    /// Скрывает выбранный объект.
    /// Если объект не выбран, ничего не происходит.
    /// </summary>
    public void HideObject()
    {
        if (selectedObject != null)
        {
            selectedObject.gameObject.SetActive(false);
            HideMenu();
        }
    }

    /// <summary>
    /// Проверяет, находится ли указатель мыши над элементами UI.
    /// Используется для предотвращения скрытия меню при клике на UI элементы.
    /// </summary>
    /// <returns>True, если указатель находится над UI элементом; иначе False.</returns>
    private bool IsPointerOverUI()
    {
        // Проверяем, находится ли указатель над UI элементами
        return EventSystem.current.IsPointerOverGameObject();
    }
}
