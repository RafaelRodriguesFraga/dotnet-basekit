#!/usr/bin/env bash

set -e

BEFORE_SHA="${1:-${GITHUB_EVENT_BEFORE:-}}"
AFTER_SHA="${2:-${GITHUB_SHA:-HEAD}}"

ALL_PROJECTS=(
  "Api"
  "Application"
  "Domain.MongoDb"
  "Domain.Sql"
  "Infra.Sql"
  "Infra.MongoDb"
  "Shared"
)

PROJECTS=()

if [ -z "$BEFORE_SHA" ] || \
   [ "$BEFORE_SHA" = "0000000000000000000000000000000000000000" ]; then
    CHANGED_FILES=$(git diff-tree --no-commit-id --name-only -r "$AFTER_SHA")
else
  CHANGED_FILES=$(git diff --name-only "$BEFORE_SHA" "$AFTER_SHA")
fi

for PROJ in "${ALL_PROJECTS[@]}"; do
  LATEST_TAG=$(git tag -l "$PROJ/v*" --sort=-v:refname | head -n 1)

  if [ -z "$LATEST_TAG" ]; then
    echo "No release found for $PROJ. Adding to release list." >&2
    PROJECTS+=("$PROJ")
    continue
  fi

  if echo "$CHANGED_FILES" | grep -q "^DotnetBaseKit.Components.${PROJ}/"; then
    echo "Changes found for $PROJ." >&2
    PROJECTS+=("$PROJ")
  fi

done

if [ ${#PROJECTS[@]} -eq 0 ]; then
  echo "[]"
  exit 0
fi

JSON="["
for i in "${!PROJECTS[@]}"; do
  if [ "$i" -gt 0 ]; then
    JSON+=","
  fi

  JSON+="\"${PROJECTS[$i]}\""
done
JSON+="]"

echo "$JSON"