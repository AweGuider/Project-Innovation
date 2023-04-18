using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    [SerializeField]
    public static AudioManager instance;

    // Audio Mixer
    [SerializeField]
    private AudioMixer mixer;

    [SerializeField]
    private AudioLibrary library;

    public enum AudioType
    {
        Music,
        Sound,
        Voice,
        Train,
        // Add additional categories as needed
    }

    private AudioSource trainSound;

    private void Awake()
    {
        // Ensure there is only one instance of the AudioManager script
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy this game object when changing scenes
        }
        else
        {
            Destroy(gameObject); // Destroy any additional instances of the AudioManager script
        }

        // Get a reference to the Library script attached to a child object
        //library = GetComponentInChildren<AudioLibrary>();

        // Check that the Library script was found
        try
        {
            if (library == null) library = GetComponentInChildren<AudioLibrary>();
        }
        catch
        {
            //Debug.LogError("Library script not found in children of AudioManager!");
        }
    }


    public void PlaySound(AudioType type, int index, bool loop = false)
    {
        AudioClip[] clips = null;

        // Determine which array of clips to use based on the specified category
        switch (type)
        {
            case AudioType.Music:
                clips = library.musicClips;
                break;
            case AudioType.Sound:
                clips = library.soundClips;
                break;
            case AudioType.Voice:
                clips = library.voiceClips;
                break;
            case AudioType.Train:
                clips = library.trainClips;
                break;
                // Add additional cases for each category of sound in your game
        }

        // Check if the specified index is within the bounds of the array
        if (index < 0 || index >= clips.Length)
        {
            //Debug.LogWarningFormat("Invalid index {0} for sound category {1}", index, type.ToString());
            return;
        }

        // Create a new audio source game object
        GameObject audioSourceObj = new GameObject("Audio Source");
        audioSourceObj.transform.SetParent(transform);

        // Add an audio source component to the game object
        AudioSource audioSource = audioSourceObj.AddComponent<AudioSource>();
        
        if (type == AudioType.Train)
        {
            trainSound = audioSource;
        }

        // Set the audio source to play the specified clip
        audioSource.clip = clips[index];

        // Set the audio source's output to the appropriate audio group in the Audio Mixer
        switch (type)
        {
            case AudioType.Music:
                audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Music")[0];
                break;
            case AudioType.Sound:
                audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Sounds")[0];
                break;
            case AudioType.Voice:
                audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Voice")[0];
                break;
            case AudioType.Train:
                audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Train")[0];
                break;
                // Add additional cases for each category of sound in your game
        }

        // Play the audio clip
        audioSource.loop = loop;
        audioSource.Play();
    }

    public void MuteTrain(bool b)
    {
        if (b)
        {
            trainSound.volume = 0f;
        }
        else
        {
            trainSound.volume = 1f;

        }
    }
}