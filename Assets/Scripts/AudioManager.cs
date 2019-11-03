using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static string FOLDER_NAME = "Audio";
    
    public Dictionary<string, AudioClip> clips;
    public AudioSource source;

    private static AudioManager instance;
    public static AudioManager Instance {
        get {
            if(!instance){
                var go = Instantiate(new GameObject("_AudioManager"));
                instance = go.AddComponent<AudioManager>();
                instance.LoadAudio();
                instance.source = instance.gameObject.AddComponent<AudioSource>();
                instance.source.spatialBlend = 0f;
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    public void Play(string clipName, bool playOnSource = false){
        if(clips.ContainsKey(clipName)){
            if(!playOnSource)
                AudioSource.PlayClipAtPoint(clips[clipName], Vector3.zero);
            else{
                source.clip = clips[clipName];
                source.Play();
            } 
        }
    }

    public void PlayAfter(string clipName, bool playOnSource = false, float duration = 0){
        StartCoroutine(DelayedPlay(clipName, playOnSource, duration));
    }

    private IEnumerator DelayedPlay(string clipName, bool playOnSource, float duration){
        yield return new WaitForSeconds(duration);
        Play(clipName, playOnSource);
    }

    private void LoadAudio(){
        var audioClips = Resources.LoadAll(FOLDER_NAME, typeof(AudioClip));
        clips = new Dictionary<string, AudioClip>();
        foreach(var clip in audioClips){
            print(clip.name.ToLower());
            clips.Add(clip.name.ToLower(), (AudioClip)clip);
        }
    }


}