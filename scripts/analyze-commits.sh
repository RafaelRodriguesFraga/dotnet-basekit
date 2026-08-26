#!/bin/bash

LATEST_TAG="$1"

if [ -z "$LATEST_TAG" ]; then
    echo "Latest tag is required." >&2
    exit 1
fi


if [ "$LATEST_TAG" = "0.0.0" ]; then
    echo "No previous tag found. Analyzing all commits..." >&2
    COMMITS=$(git log --format="%s")
else
    echo "Analyzing commits since $LATEST_TAG..." >&2
    COMMITS=$(git log "$LATEST_TAG"..HEAD --format="%s")
fi

FEATURES=0
FIXES=0
BREAKING=0

while IFS= read -r COMMIT; do
    if [[ "$COMMIT" =~ ^feat!: ]]; then
        BREAKING=$((BREAKING + 1))
    elif [[ "$COMMIT" =~ ^feat: ]]; then
        FEATURES=$((FEATURES + 1))
    elif [[ "$COMMIT" =~ ^fix: ]]; then
        FIXES=$((FIXES + 1))
    fi
done <<< "$COMMITS"

echo "" >&2
echo "Release analysis:" >&2
echo "Features: $FEATURES" >&2
echo "Fixes: $FIXES" >&2
echo "Breaking changes: $BREAKING" >&2

if [ "$BREAKING" -gt 0 ]; then
    TYPE="breaking"
elif [ "$FEATURES" -gt 0 ]; then
    TYPE="feat"
elif [ "$FIXES" -gt 0 ]; then
    TYPE="fix"
else
    echo "No releasable changes found." >&2
    exit 1
fi

echo "Type: $TYPE" >&2

echo "$TYPE"