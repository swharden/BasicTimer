:: create large PNGs with transparent backgrounds
convert -background transparent icon.svg -resize 512x512 favicon-512.png
convert -background transparent icon.svg -resize 256x256 favicon-256.png
convert -background transparent icon.svg -resize 128x128 favicon-128.png
convert -background transparent icon.svg -resize 64x64 favicon-64.png
convert -background transparent icon.svg -resize 32x32 favicon-32.png
convert -background transparent icon.svg -resize 16x16 favicon-16.png

:: create a small and simple 32px icon file
convert -background transparent favicon-32.png favicon-32.ico

:: pack individual sizes into a single icon file
convert -background transparent favicon-16.png favicon-32.png favicon-64.png favicon-128.png favicon-256.png favicon.ico

pause