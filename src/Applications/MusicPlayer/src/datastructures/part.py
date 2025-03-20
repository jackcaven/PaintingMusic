"""
Copyright ©, Painting Music Limited. All Rights Reserved. 2025.

Author: Jack Caven
"""

# Import Libraries
from scamp import ScampInstrument, Session, wait
from .music_data import MusicData, Note

# Constants
max_velocity = 127


class Part:
    def __init__(
        self,
        instrument_map: dict[str, ScampInstrument],
        music_payload: MusicData,
        session: Session = None,
    ) -> None:
        self.music = music_payload

        if session is None:
            self.instrument = instrument_map[music_payload.instrument]
        else:
            self.instrument = session.new_part(music_payload.instrument)

    # Public Method
    def play_part(self):
        counter = 0
        notes = self.music.notes
        wait(notes[0].start_time)
        while counter <= len(notes) - 1:
            self.instrument.play_chord(
                notes[counter].notes,
                notes[counter].velocity / max_velocity,
                notes[counter].duration,
            )

            counter += 1
