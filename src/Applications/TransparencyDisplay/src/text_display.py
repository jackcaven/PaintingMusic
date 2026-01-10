# COPYRIGHT TIMOTHY NISI 2025
"""
Text Display Application
------------------------
A real-time text display window that monitors a file for changes and displays its contents.
The application features automatic scrolling, custom font support, and text clearing functionality.

Features:
- Real-time file monitoring
- Automatic text scrolling
- Custom italic font support (Cousine-BoldItalic, or Jost-400-Book)
- Text clearing with |END| keyword
- Clean, scrollbar-free interface

Usage:
    python text_display.py [file_path]
    If no file path is provided, defaults to "ModelFeedback.txt"

Requirements:
    - Python 3.x
    - DearPyGUI
    - Cousine-BoldItalic.ttf or Jost-400-Book.ttf font file (included in the project)
"""

import os
import sys
import time
import threading
import dearpygui.dearpygui as dpg


class TextDisplayApp:
    """
    A DearPyGUI-based application for displaying real-time text updates from a file.

    Attributes:
        file_path (str): Path to the file being monitored
        last_position (int): Last read position in the file
        running (bool): Flag to control the monitoring thread
        text_content (str): Current content of the text display
        font (int): DearPyGUI font handle
    """

    # Constants
    ENV_VAR = "PaintingMusicCanvasCaptureImageDirectory"
    FILE_NAME = "ModelFeedback.txt"

    def __init__(self, file_path):
        """
        Initialize the text display application.

        Args:
            file_path (str): Path to the file to monitor for text updates

        Raises:
            FileNotFoundError: If the required font file is not found
        """
        self.file_path = file_path
        self.last_position = 0
        self.running = True
        self.text_content = ""

        # Initialize DearPyGUI
        dpg.create_context()
        dpg.create_viewport(title="Model Thoughts", width=500, height=1200)
        dpg.setup_dearpygui()

        # Create window
        with dpg.window(label="Model Thoughts", tag="primary_window"):
            # Create a scrollable child window to contain the text
            with dpg.child_window(
                tag="scrollable_container", autosize_x=True, no_scrollbar=True
            ):
                # Create text widget
                dpg.add_text(tag="text_display", wrap=0)

            # Configure font
            font_path = "Jost-400-Book.ttf"
            if not os.path.exists(font_path):
                raise FileNotFoundError(
                    f"Required font file '{font_path}' not found. Please ensure it's in the same directory as the script."
                )

            with dpg.font_registry():
                self.font = dpg.add_font(font_path, 40)
                dpg.bind_item_font("text_display", self.font)

        # Set primary window
        dpg.set_primary_window("primary_window", True)

        # Start file monitoring thread
        self.monitor_thread = threading.Thread(target=self.monitor_file)
        self.monitor_thread.daemon = True
        self.monitor_thread.start()

    def monitor_file(self):
        """
        Continuously monitor the specified file for changes.

        This method runs in a separate thread and:
        - Reads new content from the file
        - Handles the |END| keyword for clearing text
        - Updates the display with new content
        - Maintains the last read position

        Note:
            The file is checked every 100ms for updates
        """
        while self.running:
            try:
                with open(self.file_path, "r") as file:
                    file.seek(self.last_position)
                    new_content = file.read()

                    if new_content:
                        self.last_position = file.tell()

                        # Check for |END| keyword
                        if "|END|" in new_content:
                            self.clear_text()
                            new_content = new_content.replace("|END|", "")

                        if "|EXIT|" in new_content:
                            self.running = False
                            self.tidy_up()
                            dpg.stop_dearpygui()
                            return

                        # Update text widget if there's new content
                        if new_content.strip():
                            self.update_text(new_content)
            except Exception as e:
                print(f"Error reading file: {e}")

            time.sleep(0.1)  # Check for updates every 100ms

    def update_text(self, content):
        """
        Update the text display with new content and scroll to the bottom.

        Args:
            content (str): New text content to append to the display
        """
        self.text_content += content + "\n"
        dpg.set_value("text_display", self.text_content)

        # Scroll to bottom
        dpg.set_y_scroll("scrollable_container", -1.0)

    def clear_text(self):
        """Clear all text from the display."""
        self.text_content = ""
        dpg.set_value("text_display", "")

    def tidy_up(self):
        with open(self.file_path, "r+") as file:
            try:
                file.seek(0)
                file.truncate()
            except Exception as e:
                print(f"Error clearing file: {e}")

    def run(self):
        """
        Run the DearPyGUI application.

        This method:
        - Shows the viewport
        - Runs the main rendering loop
        - Handles cleanup on exit

        Note:
            The application will continue running until the window is closed
            or an error occurs in the main loop
        """
        dpg.show_viewport()

        try:
            while dpg.is_dearpygui_running():
                # Render frame
                dpg.render_dearpygui_frame()
        except Exception as e:
            print(f"Error in main loop: {e}")
        finally:
            self.running = False
            self.tidy_up()
            dpg.destroy_context()


def main():
    """
    Main entry point for the application.

    Usage:
        python text_display.py [file_path]
        If no file path is provided, defaults to "ModelFeedback.txt"

    Raises:
        FileNotFoundError: If the required font file is not found
    """
    env_var_value = os.getenv(TextDisplayApp.ENV_VAR)
    file_path = (
        sys.argv[1]
        if len(sys.argv) > 1
        else (
            os.path.join(env_var_value, TextDisplayApp.FILE_NAME)
            if env_var_value
            else TextDisplayApp.FILE_NAME
        )
    )

    try:
        app = TextDisplayApp(file_path)

        print(f"Monitoring file: {os.path.abspath(file_path)}")
        print("Text will be displayed in real-time")
        print("Use the '|END|' keyword in the file to clear the display")
        print("Use the '|EXIT|' keyword in the file to close the application")

        app.run()
    except FileNotFoundError as e:
        print(f"Error: {e}")
        sys.exit(1)
    except Exception as e:
        print(f"Unexpected error: {e}")
        sys.exit(1)


if __name__ == "__main__":
    main()
