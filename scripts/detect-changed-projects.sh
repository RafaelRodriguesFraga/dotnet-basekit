#!/usr/bin/env bash
set -e

# Se receber parâmetros usa eles, senão tenta variáveis do GH Actions, senão usa HEAD~1 e HEAD (local)
BEFORE_SHA="${1:-${GITHUB_EVENT_BEFORE:-}}"
AFTER_SHA="${2:-${GITHUB_SHA:-HEAD}}"

# Se for o primeiro push ou BEFORE_SHA estiver vazio/zerado, compara o commit atual (HEAD)
if [ -z "$BEFORE_SHA" ] || [ "$BEFORE_SHA" = "0000000000000000000000000000000000000000" ]; then
  # Localmente, compara o último commit feito (HEAD~1 contra HEAD)
  CHANGED_FILES=$(git diff --name-only HEAD~1 HEAD 2>/dev/null || git diff-tree --no-commit-id --name-only -r "$AFTER_SHA")
else
  CHANGED_FILES=$(git diff --name-only "$BEFORE_SHA" "$AFTER_SHA" 2>/dev/null || git diff-tree --no-commit-id --name-only -r "$AFTER_SHA")
fi

ALL_PROJECTS=("Api" "Application" "Domain.Mongo" "Api.Domain.Sql" "Infra.Sql" "Infra.MongoDb" "Shared")
PROJECTS=()

for PROJ in "${ALL_PROJECTS[@]}"; do
  if echo "$CHANGED_FILES" | grep -q "^DotnetBaseKit.Components.${PROJ}/"; then
    PROJECTS+=("$PROJ")
  fi
done

# Monta o JSON manualmente em Bash (sem precisar do jq)
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