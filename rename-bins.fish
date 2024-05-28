#!/bin/fish
set ver "$argv[1]"
set path 'bin'
set release 'bin/release'
set name 'UndercardOdyssey'

set os 'linux'
pushd "$path/$os"
zip -r - "$name.pck" "$name.x86_64" "data_UndercardOdyssey_linuxbsd_x86_64" > "../../$release/$name-$ver-$os.zip"
popd

set os 'macos'
cp "$path/$os/$name.zip" "$release/$name-$ver-$os.zip"

set os 'win'
pushd "$path/$os"
zip -r - "$name.pck" "$name.exe" "data_UndercardOdyssey_windows_x86_64" > "../../$release/$name-$ver-$os.zip"
popd
