#!/bin/bash

# #######################################################################
# (C) 2008-2012 Vladimir Lebedev-Schmidthof <vladimir@schmidthof.com>
# Made for Albion Games (http://albiongames.org)
# 
# 
#            DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
#                    Version 2, December 2004
#
# Copyright (C) 2004 Sam Hocevar <sam@hocevar.net>
# 
# Everyone is permitted to copy and distribute verbatim or modified
# copies of this license document, and changing it is allowed as long
# as the name is changed.
#
#           DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
#   TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION
#
#  0. You just DO WHAT THE FUCK YOU WANT TO.
# ####################################################################### #/


. names.sh

$PREFIX "createuser -l -R -S -P -D ${USERNAME}"
$PREFIX "createdb --owner=${USERNAME} ${DBNAME}"

