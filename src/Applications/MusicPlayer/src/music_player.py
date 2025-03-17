"""
Copyright ©, Painting Music Limited. All Rights Reserved. 2025.

Author: Jack Caven
"""

# Import Libraries
from scamp import *
from typing import List
from datastructures.part import Part
import threading
import logging
import time

# Constants
_default_instruments: List[str] = ["piano", "choir", "violin", "synth"]


class MusicPlayer:
    def __init__(self) -> None:
        self.parts: List[Part] = []
        self.instrument_map = {}
        self.current_tempo: int = 90
        self.session = Session(tempo=self.current_tempo)
        self.is_playing: bool = False
        self.first_part_added: bool = False
        self.track_thread = None
        self.lock = threading.Lock()
        self._initialize_instruments()

    # Public Methods
    def play(self) -> None:
        logging.info("Starting Music Player")
        self.is_playing = True
        self.track_thread = threading.Thread(target=self._play)
        self.track_thread.start()

    def append_part(self, new_part: Part) -> None:
        logging.info("New part added to music")

        if not self.first_part_added:
            self.first_part_added = True
        else:
            new_part = Part(self.instrument_map, new_part.music, self.session)

        with self.lock:
            self.parts.append(new_part)

    def stop(self) -> None:
        logging.info("Stopping Music Player")
        self.is_playing = False
        self.track_thread.join()
        self._fade_to_stop()
        self.parts = []

    # Private Methods
    def _initialize_instruments(self):
        for instrument in _default_instruments:
            self.instrument_map[instrument] = self.session.new_part(instrument)

    def _play(self) -> None:

        while self.is_playing:
            if len(self.parts) == 0:
                time.sleep(0.1)
                continue

            for part in self.parts:
                logging.debug("Forking new part")
                self.session.fork(part.play_part)

            logging.debug("Waiting for loop to finish")
            self.session.wait_for_children_to_finish()

    def _fade_to_stop(self) -> None:
        first_part = self.parts[0]
        final_vel = first_part.music.notes[0].velocity

        while final_vel > 0:
            for note in first_part.music.notes:
                note.velocity = max(0, final_vel)

            first_part.play_part()
            self.session.wait_for_children_to_finish()
            final_vel -= 20
