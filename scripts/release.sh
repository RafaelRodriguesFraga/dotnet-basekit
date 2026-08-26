#!/bin/bash

CURRENT_VERSION="$1"
TYPE="$2"

IFS='.' read -r MAJOR MINOR PATCH <<< "$CURRENT_VERSION"

case "$TYPE" in
    fix)
        PATCH=$((PATCH + 1))
        ;;

    feat)
        MINOR=$((MINOR + 1))
        PATCH=0
        ;;

    breaking)
        MAJOR=$((MAJOR + 1))
        MINOR=0
        PATCH=0
        ;;

    *)
        echo "Unknown type: $TYPE" >&2
        exit 1
        ;;
esac

NEXT_VERSION="$MAJOR.$MINOR.$PATCH"

echo "Current version: $CURRENT_VERSION" >&2
echo "Type: $TYPE" >&2
echo "Next version: $NEXT_VERSION" >&2

echo "$NEXT_VERSION"