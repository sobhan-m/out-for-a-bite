using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class DialogueController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueTextMeshPro;
    [SerializeField] TextMeshProUGUI speakerTextMeshPro;
    [SerializeField] DialogueScene dialogueScene;
    [SerializeField] GameObject sceneHolder;
    [SerializeField] float secondsBetweenCharacters;
    private GameObject previousScene;
    private bool isPrinting;
    private Coroutine printingProcess;
    private int currentDialogueIndex = 0;
    private InputAction nextAction;

    // Audio
    [SerializeField] private AudioClip typingEffect;
    private AudioSource audioSource;

    private void Awake()
    {
        nextAction = FindObjectOfType<InputActionContainingSystem>().actions.FindActionMap("Dialogue").FindAction("Next");
        isPrinting = false;


        audioSource = GetComponent<AudioSource>();
        audioSource.clip = typingEffect;
        audioSource.loop = true;

        Populate();
    }

    private void OnEnable()
    {
        nextAction.performed += NextDialogue;
        nextAction.Enable();
    }

    private void OnDisable()
    {
        nextAction.performed -= NextDialogue;
        nextAction.Disable();
    }

    private void Populate()
    {
        UpdateSceneText();
        UpdateSceneImage();
    }

    private void UpdateSceneText()
    {
        speakerTextMeshPro.text = GetCurrentDialogue().speaker;
        printingProcess = StartCoroutine(UpdateText());
    }

    private IEnumerator UpdateText()
    {
        isPrinting = true;
        audioSource.Play();

        string currentDialogueText = GetCurrentDialogue().dialogueText;

        for (int i = 1; i <= currentDialogueText.Length; ++i)
        {
            dialogueTextMeshPro.text = currentDialogueText.Substring(0, i);
            yield return new WaitForSeconds(secondsBetweenCharacters);
        }

        audioSource.Stop();
        isPrinting = false;
    }

    private void UpdateSceneImage()
    {
        DeleteOldSceneImage();
        CreateNewSceneImage();
    }

    private void CreateNewSceneImage()
    {
        GameObject dialogueGameObject = GetCurrentDialogue().sceneGameObject;
        if (dialogueGameObject != null)
        {
            previousScene = Instantiate(dialogueGameObject, sceneHolder.transform.position, Quaternion.identity, sceneHolder.transform);
        }
    }

    private void DeleteOldSceneImage()
    {
        if (previousScene != null)
        {
            Destroy(previousScene);
        }
    }

    private void LoadNextScene()
    {
        SceneChangeManager sceneManager = FindObjectOfType<SceneChangeManager>();
        sceneManager.LoadNextScene();
    }

    private void NextDialogue(InputAction.CallbackContext context)
    {
        if (isPrinting)
        {
            CancelPrinting();
            isPrinting = false;
        }
        else if (currentDialogueIndex + 1 == dialogueScene.dialogues.Length)
        {
            LoadNextScene();
        }
        else
        {
            currentDialogueIndex++;
            Populate();
        }
    }

    private void CancelPrinting()
    {
        audioSource.Stop();
        StopCoroutine(printingProcess);
        dialogueTextMeshPro.text = GetCurrentDialogue().dialogueText;
    }

    private Dialogue GetCurrentDialogue()
    {
        return dialogueScene.dialogues[currentDialogueIndex];
    }
}