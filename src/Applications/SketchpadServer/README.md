# Sketchpad Server

## Description

[Painting Music Sketchpad](https://ray.scot/pm/public/) is a web application that allows any user to interact with Painting Music technology.  This solution is Sketchpad Server, which is effectively the backend for the whole application.

When running the .exe output file, a socket server is spun up which allows the front end to send shapes data to be then converted to music which is sent back to the front end to play.

Front end is designed and maintained by [Ray Interactive](https://www.rayinteractive.org/).

![frontend](/docs/images/sketchpad_frontend.png)

## Running the application

Running the application should launch a console.  The console will prompt you to open a browser and navigate to the web application.

Current Supported Browsers:

- Google Chrome
- Microsoft Edge

If the application has launched successfully and the front end is able to communicate to the backend, you should see an additional "AI mode" button on the UI (see highlighted in image below):

![frontendaimode](/docs/images/sketchpad_frontend_aimode.png)

The user is able to toggle AI mode on and off to compare the music generation to human heuristics (developed by Ray Interactive).

## Testing

There are some unit tests covering business logic.  It is encourage to try add automated unit testing when adding or extending new features.  This is to help maintain high code quality.
