#!/usr/bin/env bash
set -e

BEFORE_SHA="${1:-${GITHUB_EVENT_BEFORE:-}}"
AFTER_SHA="${2:-${GITHUB_SHA:-HEAD}}"

ALL_PROJECTS=("Api" "Application" "Domain.Mongo" "Api.Domain.Sql" "Infra.Sql" "Infra.MongoDb" "Shared")
PROJECTS=()

if [ -z "$BEFORE_SHA" ] || \
   [ "$BEFORE_SHA" = "0000000000000000000000000000000000000000" ] || \
   ! git rev-parse --verify "$BEFORE_SHA" >/dev/null 2>&1; then
    PROJECTS=("${ALL_PROJECTS[@]}")
else
  CHANGED_FILES=$(git diff --name-only "$BEFORE_SHA" "$AFTER_SHA" 2>/dev/null || git diff-tree --no-commit-id --name-only -r "$AFTER_SHA")

  for PROJ in "${ALL_PROJECTS[@]}"; do
    if echo "$CHANGED_FILES" | grep -q "^DotnetBaseKit.Components.${PROJ}/"; then
      PROJECTS+=("$PROJ")
    fi
  done
fi

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