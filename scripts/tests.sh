#!/bin/bash
set -e

echo "Running tests with coverage..."

# Vai para a raiz do projeto (assumindo que o scripts/ está dentro da raiz)
cd "$(dirname "$0")/.."

dotnet test --collect:"XPlat Code Coverage"

echo "Generating HTML coverage report..."

reportgenerator -reports:"./**/coverage.cobertura.xml" -targetdir:"./coverage-report" -reporttypes:Html

echo "\nCoverage report generated at ./scripts/coverage-report/index.html"