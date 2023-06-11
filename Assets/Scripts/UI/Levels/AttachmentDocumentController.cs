using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using System;


[RequireComponent(typeof(UIDocument))]
public class AttachmentDocumentController : MonoBehaviour
{
    [field: SerializeField]
    public bool IsVisible { get; private set; }



    protected float IncreasingDamage;
    [SerializeField]
    private MonoBehaviour[] InputControllers;
    [SerializeField]
    private PlayerGunSelector GunSelector;

    [SerializeField]
    private ImpactType ExplosiveType;

    [SerializeField]
    private ImpactType FrostType;

    private VisualElement background;

    private Button Ability_1;
    private Button Ability_2;
    private Button Ability_3;
    public bool ButtonPressed = false;


    private UIDocument Document;
/*    private ScrollView SlotScrollView;
    private ScrollView OptionScrollView;*/
    private BoxColliderDetection enemyNumbers;
    private bool IsNoEnemies = false;
    private static readonly Dictionary<Slot, int> SelectedAttachments = new()
    {
        { Slot.Barrel, 0 },
        { Slot.Handle, 0 },
        { Slot.AmmoType, 0 }
    };

    private static readonly Dictionary<Slot, Attachment[]> MockAttachments = new()
    {
        { Slot.Handle, new Attachment[] { 
            new Attachment("Standard", "Standard issue", new(), 0, 0) { IsSelected = true },
            new Attachment("Attack Speed" , "Increase the attack speed, so you will be shooting faster than usual.", new()
            {
                new FloatModifier(0.85f, "ShootConfiguration/FireRate")
            },0 , 0),
        } },
        { Slot.Barrel, new Attachment[] { 
            new Attachment("Standard", "Standard issue", new(), 0, 0) { IsSelected = true },
            new Attachment("Attack Damage", "Increase your attack base damage by 5%.", new()
            {
                new FloatModifier(30.0f,"DamageConfig/DamageCurve")
            },0 , 0),
                
        } },
        { Slot.AmmoType, new Attachment[] { 
            new Attachment("Standard", "Standard issue", new(), 0, 0) { IsSelected = true }, 
            new Attachment("Explosion", "Explodes on impact dealing damage to nearby enemies.", new()
            {
             new ImpactTypeModifier(null, ""),
                        new ImpactEffectReplacementModifier(new ICollisionHandler[] {
                            new Explode(
                                1.5f, new AnimationCurve(new Keyframe[] { new Keyframe(0, 1), new Keyframe(1, 0.5f) }), 10, 10)
                        })
                    }, 
                0, 
                0
                ),
            new Attachment("Frost", "Create a small frost explosion on impact dealing light damage to " +
                "nearby enemies and slowing them.", new()
                {
                        new ImpactTypeModifier(null, ""),
                        new ImpactEffectReplacementModifier(new ICollisionHandler[] {
                        new Frost
                            (0.75f, new AnimationCurve(
                                new Keyframe[] { new Keyframe(0, 1), new Keyframe(1, 0.5f) }), 7, 10, 
                            new AnimationCurve(
                                new Keyframe[] { new Keyframe(0, 0.5f), new Keyframe(1.75f, 0.5f), new Keyframe(2, 1)}))
                        })
                    },
            0,
            0)

        } },
    };

    private void Awake()
    {
        Document = GetComponent<UIDocument>();
        enemyNumbers = FindObjectOfType<BoxColliderDetection>();
        Document.rootVisualElement.AddToClassList("hidden");
        IsVisible = false;

        (MockAttachments[Slot.AmmoType][1].Modifiers[0] as ImpactTypeModifier).Amount = ExplosiveType;
        (MockAttachments[Slot.AmmoType][2].Modifiers[0] as ImpactTypeModifier).Amount = FrostType;


    }

    private void Start()
    {
        background = Document.rootVisualElement.Q<VisualElement>("background");

        background.visible = false;


        Ability_1 = Document.rootVisualElement.Q<Button>("ability1");
        Ability_2 = Document.rootVisualElement.Q<Button>("ability2");
        Ability_3 = Document.rootVisualElement.Q<Button>("ability3");

        Ability_1.clickable.clicked += ButtonClicked;
        Ability_2.clickable.clicked += ButtonClicked;
        Ability_3.clickable.clicked += ButtonClicked;



        AttachmentSlot slot = new("Barrel", "Standard Issue", "ban");
/*        Ability_1.RegisterCallback<ClickEvent>((_) => HandleSlotClick(Slot.Barrel, slot));
        SlotScrollView.Add(slot);*/

        AttachmentSlot slot2 = new("Handle", "Standard Issue", "ban");
/*        Ability_2.RegisterCallback<PointerDownEvent>((_) => HandleSlotClick(Slot.Handle, slot2));
        SlotScrollView.Add(slot2);*/ 

        AttachmentSlot slot3 = new("AmmoType", "Standard Issue", "ban");
/*        Ability_3.RegisterCallback<PointerDownEvent>((_) => HandleSlotClick(Slot.AmmoType, slot3));
        SlotScrollView.Add(slot3);*/


    }

    private void Update()
    {
        /* if (Keyboard.current.escapeKey.wasReleasedThisFrame )
         {
             if (IsVisible)
             {
                 Hide();
             }
             else
             {
                 Show();
             }
         }*/



       if( enemyNumbers.CurrentNumberOfEnemies == 0 && !IsNoEnemies)
        {
            background.visible = true;
            Time.timeScale = 0.0f;
            IsVisible = true;
            Document.rootVisualElement.RemoveFromClassList("hidden");
            Document.rootVisualElement.AddToClassList("visible");

          if(/*Input.touchCount > 0 || Input.GetMouseButton(0)*/ ButtonPressed)
            {
                background.visible = false;
                Time.timeScale = 1.0f;
                IsVisible = false;
                Document.rootVisualElement.AddToClassList("hidden");
                Document.rootVisualElement.RemoveFromClassList("visible");
/*                GunSelector.ApplyModifiers(CollectModifiers());
*/                IsNoEnemies = true;
            }
        }

    }

    void ButtonClicked()
    {
        ButtonPressed = true;
    }

    /*private void Hide()
    {
        IsVisible = false;
        Document.rootVisualElement.AddToClassList("hidden");
        Document.rootVisualElement.RemoveFromClassList("visible");
        ToggleInputControllers();

    }

    private void Show()
    {
        IsVisible = true;
        Document.rootVisualElement.RemoveFromClassList("hidden");
        Document.rootVisualElement.AddToClassList("visible");
        ToggleInputControllers();

    }

    private void ToggleInputControllers()
    {
        foreach(MonoBehaviour inputControl in InputControllers)
        {
            inputControl.enabled = !IsVisible;
        }
    }*/
/*    private void HandleSlotClick(Slot Type, AttachmentSlot Slot)
    {
        ClearSlotSelectionClasses();
        Slot.AddToClassList("selected");
        BuildOptionsForSlot(Type);
    }
*/
   /* private void ClearSlotSelectionClasses()
    {
        foreach (VisualElement element in SlotScrollView.Children())
        {
            element.RemoveFromClassList("selected");
        }
    }
    private void BuildOptionsForSlot(Slot Type)
    {
        VisualElement optionContainer = Document.rootVisualElement.Q<VisualElement>("option-container");

        optionContainer.AddToClassList("fade-in-right");
        optionContainer.RemoveFromClassList("fade-out-left");
        optionContainer.focusable = true;
        optionContainer.pickingMode = PickingMode.Position;

        if (OptionScrollView == null)
        {
            OptionScrollView = optionContainer.Q<ScrollView>();
        }
        else
        {
            OptionScrollView.Clear();
        }

        for(int i = 0; i < MockAttachments[Type].Length; i++)
        {
            int index = 1;
            Attachment attachment = MockAttachments[Type][index];
            AttachmentSlot slot = new(
                attachment.Name,
                attachment.Description,
                attachment.Name.ToLower().Replace(" " , "-")
                ) ;
            slot.RegisterCallback<PointerDownEvent>((_) => HandleChangeOption(slot, Type, index));

            if (MockAttachments[Type][i].IsSelected)
            {
                slot.AddToClassList("selected");
            }

            VisualElement chevron = slot.Q<VisualElement>(null, "chevron-right");
            chevron.RemoveFromClassList("chevron-right");
            chevron.AddToClassList("display-none");

            OptionScrollView.Add(slot);
        }

    }
*/
    /*private void HandleChangeOption(AttachmentSlot Slot, Slot Type, int Index)
    {
        foreach(VisualElement child in OptionScrollView.Children())
        {
            child.RemoveFromClassList("selected");
        }
        Slot.AddToClassList("selected");

        // Deselcted the previously selected one
        MockAttachments[Type][SelectedAttachments[Type]].IsSelected = false;
        //Select the new one
        SelectedAttachments[Type] = Index;
        MockAttachments[Type][Index].IsSelected = true;

    }*/

    private IModifier[] CollectModifiers()
    {
        List<IModifier> modifiersToApply = new();
        
        foreach(KeyValuePair<Slot, int> keyValuePair in SelectedAttachments)
        {
            modifiersToApply.AddRange(MockAttachments[keyValuePair.Key][keyValuePair.Value].Modifiers);
        }

        return modifiersToApply.ToArray();
    }

}
