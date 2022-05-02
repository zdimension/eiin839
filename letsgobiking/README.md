# "Let's go Biking" project

## Environment

The following environment variables must be set:
- `JCDECAUX_API_KEY`
- `OPENROUTE_API_KEY`

## Thin client

The thin client can be opened directly through the `index.html` file.

**Note:** some web browsers block AJAX requests from the file:/// protocol. If
you are encountering such problems, use the `start.py` file which bootstraps a
lightweight HTTP server.