#!/bin/bash

echo "🔍 Procurando arquivos .csproj..."

find . -name "*.csproj" | while read proj; do
  if grep -q "<TargetFramework>net6.0</TargetFramework>" "$proj"; then
    echo "🔧 Atualizando: $proj"
    sed -i 's/<TargetFramework>net6.0<\/TargetFramework>/<TargetFramework>net8.0<\/TargetFramework>/' "$proj"
  else
    echo "✅ Já está atualizado: $proj"
  fi
done

echo "🎉 Atualização concluída!"