using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Main { get; private set; }

    public DuctTape DuctTape { get; private set; }
    public Rigidbody UseRigidbody { get; private set; }
    public PlayerController Controller { get; private set; }
    public PlayerModel Model { get; private set; }
    public PlayerMotor Motor { get; private set; }
    public PlayerModule[] Modules { get; private set; }


    public T GetModule<T>() where T : PlayerModule
    {
        return (T)Modules.First(x => x is T);
    }

    private void InitSingleton()
    {
        Main = this;
    }
    private void InitMods()
    {
        UseRigidbody = GetComponentInChildren<Rigidbody>();
        Modules = GetComponentsInChildren<PlayerModule>();
        Controller = GetModule<PlayerController>();
        Model = GetModule<PlayerModel>();
        Motor = GetModule<PlayerMotor>();
        foreach (PlayerModule m in Modules)
        {
            m.Init(this);
        }
    }

    #region Unity Callbacks
    private void Awake()
    {
        InitSingleton();
        InitMods();
        DuctTape = GetComponentInChildren<DuctTape>();
    }
    private void Update()
    {
        foreach(PlayerModule m in Modules)
        {
            if (m.Initialized)
            {
                m.OnUpdate();
            }
        }
    }
    private void FixedUpdate()
    {
        foreach (PlayerModule m in Modules)
        {
            if (m.Initialized)
            {
                m.OnFixedUpdate();
            }
        }
    }
    private void LateUpdate()
    {
        foreach (PlayerModule m in Modules)
        {
            if (m.Initialized)
            {
                m.OnLateUpdate();
            }
        }
    }
    #endregion
}
