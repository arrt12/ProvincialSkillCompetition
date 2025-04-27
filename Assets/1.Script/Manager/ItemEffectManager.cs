using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class: ItemEffectManager
/// <summary>
/// 아이템 효과(HP 회복, 산소 회복, 속도 증가, 유령 모드, 박스 위치 표시 등)를 관리하는 클래스
/// </summary>
public class ItemEffectManager : Singleton<ItemEffectManager>
{
    #region Serialized Fields
    [Header("Big Chest References")]
    public List<BigChest> bigChests = new();       // BigChest 위치 목록

    [Header("Box Position Indicator")]
    public GameObject boxPosObject;                // 박스 위치 표시 오브젝트

    [Header("Ghost Mode Settings")]
    public Material ghostMaterial;                 // 유령 모드 머티리얼
    public SkinnedMeshRenderer skinnedMeshRenderer;// 교체할 렌더러
    #endregion

    #region Unity Callbacks
    /// <summary>
    /// 디버그 치트 키 입력 처리 (LeftShift: Speed1, R: Speed2).
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CheckItemEffect(3);
            Debug.Log("치트 사용: Speed1");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            CheckItemEffect(4);
            Debug.Log("치트 사용: Speed2");
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// BigChest 인스턴스를 등록
    /// </summary>
    public void SetBigChests(BigChest bigChest)
    {
        bigChests.Add(bigChest);
    }

    /// <summary>
    /// 인덱스에 해당하는 아이템 효과를 실행
    /// </summary>
    public void CheckItemEffect(int index)
    {
        switch (index)
        {
            case 0: Hp(); break;
            case 1: Oxygen(); break;
            case 2: BoxPos(); break;
            case 3: Speed1(); break;
            case 4: Speed2(); break;
            case 5: NoCheckMonster(); break;
        }
    }

    public void Hp()
    {
        Player.Instance.state.Hp = Player.Instance.state.MaxHp;
    }

    public void Oxygen()
    {
        Player.Instance.state.Oxygen = Player.Instance.state.MaxOxygen;
    }

    public void BoxPos()
    {
        StartCoroutine(ItemBoxPosCoroutine(10f));
    }

    public void Speed1()
    {
        StartCoroutine(SpeedUpCoroutine(0.005f, 2f));
    }

    public void Speed2()
    {
        StartCoroutine(SpeedUpCoroutine(0.007f, 5f));
    }

    public void NoCheckMonster()
    {
        StartCoroutine(GhostCoroutine(5f));
    }
    #endregion

    #region Coroutines
    private IEnumerator SpeedUpCoroutine(float speedValue, float duration)
    {
        Debug.Log("SpeedUp 시작");
        float originalSpeed = Player.Instance.speed;
        Player.Instance.speed = speedValue;
        Player.Instance.resetSpeed = speedValue;

        yield return new WaitForSeconds(duration);

        Player.Instance.speed = originalSpeed;
        Player.Instance.resetSpeed = originalSpeed;
    }

    private IEnumerator ItemBoxPosCoroutine(float duration)
    {
        boxPosObject.SetActive(true);
        SetRandomBoxPosition();

        yield return new WaitForSeconds(duration);

        boxPosObject.SetActive(false);
    }

    private IEnumerator GhostCoroutine(float duration)
    {
        Material originalMaterial = skinnedMeshRenderer.material;
        skinnedMeshRenderer.material = ghostMaterial;
        Player.Instance.isGhost = true;

        yield return new WaitForSeconds(duration);

        skinnedMeshRenderer.material = originalMaterial;
        Player.Instance.isGhost = false;
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// 등록된 BigChest 중 랜덤하게 하나를 선택해 LookBox.pos에 설정
    /// </summary>
    private void SetRandomBoxPosition()
    {
        if (bigChests.Count == 0) return;

        int index = Random.Range(0, bigChests.Count);
        LookBox.Instance.pos = bigChests[index].transform;
    }
    #endregion
}
#endregion
