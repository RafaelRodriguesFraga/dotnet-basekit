#!/usr/bin/env bash
set -e

ACTION="$1"
PROJECT="$2"

PROJECT_DIR="DotnetBaseKit.Components.${PROJECT}"

case "$ACTION" in
  get-latest-tag)
    LATEST_TAG=$(git tag -l "$PROJECT/v*" --sort=-v:refname | head -n 1)

    if [ -z "$LATEST_TAG" ]; then
      echo "NONE"
    else
      echo "$LATEST_TAG"
    fi
    ;;

  calculate-version)
    LATEST_TAG="$3"
    TYPE="$4"

    if [[ "$LATEST_TAG" == "NONE" ]]; then
      echo "1.0.0"
      exit 0
    fi

    LATEST_VERSION="${LATEST_TAG#*/v}"

    if [ ! -f "./scripts/release.sh" ]; then
      echo "release.sh not found" >&2
      exit 1
    fi

    ./scripts/release.sh "$LATEST_VERSION" "$TYPE"
    ;;

  publish)
    NEXT_VERSION="$3"

     if [ -z "$NUGET_API_KEY" ]; then
      echo "NUGET_API_KEY is not available." >&2
      exit 1
     fi  

    dotnet build "${PROJECT_DIR}/DotnetBaseKit.Components.${PROJECT}.csproj" \
      -c Release

    dotnet pack "${PROJECT_DIR}/DotnetBaseKit.Components.${PROJECT}.csproj" \
      -c Release \
      --no-build \
      -p:PackageVersion="$NEXT_VERSION" \
      -o ./artifacts

    dotnet nuget push "./artifacts/*.nupkg" \
      --api-key "$NUGET_KEY" \
      --source "https://api.nuget.org/v3/index.json" \
      --skip-duplicate
    ;;

  tag-and-release)
    NEXT_VERSION="$3"
    LATEST_TAG="$4"
    TAG="${PROJECT}/v${NEXT_VERSION}"
    PATH_FILTER="${PROJECT_DIR}/"

    git config user.name "github-actions[bot]"
    git config user.email "github-actions[bot]@users.noreply.github.com"

    git tag "$TAG"
    git push origin "$TAG"

    # Busca o histórico de commits filtrado pela pasta do projeto
    if git rev-parse "$LATEST_TAG" >/dev/null 2>&1; then
      RAW_LOGS=$(git log "$LATEST_TAG"..HEAD --format="%s" -- "$PATH_FILTER")
    else
      RAW_LOGS=$(git log --format="%s" -- "$PATH_FILTER")
    fi

    # Limpa 'feat:', deixa a 1ª letra Maiúscula e formata como lista (-)
    RELEASE_NOTES=$(echo "$RAW_LOGS" \
      | sed -E 's/^[a-z]+(\([a-z0-9_-]+\))?!?:[[:space:]]*//I' \
      | awk '{ print toupper(substr($0,1,1)) substr($0,2) }' \
      | sed 's/^/- /')

    if [ -z "$RELEASE_NOTES" ]; then
      RELEASE_NOTES="- Atualizações e melhorias no pacote $PROJECT."
    fi

    # Passa as notas geradas com a flag --notes
    gh release create "$TAG" \
      --title "$TAG" \
      --notes "$RELEASE_NOTES"
    ;;
esac