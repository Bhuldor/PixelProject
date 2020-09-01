using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour{

    [Header("Enemies Prefabs")]
    public GameObject elf;
        
    //DamageOnEnemyController
    private GameObject enemyAttacked;
    private int amountDamage;
    private bool isAttacking;

    void Start() {
    
    }

    public void DamageOnEnemy(GameObject enemyAttacked, int amountDamage){
        this.enemyAttacked = enemyAttacked;
        this.amountDamage = amountDamage;
    }

    public void PlayerAttacking(bool isAttacking){
        this.isAttacking = isAttacking;
    }

    public bool CheckPlayerAttacking(){
        return isAttacking;
    }
}
