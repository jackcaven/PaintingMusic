# Text Display

A real-time text display application that shows content from a file as it's being updated.

## Features

- Real-time display of text being written to a file
- Works on Windows and Linux
- Custom italic font support (Cousine-BoldItalic)
- Automatic scrolling to show new content
- No scrollbar for a cleaner interface
- Clears the display when the keyword `|END|` is detected

## Usage

Run the application with:

```bash
python text_display.py [file_path]
```

Where `[file_path]` is the path to the file you want to monitor. If not specified, it defaults to `ModelFeedback.txt` in the current directory.

### Test Script

The repository includes a test script (`test_display.py`) that you can use to demonstrate the functionality:

```bash
python test_display.py [file_path]
```

This will write sample text to the specified file with delays between lines to demonstrate the real-time updating.

### Example

1. Run the display application:

   ```bash
   python text_display.py ModelFeedback.txt
   ```

2. In another terminal or application, write to the monitored file:

   ```bash
   echo "Hello, world!" >> ModelFeedback.txt
   ```

3. To clear the display:

   ```bash
   echo "|END|" >> ModelFeedback.txt
   ```

## Requirements

- Python 3.6+
- DearPyGUI (see requirements.txt)
- Cousine-BoldItalic.ttf font file (included in the project)

## Installation

1. Install the required dependencies:

   ```bash
   pip install -r requirements.txt
   ```

2. Run the application:

   ```bash
   python text_display.py
   ```

## Testing

To test the application, run the display app in one terminal:

```bash
python text_display.py
```

And run the test script in another terminal:

```bash
python test_display.py
```

## Publish

When you are ready to release a new version of Music Player, a script has been written [publish.py](/scripts/publish.py) to help with this process. All you need to do is navigate to that folder in a terminal and run the following command:

```bash
python publish.py 'MusicPlayer'
```

This will zip up all the required files to run the application and place in a publish folder inside the application directory.

## Note

The application uses DearPyGUI for the interface and requires the Cousine-BoldItalic font file to be present in the same directory as the script. The font provides a clean, italic style for better readability.

## Acknowledgments

I use Python 3 which is permissive open-source, please view <https://docs.python.org/3/license.html#history-and-license> for all legal notices. Other notices are available in the Acknowledgment-Licenses folder as related to the included font and DearPyGUI library.
