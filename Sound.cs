using NAudio.Wave;


namespace Tetromino
{
    internal class Sound
    {

        Mp3FileReader fileReader;
        Mp3FileReader filePunchReader;
        Mp3FileReader fileRowCompletedReader;

        WaveOut waveOut;
        public Sound() {
            fileReader = new Mp3FileReader("sound/RiseAbove.mp3");
            filePunchReader = new Mp3FileReader("sound/wooden2.mp3");
            fileRowCompletedReader = new Mp3FileReader("sound/whoosh.mp3");
            waveOut = new WaveOut();
        }

        public void StartBackgroundMusic()
        {
            waveOut.Init(fileReader);
            waveOut.Play();
        }

        public void StopBackgroundMusic() { 
            waveOut.Stop();
        }

        public void StartPunch()
        {
            WaveOut wo = new WaveOut();
            wo.Init(filePunchReader);
            wo.Play();

            while (wo.PlaybackState == PlaybackState.Playing) // espero que termine de reproducir
            {
                System.Threading.Thread.Sleep(25);
            }

            filePunchReader.Position = 0; // reseteo el archivo
            wo.Stop();
        }

        public void StartRowCompleted()
        {
            WaveOut wo = new WaveOut();
            wo.Init(fileRowCompletedReader);
            wo.Play();

            while (wo.PlaybackState == PlaybackState.Playing) // espero que termine de reproducir
            {
                System.Threading.Thread.Sleep(75);
            }

            filePunchReader.Position = 0; // reseteo el archivo
            wo.Stop();
        }

    }
}
