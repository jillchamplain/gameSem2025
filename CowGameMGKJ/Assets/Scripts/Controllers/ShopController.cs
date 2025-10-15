using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : LogicController
{
    public static ShopController inst;
    [Header("Refs")]
    [SerializeField] MouseManager playerMouse;
    [SerializeField] CoinManager coinManager;
    [SerializeField] UIManager uiManager;
    // Start is called before the first frame update
    private void Awake()
    {
        setGameState(GameState.SHOP);
    }
    void Start()
    {
        
    }

    private void OnEnable()
    {
        UIEventController.addCoin += OnAddCoins;
        UIEventController.takeCoin += OnPurchase;
    }

    private void OnDisable()
    {
        UIEventController.addCoin -= OnAddCoins;
        UIEventController.takeCoin -= OnPurchase;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Reset()
    {
        if (getListening())
            return;
    }

    public override void Init()
    {
        
    }

    void OnPurchase(int cost)
    {
        coinManager.takeCoins(cost);
    }

    void OnAddCoins(int amount)
    {
        coinManager.addCoins(amount);
    }
}
