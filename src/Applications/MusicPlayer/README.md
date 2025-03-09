# Painting Music Player

## Description

Painting Music Player is a console application written in Python, which allows for Painting Music music generation payloads to be played aloud.  This application is currently used by [Canvas Capture](/src/Applications/CanvasCapture/).

### Technical Overview

This application is run as a socket server, which ingests commands sent from Painting Music applications.

In order to play music, we utilise a thrid party library called [scamp](http://scamp.marcevanstein.com/scamp.html). This allows us to use a wealth of instruments to play continiously throughout a performance.

## Installation

### Prerequisites

Please ensure you have completed the following before installation:

- Install Python (see [here](https://www.python.org/downloads/))
- Ensure pip has been installed
  - This is our package manager for the application
  - Pip documentation can be found [here](https://pip.pypa.io/en/stable/installation/)

### Steps

Once you have cloned the repository, navigate to the [Music Player directory](/src/Applications/MusicPlayer/).  You will need to run the following command to install our application dependencies:

```bash
pip install -r /path/to/requirements.txt
```

or

```bash
python -m pip install -r /path/to/requirements.txt
```

Once you have successfully installed dependencies, you should be ready to work and run Music Player.

## Adding Dependencies

If you need to update the dependencies or add dependencies for the application, please remember to update the [requirements.txt](./requirements.txt) file.

In order to achieve this, it is recommended to install pipreqs using pip.

```bash
pip install pipreqs
```

Once this has installed run the following to update the file:

```bash
python -m pipreqs.pipreqs path/to/application
```
