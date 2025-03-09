"""
Copyright ©, Painting Music Limited. All Rights Reserved. 2025.

Author: Jack Caven
"""

# Import Modules
from art import tprint
from music_player import MusicPlayer
from datastructures.part import Part
from sockets.socketserver import (
    SocketServer,
    MessageReceivedEvent,
    Command,
)
from datastructures.music_data import parse_music_data, MusicData
import logging
import json

# Global Fields
music_player = MusicPlayer()


def _music_event_listener(event: MessageReceivedEvent) -> None:
    try:
        cmd = event.jsn_dict["command"]
        logging.info(f"Command recieved from {event.addr}: {cmd}")

        match cmd:
            case Command.START:
                if not music_player.is_playing:
                    music_player.play()
            case Command.STOP:
                if music_player.is_playing:
                    music_player.stop()
            case Command.NEXT_SCENE:
                if music_player.is_playing:
                    music_player.stop()
                    music_player.play()
            case Command.PAYLOAD:
                if not music_player.is_playing:
                    logging.warning("Payload recieved when player was not playing")
                    return

                if type(event.jsn_dict["payload"]) is str:
                    core_payload = json.loads(event.jsn_dict["payload"])
                else:
                    core_payload = event.jsn_dict["payload"]

                payload: MusicData = parse_music_data(core_payload)

                music_player.append_part(Part(music_player.instrument_map, payload))
            case _:
                Exception("Invalid Command given by client")

    except Exception as ex:
        logging.error(f"Error in music_reciever: {ex}")


if __name__ == "__main__":
    tprint("Painting Music Player")
    logging.basicConfig()
    logging.root.setLevel(logging.NOTSET)
    server = SocketServer()
    server.register_observer(_music_event_listener)
    server.start()
