"""
Copyright ©, Painting Music Limited. All Rights Reserved. 2025.

Author: Jack Caven
"""

import os
import shutil
import zipfile

# Step 1: Navigate up one directory
current_directory = os.getcwd()
parent_directory = os.path.dirname(current_directory)

# Step 2: Define the publish and MusicPlayer directories
publish_directory = os.path.join(parent_directory, "publish")
music_player_directory = os.path.join(publish_directory, "MusicPlayer")

# Step 3: Create the 'publish' directory if it doesn't exist
os.makedirs(publish_directory, exist_ok=True)

# Step 4: Copy the 'src' directory into the 'publish/MusicPlayer/src' directory
src_directory = os.path.join(parent_directory, "src")
if os.path.isdir(src_directory):
    # Ensure MusicPlayer/src is created
    music_player_src_directory = os.path.join(music_player_directory, "src")
    os.makedirs(music_player_src_directory, exist_ok=True)
    # Copy the contents of src into MusicPlayer/src
    for item in os.listdir(src_directory):
        src_item = os.path.join(src_directory, item)
        dest_item = os.path.join(music_player_src_directory, item)
        if os.path.isdir(src_item):
            shutil.copytree(src_item, dest_item, dirs_exist_ok=True)
        else:
            shutil.copy2(src_item, dest_item)
else:
    print(f"Error: 'src' directory not found in {parent_directory}")
    exit(1)

# Step 5: Copy the requirements.txt file into MusicPlayer
requirements_file = os.path.join(parent_directory, "requirements.txt")
if os.path.exists(requirements_file):
    shutil.copy2(requirements_file, music_player_directory)
else:
    print(f"Warning: 'requirements.txt' not found in {parent_directory}")

# Step 6: Zip the 'MusicPlayer' directory inside 'publish'
zip_file_name = "MusicPlayer.zip"
zip_file_path = os.path.join(publish_directory, zip_file_name)

# If the zip file exists, remove it to overwrite
if os.path.exists(zip_file_path):
    os.remove(zip_file_path)

# Create the zip file
with zipfile.ZipFile(zip_file_path, "w", zipfile.ZIP_DEFLATED) as zipf:
    for root, dirs, files in os.walk(music_player_directory):
        for file in files:
            file_path = os.path.join(root, file)
            arcname = os.path.relpath(file_path, publish_directory)
            zipf.write(file_path, arcname)

# Step 7: Remove the 'MusicPlayer' directory
shutil.rmtree(music_player_directory)

print(f"Publishing completed! The zip file is located at: {zip_file_path}")
