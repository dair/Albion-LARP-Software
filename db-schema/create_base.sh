#!/bin/bash

. names.sh

$PREFIX "createuser -l -R -S -P -D ${USERNAME}"
$PREFIX "createdb --owner=${USERNAME} ${DBNAME}"

