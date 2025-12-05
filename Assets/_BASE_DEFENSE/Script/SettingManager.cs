using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[System.Serializable]
public class Setting
{
    public Button settingBtn;
    public GameObject onHandle;
    public GameObject offHandle;

}

public class SettingManager : MonoBehaviour
{
    Button iconBtn;
    Button closeBtn;
    GameObject settingDialog;
    Button resetBtn;
    GameObject resetDialog;
    Button resetYesBtn;
    Button resetNoBtn;
    


    Setting sound = new Setting();
    Setting vibrate = new Setting();


    private void Awake()
    {
        iconBtn = transform.Find("/Canvas/Setting/IconBTN").GetComponent<Button>();
        settingDialog = transform.Find("/Canvas/Setting/SettingDialog").gameObject;
        resetDialog = transform.Find("/Canvas/Setting/SettingDialog/ResetDialog").gameObject;
        sound.settingBtn = transform.Find("/Canvas/Setting/SettingDialog/BG/SOUND").GetComponent<Button>();
        sound.onHandle = transform.Find("/Canvas/Setting/SettingDialog/BG/SOUND/On").gameObject;
        sound.offHandle = transform.Find("/Canvas/Setting/SettingDialog/BG/SOUND/Off").gameObject;
        vibrate.settingBtn = transform.Find("/Canvas/Setting/SettingDialog/BG/VIBRATE").GetComponent<Button>();
        vibrate.onHandle = transform.Find("/Canvas/Setting/SettingDialog/BG/VIBRATE/On").gameObject;
        vibrate.offHandle = transform.Find("/Canvas/Setting/SettingDialog/BG/VIBRATE/Off").gameObject;
        resetBtn = transform.Find("/Canvas/Setting/SettingDialog/BG/RESET/ResetBtn").GetComponent<Button>();
        resetYesBtn = transform.Find("/Canvas/Setting/SettingDialog/ResetDialog/BG/YesBtn").GetComponent<Button>();
        resetNoBtn = transform.Find("/Canvas/Setting/SettingDialog/ResetDialog/BG/NoBtn").GetComponent<Button>();
        closeBtn = transform.Find("/Canvas/Setting/SettingDialog/BG/CloseBtn").GetComponent<Button>();

        resetDialog.SetActive(false);
        settingDialog.SetActive(false);

        iconBtn.onClick.AddListener(() => CloseAndOpen(true));
        closeBtn.onClick.AddListener(() => CloseAndOpen(false));
        sound.settingBtn.onClick.AddListener(() => SetSound());
        vibrate.settingBtn.onClick.AddListener(() => SetVibrate());
        resetBtn.onClick.AddListener(() => ResetData());
        resetYesBtn.onClick.AddListener(() => ResetConfirmYes());
        resetNoBtn.onClick.AddListener(() => ResetConfirmNo());



    }

    private void Start()
    {
        GetSound();
        GetVibrate();
    }

    void CloseAndOpen(bool value)
    {
        SoundManager.ins.PlaySound(0);
        settingDialog.SetActive(value);
    }

    void GetSound()
    {
        if(PlayerPrefs.GetInt(StringManager.SOUND) == 0)
        {
            sound.onHandle.SetActive(true);
            sound.offHandle.SetActive(false);
        }
        else
        {
            sound.onHandle.SetActive(false);
            sound.offHandle.SetActive(true);
        }

        SoundManager.ins.SwichBGSound();
    }

    void GetVibrate()
    {
        if (PlayerPrefs.GetInt(StringManager.VIBRATE) == 0)
        {
            vibrate.onHandle.SetActive(true);
            vibrate.offHandle.SetActive(false);
        }
        else
        {
            vibrate.onHandle.SetActive(false);
            vibrate.offHandle.SetActive(true);
        }
    }

    void SetSound()
    {
       
        if (PlayerPrefs.GetInt(StringManager.SOUND) == 0)
        {
            PlayerPrefs.SetInt(StringManager.SOUND, 1);
        }
        else
        {
            SoundManager.ins.PlaySound(0);
            PlayerPrefs.SetInt(StringManager.SOUND, 0);
        }

        GetSound();
    }

    void SetVibrate()
    {
        SoundManager.ins.PlaySound(0);
        if (PlayerPrefs.GetInt(StringManager.VIBRATE) == 0)
        {
            PlayerPrefs.SetInt(StringManager.VIBRATE, 1);
        }
        else
        {
            PlayerPrefs.SetInt(StringManager.VIBRATE, 0);
        }

        GetVibrate();
    }

    void ResetData()
    {
        SoundManager.ins.PlaySound(0);
        resetDialog.SetActive(true);
    }

    void ResetConfirmYes()
    {
        PlayerPrefs.DeleteAll();
        StartCoroutine(LoadScene());

    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }

    void ResetConfirmNo()
    {
        SoundManager.ins.PlaySound(0);
        resetDialog.SetActive(false);
    }
}
