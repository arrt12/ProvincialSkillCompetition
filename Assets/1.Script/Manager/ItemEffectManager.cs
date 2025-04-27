using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class: ItemEffectManager
/// <summary>
/// ������ ȿ��(HP ȸ��, ��� ȸ��, �ӵ� ����, ���� ���, �ڽ� ��ġ ǥ�� ��)�� �����ϴ� Ŭ����
/// </summary>
public class ItemEffectManager : Singleton<ItemEffectManager>
{
    #region Serialized Fields
    [Header("Big Chest References")]
    public List<BigChest> bigChests = new();       // BigChest ��ġ ���

    [Header("Box Position Indicator")]
    public GameObject boxPosObject;                // �ڽ� ��ġ ǥ�� ������Ʈ

    [Header("Ghost Mode Settings")]
    public Material ghostMaterial;                 // ���� ��� ��Ƽ����
    public SkinnedMeshRenderer skinnedMeshRenderer;// ��ü�� ������
    #endregion

    #region Unity Callbacks
    /// <summary>
    /// ����� ġƮ Ű �Է� ó�� (LeftShift: Speed1, R: Speed2).
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CheckItemEffect(3);
            Debug.Log("ġƮ ���: Speed1");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            CheckItemEffect(4);
            Debug.Log("ġƮ ���: Speed2");
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// BigChest �ν��Ͻ��� ���
    /// </summary>
    public void SetBigChests(BigChest bigChest)
    {
        bigChests.Add(bigChest);
    }

    /// <summary>
    /// �ε����� �ش��ϴ� ������ ȿ���� ����
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
        Debug.Log("SpeedUp ����");
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
    /// ��ϵ� BigChest �� �����ϰ� �ϳ��� ������ LookBox.pos�� ����
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
