using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

/// <summary>
/// Менеджер для управления UI элементами, представляющими объекты с тегом "Draggable".
/// </summary>
public class DraggableUIManager : MonoBehaviour
{
    /// <summary>
    /// Префаб для отображаемого объекта в UI.
    /// </summary>
    public GameObject draggableItemPrefab;

    /// <summary>
    /// Контейнер для UI элементов.
    /// </summary>
    public Transform uiContainer;

    /// <summary>
    /// Список объектов, которые можно перетаскивать.
    /// </summary>
    private List<GameObject> draggableObjects = new List<GameObject>();

    /// <summary>
    /// Метод вызывается при старте игры.
    /// Находит все объекты с тегом "Draggable" и создает для них UI элементы.
    /// </summary>
    void Start()
    {
        // Находим все объекты с тегом "Draggable" и добавляем их в список
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Draggable");
        foreach (var obj in objects)
        {
            draggableObjects.Add(obj);
            CreateUIItem(obj);
        }
    }

    /// <summary>
    /// Создает UI элемент для заданного объекта.
    /// </summary>
    /// <param name="obj">Объект, для которого будет создан UI элемент.</param>
    void CreateUIItem(GameObject obj)
    {
        GameObject uiItem = Instantiate(draggableItemPrefab, uiContainer);

        // Получаем компонент TMP_Text
        TMP_Text itemNameText = uiItem.GetComponentInChildren<TMP_Text>();
        if (itemNameText == null)
        {
            Debug.LogError("Не удалось найти компонент TMP_Text в дочерних объектах uiItem!");
            return;
        }

        itemNameText.text = obj.name; // Устанавливаем имя объекта в текст

        Button button = uiItem.GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("Не удалось найти компонент Button в uiItem!");
            return;
        }

        button.onClick.AddListener(() => ToggleVisibility(obj)); // Добавляем обработчик нажатия
    }

    /// <summary>
    /// Переключает видимость заданного объекта.
    /// </summary>
    /// <param name="obj">Объект, видимость которого будет переключена.</param>
    void ToggleVisibility(GameObject obj)
    {
        // Переключаем видимость объекта
        obj.SetActive(!obj.activeSelf);
    }
}
