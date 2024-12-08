using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HeathController : MonoBehaviour
{
    public int MaxHeath = 100;
    public float CurrentHeath;
    public TextMeshProUGUI valueText;
    public Color ColorShield;
    public Color ColorNormal;
    public float safetime;
    float safetimecooldown;
    public SpriteRenderer PlayerSpriteRenderer;

    [SerializeField] private Image Heathbarfill;
    [SerializeField] private float DamageAmount, HeathAmount;
    [SerializeField] private PlayerRespawn playerrespawn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("spike"))
        {
            TakeDamge(DamageAmount);
        }
        else if (collision.CompareTag("Heath"))
        {
            Heal(HeathAmount);
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Deadground"))
        {
            TakeDamge(6);
        }
        else if (collision.CompareTag("chainsaw"))
        {
            TakeDamge(5);
        }else if (collision.CompareTag("fire"))
        {
            TakeDamge(4);
        }
    }
    private void Start()
    {
        CurrentHeath = MaxHeath;
        UpdateHeathBar();
    }
    public void TakeDamge(float damage)
    {
        if (safetimecooldown <= 0)
        {
            CurrentHeath -= damage;
            //CurrentHeath = Mathf.Clamp(CurrentHeath, 0, MaxHeath);
            if (CurrentHeath <= 0)
            {
                playerrespawn.Die();
                CurrentHeath = MaxHeath;
            }
            safetimecooldown = safetime;
            UpdateHeathBar();
            StartCoroutine(GivePlayerShield());
        }
    }
    public void UpdateHeathBar()
    {
        Heathbarfill.fillAmount = CurrentHeath / MaxHeath; // dieu khien fillamount trong component  
        valueText.text = CurrentHeath.ToString() + " / " + MaxHeath.ToString(); // chuyen doi tu chu sang dang so 
        if (CurrentHeath > MaxHeath)
        {
            CurrentHeath = MaxHeath;
            valueText.text = CurrentHeath.ToString() + " / " + MaxHeath.ToString();
            UpdateHeathBar();
        }
    }
    private void Update()
    {
        safetimecooldown -= Time.deltaTime;
    }
    IEnumerator GivePlayerShield()
    {
        PlayerSpriteRenderer.color = ColorShield;
        yield return new WaitForSeconds(1f); // trong vong 1 giay player khonbg nhan sat thuong  
        safetime = 1f;
        PlayerSpriteRenderer.color = ColorNormal;// trang thai binh thuong
    }
    public void Heal(float amount)
    {
        CurrentHeath += amount;
        //CurrentHeath = Mathf.Clamp(CurrentHeath, 0, MaxHeath);
        UpdateHeathBar();
    }
}
