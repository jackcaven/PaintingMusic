"""
Copyright ©, Painting Music Limited. All Rights Reserved. 2025.

Author: Jack Caven
"""

from dataclasses import dataclass
from typing import List


# Structs
@dataclass
class MusicData:
    def __init__(self, instrument: str, bpm: int, notes: list) -> None:
        self.instrument: str = instrument
        self.bpm: int = bpm
        self.notes: List[Note] = notes

    @property
    def melody_end_time(self) -> float:
        if not self.notes:
            return 0.0
        return max(note.start_time + note.duration for note in self.notes)

    @property
    def melody_length(self) -> float:
        return self.melody_end_time - self.notes[0].start_time

    def to_dict(self) -> dict:
        return {
            "instrument": self.instrument,
            "bpm": self.bpm,
            "notes": [vars(note) for note in self.notes],
        }


@dataclass
class Note:
    def __init__(
        self, notes: List[int], velocity: int, start_time: float, duration: float
    ) -> None:
        self.notes: List[int] = notes
        self.velocity: int = velocity
        self.start_time: float = start_time
        self.duration: float = duration


# Static Helper Methods
@staticmethod
def parse_music_data(dict: dict) -> MusicData:
    notes = []
    for note in dict["notes"]:
        notes.append(
            Note(note["notes"], note["velocity"], note["start_time"], note["duration"])
        )

    return MusicData(dict["instrument"], dict["bpm"], notes)
