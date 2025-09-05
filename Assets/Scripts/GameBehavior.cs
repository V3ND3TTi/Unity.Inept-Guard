using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CustomExtensions;
using System.Linq;
using System;

public class GameBehavior : MonoBehaviour, IManager
{
    public int MaxItems = 4;
    public Button WinButton;
    public Button LossButton;

    public TMP_Text HealthText;
    public TMP_Text ItemText;
    public TMP_Text ProgressText;

    public Stack<Loot> LootStack = new Stack<Loot>();

    public delegate void DebugDelegate(string newText);
    public DebugDelegate debug = Print;

    private string _state;
    public string State
    {
        get => _state;
        set => _state = value;
    }

    private int _itemsCollected = 0;
    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            ItemText.text = $"Items: {Items}";

            if (_itemsCollected >= MaxItems)
            {
                WinButton.gameObject.SetActive(true);
                UpdateScene("You've found all the items!");
            }
            else
            {
                ProgressText.text = $"Item found, only {MaxItems - _itemsCollected} more!";
            }
        }
    }

    private int _playerHP = 3;
    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            HealthText.text = $"Health: {HP}";

            if (_playerHP <= 0)
            {
                LossButton.gameObject.SetActive(true);
                UpdateScene("You want another life with that?");
            }
            else
            {
                ProgressText.text = "Ouch... that's gotta hurt.";
            }
        }
    }

    void Start()
    {
        ItemText.text += _itemsCollected;
        HealthText.text += _playerHP;

        Initialize();
    }

    void OnEnable()
    {
        PlayerBehavior.playerJump += HandlePlayerJump;
        debug("Jump event subscribed...");
    }

    void OnDisable()
    {
        PlayerBehavior.playerJump -= HandlePlayerJump;
        debug("Jump event unsubscribed...");
    }

    public void UpdateScene(string updatedText)
    {
        ProgressText.text = updatedText;
        Time.timeScale = 0f;
    }

    public void RestartScene()
    {
        try
        {
            Utilties.RestartLevel(0);
            debug("Level successfully restarted...");
        }
        catch (ArgumentException e)
        {
            Utilties.RestartLevel(0);
            debug($"Reverting to scene 0: {e.ToString()}");
        }
        finally
        {
            debug("Level restart has completed...");
        }
    }

    public void Initialize()
    {
        _state = "Game Manager initialized...";
        //_state.FancyDebug();
        debug(_state);

        LogWithDelegate(debug);

        var itemShop = new Shop<Collectable>();
        itemShop.AddItem(new Potion());
        itemShop.AddItem(new Antidote());
        Debug.Log($"Items for sale: {itemShop.GetStockCount<Collectable>()}");

        LootStack.Push(new Loot("Sword of Doom", 5));
        LootStack.Push(new Loot("HP Boost", 1));
        LootStack.Push(new Loot("Golden Key", 3));
        LootStack.Push(new Loot("Pair of Winged Boots", 2));
        LootStack.Push(new Loot("Mythril Bracer", 4));

        //FilterLoot();
    }

    public void PrintLootReport()
    {
        var currentItem = LootStack.Pop();
        var nextItem = LootStack.Peek();
        Debug.LogFormat($"You got a {currentItem.Name}! You've got a good chance of find a {nextItem.Name} next!");
        Debug.LogFormat($"There are {LootStack.Count} random loot items waiting for you!");
    }

    public void FilterLoot()
    {
        var rareLoot = from item in LootStack
                       where item.Rarity >= 3
                       orderby item.Rarity descending
                       select item.Name;

        foreach (var item in rareLoot)
        {
            Debug.LogFormat($"Rare item: {item}!");
        }
    }

    public static void Print(string newText)
    {
        Debug.Log(newText);
    }

    public void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task...");
    }

    public void HandlePlayerJump()
    {
        debug("Player has jumped...");
    }
}
