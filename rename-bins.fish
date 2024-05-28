#!/bin/fish
set ver "$argv[1]"
set path 'bin'
set release 'bin/release'
set name 'UndercardOdyssey'

set os 'linux'
zip --junk-paths - "$path/$os/$name.pck" "$path/$os/$name.x86_64" > "$release/$name-$ver-$os.zip"

set os 'macos'
cp "$path/$os/$name.zip" "$release/$name-$ver-$os.zip"

set os 'win'
zip --junk-paths - "$path/$os/$name.pck" "$path/$os/$name.exe" > "$release/$name-$ver-$os.zip"
