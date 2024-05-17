using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    [Header("Spells and Combat parameters")]
    [SerializeField] KeyCode spellUpButton;
    [SerializeField] KeyCode spellDownButton;
    [SerializeField] KeyCode mageInput;
    [SerializeField] KeyCode attackInput;
    [SerializeField] int currentSpell;
    [SerializeField] float bonusFireRate = 0;
    [SerializeField] private List<Spells> inventary = new List<Spells>();
    [SerializeField] private Vector3 attackDirection;
    [SerializeField] private GameObject attackOrigin;
    [SerializeField] private int bonusDamage;
    
    [Header("Life Related")]
    [SerializeField] private int maxLife = 5;
    [SerializeField] List<Image> lifeUi;
    [SerializeField] Sprite lifeSprite;
    [SerializeField] Sprite lifelessSprite;
    [SerializeField] float life;

    [Header("Mana Related")]
    [SerializeField] bool regenaratingMana;
    [SerializeField] float mana;
    [SerializeField] float manaRegen; //how much mana regen by second
    [SerializeField] float timeToRegenMana; //time without casting any spell to regen mana
    [SerializeField] float timeSinceLastSpell;
    [SerializeField] Slider manaSlider;
    [SerializeField] float maxMana;
    [SerializeField] Vector3 worldPosition;

    [SerializeField]Animator anim;
    public Camera mainCamera; // Set this to the main camera in the scene
    public float groundLevel; // Set this to the ground level
    private bool canCast;


    void Start()
    {
        attackOrigin = GameObject.FindGameObjectWithTag("AttackOrigin");
        mainCamera = Camera.main;
        canCast = true;
        manaSlider.maxValue = maxMana;
    }
    private void Update()
    {

        //switchSpell
        if (Input.GetKeyDown(spellUpButton) && inventary.Count > currentSpell+1)
        {
            currentSpell++;
        }
        else if(Input.GetKeyDown(spellUpButton) && inventary.Count <= currentSpell+1)
        {
            currentSpell = 0;
        }

        if (Input.GetKeyDown(spellDownButton) && currentSpell == 0)
        {
            currentSpell = inventary.Count-1;
        }
        else if(Input.GetKeyDown(spellDownButton) && currentSpell != 0)
        {
            currentSpell--;
        }
        



        //getMousePosition
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            worldPosition = raycastHit.point;
            worldPosition.y = groundLevel;
        }
        
        

        //Attack
        if(Input.GetKeyDown(mageInput))
        {
            attackDirection = worldPosition - transform.position;
            transform.LookAt(worldPosition);
            if(mana >= inventary[currentSpell].manaCost && canCast)
            {
                inventary[currentSpell].bonusDamage = bonusDamage;
                inventary[currentSpell].Attack(attackDirection, attackOrigin.transform.position);
                mana -= inventary[currentSpell].manaCost;
                canCast = false;
                StartCoroutine(WaitForFireRate());
                timeSinceLastSpell = 0;
            }
        }
        if(Input.GetKeyDown(attackInput))
        {
            attackDirection = worldPosition - transform.position;
            transform.LookAt(worldPosition);
            anim.SetTrigger("Attack");
        }



        //regen mana
        if(regenaratingMana)
        {
            if(mana < maxMana)
            mana += manaRegen * Time.deltaTime;
            else
            mana = maxMana;
        }
        timeSinceLastSpell += Time.deltaTime;
        if(timeSinceLastSpell >= timeToRegenMana)
        {
            regenaratingMana = true;
        }
        else
        regenaratingMana = false;
        //mana Slider
        manaSlider.value = mana;
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        UpdateLife();
        if(life <= 0)
        {
            GameManager.Instance.PlayerDie();
        }

    }
    public void AddSpell(GameObject spell)
    {
        inventary.Add(spell.GetComponent<Spells>());
        spell.transform.SetParent(gameObject.transform);
        spell.gameObject.SetActive(false);
        GetComponent<Interact>().RemoveFromInteract();
    }

    public void IncreaseFireRate( float bonusValue)
    {
        bonusFireRate += bonusValue;
    }
    public void IncreaseMaxLife(int bonusValue)
    {
        maxLife += bonusValue;
        lifeUi[maxLife-1].gameObject.SetActive(true);
        life += bonusValue;
        UpdateLife();
    }
    public void IncreaseMaxMana(float bonusValue)
    {
        maxMana += bonusValue;
        manaSlider.maxValue = maxMana;
    }

    public void IncreaseManaRegen(float bonusValue1, float bonusValue2)
    {
        manaRegen += bonusValue1;
        timeToRegenMana -= bonusValue2;
    }

    public void IncreaseDamage(int bonusValue)
    {
        bonusDamage += bonusValue;
    }
    


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(worldPosition, 0.3f); 
    }


    private void UpdateLife()
    {

        foreach(Image icon in lifeUi)
        {
            icon.sprite = lifelessSprite;
        }
        for (int i = 0; i < life; i++)
        {
            lifeUi[i].sprite = lifeSprite;
        }
    }

    IEnumerator WaitForFireRate()
    {
        float modifiedFireRate = inventary[currentSpell].fireRate;
        modifiedFireRate *= 1 - bonusFireRate;
        Debug.Log(modifiedFireRate);
        yield return new WaitForSeconds(modifiedFireRate);
        canCast = true;
    }


}
