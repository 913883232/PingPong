using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour,ICanTakeDamage {
    [SerializeField]
    private int InitialHealth = 1;
    [SerializeField]
    private Color finalColor;
    private SpriteRenderer rend;
    private BlockerControl block;
    private int currentHealth;

    private float damageFrequency = 0.5f;

    public int CurrentHealth { get; set; }
    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        block = GetComponentInParent<BlockerControl>();
    }
    private void Start()
    {
        currentHealth = InitialHealth;
    }
    private void Update()
    {
        damageFrequency += Time.deltaTime;
    }
    private IEnumerator DestroySelf()
    {
        GameManager.Instance.Score += 1;
        rend.enabled = false;
        yield return null;
        Destroy(this.gameObject);
        block.BlockAmount--;
        print(block.BlockAmount);
    }
    public void TakeDamage(int damage,GameObject instigator)
    {
        if (damageFrequency < 0.5f) return;
            currentHealth -= damage;
        damageFrequency = 0;
        rend.color = finalColor;
        if(currentHealth <= 0)
        {
            StartCoroutine(DestroySelf());
            return;
        }
    }
}
