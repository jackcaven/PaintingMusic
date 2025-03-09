"""
Copyright ©, Painting Music Limited. All Rights Reserved. 2025.

Author: Jack Caven
"""

# Import libraries
import json
import logging
import socket
from dataclasses import dataclass
from enum import Enum

# Constants
DEFAULT_IP = "127.0.0.1"
DEFAULT_PORT = 12345


# Enums
class StatusCode(Enum):
    CONNECTED = "Connected"
    OK = "OK"
    BAD_REQUEST = "Bad Request"
    ERROR = "Internal Server Error"
    CLOSING = "Closing"

    def __eq__(self, other):
        if isinstance(other, StatusCode):
            return self.value == other.value
        elif isinstance(other, str):
            return self.value == other
        return NotImplemented


class Command(Enum):
    INIT = "init"
    START = "start"
    STOP = "stop"
    NEXT_SCENE = "next scene"
    PAYLOAD = "music payload"

    def __eq__(self, other):
        if isinstance(other, Command):
            return self.value == other.value
        elif isinstance(other, str):
            return self.value == other
        return NotImplemented

    @classmethod
    def IsAcceptable(cls, value):
        return value in cls._value2member_map_


# Events
class MessageReceivedEvent:
    def __init__(self, message, client_addr) -> None:
        self.jsn_dict = message
        self.addr = client_addr


# Clases
class CommunicationsMessage:
    def __init__(self, command: StatusCode, payload=None) -> None:
        self.command: str = command.value
        self.payload = payload

    def serialize(self) -> str:
        if self.command == None:
            self.command = ""

        if self.payload == None:
            self.payload = ""

        communication_dict = {"command": self.command, "payload": self.payload}

        json_str = json.dumps(communication_dict)
        logging.debug(f"Json payload: {json_str}")
        return json_str


class SocketServer:
    def __init__(self) -> None:
        self.observers = []
        self.socket = None
        self.client = None
        self.is_running: bool = False
        self._initialize()

    # Public Methods
    def start(self) -> None:
        if self.socket is None:
            logging.error(
                "Socket has not been initialized. Recommend restarting application"
            )

        self.socket.listen(5)
        self.client, addr = self.socket.accept()
        logging.info(f"Connected to {addr} successfully")
        self.client.send(StatusCode.CONNECTED.value.encode())
        self.is_running: bool = True
        self.payload_size: int = None
        self.expecting_payload: bool = False

        try:
            while self.is_running:

                msg = self.client.recv(1024).decode("utf-8")

                if self.expecting_payload:
                    while len(msg) < self.payload_size:
                        msg += self.client.recv(1024).decode("utf-8")

                    self.expecting_payload = False

                try:
                    jsn = json.loads(msg)
                except:
                    self.client.send(StatusCode.BAD_REQUEST.value.encode())
                    continue

                if not (jsn and Command.IsAcceptable(jsn["command"])):
                    self.client.send(StatusCode.BAD_REQUEST.value.encode())
                    continue

                # We need to send payload metadata to ensure entire payload is parsed
                if jsn["command"] == Command.PAYLOAD and "size" in jsn["payload"]:
                    self.payload_size = jsn["payload"]["size"]
                    self.expecting_payload = True
                    self.client.send(StatusCode.OK.value.encode())
                    continue

                event = MessageReceivedEvent(jsn, addr)
                self._notify_observers(event)
                self.client.send(StatusCode.OK.value.encode())

                if jsn["command"] != Command.STOP:
                    continue

                self._stop()
        except Exception as e:
            logging.error(f"Exception message: {e}")
            self.client.send(StatusCode.CLOSING.value.encode())
            self._stop()

    def register_observer(self, observer):
        self.observers.append(observer)

    def remove_observer(self, observer):
        self.observers.remove(observer)

    # Private Methods
    def _initialize(self) -> None:
        self.socket = socket.socket()
        logging.info("Initializing Socket Server")
        self.socket.bind((DEFAULT_IP, DEFAULT_PORT))
        logging.info(
            f"Server configured to listen on address: {DEFAULT_IP}, on port {DEFAULT_PORT}"
        )

    def _notify_observers(self, event):
        for observer in self.observers:
            observer(event)

    def _stop(self):
        logging.info("Shutting down server")
        self.socket.close()
        self.is_running = False
