#!/bin/bash

. names.sh

$PREFIX "dropdb ${DBNAME}"
$PREFIX "dropuser ${USERNAME}"

