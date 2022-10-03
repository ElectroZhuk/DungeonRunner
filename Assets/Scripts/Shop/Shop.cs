using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Item> _items;
    [SerializeField] private ItemPresenter _template;
    [SerializeField] private Transform _itemContainer;
    [SerializeField] private TMP_Text _moneyPresenter;

    private List<ItemPresenter> _itemPresenters = new List<ItemPresenter>();

    private void OnEnable()
    {
        foreach (ItemPresenter presenter in _itemPresenters)
            presenter.ButtonClicked += OnButtonClicked;

        UpdateMoneyAmount();
    }

    private void Start()
    {
        foreach (Item item in _items)
            AddItem(item);
    }

    private void OnDisable()
    {
        foreach (ItemPresenter presenter in _itemPresenters)
            presenter.ButtonClicked -= OnButtonClicked;
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void AddItem(Item item)
    {
        ItemPresenter presenter = Instantiate(_template, _itemContainer);
        presenter.Init(item);
        presenter.ButtonClicked += OnButtonClicked;
        _itemPresenters.Add(presenter);
    }

    private void OnButtonClicked(Item item, ItemPresenter presenter)
    {
        if (CanBuy(item))
        {
            Buy(item);
            UpdateSavedInfo();
        }
        else if (item is AvatarItem avatar)
        {
            if (avatar.IsBought() && avatar.IsSelected() == false)
            {
                avatar.Select();
                UpdateSavedInfo();
            }
        }
    }

    private void Buy(Item item)
    {
        item.Buy();
        int changedMoneyAmount = PlayerPrefs.GetInt(PlayerPrefsNames.Keys.Money) - item.Cost;
        PlayerPrefs.SetInt(PlayerPrefsNames.Keys.Money, changedMoneyAmount);
        PlayerPrefs.Save();
        UpdateMoneyAmount();
    }

    private void UpdateSavedInfo()
    {
        foreach (ItemPresenter presenter in _itemPresenters)
            presenter.UpdateSavedInfo();
    }

    private bool CanBuy(Item item)
    {
        if (PlayerPrefs.HasKey(PlayerPrefsNames.Keys.Money) == false)
        {
            PlayerPrefs.SetInt(PlayerPrefsNames.Keys.Money, 0);
            PlayerPrefs.Save();
        }

        return (item.IsBought() == false) && (item.Cost <= PlayerPrefs.GetInt(PlayerPrefsNames.Keys.Money));
    }

    private void UpdateMoneyAmount()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsNames.Keys.Money) == false)
        {
            PlayerPrefs.SetInt(PlayerPrefsNames.Keys.Money, 0);
            PlayerPrefs.Save();
        }

        _moneyPresenter.text = PlayerPrefs.GetInt(PlayerPrefsNames.Keys.Money).ToString();
    }
}
