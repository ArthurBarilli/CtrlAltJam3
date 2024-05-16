using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    [SerializeField] KeyCode spellUpButton;
    [SerializeField] KeyCode spellDownButton;
    [SerializeField] KeyCode mageInput;
    [SerializeField] KeyCode attackInput;
    [SerializeField]Animator anim;
    [SerializeField] int currentSpell;
    [SerializeField] private List<Spells> inventary = new List<Spells>();
    [SerializeField] private Vector3 attackDirection;
    [SerializeField] private GameObject attackOrigin;
    [SerializeField] float life;
    [Header("Mana Related")]
    [SerializeField] bool regenaratingMana;
    [SerializeField] float mana;
    [SerializeField] float manaRegen; //how much mana regen by second
    [SerializeField] float timeToRegenMana; //time without casting any spell to regen mana
    [SerializeField] float timeSinceLastSpell;
    [SerializeField] Vector3 worldPosition;
    [SerializeField] List<Image> lifeUi;
    [SerializeField] Sprite lifeSprite;
    [SerializeField] Sprite lifelessSprite;
    public Camera mainCamera; // Set this to the main camera in the scene
    public float groundLevel; // Set this to the ground level
    private bool canCast;

    void Start()
    {
        attackOrigin = GameObject.FindGameObjectWithTag("AttackOrigin");
        mainCamera = Camera.main;
        canCast = true;
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
            if(mana < 100)
            mana += manaRegen * Time.deltaTime;
            else
            mana = 100;
        }
        timeSinceLastSpell += Time.deltaTime;
        if(timeSinceLastSpell >= timeToRegenMana)
        {
            regenaratingMana = true;
        }
        else
        regenaratingMana = false;
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
        switch (life)
        {
            case 5:
                for (int i = 0; i < 5; i++)
                {
                    lifeUi[i].sprite = lifeSprite;
                }    
            break;
            case 4:
                for (int i = 0; i < 4; i++)
                {
                    lifeUi[i].sprite = lifeSprite;
                }   
            break;
            case 3:
                for (int i = 0; i < 3; i++)
                {
                    lifeUi[i].sprite = lifeSprite;
                }   
            break;
            case 2:
                for (int i = 0; i < 2; i++)
                {
                    lifeUi[i].sprite = lifeSprite;
                }  
            break;
            case 1:
                lifeUi[0].sprite = lifeSprite;
            break;
        }
    }
    IEnumerator WaitForFireRate()
    {
        yield return new WaitForSeconds(inventary[currentSpell].fireRate);
        canCast = true;
    }


}
