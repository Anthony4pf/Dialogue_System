using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI m_dialogue;
    private string[] m_sentences = {"Default", "Text"};
    private int m_index;

    public GameObject m_nextButton;

    public CharacterData[] m_characters;
    public UnityEvent OnEndDialogue;
    public UnityEvent OnStartDialogue;

    public void StartDialogue(int characterIndex)
    {
        OnStartDialogue.Invoke();
        m_sentences = m_characters[characterIndex].m_responses;
        m_index = 0;
        StartCoroutine(TypeDialogue());
    }

    private void Update()
    {
        if (m_dialogue.text == m_sentences[m_index])
        {
            m_nextButton.SetActive(true);
        }
    }

    IEnumerator TypeDialogue()
    {
        foreach (char letter in m_sentences[m_index].ToCharArray())
        {
            m_dialogue.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void NextResponse()
    {
        m_nextButton.SetActive(false);
        if (m_index < m_sentences.Length - 1)
        {
            m_index++;
            m_dialogue.text = "";
            StartCoroutine(TypeDialogue());
        }
        else
        {
            m_dialogue.text = "";
            m_nextButton.SetActive(false);
            OnEndDialogue.Invoke();
        }
    }
}