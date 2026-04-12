#!/usr/bin/env bash
set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo "==> Building and starting all services..."
docker compose -f "$SCRIPT_DIR/infra/docker/docker-compose.yml" up -d --build "$@"

echo "==> All services are up and running!"
docker ps
