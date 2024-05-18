using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    [Header("Mesh and materials related")]
    [SerializeField] Material playerMaterial;
    [SerializeField] Renderer playerRenderer;
    [SerializeField] GameObject playerModel;

    [Header("VFX")]
    [SerializeField] ParticleSystem noManaFx;

    [Header("Spells and Combat parameters")]
    [SerializeField] KeyCode spellUpButton;
    [SerializeField] KeyCode spellDownButton;
    [SerializeField] KeyCode mageInput;
    [SerializeField] KeyCode attackInput;
    public PlayerStatus status;
    [SerializeField] int currentSpell;
    [SerializeField] float bonusFireRate = 0;
    [SerializeField] float meleeFireRate;
    [SerializeField] bool canMelee;
    [SerializeField] private List<Spells> inventary = new List<Spells>();
    [SerializeField] private Vector3 attackDirection;
    [SerializeField] private GameObject attackOrigin;
    [SerializeField] private int bonusDamage;
    [SerializeField] GameObject playerAim;
    [SerializeField] Image currentSpellSprite;

    
    [Header("Life Related")]
    [SerializeField] private int maxLife = 5;
    [SerializeField] List<Image> lifeUi;
    [SerializeField] Sprite lifeSprite;
    [SerializeField] Sprite lifelessSprite;
    [SerializeField] float life;
    [SerializeField] bool canTake;
    [SerializeField] float immunityTime;
    [SerializeField] float immunityBlinkInverval;
    [SerializeField] float blinkCounter;
    [SerializeField] bool blinking;

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
        canMelee = true;
        manaSlider.maxValue = maxMana;
        playerRenderer = playerModel.GetComponent<Renderer>();
        playerMaterial = playerRenderer.material;

    }
    private void Update()
    {

        //switchSpell
        if (Input.GetKeyDown(spellUpButton) && inventary.Count > currentSpell+1)
        {
            currentSpell++;
            anim.SetTrigger("Change+");
        }
        else if(Input.GetKeyDown(spellUpButton) && inventary.Count <= currentSpell+1)
        {
            currentSpell = 0;
            anim.SetTrigger("Change+");
        }

        if (Input.GetKeyDown(spellDownButton) && currentSpell == 0)
        {
            currentSpell = inventary.Count-1;
            anim.SetTrigger("Change-");
        }
        else if(Input.GetKeyDown(spellDownButton) && currentSpell != 0)
        {
            currentSpell--;
            anim.SetTrigger("Change-");
        }
        



        //getMousePosition
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            worldPosition = raycastHit.point;
            worldPosition.y = groundLevel;
        }
        
        //pointsTheAim
        playerAim.transform.LookAt(worldPosition);

        //Attack
        if(Input.GetKeyDown(mageInput) &&  status != PlayerStatus.ATTACKING)
        {

            attackDirection = worldPosition - transform.position;
            transform.LookAt(worldPosition);
            if(mana >= inventary[currentSpell].manaCost && canCast)
            {
                status = PlayerStatus.ATTACKING;
                inventary[currentSpell].bonusDamage = bonusDamage;
                inventary[currentSpell].Attack(attackDirection, attackOrigin.transform.position);
                mana -= inventary[currentSpell].manaCost;
                canCast = false;
                StartCoroutine(WaitForFireRate());
                timeSinceLastSpell = 0;
            }
            else
            {
                noManaFx.Play();
            }
        }
        if(Input.GetKeyDown(attackInput) &&  status != PlayerStatus.ATTACKING && canMelee)
        {
            status = PlayerStatus.ATTACKING;
            canMelee = false;
            StartCoroutine(WaitForMeleeFireRate());
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

        //blink in immunity
        if(blinking)
        {
            playerMaterial.color = new Color(1,0,0,0.5f);
        }
        else
        {
            playerMaterial.color = Color.red;
        }

        if(canTake == false)
        {
            blinkCounter += Time.deltaTime;
            if(blinkCounter >= immunityBlinkInverval && blinking == false)
            {
                blinkCounter = 0;
                blinking = true;
            }
            else if(blinkCounter >= immunityBlinkInverval && blinking == true)
            {
                blinkCounter = 0;
                blinking = false;
            }
        }
        else
        {
            blinking = false;
        }
    }

    public void TakeDamage(int damage)
    {
        if(canTake)
        {
            canTake = false;
            life -= damage;
            UpdateLife();
            if(life <= 0)
            {
                GameManager.Instance.PlayerDie();
            }
            StartCoroutine(WaitForImmunity());
        }


    }


    public void AddSpell(GameObject spell)
    {
        inventary.Add(spell.GetComponent<Spells>());
        spell.transform.SetParent(gameObject.transform);
        spell.gameObject.SetActive(false);
        GetComponent<Interact>().RemoveFromInteract();
    }

    public void ChangeSpell()
    {
        currentSpellSprite.sprite = inventary[currentSpell].spellSprite;
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
        yield return new WaitForSeconds(modifiedFireRate);
        status = PlayerStatus.WALKING;
        canCast = true;
    }

    IEnumerator WaitForMeleeFireRate()
    {
        yield return new WaitForSeconds(meleeFireRate);
        status = PlayerStatus.WALKING;
        canMelee = true;
    }



    IEnumerator WaitForImmunity()
    {
        yield return new WaitForSeconds(immunityTime);
        canTake = true;
    }


}
