using MusicGeneration.Models.Markov.Model;
using Core.DataStructures.Art;
using Core.DataStructures.Music;
using Core.Interfaces;
using MusicGeneration.Models.Markov.Helpers;
using Core.Utilities.DataStructures;
using Core.DataStructures.Result;

namespace MusicGeneration.Models.Markov
{
    public class MarkovMusicGenerator() : ICoreMusicProducer
    {
        private const string noteMarkovNamespace = "MusicGeneration.Models.Markov.Data.MarkovMatrixNotes.csv";
        private const string chordMarkovNamespace = "MusicGeneration.Models.Markov.Data.MarkovMatrixChords.csv";
        private const int defaultBPM = 130;
        private const double closeObjectThreshold = 0.1;

        private Dictionary<string, MusicData> musicDataCache = [];
        private List<ObjectAttributes> objectAttributesCache = [];
        private CanvasAttributes? canvasAttributesCache;

        private readonly MarkovModel noteModel = new(noteMarkovNamespace);
        private readonly MarkovModel chordModel = new(chordMarkovNamespace);
        

        public CoreResult Add(ObjectAttributes objectAttributes, CanvasAttributes canvasAttributes)
        {
            CoreResult result = new()
            {
                MusicData = new() { Instrument = string.Empty, Notes = [] }
            };

            canvasAttributesCache = canvasAttributes;

            string feedback = string.Empty;

            // Handle Fist Object
            if (objectAttributesCache.Count == 0)
            {
                result.ModelDecisionLogic += "This is the first object, therefore I am creating a new motif. ";

                result.MusicData.Notes.AddRange(GenerateMotif(startTime: 0, ref objectAttributes, ref canvasAttributesCache, out feedback));

                musicDataCache[objectAttributes.Id] = result.MusicData;
                objectAttributesCache.Add(objectAttributes);

                result.MusicData.BPM = defaultBPM;

                result.ModelDecisionLogic += feedback;

                return result;
            }

            // Check whether to build a chord
            (ObjectAttributes closestObject, double distance) = objectAttributesCache.GetClosestObject(objectAttributes);

            if (distance <= closeObjectThreshold)
            {
                result.ModelDecisionLogic += "This shape seems to be quite close to another, I am going to create a chord using the closest shape. ";
                result.MusicData.Notes.AddRange(GenerateChords(ref objectAttributes, musicDataCache[closestObject.Id], out feedback));
                musicDataCache[objectAttributes.Id] = result.MusicData;
                objectAttributesCache.Add(objectAttributes);

                result.MusicData.BPM = AttributeMapper.GetBPM(ref canvasAttributesCache, objectAttributesCache.Count);

                return result;
            }

            // Extend melody
            result.ModelDecisionLogic += "This shape seems to be independent, I am going to add a new motif";
            double startTime = musicDataCache.Values.Select(x => x.GetEndTime()).Max();
            
            result.MusicData.Notes.AddRange(GenerateMotif(startTime, ref objectAttributes, ref canvasAttributes, out feedback));
            musicDataCache[objectAttributes.Id] = result.MusicData;
            objectAttributesCache.Add(objectAttributes);

            result.MusicData.BPM = AttributeMapper.GetBPM(ref canvasAttributesCache, objectAttributesCache.Count);

            result.ModelDecisionLogic += feedback;

            return result;
        }

        public void Remove(ObjectAttributes imageAttributes, CanvasAttributes canvasAttributes)
        {
            canvasAttributesCache = canvasAttributes;
            objectAttributesCache.Remove(imageAttributes);
            musicDataCache.Remove(imageAttributes.Id);
        }

        public void Clear()
        {
            musicDataCache.Clear();
            objectAttributesCache.Clear();
            canvasAttributesCache = null;
        }

        private List<Note> GenerateMotif(double startTime, ref ObjectAttributes objectAttributes, ref CanvasAttributes canvasAttributes, out string feedback)
        {
            List<Note> notesToReturn = [];
            feedback = string.Empty;

            int initialPitch = AttributeMapper.GetPitch(ref objectAttributes, ref feedback);
            double noteLength = AttributeMapper.GetNoteLength(ref objectAttributes, ref feedback);
            int velocity = AttributeMapper.GetVelocity(ref objectAttributes);
            int numberOfNotes = AttributeMapper.GetNumberOfNotes(ref objectAttributes, ref feedback);

            List<int> notes = noteModel.GenerateNotes(initialPitch, numberOfNotes);

            for (int i = 0; i < notes.Count; i++)
            {
                notesToReturn.Add(new Note
                {
                    Notes = [notes[i]],
                    StartTime = startTime + i * noteLength,
                    Duration = noteLength,
                    Velocity = velocity,
                });
            };

            return notesToReturn;
        }
        private List<Note> GenerateChords(ref ObjectAttributes objectAttributes, MusicData motifToBuildChordFrom, out string feedback)
        {
            List<Note> chordsToReturn = [];
            List<Note> notesToBuildChord = motifToBuildChordFrom.Notes;
            feedback = string.Empty;

            int velocity = AttributeMapper.GetVelocity(ref objectAttributes);
            int numberOfNotes = AttributeMapper.GetNumberOfNotes(ref objectAttributes, ref feedback);

            if (numberOfNotes == notesToBuildChord.Count)
            {
                foreach (Note note in notesToBuildChord)
                {
                    int chordPitch = chordModel.GenerateNotes(note.Notes.First(), numberOfNotesToGenerate: 1).First();
                    chordsToReturn.Add(new Note()
                    {
                        Notes = [chordPitch],
                        Velocity = velocity,
                        Duration = note.Duration,
                        StartTime = note.StartTime,
                    });
                }
            }
            else if (numberOfNotes < notesToBuildChord.Count)
            {
                // Find a suitable place to add chords (i.e. every second note) and extend the length of them. 
                int chordStep = (int)Math.Round((double)(notesToBuildChord.Count / numberOfNotes));
                int pos = 0;

                while (pos < notesToBuildChord.Count)
                {
                    Note noteToBuildChordFrom = notesToBuildChord[pos];
                    List<int> chordPitches = chordModel.GenerateNotes(noteToBuildChordFrom.Notes.First(), numberOfNotesToGenerate: 1);
                    chordsToReturn.Add(new Note()
                    {
                        Notes = chordPitches,
                        Velocity = velocity,
                        StartTime = noteToBuildChordFrom.StartTime,
                        Duration = noteToBuildChordFrom.Duration * chordStep
                    });

                    pos += chordStep;
                }
            }
            else
            {
                // Add a chord for each note and try squeeze "new motif" notes in between chords
                int extraNotes = numberOfNotes - notesToBuildChord.Count;
                int extraNotesPerChord = numberOfNotes / notesToBuildChord.Count;
                int pos = 0;
                while (pos < motifToBuildChordFrom.Notes.Count)
                {
                    Note currentNote = motifToBuildChordFrom.Notes[pos];
                    double noteDuration = currentNote.Duration;

                    if (extraNotes > 0)
                    {
                        noteDuration = noteDuration / (extraNotesPerChord + 1);
                        extraNotesPerChord -= extraNotesPerChord;
                    }

                    // Append chord note
                    List<int> chordPitch = chordModel.GenerateNotes(currentNote.Notes.First(), 1);

                    chordsToReturn.Add(new Note()
                    {
                        Notes = chordPitch,
                        Velocity = velocity,
                        Duration = noteDuration,
                        StartTime = currentNote.StartTime,
                    });

                    // Build Motif between chords
                    int chordPtr = chordsToReturn.Count - 1;
                    List<int> miniMotif = noteModel.GenerateNotes(chordPitch.First(), extraNotesPerChord);

                    for (int i = 0; i < miniMotif.Count; i++) 
                    {
                        Note previousNote = chordsToReturn[chordPtr];
                        chordsToReturn.Add(new Note()
                        {
                            Notes = [miniMotif[i]],
                            Velocity = velocity,
                            StartTime = previousNote.StartTime + previousNote.Duration,
                            Duration = noteDuration
                        });
                        chordPtr += 1;
                    }

                    pos += 1;
                }

            }
            return chordsToReturn;
        }
    }
}
