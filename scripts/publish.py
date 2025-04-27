"""
Copyright ©, Painting Music Limited. All Rights Reserved. 2025.

Author: Jack Caven

Description:
    This script is used to publish the python applications in this repo by copying the necessary files and creating a zip archive.
"""

# Import Libraries
import argparse
import os
import shutil
import zipfile

# Constants
accepted_applications = ["MusicPlayer", "TransparencyDisplay"]

# Script
parser = argparse.ArgumentParser(
    description="Publish painting music python applications"
)

parser.add_argument(
    "app_name",
    type=str,
    help="Name of the application to publish. Either 'MusicPlayer' or 'TransparencyDisplay'",
)

args = parser.parse_args()

if args.app_name not in accepted_applications:
    raise ValueError(
        f"Invalid application name '{args.app_name}'. Accepted values are: {accepted_applications}"
    )

current_directory = os.getcwd()
parent_directory = os.path.dirname(current_directory)
parent_directory = os.path.join(parent_directory, f"src/Applications/{args.app_name}")

if not os.path.exists(parent_directory):
    raise FileNotFoundError(
        f"Parent directory '{parent_directory}' does not exist. Please check the path."
    )

publish_directory = os.path.join(parent_directory, "publish")
app_dir = os.path.join(publish_directory, args.app_name)

os.makedirs(publish_directory, exist_ok=True)

src_directory = os.path.join(parent_directory, "src")
if os.path.isdir(src_directory):
    # Ensure MusicPlayer/src is created
    music_player_src_directory = os.path.join(app_dir, "src")
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
    shutil.copy2(requirements_file, app_dir)
else:
    print(f"Warning: 'requirements.txt' not found in {parent_directory}")

# Step 6: Zip the 'MusicPlayer' directory inside 'publish'
zip_file_name = f"{args.app_name}.zip"
zip_file_path = os.path.join(publish_directory, zip_file_name)

# If the zip file exists, remove it to overwrite
if os.path.exists(zip_file_path):
    os.remove(zip_file_path)

# Create the zip file
with zipfile.ZipFile(zip_file_path, "w", zipfile.ZIP_DEFLATED) as zipf:
    for root, dirs, files in os.walk(app_dir):
        for file in files:
            file_path = os.path.join(root, file)
            arcname = os.path.relpath(file_path, publish_directory)
            zipf.write(file_path, arcname)

# Step 7: Remove the 'MusicPlayer' directory
shutil.rmtree(app_dir)

print(f"Publishing completed! The zip file is located at: {zip_file_path}")
