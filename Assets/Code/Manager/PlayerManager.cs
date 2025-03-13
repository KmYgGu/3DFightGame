using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerStat playerStat;
    [SerializeField] GameObject PlayerControlOBJ;
    PlayerControler playerControler;

    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private PlayerDefence playerDefence;
    [SerializeField] private PlayerJump playerJump;

    private void Awake()
    {
        playerControler = PlayerControlOBJ.GetComponent<PlayerControler>();
    }

    public void PlayerSet()
    {
        playerStat.playerHp = 1000;
        playerJump.isFalling = false;
        PlayerControlOBJ.transform.position = new Vector3(-1.5f, 0, 0);
        PlayerControlOBJ.transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    public void PlayerAllMove(bool iscan)
    {
        if (iscan)
        {
            playerControler.PlayerControlerUpdate();
            playerAttack.PlayerAttackUpdate();
            playerDefence.PlayerDefenceUpdate();
        }
    }

    public void StartJumpCoroutine()
    {
        StartCoroutine(playerJump.FallJump());
    }
    public void STopJumpCoroutine()
    {
        StopCoroutine(playerJump.FallJump());
    }
}
