#!/bin/fish
set ver "$argv[1]"
set path 'bin'
set release 'bin/release'
set name 'UndercardOdyssey'

set os 'linux'
zip "$release/$name-$ver-$os.zip" "$path/$os/$name.pck" "$path/$os/$name.x86_64"

set os 'macos'
cp "$path/$os/$name.zip" "$release/$name-$ver-$os.zip"

set os 'win'
zip "$release/$name-$ver-$os.zip" "$path/$os/$name.pck" "$path/$os/$name.exe"
