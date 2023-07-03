using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionScript : MonoBehaviour
{
    [Header("Sprite")]
    public Sprite[] musicSprite;
    public Sprite[] sfxSprite;
    public Image musicToggle;
    public Image sfxToggle;

    [Header("Button")]
    public Button musicButton;
    public Button sfxButton;
    public Button creditButton;
    public Button backButton;

    private void Start()
    {
        musicButton.onClick.AddListener(() => MusicToggle());
        sfxButton.onClick.AddListener(() => SFXToggle());
        backButton.onClick.AddListener(() => BackMain());
        creditButton.onClick.AddListener(() => ToCredit());
    }

    private void ChangeSprite(Sprite[] buttonSprites,Image targetButton)
    {
        if (targetButton.sprite == buttonSprites[0])
        {
            targetButton.sprite = buttonSprites[1];
            return;
        }
        targetButton.sprite = buttonSprites[0];

    }

    private void BackMain()
    {
        AudioManager.Instance.PlaySFX("clicked");
        SceneManager.LoadScene("main");
    }

    private void ToCredit()
    {
        AudioManager.Instance.PlaySFX("clicked");
        SceneManager.LoadScene("credit");
    }

    private void MusicToggle()
    {
        AudioManager.Instance.PlaySFX("clicked");
        if (AudioManager.musicIsPlaying)
        {
            AudioManager.musicIsPlaying = false;
            AudioManager.Instance.musicSource.Pause();
            Debug.Log("1. music off ");
        }
        else
        {
            AudioManager.musicIsPlaying = true;
            AudioManager.Instance.musicSource.UnPause();
            Debug.Log("2. music on ");
        }

        ChangeSprite(musicSprite, musicToggle);
    }

    private void SFXToggle()
    {
        AudioManager.Instance.PlaySFX("clicked");
        if (AudioManager.sfxIsPlaying)
        {
            AudioManager.sfxIsPlaying = false;
            AudioManager.Instance.sfxSource.Pause();
            Debug.Log("1. sfx off ");
        }
        else
        {
            AudioManager.sfxIsPlaying = true;
            AudioManager.Instance.sfxSource.UnPause();
            Debug.Log("2. sfx on ");
        }

        ChangeSprite(sfxSprite, sfxToggle);
    }
}
