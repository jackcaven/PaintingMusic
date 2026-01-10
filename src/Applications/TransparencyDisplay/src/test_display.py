# COPYRIGHT TIMOTHY NISI 2025
"""
Test script for text_display.py
This script writes sample text to a file to demonstrate the text display application.
Run this in one terminal while running the text display app in another.
"""

import time
import os
import sys

# Contants
ENV_VAR = "PaintingMusicCanvasCaptureImageDirectory"
FILE_NAME = "ModelFeedback.txt"


def main():
    env_var_value = os.getenv(ENV_VAR)
    # Default output file
    output_file = os.path.join(env_var_value, FILE_NAME) if env_var_value else FILE_NAME

    # Use command line argument if provided
    if len(sys.argv) > 1:
        output_file = sys.argv[1]

    # Create or clear the file
    with open(output_file, "w") as f:
        f.write("")

    print(f"Writing to file: {os.path.abspath(output_file)}")
    print("Make sure text_display.py is running to see the output")

    # Sample text to display
    texts = [
        "Hello World!",
        "This is a test of the text display application.",
        "The text should appear in real-time as it's written to the file.",
        "The font should resize to fit the window.",
        "Now we'll clear the display with the |END| keyword.",
        "|END|",
        "The display should now be cleared.",
        "Keep in mind that the keyword here -> |END| <- is invisible when added with other text!",
        "Let's add some more text to demonstrate scrolling.",
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
        "Nulla facilisi. Mauris euismod, nisi eget ultricies ultricies.",
        "Nunc auctor, magna eget aliquam aliquam, nunc nisl ultricies.",
        "The window should automatically scroll to show new text.",
        "Resize the window to see it dynamically adjust.",
        "|EXIT|",
    ]

    # Write each text sample with a delay
    for text in texts:
        with open(output_file, "a") as f:
            f.write(text + "\n")

        print(f"Added: {text}")
        time.sleep(2)  # Pause between lines

    # Delete the file
    os.remove(output_file)
    print("Test completed. You can close the text display application.")


if __name__ == "__main__":
    main()
