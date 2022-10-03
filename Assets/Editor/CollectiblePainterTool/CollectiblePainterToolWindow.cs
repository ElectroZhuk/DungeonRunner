using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class CollectiblePainterToolWindow : EditorWindow
{
    private static bool _isWindowOpened = false;
    private static EditorWindow _currentWindow;
    private static List<Collectible> _collectibles = new List<Collectible>();
    private static Collectible _selectedCollectible;
    private static float _distanceFromGround;
    private static FloatField _floatField;

    public static UnityAction<Collectible> CollectibleSelected;

    public static void ShowWindow()
    {
        var allCollectiblesGuids = AssetDatabase.FindAssets("t:Prefab");

        foreach (var guid in allCollectiblesGuids)
        {
            Collectible collectible = AssetDatabase.LoadAssetAtPath<Collectible>(AssetDatabase.GUIDToAssetPath(guid));

            if (collectible == null)
                continue;

            _collectibles.Add(AssetDatabase.LoadAssetAtPath<Collectible>(AssetDatabase.GUIDToAssetPath(guid)));
        }

        if (_selectedCollectible == null)
            OnCollectibleSelected(_collectibles[0]);
            
        _currentWindow = GetWindow<CollectiblePainterToolWindow>();
        _isWindowOpened = true;
        _currentWindow.titleContent = new GUIContent("Collectible Painter Tool");
        _currentWindow.position = new Rect(1450f, 100f, 300f, 300f);
    }

    public void CreateGUI()
    {
        List<string> collectiblesNames = new List<string>();

        foreach (var collectible in _collectibles)
        {
            collectiblesNames.Add(collectible.name);
        }

        Func<VisualElement> makeItem = () => new Label();
        Action<VisualElement, int> bindItem = (e, i) => (e as Label).text = collectiblesNames[i];
        var collectiblesListView = new ListView(collectiblesNames, 20, makeItem, bindItem);
        collectiblesListView.style.flexGrow = 1.0f;
        collectiblesListView.onSelectionChange += objects => OnCollectibleSelected(_collectibles[collectiblesListView.selectedIndex]);

        var distanceBox = new Box();

        var textToDistanceField = new Label("Distance up from ground:");
        textToDistanceField.style.flexGrow = 1.0f;
        textToDistanceField.style.alignSelf = Align.FlexStart;
        distanceBox.Add(textToDistanceField);

        var distanceField = new FloatField();
        distanceField.style.flexGrow = 1.0f;
        distanceField.style.alignSelf = Align.FlexEnd;
        distanceField.SetValueWithoutNotify(_distanceFromGround);
        _floatField = distanceField;
        distanceBox.Add(distanceField);

        rootVisualElement.Add(distanceBox);

        var textCollectiblesBox = new Label("Select collectible:");
        textCollectiblesBox.style.marginTop = 10f;

        collectiblesListView.style.marginTop = 5f;
        collectiblesListView.selectedIndex = _collectibles.IndexOf(_selectedCollectible);

        rootVisualElement.Add(textCollectiblesBox);
        rootVisualElement.Add(collectiblesListView);
    }

    public static void CloseWindow()
    {
        _collectibles.Clear();

        if (_isWindowOpened)
            _currentWindow.Close();
    }

    public static float GetDistanceFromGround()
    {
        _distanceFromGround = _floatField.value;
        return _distanceFromGround;
    }

    private static void OnCollectibleSelected(Collectible collectible)
    {
        _selectedCollectible = collectible;
        CollectibleSelected?.Invoke(collectible);
    }
}
