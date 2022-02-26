using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.gamedeveloper.com/audio/coding-to-the-beat---under-the-hood-of-a-rhythm-game-in-unity

public class Conductor : MonoBehaviour
{
    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    [SerializeField] private float songBpm;

    //The number of seconds for each song beat
    [SerializeField] private float secPerBeatDouble;

    [SerializeField] private float secPerBeatBase;

    [SerializeField] private float secPerBeatHalf;

    [SerializeField] private float secPerBeatQuarter;

    //Current song position, in seconds
    [SerializeField] private float songPosition;

    //Current song position, in beats
    public float songBeatsPosDouble;

    public float songBeatsPosBase;

    public float songBeatsPosHalf;

    public float songBeatsPosQuarter;

    //How many seconds have passed since the song started
    [SerializeField] private float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    [SerializeField] private AudioSource musicSource;

    //the number of beats in each loop
    [SerializeField] private float beatsPerLoop;

    //the total number of loops completed since the looping clip first started
    [SerializeField] private int completedLoops = 0;

    //The current position of the song within the loop in beats.
    [SerializeField] private float loopPositionInBeats;

    //The current relative position of the song within the loop measured between 0 and 1.
    public float loopPositionInAnalog;

    //Conductor instance
    public static Conductor instance;

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //Calculate the number of seconds in each beat
        secPerBeatDouble = 60f / (songBpm * 2);

        secPerBeatBase = 60f / songBpm;

        secPerBeatHalf = 60f / (songBpm / 2);

        secPerBeatQuarter = 60f / (songBpm / 4);

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        DoubleBPM();
        BaseBPM();
        HalfBPM();
        QuarterBPM();
    }

    void DoubleBPM()
    {
        songBeatsPosDouble = songPosition / secPerBeatDouble;
    }

    void BaseBPM()
    {
        //determine how many beats since the song started
        songBeatsPosBase = songPosition / secPerBeatBase; //TODO: truncate this, also beats count from 1 and this counts from 0, so it will display 1 beat less

        if (songBeatsPosBase >= (completedLoops + 1) * beatsPerLoop) // define beatsPerLoop when i have my own music, rn 10min 120 bpm is not worth fixing
            completedLoops++;
        loopPositionInBeats = songBeatsPosBase - completedLoops * beatsPerLoop;

        loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;
    }

    void HalfBPM()
    {
        songBeatsPosHalf = songPosition / secPerBeatHalf;
    }

    void QuarterBPM()
    {
        songBeatsPosQuarter = songPosition / secPerBeatQuarter;
    }
}
