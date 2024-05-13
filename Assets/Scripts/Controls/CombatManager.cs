using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using Unity.VisualScripting;
using UnityEngine;

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
    [SerializeField] float mana;
    [SerializeField] Vector3 worldPosition;
    public Camera mainCamera; // Set this to the main camera in the scene
    public float groundLevel; // Set this to the ground level


    void Start()
    {
        attackOrigin = GameObject.FindGameObjectWithTag("AttackOrigin");
        mainCamera = Camera.main;
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
            inventary[currentSpell].Attack(attackDirection, attackOrigin.transform.position);
        }
        if(Input.GetKeyDown(attackInput))
        {
            attackDirection = worldPosition - transform.position;
            transform.LookAt(worldPosition);
            anim.SetTrigger("Attack");
        }
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
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
}
