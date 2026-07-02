
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] songs;
    private AudioSource audioSource;
    private int lastSongIndex;

    private void Awake() {
        MusicPlayer[] musicPlayers = FindObjectsByType<MusicPlayer>(FindObjectsSortMode.None);
        if (musicPlayers.Length > 1)
        {
            Destroy(this.gameObject);
        }
        if (SceneChangeManager.IsCombatScene())
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        PlaySong();
    }

    void Update()
    {
        if (SceneChangeManager.IsCombatScene())
        {
            Destroy(this.gameObject);
        }
        if (!audioSource.isPlaying)
        {
            PlaySong();
        }
    }

    public AudioClip PickSong()
    {
        int songIndex = Random.Range(0, songs.Length - 1);
        while (songIndex == lastSongIndex && songs.Length > 1)
        {
            songIndex = Random.Range(0, songs.Length - 1);
        }
        lastSongIndex = songIndex;
        return songs[songIndex];
    }

    public void PlaySong()
    {
        audioSource.clip = PickSong();
        audioSource.loop = false;
        audioSource.Play();
    }


}
