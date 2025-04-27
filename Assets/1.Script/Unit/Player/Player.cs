using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#region ENUM
/// <summary>
/// 플레이어가 획득할 수 있는 아이템 종류
/// </summary>
public enum GetItem
{
    None,
    Barrel,
    Chest,
    BigChest,
    ClearFire,
    Box
}
#endregion

/// <summary>
/// 플레이어 클래스 (이동, 공격, 아이템 획득 등 처리)
/// </summary>
public class Player : Unit
{
    #region Singleton
    public static Player Instance;

    private void Singleton()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    #region Player State & Data
    [SerializeField] public GetItem getItem;
    public State state = new();
    private PlayerInputHandler inputHandler = new();
    #endregion

    #region Player Settings
    [Header("플레이어 설정")]
    public float resetSpeed;
    public float roSpeed;
    public float attackTime = 0.7f;
    public float oxygenTime;
    public float maxOxygenTime;
    #endregion

    #region Movement
    [Header("이동 관련")]
    public Vector3 moveDirection;
    public Vector3 movePos;
    #endregion

    #region Components
    [Header("컴포넌트")]
    public Animator ani;
    public Transform axis;
    public Transform GetBPos;
    public Transform PutBPos;
    public GameObject GetItemObject;
    public ParticleSystem particleSystem;
    public ParticleSystem.EmissionModule emissionModule;
    #endregion

    #region State Variables
    [Header("상태 변수")]
    public bool isGetB;
    public bool isFire;
    public bool isGhost;
    public bool isGameEnd;
    public bool isStart;
    public bool isDeath;
    public bool isMove;
    #endregion

    #region UI & Effects
    [Header("UI & 이펙트")]
    public GameObject GameOver;
    public GameObject playerBody;
    public Transform hitEffectPos;
    public GameObject HitEffect;
    public GameObject HpUI;
    public List<Monster> monsters = new();
    #endregion

    #region Unity Life Cycle
    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        emissionModule = particleSystem.emission;
        LoadData();
        resetSpeed = speed;
    }

    private void Update()
    {
        if (isGameEnd || !isStart || isDeath || !isMove) return;

        PlayerInput();
        Rotate(moveDirection, roSpeed, axis);
        RunEffect();
        Oxygen();
        CheckState();
    }

    private void FixedUpdate()
    {
        if (isGameEnd || !isStart || isDeath || !isMove) return;
        Move(MovePosition());
    }
    #endregion

    #region Input Handling
    private void PlayerInput()
    {
        MoveInput();
        PlayerGetInput();
        AttackInput();
        UseItemInput();
    }

    private void MoveInput()
    {
        Vector2 axisInput = inputHandler.MoveAxis;
        moveDirection = new Vector3(axisInput.y, 0, -axisInput.x).normalized;
    }

    private void PlayerGetInput()
    {
        if (inputHandler.IsInteractPressed)
            ItemGet();
    }

    private void AttackInput()
    {
        if (inputHandler.IsAttackPressed)
            Attack();
    }

    private void UseItemInput()
    {
        if (inputHandler.IsUseItemPressed)
            UseItem();
    }

    private static void UseItem()
    {
        if (BagManger.Instance.itemIndex.Count == 0)
            return;

        ItemEffectManager.Instance.CheckItemEffect(BagManger.Instance.itemIndex[0]);
        BagManger.Instance.UseItem();
    }
    #endregion

    #region Movement & Rotation
    private Vector3 MovePosition()
    {
        movePos = transform.position + (moveDirection * speed);
        return movePos;
    }

    protected override void Move(Vector3 movePos)
    {
        base.Move(movePos);
        ani.SetBool("isWalk", moveDirection != Vector3.zero);
    }

    protected override void Rotate(Vector3 direction, float speed, Transform axisTransform)
    {
        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        axisTransform.rotation = Quaternion.RotateTowards(axisTransform.rotation, targetRotation, speed * Time.deltaTime);
    }
    #endregion

    #region Attack
    protected override void Attack()
    {
        ani.SetTrigger("Attack");
        GameManager.Instance.attackAudio.Play();

        if (monsters.Count != 0)
        {
            foreach (var monster in monsters.ToList())
                monster.Dead();

            monsters.Clear();
        }

        if (getItem == GetItem.Box)
        {
            if (GetItemObject.TryGetComponent(out Animator anim))
                anim.SetTrigger("Hit");

            if (GetItemObject.TryGetComponent(out ClearObject clear))
                clear.isClear = true;
        }
    }
    #endregion

    #region State Check
    private void CheckState()
    {
        CheckDead();
        HpUI.SetActive(state.Hp <= 20 || state.Oxygen <= 20);
    }

    private void CheckDead()
    {
        if ((state.Hp <= 0 || state.Oxygen <= 0) && !isDeath)
            Dead();
    }

    private void Dead()
    {
        ani.SetTrigger("Dead");
        isDeath = true;
        GameOver.SetActive(true);
    }
    #endregion

    #region Oxygen & Bag
    private void RunEffect()
    {
        float effectPos = moveDirection.magnitude;
        float rate = effectPos * 20;
        emissionModule.rateOverTime = Mathf.Clamp(rate, 0, 100);
    }

    private void Oxygen()
    {
        oxygenTime += Time.deltaTime;
        if (oxygenTime >= maxOxygenTime)
        {
            state.Oxygen -= GameManager.Instance.oxygen;
            oxygenTime = 0;
        }
    }

    public void CheckBag() => speed = (state.Bag >= state.MaxBag) ? 0.001f : resetSpeed;
    #endregion

    #region Item Get
    private void ItemGet()
    {
        switch (getItem)
        {
            case GetItem.None: break;
            case GetItem.Barrel: GetBarrel(); break;
            case GetItem.Chest: ChestBox(); GameManager.Instance.boxOpen.Play(); break;
            case GetItem.BigChest:BigChestBox(); GameManager.Instance.boxOpen.Play(); break;
            default: break;
        }
    }

    private void GetBarrel()
    {
        if (!isGetB)
        {
            GetItemObject.transform.SetParent(axis);
            GetItemObject.transform.position = GetBPos.position;
            isGetB = true;

            if (GetItemObject.TryGetComponent(out Fire fire) && fire.isfire)
                isFire = true;
        }
        else
        {
            GetItemObject.transform.SetParent(null);
            GetItemObject.transform.position = PutBPos.position;
            isGetB = false;
            isFire = false;
        }
    }

    private void ChestBox()
    {
        if (GetItemObject.TryGetComponent(out ChestBox chest) && chest.isOpen)
            return;

        OpenChestBoxs(GetItemObject);
        chest.isOpen = true;
    }

    private void BigChestBox()
    {
        if (GetItemObject.TryGetComponent(out BigChest chest) && chest.isOpen)
            return;

        OpenChestBoxs(GetItemObject);
        chest.isOpen = true;
    }

    private void OpenChestBoxs(GameObject chest)
    {
        if (chest.TryGetComponent(out Animator anim))
            anim.SetTrigger("Open");
    }
    #endregion

    #region Damage
    public void Hit()
    {
        StopCoroutine(PlayerHit());
        state.Hp -= 20;
        GameManager.Instance.hitAudio.Play();
        StartCoroutine(PlayerHit());
    }

    private IEnumerator PlayerHit()
    {
        playerBody.SetActive(false);
        Instantiate(HitEffect, hitEffectPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        playerBody.SetActive(true);
    }
    #endregion

    #region Collision Handling
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "FireObject": HandleFireObjectCollision(); break;
            case "ClearFire": HandleClearFireCollision(collision.gameObject); break;
            case "Coin": HandleCoinCollision(collision.gameObject); break;
            case "EndCoin": HandleEndCoinCollision(collision.gameObject); break;
            case "Puzzle": StartPuzzleStart.Instance.StartAnimator(); break;
        }
    }

    private void HandleFireObjectCollision()
    {
        if (!isGetB) return;
        if (GetItemObject.TryGetComponent(out Fire fire) && fire.isfire) return;

        fire.fire.SetActive(true);
        fire.isfire = true;
        isFire = true;
    }

    private void HandleClearFireCollision(GameObject collisionObject)
    {
        if (!isFire) return;

        if (collisionObject.TryGetComponent(out Fire clearFire))
            clearFire.fire.SetActive(true);

        if (collisionObject.TryGetComponent(out ClearObject clearObj))
            clearObj.isClear = true;
    }

    private void HandleCoinCollision(GameObject coin)
    {
        state.Bag += 100;
        CheckBag();
        Destroy(coin);
    }

    private void HandleEndCoinCollision(GameObject coin)
    {
        StageClearManager.Instance.GameEnd();
        Destroy(coin);
    }
    #endregion

    #region Save & Load
    private void LoadData()
    {
        if (PlayGameManger.Instance == null)
            return;

        state.MaxBag = PlayGameManger.Instance.bag;
        BagManger.Instance.BagValue = (int)PlayGameManger.Instance.bagValue;
        state.MaxOxygen = PlayGameManger.Instance.oxygen;
        state.Oxygen = state.MaxOxygen;
    }
    #endregion
}
