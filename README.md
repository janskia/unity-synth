# Unity Synth

![Alt text](/Documentation~/Preview.gif?raw=true)

### About
This is and experimental implementation of audio synthesizer in Unity utilizing `OnAudioFilterRead`. I wanted to try some ideas related to audio generation and as I already knew Unity that was the best tool of choice. This synth has basic midi support, but the latency is pretty high. With a little bit of additional coding can be used for procedural audio generation in games, e.g. robotic speech, sci-fi noises, etc.

Main components:
- **Samplers** - these are waveform generators. Waveforms can be prepared from audio clip, drawn as animation curve. Can be groupped in `CompositeSampler` and blended in between.
- **Oscillators** - uses samplers and generate sound with given waveform, frequency, amplitude and panorama. Can be groupped in `CompositeOscillator` which spawns desired number of oscillators and alter it properties slightly (detune, change panorama position) to create more spatial sound.
- **Effects** - audio processing components which takes audio input, processes it and returns audio output. Currently only simple `Saturation` plugin is implemented.

### Demo
Take a look on the demo of first iteration on youtube:

[![Unity synth demo](https://img.youtube.com/vi/6o2-Eeih-KI/0.jpg)](https://www.youtube.com/watch?v=6o2-Eeih-KI)
[Open on youtube](https://www.youtube.com/watch?v=6o2-Eeih-KI)

### !!! Caution !!!
When dealing with this plugin (and audio development in general) always do this at low volume, preferably on speakers not headphones. Setting wrong input values in runtime or bugs in audio code may possibly result in extremely loud noise and damage your hearing if your audio sustem is too loud.
