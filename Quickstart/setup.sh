#!/bin/sh

tar xvf ./extracted.tar.gz
exec /usr/bin/mono MeeGen.exe --create-db ./extracted/



