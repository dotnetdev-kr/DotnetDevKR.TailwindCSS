#!/bin/bash

VERSION=${1:-"v4.1.17"}

if [ -n "$1" ]; then
  VERSION=$1
fi

set -e

BASE_URL="https://github.com/tailwindlabs/tailwindcss/releases/download"
DEST_DIR="$(dirname "$0")/"

# TODO: check sha256 checksum of downloaded files
FILES=(
  "tailwindcss-linux-arm64"
  "tailwindcss-linux-armv7"
  "tailwindcss-linux-x64"
  "tailwindcss-macos-arm64"
  "tailwindcss-macos-x64"
  "tailwindcss-windows-arm64.exe"
  "tailwindcss-windows-x64.exe"
)

# Create destination directory if it doesn't exist
mkdir -p "$DEST_DIR"

# Download each file
for file in "${FILES[@]}"; do
  URL="$BASE_URL/$VERSION/$file"
  OUTPUT_PATH="$DEST_DIR/$file"

  echo "Downloading $file from $URL..."
  curl -L -o "$OUTPUT_PATH" "$URL"

  # Make non-Windows files executable
  if [[ "$file" != *.exe ]]; then
    chmod +x "$OUTPUT_PATH"
    echo "Made $file executable."
  fi
done

echo "All executables have been downloaded to $DEST_DIR"
