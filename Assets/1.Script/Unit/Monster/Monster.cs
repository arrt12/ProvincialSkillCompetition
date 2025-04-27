using UnityEngine;

/// <summary>
/// ���� �⺻ �ൿ (�̵�, ����, ��� ó��)
/// </summary>
public class Monster : Unit
{
    #region Variables
    // ���� ���� ��
    public Animator ani;
    public float attackDamage;
    public float attackR;
    public float CheckR;
    private float attackTime;
    public float maxAttackTime;

    // �÷��̾� ���� �� ���� ����
    public Collider[] attackRang;
    public Collider[] playerCheckRang;
    public LayerMask playerLayer;
    public bool isCheck;
    public bool isAttack;

    // ��Ÿ
    public GameObject DeadEffect;
    #endregion

    #region Unity Methods

    private void Update()
    {
        if (Player.Instance == null || Player.Instance.isDeath || Player.Instance.isGhost)
            return;

        MonsterLogic();
    }

    private void FixedUpdate()
    {
        if (!isCheck && !isAttack)
            return;
        Move(MovePosition());
    }

    private void OnDrawGizmos()
    {
        // ���� / Ž�� ���� �׸���
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackR);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, CheckR);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GetPut"))
            Player.Instance.monsters.Add(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GetPut"))
            Player.Instance.monsters.Remove(this);
    }

    #endregion

    #region Monster Logic

    /// <summary>
    /// ������ �⺻ ������ ����
    /// </summary>
    private void MonsterLogic()
    {
        CheckCollider();
        LookAtPlayer();
        Attack();
    }

    /// <summary>
    /// �÷��̾� Ž�� �� ���� ���� Ȯ��
    /// </summary>
    private void CheckCollider()
    {
        playerCheckRang = Physics.OverlapSphere(transform.position, CheckR, playerLayer);
        attackRang = Physics.OverlapSphere(transform.position, attackR, playerLayer);

        isCheck = (playerCheckRang.Length != 0 && !isAttack) && !Player.Instance.isGhost;
        isAttack = (attackRang.Length != 0 && !Player.Instance.isGhost);

        ani.SetBool("isWalk", isCheck);
    }

    /// <summary>
    /// �̵��� ���� ���
    /// </summary>
    private Vector3 MovePosition()
    {
        Vector3 playerTargetPos = Player.Instance.transform.position - transform.position;
        return transform.position + (playerTargetPos.normalized * speed);
    }

    /// <summary>
    /// �÷��̾ �ٶ󺸱�
    /// </summary>
    private void LookAtPlayer()
    {
        if (!isCheck && !isAttack)
            return;

        Vector3 dir = Player.Instance.transform.position - transform.position;
        dir.y = 0f;

        if (dir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }
    }

    #endregion

    #region Attack & Death

    /// <summary>
    /// ���� ���� ó��
    /// </summary>
    protected override void Attack()
    {
        if (!isAttack)
            return;

        attackTime += Time.deltaTime;

        if (attackTime >= maxAttackTime)
        {
            ani.SetTrigger("Attack");
            Player.Instance.Hit();
            attackTime = 0f;
        }
    }

    /// <summary>
    /// ���� ��� ó��
    /// </summary>
    public void Dead()
    {
        if (DeadEffect != null)
            Instantiate(DeadEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    #endregion
}
